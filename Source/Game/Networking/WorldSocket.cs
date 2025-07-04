﻿// Copyright (c) CypherCore <http://github.com/CypherCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Framework.ClientBuild;
using Framework.Constants;
using Framework.Cryptography;
using Framework.Database;
using Framework.IO;
using Framework.Networking;
using Framework.Web;
using Game.Networking.Packets;
using System;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;

namespace Game.Networking
{
    public class WorldSocket : SocketBase
    {
        static readonly string ClientConnectionInitialize = "WORLD OF WARCRAFT CONNECTION - CLIENT TO SERVER - V2";
        static readonly string ServerConnectionInitialize = "WORLD OF WARCRAFT CONNECTION - SERVER TO CLIENT - V2";

        static readonly byte[] AuthCheckSeed = { 0xDE, 0x3A, 0x2A, 0x8E, 0x6B, 0x89, 0x52, 0x66, 0x88, 0x9D, 0x7E, 0x7A, 0x77, 0x1D, 0x5D, 0x1F,
            0x4E, 0xD9, 0x0C, 0x23, 0x9B, 0xCD, 0x0E, 0xDC, 0xD2, 0xE8, 0x04, 0x3A, 0x68, 0x64, 0xC7, 0xB0 };
        static readonly byte[] SessionKeySeed = { 0xE8, 0x1E, 0x8B, 0x59, 0x27, 0x62, 0x1E, 0xAA, 0x86, 0x15, 0x18, 0xEA, 0xC0, 0xBF, 0x66, 0x8C,
            0x6D, 0xBF, 0x83, 0x93, 0xBC, 0xAA, 0x80, 0x52, 0x5B, 0x1E, 0xDC, 0x23, 0xA0, 0x12, 0xB7, 0x50 };
        static readonly byte[] ContinuedSessionSeed = { 0x56, 0x5C, 0x61, 0x9C, 0x48, 0x3A, 0x52, 0x1F, 0x61, 0x5D, 0x05, 0x49, 0xB2, 0x9A, 0x39, 0xBF,
            0x4B, 0x97, 0xB0, 0x1B, 0xF9, 0x6C, 0xDE, 0xD6, 0x80, 0x1D, 0xAB, 0x26, 0x02, 0xA9, 0x9B, 0x9D };
        static readonly byte[] EncryptionKeySeed = { 0x71, 0xC9, 0xED, 0x5A, 0xA7, 0x0E, 0x4D, 0xFF, 0x4C, 0x36, 0xA6, 0x5A, 0x3E, 0x46, 0x8A, 0x4A,
            0x5D, 0xA1, 0x48, 0xC8, 0x30, 0x47, 0x4A, 0xDE, 0xF6, 0x0D, 0x6C, 0xBE, 0x6F, 0xE4, 0x55, 0x73 };

        static readonly int HeaderSize = 16;

        SocketBuffer _headerBuffer;
        SocketBuffer _packetBuffer;

        ConnectionType _connectType;
        ulong _key;

        byte[] _serverChallenge;
        WorldCrypt _worldCrypt;
        byte[] _sessionKey;
        byte[] _encryptKey;

        long _LastPingTime;
        uint _OverSpeedPings;

        object _worldSessionLock = new();
        WorldSession _worldSession;

        ZLib.z_stream _compressionStream;

        AsyncCallbackProcessor<QueryCallback> _queryProcessor = new();
        string _ipCountry;

        public WorldSocket(Socket socket) : base(socket)
        {
            _connectType = ConnectionType.Realm;
            _worldCrypt = new WorldCrypt();

            _encryptKey = new byte[32];

            _headerBuffer = new SocketBuffer(HeaderSize);
            _packetBuffer = new SocketBuffer(0);
        }

        public override void Dispose()
        {
            _worldSession = null;
            _queryProcessor = null;
            _serverChallenge = null;
            _sessionKey = null;
            _compressionStream = null;

            base.Dispose();
        }

        public override void Start()
        {
            string ip_address = GetRemoteIpAddress().ToString();

            PreparedStatement stmt = LoginDatabase.GetPreparedStatement(LoginStatements.SEL_IP_INFO);
            stmt.AddValue(0, ip_address);
            stmt.AddValue(1, BitConverter.ToUInt32(GetRemoteIpAddress().GetAddressBytes(), 0));

            _queryProcessor.AddCallback(DB.Login.AsyncQuery(stmt).WithCallback(CheckIpCallback));
        }

