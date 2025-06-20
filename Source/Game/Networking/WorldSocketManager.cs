﻿// Copyright (c) CypherCore <http://github.com/CypherCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Framework.Configuration;
using Framework.Constants;
using Framework.Networking;
using System.Net.Sockets;

namespace Game.Networking
{
    public class WorldSocketManager : SocketManager<WorldSocket>
    {
        public static WorldSocketManager Instance { get; } = new WorldSocketManager();

        public override bool StartNetwork(string bindIp, int port, int threadCount = 1)
        {
            _tcpNoDelay = ConfigMgr.GetDefaultValue("Network.TcpNodelay", true);

            Log.outDebug(LogFilter.Misc, "Max allowed socket connections {0}", ushort.MaxValue);

            // -1 means use default
            _socketSendBufferSize = ConfigMgr.GetDefaultValue("Network.OutKBuff", -1);

            if (!base.StartNetwork(bindIp, port, threadCount))
                return false;

            Acceptor.AsyncAcceptSocket(OnSocketAccept);

            return true;
        }

        public override void StopNetwork()
        {
            base.StopNetwork();
        }

        public override void OnSocketOpen(Socket sock)
        {
            // set some options here
            try
            {
                if (_socketSendBufferSize >= 0)
                    sock.SendBufferSize = _socketSendBufferSize;

                // Set TCP_NODELAY.
                sock.NoDelay = _tcpNoDelay;
            }
            catch (SocketException ex)
            {
                Log.outException(ex);
                return;
            }

            base.OnSocketOpen(sock);
        }

        static void OnSocketAccept(Socket sock)
        {
            Global.WorldSocketMgr.OnSocketOpen(sock);
        }

        int _socketSendBufferSize;
        bool _tcpNoDelay;
    }
}
