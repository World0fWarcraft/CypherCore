﻿// Copyright (c) CypherCore <http://github.com/CypherCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Framework.Constants;
using Game.Entities;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game.Networking.Packets
{
    public class GMTicketGetSystemStatus : ClientPacket
    {
        public GMTicketGetSystemStatus(WorldPacket packet) : base(packet) { }

        public override void Read() { }
    }

    public class GMTicketSystemStatusPkt : ServerPacket
    {
        public GMTicketSystemStatusPkt() : base(ServerOpcodes.GmTicketSystemStatus) { }

        public override void Write()
        {
            _worldPacket.WriteInt32(Status);
        }

        public int Status;
    }

    public class GMTicketGetCaseStatus : ClientPacket
    {
        public GMTicketGetCaseStatus(WorldPacket packet) : base(packet) { }

        public override void Read() { }
    }

    public class GMTicketCaseStatus : ServerPacket
    {
        public GMTicketCaseStatus() : base(ServerOpcodes.GmTicketCaseStatus) { }

        public override void Write()
        {
            _worldPacket.WriteInt32(Cases.Count);

            foreach (var c in Cases)
            {
                _worldPacket.WriteInt32(c.CaseID);
                _worldPacket.WriteInt64(c.CaseOpened);
                _worldPacket.WriteInt32(c.CaseStatus);
                _worldPacket.WriteUInt16(c.CfgRealmID);
                _worldPacket.WriteUInt64(c.CharacterID);
                _worldPacket.WriteInt32(c.WaitTimeOverrideMinutes);

                _worldPacket.WriteBits(c.Url.GetByteCount(), 11);
                _worldPacket.WriteBits(c.WaitTimeOverrideMessage.GetByteCount(), 10);
                _worldPacket.WriteBits(c.Title, 24);
                _worldPacket.WriteBits(c.Description, 24);

                _worldPacket.WriteString(c.Url);
                _worldPacket.WriteString(c.WaitTimeOverrideMessage);
                _worldPacket.WriteString(c.Title);
                _worldPacket.WriteString(c.Description);
            }
        }

        public List<GMTicketCase> Cases = new();

        public struct GMTicketCase
        {
            public int CaseID;
            public long CaseOpened;
            public int CaseStatus;
            public ushort CfgRealmID;
            public ulong CharacterID;
            public int WaitTimeOverrideMinutes;
            public string Url;
            public string WaitTimeOverrideMessage;
            public string Title;
            public string Description;
        }
    }

    public class GMTicketAcknowledgeSurvey : ClientPacket
    {
        public GMTicketAcknowledgeSurvey(WorldPacket packet) : base(packet) { }

        public override void Read()
        {
            CaseID = _worldPacket.ReadInt32();
        }

        int CaseID;
    }

    public class SubmitUserFeedback : ClientPacket
    {
        public SupportTicketHeader Header;
        public string Note;
        public bool IsSuggestion;

        public SubmitUserFeedback(WorldPacket packet) : base(packet) { }

        public override void Read()
        {
            Header.Read(_worldPacket);
            uint noteLength = _worldPacket.ReadBits<uint>(24);
            IsSuggestion = _worldPacket.HasBit();

            if (noteLength != 0)
                Note = _worldPacket.ReadString(noteLength - 1);
        }
    }

    public class SupportTicketSubmitComplaint : ClientPacket
    {
        public SupportTicketSubmitComplaint(WorldPacket packet) : base(packet) { }

        public override void Read()
        {
            Header.Read(_worldPacket);
            TargetCharacterGUID = _worldPacket.ReadPackedGuid();
            ReportType = _worldPacket.ReadInt32();
            MajorCategory = _worldPacket.ReadInt32();
            MinorCategoryFlags = _worldPacket.ReadInt32();
            ChatLog.Read(_worldPacket);

            uint noteLength = _worldPacket.ReadBits<uint>(10);
            bool hasMailInfo = _worldPacket.HasBit();
            bool hasCalendarInfo = _worldPacket.HasBit();
            bool hasPetInfo = _worldPacket.HasBit();
            bool hasGuildInfo = _worldPacket.HasBit();
            bool hasLfgListEntryInfo = _worldPacket.HasBit();
            bool hasLfgListAppInfo = _worldPacket.HasBit();
            bool hasVoiceChatInfo = _worldPacket.HasBit();
            bool hasClubFinderResult = _worldPacket.HasBit();
            bool hasArenaTeamInfo = _worldPacket.HasBit();

            _worldPacket.ResetBitPos();

            if (hasVoiceChatInfo)
            {
                SupportTicketVoiceChatInfo voiceChatInfo = new();
                voiceChatInfo.TargetIsCurrentlyInVoiceChatWithPlayer = _worldPacket.HasBit();
                VoiceChatInfo = voiceChatInfo;
                _worldPacket.ResetBitPos();
            }

            HorusChatLog.Read(_worldPacket);

            Note = _worldPacket.ReadString(noteLength);

            if (hasMailInfo)
            {
                MailInfo = new();
                MailInfo.Value.Read(_worldPacket);
            }

            if (hasCalendarInfo)
            {
                CalenderInfo = new();
                CalenderInfo.Value.Read(_worldPacket);
            }

            if (hasPetInfo)
            {
                PetInfo = new();
                PetInfo.Value.Read(_worldPacket);
            }

            if (hasGuildInfo)
            {
                GuildInfo = new();
                GuildInfo.Value.Read(_worldPacket);
            }

            if (hasLfgListEntryInfo)
            {
                LfgListEntryInfo = new();
                LfgListEntryInfo.Value.Read(_worldPacket);
            }

            if (hasLfgListAppInfo)
            {
                LfgListAppInfo = new();
                LfgListAppInfo.Value.Read(_worldPacket);
            }

            if (hasClubFinderResult)
            {
                ClubFinderInfo = new();
                ClubFinderInfo.Value.Read(_worldPacket);
            }

            if (hasArenaTeamInfo)
            {
                ArenaTeamInfo = new();
                ArenaTeamInfo.Value.Read(_worldPacket);
            }
        }

        public SupportTicketHeader Header;
        public SupportTicketChatLog ChatLog;
        public ObjectGuid TargetCharacterGUID;
        public int ReportType;
        public int MajorCategory;
        public int MinorCategoryFlags;
        public string Note;
        public SupportTicketHorusChatLog HorusChatLog;
        public SupportTicketMailInfo? MailInfo;
        public SupportTicketCalendarEventInfo? CalenderInfo;
        public SupportTicketPetInfo? PetInfo;
        public SupportTicketGuildInfo? GuildInfo;
        public SupportTicketLFGListEntryInfo? LfgListEntryInfo;
        public SupportTicketLFGListApplicant? LfgListAppInfo;
        public SupportTicketVoiceChatInfo? VoiceChatInfo;
        public SupportTicketClubFinderInfo? ClubFinderInfo;
        public SupportTicketArenaTeamInfo? ArenaTeamInfo;

        public struct SupportTicketChatLine
        {
            public long Timestamp;
            public string Text;

            public SupportTicketChatLine(WorldPacket data)
            {
                Timestamp = data.ReadInt64();
                Text = data.ReadString(data.ReadBits<uint>(12));
            }

            public SupportTicketChatLine(long timestamp, string text)
            {
                Timestamp = timestamp;
                Text = text;
            }

            public void Read(WorldPacket data)
            {
                Timestamp = data.ReadUInt32();
                Text = data.ReadString(data.ReadBits<uint>(12));
            }
        }

        public class SupportTicketChatLog
        {
            public void Read(WorldPacket data)
            {
                uint linesCount = data.ReadUInt32();
                bool hasReportLineIndex = data.HasBit();

                data.ResetBitPos();

                for (uint i = 0; i < linesCount; i++)
                    Lines.Add(new SupportTicketChatLine(data));

                if (hasReportLineIndex)
                    ReportLineIndex = data.ReadUInt32();
            }

            public List<SupportTicketChatLine> Lines = new();
            public uint? ReportLineIndex;
        }

        public struct SupportTicketHorusChatLine
        {
            public void Read(WorldPacket data)
            {
                Timestamp = data.ReadInt64();
                PlayerGuid = data.ReadPackedGuid();

                bool hasClubID = data.HasBit();
                bool hasChannelGUID = data.HasBit();
                bool hasRealmAddress = data.HasBit();
                bool hasSlashCmd = data.HasBit();
                uint textLength = data.ReadBits<uint>(12);

                if (hasClubID)
                    ClubID = data.ReadUInt64();

                if (hasChannelGUID)
                    ChannelGuid = data.ReadPackedGuid();

                if (hasRealmAddress)
                {
                    ServerSpec senderRealm = new();
                    senderRealm.Realm = data.ReadUInt32();
                    senderRealm.Server = data.ReadUInt16();
                    senderRealm.Type = data.ReadUInt8();
                    WorldServer = senderRealm;
                }

                if (hasSlashCmd)
                    Cmd = data.ReadInt32();

                Text = data.ReadString(textLength);
            }

            public struct ServerSpec
            {
                public uint Realm;
                public ushort Server;
                public byte Type;
            }

            public long Timestamp;
            public ObjectGuid PlayerGuid;
            public ulong? ClubID;
            public ObjectGuid? ChannelGuid;
            public ServerSpec? WorldServer;
            public int? Cmd;
            public string Text;
        }

        public class SupportTicketHorusChatLog
        {
            public List<SupportTicketHorusChatLine> Lines = new();

            public void Read(WorldPacket data)
            {
                uint linesCount = data.ReadUInt32();
                data.ResetBitPos();

                for (uint i = 0; i < linesCount; i++)
                {
                    var chatLine = new SupportTicketHorusChatLine();
                    chatLine.Read(data);
                    Lines.Add(chatLine);
                }
            }
        }

        public struct SupportTicketMailInfo
        {
            public void Read(WorldPacket data)
            {
                MailID = data.ReadUInt64();
                uint bodyLength = data.ReadBits<uint>(13);
                uint subjectLength = data.ReadBits<uint>(9);

                MailBody = data.ReadString(bodyLength);
                MailSubject = data.ReadString(subjectLength);
            }

            public ulong MailID;
            public string MailSubject;
            public string MailBody;
        }

        public struct SupportTicketCalendarEventInfo
        {
            public void Read(WorldPacket data)
            {
                EventID = data.ReadUInt64();
                InviteID = data.ReadUInt64();

                EventTitle = data.ReadString(data.ReadBits<byte>(8));
            }

            public ulong EventID;
            public ulong InviteID;
            public string EventTitle;
        }

        public struct SupportTicketPetInfo
        {
            public void Read(WorldPacket data)
            {
                PetID = data.ReadPackedGuid();

                PetName = data.ReadString(data.ReadBits<byte>(8));
            }

            public ObjectGuid PetID;
            public string PetName;
        }

        public struct SupportTicketGuildInfo
        {
            public void Read(WorldPacket data)
            {
                byte nameLength = data.ReadBits<byte>(8);
                GuildID = data.ReadPackedGuid();

                GuildName = data.ReadString(nameLength);
            }

            public ObjectGuid GuildID;
            public string GuildName;
        }

        public struct SupportTicketLFGListEntryInfo
        {
            public RideTicket Ticket;
            public uint ActivityID;
            public byte FactionID;
            public ObjectGuid LastTouchedName;
            public ObjectGuid LastTouchedComment;
            public ObjectGuid LastTouchedVoiceChat;
            public ObjectGuid LastTouchedAny;
            public ObjectGuid PartyGuid;
            public string Name;
            public string Comment;
            public string VoiceChat;

            public void Read(WorldPacket data)
            {
                Ticket = new RideTicket();
                Ticket.Read(data);

                ActivityID = data.ReadUInt32();
                FactionID = data.ReadUInt8();
                LastTouchedName = data.ReadPackedGuid();
                LastTouchedComment = data.ReadPackedGuid();
                LastTouchedVoiceChat = data.ReadPackedGuid();
                LastTouchedAny = data.ReadPackedGuid();
                PartyGuid = data.ReadPackedGuid();

                byte nameLength = data.ReadBits<byte>(10);
                byte commentLength = data.ReadBits<byte>(11);
                byte voiceChatLength = data.ReadBits<byte>(8);

                Name = data.ReadString(nameLength);
                Comment = data.ReadString(commentLength);
                VoiceChat = data.ReadString(voiceChatLength);
            }
        }

        public struct SupportTicketLFGListApplicant
        {
            public RideTicket Ticket;
            public string Comment;

            public void Read(WorldPacket data)
            {
                Ticket = new RideTicket();
                Ticket.Read(data);

                Comment = data.ReadString(data.ReadBits<uint>(9));
            }
        }

        public struct SupportTicketVoiceChatInfo
        {
            public bool TargetIsCurrentlyInVoiceChatWithPlayer;
        }

        public struct SupportTicketClubFinderInfo
        {
            public ulong PostingID;
            public ulong ClubID;
            public ObjectGuid GuildID;
            public string PostingDescription;

            public void Read(WorldPacket data)
            {
                PostingID = data.ReadUInt64();
                ClubID = data.ReadUInt64();
                GuildID = data.ReadPackedGuid();
                PostingDescription = data.ReadString(data.ReadBits<uint>(12));
            }
        }

        public struct SupportTicketArenaTeamInfo
        {
            public string ArenaTeamName;
            public ObjectGuid ArenaTeamID;

            public void Read(WorldPacket data)
            {
                uint arenaTeamNameLength = data.ReadBits<uint>(7);
                ArenaTeamID = data.ReadPackedGuid();
                ArenaTeamName = data.ReadString(arenaTeamNameLength);
            }
        }
    }

    class Complaint : ClientPacket
    {
        public Complaint(WorldPacket packet) : base(packet) { }

        public override void Read()
        {
            ComplaintType = (SupportSpamType)_worldPacket.ReadUInt8();
            Offender.Read(_worldPacket);

            switch (ComplaintType)
            {
                case SupportSpamType.Mail:
                    MailID = _worldPacket.ReadUInt64();
                    break;
                case SupportSpamType.Chat:
                    Chat.Read(_worldPacket);
                    break;
                case SupportSpamType.Calendar:
                    EventGuid = _worldPacket.ReadUInt64();
                    InviteGuid = _worldPacket.ReadUInt64();
                    break;
            }
        }

        public SupportSpamType ComplaintType;
        public ComplaintOffender Offender;
        public ulong MailID;
        public ComplaintChat Chat;

        public ulong EventGuid;
        public ulong InviteGuid;

        public struct ComplaintOffender
        {
            public void Read(WorldPacket data)
            {
                PlayerGuid = data.ReadPackedGuid();
                RealmAddress = data.ReadUInt32();
                TimeSinceOffence = data.ReadUInt32();
            }

            public ObjectGuid PlayerGuid;
            public uint RealmAddress;
            public uint TimeSinceOffence;
        }

        public struct ComplaintChat
        {
            public void Read(WorldPacket data)
            {
                Command = data.ReadUInt32();
                ChannelID = data.ReadUInt32();
                MessageLog = data.ReadString(data.ReadBits<uint>(12));
            }

            public uint Command;
            public uint ChannelID;
            public string MessageLog;
        }
    }

    public class ComplaintResult : ServerPacket
    {
        public ComplaintResult() : base(ServerOpcodes.ComplaintResult) { }

        public override void Write()
        {
            _worldPacket.WriteUInt32((uint)ComplaintType);
            _worldPacket.WriteUInt8(Result);
        }

        public SupportSpamType ComplaintType;
        public byte Result;
    }

    class BugReport : ClientPacket
    {
        public BugReport(WorldPacket packet) : base(packet) { }

        public override void Read()
        {
            Type = _worldPacket.ReadBit();
            uint diagLen = _worldPacket.ReadBits<uint>(12);
            uint textLen = _worldPacket.ReadBits<uint>(10);
            DiagInfo = _worldPacket.ReadString(diagLen);
            Text = _worldPacket.ReadString(textLen);
        }

        public uint Type;
        public string Text;
        public string DiagInfo;
    }

    //Structs
    public struct SupportTicketHeader
    {
        public void Read(WorldPacket packet)
        {
            MapID = packet.ReadUInt32();
            Position = packet.ReadVector3();
            Facing = packet.ReadFloat();
            Program = packet.ReadInt32();
        }

        public uint MapID;
        public Vector3 Position;
        public float Facing;
        public int Program;
    }
}