        async void CheckIpCallback(SQLResult result)
        {
            if (!result.IsEmpty())
            {
                bool banned = false;
                do
                {
                    if (result.Read<ulong>(0) != 0)
                        banned = true;

                    _ipCountry = result.Read<string>(1);

                } while (result.NextRow());

                if (banned)
                {
                    Log.outError(LogFilter.Network, "WorldSocket.Connect: Sent Auth Response (IP {0} banned).", GetRemoteIpAddress().ToString());
                    CloseSocket();
                    return;
                }
            }

            _packetBuffer.Resize(ClientConnectionInitialize.Length + 1);

            AsyncReadWithCallback(InitializeHandler);

            ByteBuffer packet = new();
            packet.WriteString(ServerConnectionInitialize);
            packet.WriteString("\n");
            await AsyncWrite(packet.GetData());
        }

        async Task InitializeHandler(byte[] data, int receivedLength)
        {
            if (receivedLength > 0)
            {
                if (_packetBuffer.GetRemainingSpace() > 0)
                {
                    // need to receive the header
                    int readHeaderSize = Math.Min(receivedLength, _packetBuffer.GetRemainingSpace());
                    _packetBuffer.Write(data, 0, readHeaderSize);

                    if (_packetBuffer.GetRemainingSpace() > 0)
                    {
                        // Couldn't receive the whole header this time.
                        AsyncReadWithCallback(InitializeHandler);
                        return;
                    }

                    ByteBuffer buffer = new(_packetBuffer.GetData());
                    string initializer = buffer.ReadString((uint)ClientConnectionInitialize.Length);
                    if (initializer != ClientConnectionInitialize)
                    {
                        CloseSocket();
                        return;
                    }

                    byte terminator = buffer.ReadUInt8();
                    if (terminator != '\n')
                    {
                        CloseSocket();
                        return;
                    }

                    // Initialize the zlib stream
                    _compressionStream = new ZLib.z_stream();

                    // Initialize the deflate algo...
                    var z_res1 = ZLib.deflateInit2(_compressionStream, 1, 8, -15, 8, 0);
                    if (z_res1 != 0)
                    {
                        CloseSocket();
                        Log.outError(LogFilter.Network, "Can't initialize packet compression (zlib: deflateInit2_) Error code: {0}", z_res1);
                        return;
                    }

                    _packetBuffer.Resize(0);
                    _packetBuffer.Reset();
                    HandleSendAuthSession();
                    await AsyncRead();
                    return;
                }
            }
        }

        public async override void ReadHandler(byte[] data, int receivedLength)
        {
            if (!IsOpen())
                return;

            int currentReadIndex = 0;
            while (currentReadIndex < receivedLength)
            {
                if (_headerBuffer.GetRemainingSpace() > 0)
                {
                    // need to receive the header
                    int readHeaderSize = Math.Min(receivedLength - currentReadIndex, _headerBuffer.GetRemainingSpace());
                    _headerBuffer.Write(data, currentReadIndex, readHeaderSize);
                    currentReadIndex += readHeaderSize;

                    if (_headerBuffer.GetRemainingSpace() > 0)
                        break; // Couldn't receive the whole header this time.

                    // We just received nice new header
                    if (!ReadHeader())
                    {
                        CloseSocket();
                        return;
                    }
                }

                // We have full read header, now check the data payload
                if (_packetBuffer.GetRemainingSpace() > 0)
                {
                    // need more data in the payload
                    int readDataSize = Math.Min(receivedLength - currentReadIndex, _packetBuffer.GetRemainingSpace());
                    _packetBuffer.Write(data, currentReadIndex, readDataSize);
                    currentReadIndex += readDataSize;

                    if (_packetBuffer.GetRemainingSpace() > 0)
                        break; // Couldn't receive the whole data this time.
                }

                // just received fresh new payload
                ReadDataHandlerResult result = ReadData();
                _headerBuffer.Reset();
                if (result != ReadDataHandlerResult.Ok)
                {
                    if (result != ReadDataHandlerResult.WaitingForQuery)
                        CloseSocket();

                    return;
                }
            }

            await AsyncRead();
        }

        bool ReadHeader()
        {
            PacketHeader header = new();
            header.Read(_headerBuffer.GetData());

            _packetBuffer.Resize(header.Size);
            return true;
        }

        ReadDataHandlerResult ReadData()
        {
            PacketHeader header = new();
            header.Read(_headerBuffer.GetData());

            if (!_worldCrypt.Decrypt(_packetBuffer.GetData(), header.Tag))
            {
                Log.outError(LogFilter.Network, $"WorldSocket.ReadData(): client {GetRemoteIpAddress()} failed to decrypt packet (size: {header.Size})");
                return ReadDataHandlerResult.Error;
            }

            WorldPacket packet = new(_packetBuffer.GetData());
            _packetBuffer.Reset();

            if (!packet.IsValidOpcode())
            {
                Log.outError(LogFilter.Network, $"WorldSocket.ReadData(): client {GetRemoteIpAddress()} sent wrong opcode (opcode: {packet.GetOpcode()})");
                Log.outError(LogFilter.Network, $"Header: {_headerBuffer.GetData().ToHexString()} Data: {_packetBuffer.GetData().ToHexString()}");
                return ReadDataHandlerResult.Error;
            }

            PacketLog.Write(packet.GetData(), packet.GetOpcode(), GetRemoteIpEndPoint(), _connectType, true);

            ClientOpcodes opcode = (ClientOpcodes)packet.GetOpcode();

            if (opcode != ClientOpcodes.HotfixRequest && !header.IsValidSize())
            {
                Log.outError(LogFilter.Network, $"WorldSocket.ReadHeaderHandler(): client {GetRemoteIpAddress()} sent malformed packet (size: {header.Size})");
                return ReadDataHandlerResult.Error;
            }

            switch (opcode)
            {
                case ClientOpcodes.Ping:
                    Ping ping = new(packet);
                    ping.Read();
                    if (!HandlePing(ping))
                        return ReadDataHandlerResult.Error;
                    break;
                case ClientOpcodes.AuthSession:
                    if (_worldSession != null)
                    {
                        Log.outError(LogFilter.Network, $"WorldSocket.ReadData(): received duplicate CMSG_AUTH_SESSION from {_worldSession.GetPlayerInfo()}");
                        return ReadDataHandlerResult.Error;
                    }

                    AuthSession authSession = new(packet);
                    authSession.Read();
                    HandleAuthSession(authSession);
                    return ReadDataHandlerResult.WaitingForQuery;
                case ClientOpcodes.AuthContinuedSession:
                    if (_worldSession != null)
                    {
                        Log.outError(LogFilter.Network, $"WorldSocket.ReadData(): received duplicate CMSG_AUTH_CONTINUED_SESSION from {_worldSession.GetPlayerInfo()}");
                        return ReadDataHandlerResult.Error;
                    }

                    AuthContinuedSession authContinuedSession = new(packet);
                    authContinuedSession.Read();
                    HandleAuthContinuedSession(authContinuedSession);
                    return ReadDataHandlerResult.WaitingForQuery;
                case ClientOpcodes.KeepAlive:
                    if (_worldSession != null)
                    {
                        _worldSession.ResetTimeOutTime(true);
                        return ReadDataHandlerResult.Ok;
                    }

                    Log.outError(LogFilter.Network, $"WorldSocket::ReadDataHandler: client {GetRemoteIpAddress()} sent CMSG_KEEP_ALIVE without being authenticated");
                    return ReadDataHandlerResult.Error;
                case ClientOpcodes.LogDisconnect:
                    break;
                case ClientOpcodes.EnableNagle:
                    SetNoDelay(false);
                    break;
                case ClientOpcodes.ConnectToFailed:
                    ConnectToFailed connectToFailed = new(packet);
                    connectToFailed.Read();
                    HandleConnectToFailed(connectToFailed);
                    break;
                case ClientOpcodes.EnterEncryptedModeAck:
                    HandleEnterEncryptedModeAck();
                    break;
                default:
                    lock (_worldSessionLock)
                    {
                        if (_worldSession == null)
                        {
                            Log.outError(LogFilter.Network, $"ProcessIncoming: Client not authed opcode = {opcode}");
                            return ReadDataHandlerResult.Error;
                        }

                        if (!PacketManager.ContainsHandler(opcode))
                        {
                            Log.outError(LogFilter.Network, $"No defined handler for opcode {opcode} sent by {_worldSession.GetPlayerInfo()}");
                            break;
                        }

                        if (opcode == ClientOpcodes.TimeSyncResponse)
                            packet.SetReceiveTime(DateTime.Now);

                        // Our Idle timer will reset on any non PING opcodes on login screen, allowing us to catch people idling.
                        _worldSession.ResetTimeOutTime(false);

                        // Copy the packet to the heap before enqueuing
                        _worldSession.QueuePacket(packet);
                    }
                    break;
            }

            return ReadDataHandlerResult.Ok;
        }

        public async void SendPacket(ServerPacket packet)
        {
            if (!IsOpen())
                return;

            packet.LogPacket(_worldSession);
            packet.WritePacketData();

            var data = packet.GetData();
            ServerOpcodes opcode = packet.GetOpcode();
            PacketLog.Write(data, (uint)opcode, GetRemoteIpEndPoint(), _connectType, false);

            ByteBuffer buffer = new();

            int packetSize = data.Length;
            if (packetSize > 0x400 && _worldCrypt.IsInitialized)
            {
                buffer.WriteInt32(packetSize + 4);
                buffer.WriteUInt32(ZLib.adler32(ZLib.adler32(0x9827D8F1, BitConverter.GetBytes((uint)opcode), 4), data, (uint)packetSize));

                byte[] compressedData;
                uint compressedSize = CompressPacket(data, opcode, out compressedData);
                buffer.WriteUInt32(ZLib.adler32(0x9827D8F1, compressedData, compressedSize));
                buffer.WriteBytes(compressedData, compressedSize);

                packetSize = (int)(compressedSize + 12);
                opcode = ServerOpcodes.CompressedPacket;

                data = buffer.GetData();
            }

            buffer = new ByteBuffer();
            buffer.WriteUInt32((uint)opcode);
            buffer.WriteBytes(data);
            packetSize += 4 /*opcode*/;

            data = buffer.GetData();

            PacketHeader header = new();
            header.Size = packetSize;
            _worldCrypt.Encrypt(ref data, ref header.Tag);

            ByteBuffer byteBuffer = new();
            header.Write(byteBuffer);
            byteBuffer.WriteBytes(data);

            await AsyncWrite(byteBuffer.GetData());
        }

        public void SetWorldSession(WorldSession session)
        {
            lock (_worldSessionLock)
                _worldSession = session;
        }

        public uint CompressPacket(byte[] data, ServerOpcodes opcode, out byte[] outData)
        {
            byte[] uncompressedData = BitConverter.GetBytes((uint)opcode).Combine(data);

            uint bufferSize = ZLib.deflateBound(_compressionStream, (uint)data.Length);
            outData = new byte[bufferSize];

            _compressionStream.next_out = 0;
            _compressionStream.avail_out = bufferSize;
            _compressionStream.out_buf = outData;

            _compressionStream.next_in = 0;
            _compressionStream.avail_in = (uint)uncompressedData.Length;
            _compressionStream.in_buf = uncompressedData;

            int z_res = ZLib.deflate(_compressionStream, 2);
            if (z_res != 0)
            {
                Log.outError(LogFilter.Network, "Can't compress packet data (zlib: deflate) Error code: {0} msg: {1}", z_res, _compressionStream.msg);
                return 0;
            }

            return bufferSize - _compressionStream.avail_out;
        }

        public override bool Update()
        {
            if (!base.Update())
                return false;

            _queryProcessor.ProcessReadyCallbacks();

            return true;
        }

        public override void OnClose()
        {
            lock (_worldSessionLock)
                _worldSession = null;

            base.OnClose();
        }

        void HandleSendAuthSession()
        {
            _serverChallenge = Array.Empty<byte>().GenerateRandomKey(32);

            AuthChallenge challenge = new();
            challenge.Challenge = _serverChallenge;
            challenge.DosChallenge = new byte[32].GenerateRandomKey(32);
            challenge.DosZeroBits = 1;

            SendPacket(challenge);
        }

        void HandleAuthSession(AuthSession authSession)
        {
            RealmJoinTicket joinTicket = JsonSerializer.Deserialize<RealmJoinTicket>(authSession.RealmJoinTicket);
            if (joinTicket == null)
            {
                SendAuthResponseError(BattlenetRpcErrorCode.WowServicesInvalidJoinTicket);
                CloseSocket();
                return;
            }

            // Get the account information from the realmd database
            PreparedStatement stmt = LoginDatabase.GetPreparedStatement(LoginStatements.SEL_ACCOUNT_INFO_BY_NAME);
            stmt.AddValue(0, Global.RealmMgr.GetCurrentRealmId().Index);
            stmt.AddValue(1, joinTicket.GameAccount);

            _queryProcessor.AddCallback(DB.Login.AsyncQuery(stmt).WithCallback(HandleAuthSessionCallback, authSession, joinTicket));
        }

        async void HandleAuthSessionCallback(AuthSession authSession, RealmJoinTicket joinTicket, SQLResult result)
        {
            // Stop if the account is not found
            if (result.IsEmpty())
            {
                Log.outError(LogFilter.Network, "HandleAuthSession: Sent Auth Response (unknown account).");
                CloseSocket();
                return;
            }

            AccountInfo account = new(result.GetFields());

            ClientBuildInfo buildInfo = ClientBuildHelper.GetBuildInfo(account.game.Build);
            if (buildInfo == null)
            {
                SendAuthResponseError(BattlenetRpcErrorCode.BadVersion);
                Log.outError(LogFilter.Network, $"WorldSocket.HandleAuthSessionCallback: Missing client build info for realm build {account.game.Build} ({GetRemoteIpAddress()}).");
                CloseSocket();
                return;
            }

            ClientBuildVariantId buildVariant = new() { Platform = joinTicket.Platform, Arch = joinTicket.ClientArch, Type = joinTicket.Type };
            var clientBuildAuthKey = buildInfo.AuthKeys.Find(p => p.Variant == buildVariant);
            if (clientBuildAuthKey == null)
            {
                SendAuthResponseError(BattlenetRpcErrorCode.BadVersion);
                Log.outError(LogFilter.Network, $"WorldSocket::HandleAuthSession: Missing client build auth key for build {account.game.Build} variant {buildVariant.Platform}-{buildVariant.Arch}-{buildVariant.Type} ({GetRemoteIpAddress()}).");
                CloseSocket();
                return;
            }

            // For hook purposes, we get Remoteaddress at this point.
            var address = GetRemoteIpAddress();

            Sha512 digestKeyHash = new();
            digestKeyHash.Process(account.game.KeyData, account.game.KeyData.Length);
            digestKeyHash.Finish(clientBuildAuthKey.Key);

            HmacSha512 hmac = new(digestKeyHash.Digest);
            hmac.Process(authSession.LocalChallenge, authSession.LocalChallenge.Count);
            hmac.Process(_serverChallenge, 32);
            hmac.Finish(AuthCheckSeed, AuthCheckSeed.Length);

            // Check that Key and account name are the same on client and server
            if (!hmac.Digest.Compare(authSession.Digest))
            {
                SendAuthResponseError(BattlenetRpcErrorCode.Denied);
                Log.outError(LogFilter.Network, $"WorldSocket.HandleAuthSession: Authentication failed for account: {account.game.Id} ('{joinTicket.GameAccount}') address: {address}");
                CloseSocket();
                return;
            }

            Sha512 keyData = new();
            keyData.Finish(account.game.KeyData);

            HmacSha512 sessionKeyHmac = new(keyData.Digest);
            sessionKeyHmac.Process(_serverChallenge, 32);
            sessionKeyHmac.Process(authSession.LocalChallenge, authSession.LocalChallenge.Count);
            sessionKeyHmac.Finish(SessionKeySeed, SessionKeySeed.Length);

            _sessionKey = new byte[40];
            var sessionKeyGenerator = new SessionKeyGenerator512(sessionKeyHmac.Digest, 32);
            sessionKeyGenerator.Generate(_sessionKey, 40);

            HmacSha512 encryptKeyGen = new(_sessionKey);
            encryptKeyGen.Process(authSession.LocalChallenge, authSession.LocalChallenge.Count);
            encryptKeyGen.Process(_serverChallenge, 32);
            encryptKeyGen.Finish(EncryptionKeySeed, EncryptionKeySeed.Length);

            // only first 32 bytes of the hmac are used
            Buffer.BlockCopy(encryptKeyGen.Digest, 0, _encryptKey, 0, 32);

            PreparedStatement stmt = null;

            if (WorldConfig.GetBoolValue(WorldCfg.AllowLogginIpAddressesInDatabase))
            {
                // As we don't know if attempted login process by ip works, we update last_attempt_ip right away
                stmt = LoginDatabase.GetPreparedStatement(LoginStatements.UPD_LAST_ATTEMPT_IP);
                stmt.AddValue(0, address.ToString());
                stmt.AddValue(1, joinTicket.GameAccount);
                DB.Login.Execute(stmt);
                // This also allows to check for possible "hack" attempts on account
            }

            stmt = LoginDatabase.GetPreparedStatement(LoginStatements.UPD_ACCOUNT_INFO_CONTINUED_SESSION);
            stmt.AddValue(0, _sessionKey);
            stmt.AddValue(1, account.game.Id);
            DB.Login.Execute(stmt);

            // First reject the connection if packet contains invalid data or realm state doesn't allow logging in
            if (Global.WorldMgr.IsClosed())
            {
                SendAuthResponseError(BattlenetRpcErrorCode.Denied);
                Log.outError(LogFilter.Network, "WorldSocket.HandleAuthSession: World closed, denying client ({0}).", GetRemoteIpAddress());
                CloseSocket();
                return;
            }

            if (authSession.RealmID != Global.RealmMgr.GetCurrentRealmId().Index)
            {
                SendAuthResponseError(BattlenetRpcErrorCode.Denied);
                Log.outError(LogFilter.Network, "WorldSocket.HandleAuthSession: Client {0} requested connecting with realm id {1} but this realm has id {2} set in config.",
                    GetRemoteIpAddress().ToString(), authSession.RealmID, Global.RealmMgr.GetCurrentRealmId().Index);
                CloseSocket();
                return;
            }

            // Must be done before WorldSession is created
            bool wardenActive = WorldConfig.GetBoolValue(WorldCfg.WardenEnabled);
            if (wardenActive && !ClientBuildHelper.IsValid(account.game.OS))
            {
                SendAuthResponseError(BattlenetRpcErrorCode.Denied);
                Log.outError(LogFilter.Network, $"WorldSocket.HandleAuthSession: Client {address} attempted to log in using invalid client OS ({account.game.OS}).");
                CloseSocket();
                return;
            }

            //Re-check ip locking (same check as in auth).
            if (account.battleNet.IsLockedToIP) // if ip is locked
            {
                if (account.battleNet.LastIP != address.ToString())
                {
                    SendAuthResponseError(BattlenetRpcErrorCode.RiskAccountLocked);
                    Log.outDebug(LogFilter.Network, "HandleAuthSession: Sent Auth Response (Account IP differs).");
                    // We could log on hook only instead of an additional db log, however action logger is config based. Better keep DB logging as well
                    Global.ScriptMgr.OnFailedAccountLogin(account.game.Id);
                    CloseSocket();
                    return;
                }
            }
            else if (!account.battleNet.LockCountry.IsEmpty() && account.battleNet.LockCountry != "00" && !_ipCountry.IsEmpty())
            {
                if (account.battleNet.LockCountry != _ipCountry)
                {
                    SendAuthResponseError(BattlenetRpcErrorCode.RiskAccountLocked);
                    Log.outDebug(LogFilter.Network, "WorldSocket.HandleAuthSession: Sent Auth Response (Account country differs. Original country: {0}, new country: {1}).", account.battleNet.LockCountry, _ipCountry);
                    // We could log on hook only instead of an additional db log, however action logger is config based. Better keep DB logging as well
                    Global.ScriptMgr.OnFailedAccountLogin(account.game.Id);
                    CloseSocket();
                    return;
                }
            }

            long mutetime = account.game.MuteTime;
            //! Negative mutetime indicates amount of seconds to be muted effective on next login - which is now.
            if (mutetime < 0)
            {
                mutetime = GameTime.GetGameTime() + mutetime;

                stmt = LoginDatabase.GetPreparedStatement(LoginStatements.UPD_MUTE_TIME_LOGIN);
                stmt.AddValue(0, mutetime);
                stmt.AddValue(1, account.game.Id);
                DB.Login.Execute(stmt);
            }

            if (account.IsBanned()) // if account banned
            {
                SendAuthResponseError(BattlenetRpcErrorCode.GameAccountBanned);
                Log.outError(LogFilter.Network, "WorldSocket:HandleAuthSession: Sent Auth Response (Account banned).");
                Global.ScriptMgr.OnFailedAccountLogin(account.game.Id);
                CloseSocket();
                return;
            }

            // Check locked state for server
            AccountTypes allowedAccountType = Global.WorldMgr.GetPlayerSecurityLimit();
            if (allowedAccountType > AccountTypes.Player && account.game.Security < allowedAccountType)
            {
                SendAuthResponseError(BattlenetRpcErrorCode.ServerIsPrivate);
                Log.outInfo(LogFilter.Network, "WorldSocket:HandleAuthSession: User tries to login but his security level is not enough");
                Global.ScriptMgr.OnFailedAccountLogin(account.game.Id);
                CloseSocket();
                return;
            }

            Log.outDebug(LogFilter.Network, $"WorldSocket:HandleAuthSession: Client '{joinTicket.GameAccount}' authenticated successfully from {address}.");

            if (WorldConfig.GetBoolValue(WorldCfg.AllowLogginIpAddressesInDatabase))
            {
                // Update the last_ip in the database
                stmt = LoginDatabase.GetPreparedStatement(LoginStatements.UPD_LAST_IP);
                stmt.AddValue(0, address.ToString());
                stmt.AddValue(1, joinTicket.GameAccount);
                DB.Login.Execute(stmt);
            }

            // At this point, we can safely hook a successful login
            Global.ScriptMgr.OnAccountLogin(account.game.Id);

            _worldSession = new WorldSession(account.game.Id, joinTicket.GameAccount, account.battleNet.Id, this, account.game.Security, (Expansion)account.game.Expansion,
                mutetime, account.game.OS, account.game.TimezoneOffset, account.game.Build, buildVariant, account.game.Locale, account.game.Recruiter, account.game.IsRectuiter);

            // Initialize Warden system only if it is enabled by config
            //if (wardenActive)
            //_worldSession.InitWarden(_sessionKey);

            _queryProcessor.AddCallback(_worldSession.LoadPermissionsAsync().WithCallback(LoadSessionPermissionsCallback));
            await AsyncRead();
        }

        void LoadSessionPermissionsCallback(SQLResult result)
        {
            // RBAC must be loaded before adding session to check for skip queue permission
            _worldSession.GetRBACData().LoadFromDBCallback(result);

            SendPacket(new EnterEncryptedMode(_encryptKey, true));
        }

        void HandleAuthContinuedSession(AuthContinuedSession authSession)
        {
            ConnectToKey key = new();
            _key = key.Raw = authSession.Key;

            _connectType = key.connectionType;
            if (_connectType != ConnectionType.Instance)
            {
                SendAuthResponseError(BattlenetRpcErrorCode.Denied);
                CloseSocket();
                return;
            }

            uint accountId = key.AccountId;
            PreparedStatement stmt = LoginDatabase.GetPreparedStatement(LoginStatements.SEL_ACCOUNT_INFO_CONTINUED_SESSION);
            stmt.AddValue(0, accountId);

            _queryProcessor.AddCallback(DB.Login.AsyncQuery(stmt).WithCallback(HandleAuthContinuedSessionCallback, authSession));
        }

        async void HandleAuthContinuedSessionCallback(AuthContinuedSession authSession, SQLResult result)
        {
            if (result.IsEmpty())
            {
                SendAuthResponseError(BattlenetRpcErrorCode.Denied);
                CloseSocket();
                return;
            }

            ConnectToKey key = new();
            _key = key.Raw = authSession.Key;

            uint accountId = key.AccountId;
            string login = result.Read<string>(0);
            _sessionKey = result.Read<byte[]>(1);

            HmacSha512 hmac = new(_sessionKey);
            hmac.Process(BitConverter.GetBytes(authSession.Key), 8);
            hmac.Process(authSession.LocalChallenge, authSession.LocalChallenge.Length);
            hmac.Process(_serverChallenge, 32);
            hmac.Finish(ContinuedSessionSeed, ContinuedSessionSeed.Length);

            if (!hmac.Digest.Compare(authSession.Digest))
            {
                Log.outError(LogFilter.Network, "WorldSocket.HandleAuthContinuedSession: Authentication failed for account: {0} ('{1}') address: {2}", accountId, login, GetRemoteIpAddress());
                CloseSocket();
                return;
            }

            HmacSha512 encryptKeyGen = new(_sessionKey);
            encryptKeyGen.Process(authSession.LocalChallenge, authSession.LocalChallenge.Length);
            encryptKeyGen.Process(_serverChallenge, 32);
            encryptKeyGen.Finish(EncryptionKeySeed, EncryptionKeySeed.Length);

            // only first 32 bytes of the hmac are used
            Buffer.BlockCopy(encryptKeyGen.Digest, 0, _encryptKey, 0, 32);

            SendPacket(new EnterEncryptedMode(_encryptKey, true));
            await AsyncRead();
        }

        void HandleConnectToFailed(ConnectToFailed connectToFailed)
        {
            if (_worldSession != null)
            {
                if (_worldSession.PlayerLoading())
                {
                    switch (connectToFailed.Serial)
                    {
                        case ConnectToSerial.WorldAttempt1:
                            _worldSession.SendConnectToInstance(ConnectToSerial.WorldAttempt2);
                            break;
                        case ConnectToSerial.WorldAttempt2:
                            _worldSession.SendConnectToInstance(ConnectToSerial.WorldAttempt3);
                            break;
                        case ConnectToSerial.WorldAttempt3:
                            _worldSession.SendConnectToInstance(ConnectToSerial.WorldAttempt4);
                            break;
                        case ConnectToSerial.WorldAttempt4:
                            _worldSession.SendConnectToInstance(ConnectToSerial.WorldAttempt5);
                            break;
                        case ConnectToSerial.WorldAttempt5:
                        {
                            Log.outError(LogFilter.Network, "{0} failed to connect 5 times to world socket, aborting login", _worldSession.GetPlayerInfo());
                            _worldSession.AbortLogin(LoginFailureReason.NoWorld);
                            break;
                        }
                        default:
                            return;
                    }
                }
                //else
                //{
                //    transfer_aborted when/if we get map node redirection
                //    SendPacket(*WorldPackets.Auth.ResumeComms());
                //}
            }

        }

        void HandleEnterEncryptedModeAck()
        {
            _worldCrypt.Initialize(_encryptKey);
            if (_connectType == ConnectionType.Realm)
                Global.WorldMgr.AddSession(_worldSession);
            else
                Global.WorldMgr.AddInstanceSocket(this, _key);
        }

        public void SendAuthResponseError(BattlenetRpcErrorCode code)
        {
            AuthResponse response = new();
            response.SuccessInfo = null;
            response.WaitInfo = null;
            response.Result = code;
            SendPacket(response);
        }

        bool HandlePing(Ping ping)
        {
            if (_LastPingTime == 0)
                _LastPingTime = GameTime.GetGameTime(); // for 1st ping
            else
            {
                long now = GameTime.GetGameTime();
                long diff = now - _LastPingTime;
                _LastPingTime = now;

                if (diff < 27)
                {
                    ++_OverSpeedPings;

                    uint maxAllowed = WorldConfig.GetUIntValue(WorldCfg.MaxOverspeedPings);
                    if (maxAllowed != 0 && _OverSpeedPings > maxAllowed)
                    {
                        lock (_worldSessionLock)
                        {
                            if (_worldSession != null && !_worldSession.HasPermission(RBACPermissions.SkipCheckOverspeedPing))
                            {
                                Log.outError(LogFilter.Network, "WorldSocket:HandlePing: {0} kicked for over-speed pings (address: {1})", _worldSession.GetPlayerInfo(), GetRemoteIpAddress());
                                //return ReadDataHandlerResult.Error;
                            }
                        }
                    }
                }
                else
                    _OverSpeedPings = 0;
            }

            lock (_worldSessionLock)
            {
                if (_worldSession != null)
                    _worldSession.SetLatency(ping.Latency);
                else
                {
                    Log.outError(LogFilter.Network, "WorldSocket:HandlePing: peer sent CMSG_PING, but is not authenticated or got recently kicked, address = {0}", GetRemoteIpAddress());
                    return false;
                }
            }

            SendPacket(new Pong(ping.Serial));
            return true;
        }
    }

    class AccountInfo
    {
        public AccountInfo(SQLFields fields)
        {
            //           0              1           2          3                4            5           6               7         8            9    10                 11     12                13
            // SELECT a.id, a.session_key, ba.last_ip, ba.locked, ba.lock_country, a.expansion, a.mutetime, a.client_build, a.locale, a.recruiter, a.os, a.timezone_offset, ba.id, aa.SecurityLevel,
            //                                                              14                                                            15    16
            // bab.unbandate > UNIX_TIMESTAMP() OR bab.unbandate = bab.bandate, ab.unbandate > UNIX_TIMESTAMP() OR ab.unbandate = ab.bandate, r.id
            // FROM account a LEFT JOIN battlenet_accounts ba ON a.battlenet_account = ba.id LEFT JOIN account_access aa ON a.id = aa.AccountID AND aa.RealmID IN (-1, ?)
            // LEFT JOIN battlenet_account_bans bab ON ba.id = bab.id LEFT JOIN account_banned ab ON a.id = ab.id LEFT JOIN account r ON a.id = r.recruiter
            // WHERE a.username = ? AND LENGTH(a.session_key) = 40 ORDER BY aa.RealmID DESC LIMIT 1
            game.Id = fields.Read<uint>(0);
            game.KeyData = fields.Read<byte[]>(1);
            battleNet.LastIP = fields.Read<string>(2);
            battleNet.IsLockedToIP = fields.Read<bool>(3);
            battleNet.LockCountry = fields.Read<string>(4);
            game.Expansion = fields.Read<byte>(5);
            game.MuteTime = fields.Read<long>(6);
            game.Build = fields.Read<uint>(7);
            game.Locale = (Locale)fields.Read<byte>(8);
            game.Recruiter = fields.Read<uint>(9);
            game.OS = fields.Read<string>(10);
            game.TimezoneOffset = TimeSpan.FromMinutes(fields.Read<short>(11));
            battleNet.Id = fields.Read<uint>(12);
            game.Security = (AccountTypes)fields.Read<byte>(13);
            battleNet.IsBanned = fields.Read<uint>(14) != 0;
            game.IsBanned = fields.Read<uint>(15) != 0;
            game.IsRectuiter = fields.Read<uint>(16) != 0;

            if (game.Locale >= Locale.Total)
                game.Locale = Locale.enUS;
        }

        public bool IsBanned() { return battleNet.IsBanned || game.IsBanned; }

        public BattleNet battleNet;
        public Game game;

        public struct BattleNet
        {
            public uint Id;
            public bool IsLockedToIP;
            public string LastIP;
            public string LockCountry;
            public bool IsBanned;
        }

        public struct Game
        {
            public uint Id;
            public byte[] KeyData;
            public byte Expansion;
            public long MuteTime;
            public uint Build;
            public Locale Locale;
            public uint Recruiter;
            public string OS;
            public TimeSpan TimezoneOffset;
            public bool IsRectuiter;
            public AccountTypes Security;
            public bool IsBanned;
        }
    }

    enum ReadDataHandlerResult
    {
        Ok = 0,
        Error = 1,
        WaitingForQuery = 2
    }
}
