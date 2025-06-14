﻿// Copyright (c) CypherCore <http://github.com/CypherCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Framework.Configuration;
using Framework.Constants;
using Game.DataStorage;
using System;
using System.Collections.Generic;

namespace Game
{
    public class WorldConfig : ConfigMgr
    {
        public static void Load(bool reload = false)
        {
            if (reload)
                Load("WorldServer.conf");

            // Read support system setting from the config file
            Values[WorldCfg.SupportEnabled] = GetDefaultValue("Support.Enabled", true);
            Values[WorldCfg.SupportTicketsEnabled] = GetDefaultValue("Support.TicketsEnabled", false);
            Values[WorldCfg.SupportBugsEnabled] = GetDefaultValue("Support.BugsEnabled", false);
            Values[WorldCfg.SupportComplaintsEnabled] = GetDefaultValue("Support.ComplaintsEnabled", false);
            Values[WorldCfg.SupportSuggestionsEnabled] = GetDefaultValue("Support.SuggestionsEnabled", false);

            // Send server info on login?
            Values[WorldCfg.EnableSinfoLogin] = GetDefaultValue("Server.LoginInfo", 0);

            // Read all rates from the config file
            static void SetRegenRate(WorldCfg rate, string configKey)
            {
                Values[rate] = GetDefaultValue(configKey, 1.0f);
                if ((float) Values[rate] < 0.0f)
                {
                    Log.outError(LogFilter.ServerLoading, "{0} ({1}) must be > 0. Using 1 instead.", configKey, Values[rate]);
                    Values[rate] = 1;
                }
            }

            SetRegenRate(WorldCfg.RateHealth, "Rate.Health");
            SetRegenRate(WorldCfg.RatePowerMana, "Rate.Mana");
            SetRegenRate(WorldCfg.RatePowerRageIncome, "Rate.Rage.Gain");
            SetRegenRate(WorldCfg.RatePowerRageLoss, "Rate.Rage.Loss");
            SetRegenRate(WorldCfg.RatePowerFocus, "Rate.Focus");
            SetRegenRate(WorldCfg.RatePowerEnergy, "Rate.Energy");
            SetRegenRate(WorldCfg.RatePowerComboPointsLoss, "Rate.ComboPoints.Loss");
            SetRegenRate(WorldCfg.RatePowerRunicPowerIncome, "Rate.RunicPower.Gain");
            SetRegenRate(WorldCfg.RatePowerRunicPowerLoss, "Rate.RunicPower.Loss");
            SetRegenRate(WorldCfg.RatePowerSoulShards, "Rate.SoulShards.Loss");
            SetRegenRate(WorldCfg.RatePowerLunarPower, "Rate.LunarPower.Loss");
            SetRegenRate(WorldCfg.RatePowerHolyPower, "Rate.HolyPower.Loss");
            SetRegenRate(WorldCfg.RatePowerMaelstrom, "Rate.Maelstrom.Loss");
            SetRegenRate(WorldCfg.RatePowerChi, "Rate.Chi.Loss");
            SetRegenRate(WorldCfg.RatePowerInsanity, "Rate.Insanity.Loss");
            SetRegenRate(WorldCfg.RatePowerArcaneCharges, "Rate.ArcaneCharges.Loss");
            SetRegenRate(WorldCfg.RatePowerFury, "Rate.Fury.Loss");
            SetRegenRate(WorldCfg.RatePowerPain, "Rate.Pain.Loss");
            SetRegenRate(WorldCfg.RatePowerEssence, "Rate.Essence.Loss");

            Values[WorldCfg.RateSkillDiscovery] = GetDefaultValue("Rate.Skill.Discovery", 1.0f);
            Values[WorldCfg.RateDropItemPoor] = GetDefaultValue("Rate.Drop.Item.Poor", 1.0f);
            Values[WorldCfg.RateDropItemNormal] = GetDefaultValue("Rate.Drop.Item.Normal", 1.0f);
            Values[WorldCfg.RateDropItemUncommon] = GetDefaultValue("Rate.Drop.Item.Uncommon", 1.0f);
            Values[WorldCfg.RateDropItemRare] = GetDefaultValue("Rate.Drop.Item.Rare", 1.0f);
            Values[WorldCfg.RateDropItemEpic] = GetDefaultValue("Rate.Drop.Item.Epic", 1.0f);
            Values[WorldCfg.RateDropItemLegendary] = GetDefaultValue("Rate.Drop.Item.Legendary", 1.0f);
            Values[WorldCfg.RateDropItemArtifact] = GetDefaultValue("Rate.Drop.Item.Artifact", 1.0f);
            Values[WorldCfg.RateDropItemReferenced] = GetDefaultValue("Rate.Drop.Item.Referenced", 1.0f);
            Values[WorldCfg.RateDropItemReferencedAmount] = GetDefaultValue("Rate.Drop.Item.ReferencedAmount", 1.0f);
            Values[WorldCfg.RateDropMoney] = GetDefaultValue("Rate.Drop.Money", 1.0f);
            Values[WorldCfg.RateXpKill] = GetDefaultValue("Rate.XP.Kill", 1.0f);
            Values[WorldCfg.RateXpBgKill] = GetDefaultValue("Rate.XP.BattlegroundKill", 1.0f);
            Values[WorldCfg.RateXpQuest] = GetDefaultValue("Rate.XP.Quest", 1.0f);
            Values[WorldCfg.RateXpExplore] = GetDefaultValue("Rate.XP.Explore", 1.0f);

            Values[WorldCfg.XpBoostDaymask] = GetDefaultValue("XP.Boost.Daymask", 0);
            Values[WorldCfg.RateXpBoost] = GetDefaultValue("XP.Boost.Rate", 2.0f);

            Values[WorldCfg.RateRepaircost] = GetDefaultValue("Rate.RepairCost", 1.0f);
            if ((float)Values[WorldCfg.RateRepaircost] < 0.0f)
            {
                Log.outError(LogFilter.ServerLoading, "Rate.RepairCost ({0}) must be >=0. Using 0.0 instead.", Values[WorldCfg.RateRepaircost]);
                Values[WorldCfg.RateRepaircost] = 0.0f;
            }
            Values[WorldCfg.RateReputationGain] = GetDefaultValue("Rate.Reputation.Gain", 1.0f);
            Values[WorldCfg.RateReputationLowLevelKill] = GetDefaultValue("Rate.Reputation.LowLevel.Kill", 1.0f);
            Values[WorldCfg.RateReputationLowLevelQuest] = GetDefaultValue("Rate.Reputation.LowLevel.Quest", 1.0f);
            Values[WorldCfg.RateReputationRecruitAFriendBonus] = GetDefaultValue("Rate.Reputation.RecruitAFriendBonus", 0.1f);
            Values[WorldCfg.RateCreatureHpNormal] = GetDefaultValue("Rate.Creature.HP.Normal", 1.0f);
            Values[WorldCfg.RateCreatureHpElite] = GetDefaultValue("Rate.Creature.HP.Elite", 1.0f);
            Values[WorldCfg.RateCreatureHpRareelite] = GetDefaultValue("Rate.Creature.HP.RareElite", 1.0f);
            Values[WorldCfg.RateCreatureHpObsolete] = GetDefaultValue("Rate.Creature.HP.Obsolete", 1.0f);
            Values[WorldCfg.RateCreatureHpRare] = GetDefaultValue("Rate.Creature.HP.Rare", 1.0f);
            Values[WorldCfg.RateCreatureHpTrivial] = GetDefaultValue("Rate.Creature.HP.Trivial", 1.0f);
            Values[WorldCfg.RateCreatureHpMinusmob] = GetDefaultValue("Rate.Creature.HP.MinusMob", 1.0f);
            Values[WorldCfg.RateCreatureDamageNormal] = GetDefaultValue("Rate.Creature.Damage.Normal", 1.0f);
            Values[WorldCfg.RateCreatureDamageElite] = GetDefaultValue("Rate.Creature.Damage.Elite", 1.0f);
            Values[WorldCfg.RateCreatureDamageRareelite] = GetDefaultValue("Rate.Creature.Damage.RareElite", 1.0f);
            Values[WorldCfg.RateCreatureDamageObsolete] = GetDefaultValue("Rate.Creature.Damage.Obsolete", 1.0f);
            Values[WorldCfg.RateCreatureDamageRare] = GetDefaultValue("Rate.Creature.Damage.Rare", 1.0f);
            Values[WorldCfg.RateCreatureDamageTrivial] = GetDefaultValue("Rate.Creature.Damage.Trivial", 1.0f);
            Values[WorldCfg.RateCreatureDamageMinusmob] = GetDefaultValue("Rate.Creature.Damage.MinusMob", 1.0f);
            Values[WorldCfg.RateCreatureSpelldamageNormal] = GetDefaultValue("Rate.Creature.SpellDamage.Normal", 1.0f);
            Values[WorldCfg.RateCreatureSpelldamageElite] = GetDefaultValue("Rate.Creature.SpellDamage.Elite", 1.0f);
            Values[WorldCfg.RateCreatureSpelldamageRareelite] = GetDefaultValue("Rate.Creature.SpellDamage.RareElite", 1.0f);
            Values[WorldCfg.RateCreatureSpelldamageObsolete] = GetDefaultValue("Rate.Creature.SpellDamage.Obsolete", 1.0f);
            Values[WorldCfg.RateCreatureSpelldamageRare] = GetDefaultValue("Rate.Creature.SpellDamage.Rare", 1.0f);
            Values[WorldCfg.RateCreatureSpelldamageTrivial] = GetDefaultValue("Rate.Creature.SpellDamage.Trivial", 1.0f);
            Values[WorldCfg.RateCreatureSpelldamageMinusmob] = GetDefaultValue("Rate.Creature.SpellDamage.MinusMob", 1.0f);
            Values[WorldCfg.RateCreatureAggro] = GetDefaultValue("Rate.Creature.Aggro", 1.0f);
            Values[WorldCfg.RateRestIngame] = GetDefaultValue("Rate.Rest.InGame", 1.0f);
            Values[WorldCfg.RateRestOfflineInTavernOrCity] = GetDefaultValue("Rate.Rest.Offline.InTavernOrCity", 1.0f);
            Values[WorldCfg.RateRestOfflineInWilderness] = GetDefaultValue("Rate.Rest.Offline.InWilderness", 1.0f);
            Values[WorldCfg.RateDamageFall] = GetDefaultValue("Rate.Damage.Fall", 1.0f);
            Values[WorldCfg.RateAuctionTime] = GetDefaultValue("Rate.Auction.Time", 1.0f);
            Values[WorldCfg.RateAuctionDeposit] = GetDefaultValue("Rate.Auction.Deposit", 1.0f);
            Values[WorldCfg.RateAuctionCut] = GetDefaultValue("Rate.Auction.Cut", 1.0f);
            Values[WorldCfg.RateHonor] = GetDefaultValue("Rate.Honor", 1.0f);
            Values[WorldCfg.RateInstanceResetTime] = GetDefaultValue("Rate.InstanceResetTime", 1.0f);
            Values[WorldCfg.RateMovespeed] = GetDefaultValue("Rate.MoveSpeed", 1.0f);
            if ((float)Values[WorldCfg.RateMovespeed] < 0.0f)
            {
                Log.outError(LogFilter.ServerLoading, "Rate.MoveSpeed ({0}) must be > 0. Using 1 instead.", Values[WorldCfg.RateMovespeed]);
                Values[WorldCfg.RateMovespeed] = 1.0f;
            }

            Values[WorldCfg.RateCorpseDecayLooted] = GetDefaultValue("Rate.Corpse.Decay.Looted", 0.5f);

            Values[WorldCfg.RateDurabilityLossOnDeath] = GetDefaultValue("DurabilityLoss.OnDeath", 10.0f);
            if ((float)Values[WorldCfg.RateDurabilityLossOnDeath] < 0.0f)
            {
                Log.outError(LogFilter.ServerLoading, "DurabilityLoss.OnDeath ({0}) must be >=0. Using 0.0 instead.", Values[WorldCfg.RateDurabilityLossOnDeath]);
                Values[WorldCfg.RateDurabilityLossOnDeath] = 0.0f;
            }
            if ((float)Values[WorldCfg.RateDurabilityLossOnDeath] > 100.0f)
            {
                Log.outError(LogFilter.ServerLoading, "DurabilityLoss.OnDeath ({0}) must be <= 100. Using 100.0 instead.", Values[WorldCfg.RateDurabilityLossOnDeath]);
                Values[WorldCfg.RateDurabilityLossOnDeath] = 0.0f;
            }
            Values[WorldCfg.RateDurabilityLossOnDeath] = (float)Values[WorldCfg.RateDurabilityLossOnDeath] / 100.0f;

            Values[WorldCfg.RateDurabilityLossDamage] = GetDefaultValue("DurabilityLossChance.Damage", 0.5f);
            if ((float)Values[WorldCfg.RateDurabilityLossDamage] < 0.0f)
            {
                Log.outError(LogFilter.ServerLoading, "DurabilityLossChance.Damage ({0}) must be >=0. Using 0.0 instead.", Values[WorldCfg.RateDurabilityLossDamage]);
                Values[WorldCfg.RateDurabilityLossDamage] = 0.0f;
            }
            Values[WorldCfg.RateDurabilityLossAbsorb] = GetDefaultValue("DurabilityLossChance.Absorb", 0.5f);
            if ((float)Values[WorldCfg.RateDurabilityLossAbsorb] < 0.0f)
            {
                Log.outError(LogFilter.ServerLoading, "DurabilityLossChance.Absorb ({0}) must be >=0. Using 0.0 instead.", Values[WorldCfg.RateDurabilityLossAbsorb]);
                Values[WorldCfg.RateDurabilityLossAbsorb] = 0.0f;
            }
            Values[WorldCfg.RateDurabilityLossParry] = GetDefaultValue("DurabilityLossChance.Parry", 0.05f);
            if ((float)Values[WorldCfg.RateDurabilityLossParry] < 0.0f)
            {
                Log.outError(LogFilter.ServerLoading, "DurabilityLossChance.Parry ({0}) must be >=0. Using 0.0 instead.", Values[WorldCfg.RateDurabilityLossParry]);
                Values[WorldCfg.RateDurabilityLossParry] = 0.0f;
            }
            Values[WorldCfg.RateDurabilityLossBlock] = GetDefaultValue("DurabilityLossChance.Block", 0.05f);
            if ((float)Values[WorldCfg.RateDurabilityLossBlock] < 0.0f)
            {
                Log.outError(LogFilter.ServerLoading, "DurabilityLossChance.Block ({0}) must be >=0. Using 0.0 instead.", Values[WorldCfg.RateDurabilityLossBlock]);
                Values[WorldCfg.RateDurabilityLossBlock] = 0.0f;
            }
            Values[WorldCfg.RateMoneyQuest] = GetDefaultValue("Rate.Quest.Money.Reward", 1.0f);
            if ((float)Values[WorldCfg.RateMoneyQuest] < 0.0f)
            {
                Log.outError(LogFilter.ServerLoading, "Rate.Quest.Money.Reward ({0}) must be >=0. Using 0 instead.", Values[WorldCfg.RateMoneyQuest]);
                Values[WorldCfg.RateMoneyQuest] = 0.0f;
            }
            Values[WorldCfg.RateMoneyMaxLevelQuest] = GetDefaultValue("Rate.Quest.Money.Max.Level.Reward", 1.0f);
            if ((float)Values[WorldCfg.RateMoneyMaxLevelQuest] < 0.0f)
            {
                Log.outError(LogFilter.ServerLoading, "Rate.Quest.Money.Max.Level.Reward ({0}) must be >=0. Using 0 instead.", Values[WorldCfg.RateMoneyMaxLevelQuest]);
                Values[WorldCfg.RateMoneyMaxLevelQuest] = 0.0f;
            }

            // Read other configuration items from the config file
            Values[WorldCfg.DurabilityLossInPvp] = GetDefaultValue("DurabilityLoss.InPvP", false);

            Values[WorldCfg.Compression] = GetDefaultValue("Compression", 1);
            if ((int)Values[WorldCfg.Compression] < 1 || (int)Values[WorldCfg.Compression] > 9)
            {
                Log.outError(LogFilter.ServerLoading, "Compression Level ({0}) must be in range 1..9. Using default compression Level (1).", Values[WorldCfg.Compression]);
                Values[WorldCfg.Compression] = 1;
            }
            Values[WorldCfg.AddonChannel] = GetDefaultValue("AddonChannel", true);
            Values[WorldCfg.CleanCharacterDb] = GetDefaultValue("CleanCharacterDB", false);
            Values[WorldCfg.PersistentCharacterCleanFlags] = GetDefaultValue("PersistentCharacterCleanFlags", 0);
            Values[WorldCfg.AuctionReplicateDelay] = GetDefaultValue("Auction.ReplicateItemsCooldown", 900);
            Values[WorldCfg.AuctionSearchDelay] = GetDefaultValue("Auction.SearchDelay", 300);
            if ((int)Values[WorldCfg.AuctionSearchDelay] < 100 || (int)Values[WorldCfg.AuctionSearchDelay] > 10000)
            {
                Log.outError(LogFilter.ServerLoading, "Auction.SearchDelay ({0}) must be between 100 and 10000. Using default of 300ms", Values[WorldCfg.AuctionSearchDelay]);
                Values[WorldCfg.AuctionSearchDelay] = 300;
            }
            Values[WorldCfg.AuctionTaintedSearchDelay] = GetDefaultValue("Auction.TaintedSearchDelay", 3000);
            if ((int)Values[WorldCfg.AuctionTaintedSearchDelay] < 100 || (int)Values[WorldCfg.AuctionTaintedSearchDelay] > 10000)
            {
                Log.outError(LogFilter.ServerLoading, $"Auction.TaintedSearchDelay ({Values[WorldCfg.AuctionTaintedSearchDelay]}) must be between 100 and 10000. Using default of 3s");
                Values[WorldCfg.AuctionTaintedSearchDelay] = 3000;
            }
            Values[WorldCfg.ChatChannelLevelReq] = GetDefaultValue("ChatLevelReq.Channel", 1);
            Values[WorldCfg.ChatWhisperLevelReq] = GetDefaultValue("ChatLevelReq.Whisper", 1);
            Values[WorldCfg.ChatEmoteLevelReq] = GetDefaultValue("ChatLevelReq.Emote", 1);
            Values[WorldCfg.ChatSayLevelReq] = GetDefaultValue("ChatLevelReq.Say", 1);
            Values[WorldCfg.ChatYellLevelReq] = GetDefaultValue("ChatLevelReq.Yell", 1);
            Values[WorldCfg.PartyLevelReq] = GetDefaultValue("PartyLevelReq", 1);
            Values[WorldCfg.TradeLevelReq] = GetDefaultValue("LevelReq.Trade", 1);
            Values[WorldCfg.AuctionLevelReq] = GetDefaultValue("LevelReq.Auction", 1);
            Values[WorldCfg.MailLevelReq] = GetDefaultValue("LevelReq.Mail", 1);
            Values[WorldCfg.PreserveCustomChannels] = GetDefaultValue("PreserveCustomChannels", false);
            Values[WorldCfg.PreserveCustomChannelDuration] = GetDefaultValue("PreserveCustomChannelDuration", 14);
            Values[WorldCfg.PreserveCustomChannelInterval] = GetDefaultValue("PreserveCustomChannelInterval", 5);
            Values[WorldCfg.GridUnload] = GetDefaultValue("GridUnload", true);
            Values[WorldCfg.BasemapLoadGrids] = GetDefaultValue("BaseMapLoadAllGrids", false);
            if ((bool)Values[WorldCfg.BasemapLoadGrids] && (bool)Values[WorldCfg.GridUnload])
            {
                Log.outError(LogFilter.ServerLoading, "BaseMapLoadAllGrids enabled, but GridUnload also enabled. GridUnload must be disabled to enable base map pre-loading. Base map pre-loading disabled");
                Values[WorldCfg.BasemapLoadGrids] = false;
            }
            Values[WorldCfg.InstancemapLoadGrids] = GetDefaultValue("InstanceMapLoadAllGrids", false);
            if ((bool)Values[WorldCfg.InstancemapLoadGrids] && (bool)Values[WorldCfg.GridUnload])
            {
                Log.outError(LogFilter.ServerLoading, "InstanceMapLoadAllGrids enabled, but GridUnload also enabled. GridUnload must be disabled to enable instance map pre-loading. Instance map pre-loading disabled");
                Values[WorldCfg.InstancemapLoadGrids] = false;
            }

            Values[WorldCfg.BattlegroundMapLoadGrids] = GetDefaultValue("BattlegroundMapLoadAllGrids", true);
            Values[WorldCfg.IntervalSave] = GetDefaultValue("PlayerSaveInterval", 15 * Time.Minute * Time.InMilliseconds);
            Values[WorldCfg.IntervalDisconnectTolerance] = GetDefaultValue("DisconnectToleranceInterval", 0);
            Values[WorldCfg.StatsSaveOnlyOnLogout] = GetDefaultValue("PlayerSave.Stats.SaveOnlyOnLogout", true);

            Values[WorldCfg.MinLevelStatSave] = GetDefaultValue("PlayerSave.Stats.MinLevel", 0);
            if ((int)Values[WorldCfg.MinLevelStatSave] > SharedConst.MaxLevel)
            {
                Log.outError(LogFilter.ServerLoading, "PlayerSave.Stats.MinLevel ({0}) must be in range 0..80. Using default, do not save character stats (0).", Values[WorldCfg.MinLevelStatSave]);
                Values[WorldCfg.MinLevelStatSave] = 0;
            }

            Values[WorldCfg.IntervalGridclean] = GetDefaultValue("GridCleanUpDelay", 5 * Time.Minute * Time.InMilliseconds);
            if ((int)Values[WorldCfg.IntervalGridclean] < MapConst.MinGridDelay)
            {
                Log.outError(LogFilter.ServerLoading, "GridCleanUpDelay ({0}) must be greater {1} Use this minimal value.", Values[WorldCfg.IntervalGridclean], MapConst.MinGridDelay);
                Values[WorldCfg.IntervalGridclean] = MapConst.MinGridDelay;
            }

            Values[WorldCfg.IntervalMapupdate] = GetDefaultValue("MapUpdateInterval", 10);
            if ((int)Values[WorldCfg.IntervalMapupdate] < MapConst.MinMapUpdateDelay)
            {
                Log.outError(LogFilter.ServerLoading, "MapUpdateInterval ({0}) must be greater {1}. Use this minimal value.", Values[WorldCfg.IntervalMapupdate], MapConst.MinMapUpdateDelay);
                Values[WorldCfg.IntervalMapupdate] = MapConst.MinMapUpdateDelay;
            }

            Values[WorldCfg.IntervalChangeweather] = GetDefaultValue("ChangeWeatherInterval", 10 * Time.Minute * Time.InMilliseconds);
            if (reload)
            {
                int val = GetDefaultValue("WorldServerPort", 8085);
                if (val != (int)Values[WorldCfg.PortWorld])
                    Log.outError(LogFilter.ServerLoading, "WorldServerPort option can't be changed at worldserver.conf reload, using current value ({0}).", Values[WorldCfg.PortWorld]);
            }
            else
                Values[WorldCfg.PortWorld] = GetDefaultValue("WorldServerPort", 8085);

            // Config values are in "milliseconds" but we handle SocketTimeOut only as "seconds" so divide by 1000
            Values[WorldCfg.SocketTimeoutTime] = GetDefaultValue("SocketTimeOutTime", 900000) / 1000;
            Values[WorldCfg.SocketTimeoutTimeActive] = GetDefaultValue("SocketTimeOutTimeActive", 60000) / 1000;
            Values[WorldCfg.SessionAddDelay] = GetDefaultValue("SessionAddDelay", 10000);

            Values[WorldCfg.GroupXpDistance] = GetDefaultValue("MaxGroupXPDistance", 74.0f);
            Values[WorldCfg.MaxRecruitAFriendDistance] = GetDefaultValue("MaxRecruitAFriendBonusDistance", 100.0f);
            Values[WorldCfg.MinQuestScaledXpRatio] = GetDefaultValue("MinQuestScaledXPRatio", 0);
            if ((int)Values[WorldCfg.MinQuestScaledXpRatio] > 100)
            {
                Log.outError(LogFilter.ServerLoading, $"MinQuestScaledXPRatio ({Values[WorldCfg.MinQuestScaledXpRatio]}) must be in range 0..100. Set to 0.");
                Values[WorldCfg.MinQuestScaledXpRatio] = 0;
            }

            Values[WorldCfg.MinCreatureScaledXpRatio] = GetDefaultValue("MinCreatureScaledXPRatio", 0);
            if ((int)Values[WorldCfg.MinCreatureScaledXpRatio] > 100)
            {
                Log.outError(LogFilter.ServerLoading, $"MinCreatureScaledXPRatio ({Values[WorldCfg.MinCreatureScaledXpRatio]}) must be in range 0..100. Set to 0.");
                Values[WorldCfg.MinCreatureScaledXpRatio] = 0;
            }

            Values[WorldCfg.MinDiscoveredScaledXpRatio] = GetDefaultValue("MinDiscoveredScaledXPRatio", 0);
            if ((int)Values[WorldCfg.MinDiscoveredScaledXpRatio] > 100)
            {
                Log.outError(LogFilter.ServerLoading, $"MinDiscoveredScaledXPRatio ({Values[WorldCfg.MinDiscoveredScaledXpRatio]}) must be in range 0..100. Set to 0.");
                Values[WorldCfg.MinDiscoveredScaledXpRatio] = 0;
            }

            /// @todo Add MonsterSight (with meaning) in worldserver.conf or put them as define
            Values[WorldCfg.SightMonster] = GetDefaultValue("MonsterSight", 50.0f);

            Values[WorldCfg.RegenHpCannotReachTargetInRaid] = GetDefaultValue("Creature.RegenHPCannotReachTargetInRaid", true);

            if (reload)
            {
                int val = GetDefaultValue("GameType", 0);
                if (val != (int)Values[WorldCfg.GameType])
                    Log.outError(LogFilter.ServerLoading, "GameType option can't be changed at worldserver.conf reload, using current value ({0}).", Values[WorldCfg.GameType]);
            }
            else
                Values[WorldCfg.GameType] = GetDefaultValue("GameType", 0);

            if (reload)
            {
                int val = (int)GetDefaultValue("RealmZone", RealmManager.HardcodedDevelopmentRealmCategoryId);
                if (val != (int)Values[WorldCfg.RealmZone])
                    Log.outError(LogFilter.ServerLoading, "RealmZone option can't be changed at worldserver.conf reload, using current value ({0}).", Values[WorldCfg.RealmZone]);
            }
            else
                Values[WorldCfg.RealmZone] = GetDefaultValue("RealmZone", RealmManager.HardcodedDevelopmentRealmCategoryId);

            Values[WorldCfg.AllowTwoSideInteractionCalendar] = GetDefaultValue("AllowTwoSide.Interaction.Calendar", false);
            Values[WorldCfg.AllowTwoSideInteractionChannel] = GetDefaultValue("AllowTwoSide.Interaction.Channel", false);
            Values[WorldCfg.AllowTwoSideInteractionGroup] = GetDefaultValue("AllowTwoSide.Interaction.Group", false);
            Values[WorldCfg.AllowTwoSideInteractionGuild] = GetDefaultValue("AllowTwoSide.Interaction.Guild", false);
            Values[WorldCfg.AllowTwoSideInteractionAuction] = GetDefaultValue("AllowTwoSide.Interaction.Auction", true);
            Values[WorldCfg.AllowTwoSideTrade] = GetDefaultValue("AllowTwoSide.Trade", false);
            Values[WorldCfg.StrictPlayerNames] = GetDefaultValue("StrictPlayerNames", 0);
            Values[WorldCfg.StrictCharterNames] = GetDefaultValue("StrictCharterNames", 0);
            Values[WorldCfg.StrictPetNames] = GetDefaultValue("StrictPetNames", 0);

            Values[WorldCfg.MinPlayerName] = GetDefaultValue("MinPlayerName", 2);
            if ((int)Values[WorldCfg.MinPlayerName] < 1 || (int)Values[WorldCfg.MinPlayerName] > 12)
            {
                Log.outError(LogFilter.ServerLoading, "MinPlayerName ({0}) must be in range 1..{1}. Set to 2.", Values[WorldCfg.MinPlayerName], 12);
                Values[WorldCfg.MinPlayerName] = 2;
            }

            Values[WorldCfg.MinCharterName] = GetDefaultValue("MinCharterName", 2);
            if ((int)Values[WorldCfg.MinCharterName] < 1 || (int)Values[WorldCfg.MinCharterName] > 24)
            {
                Log.outError(LogFilter.ServerLoading, "MinCharterName ({0}) must be in range 1..{1}. Set to 2.", Values[WorldCfg.MinCharterName], 24);
                Values[WorldCfg.MinCharterName] = 2;
            }

            Values[WorldCfg.MinPetName] = GetDefaultValue("MinPetName", 2);
            if ((int)Values[WorldCfg.MinPetName] < 1 || (int)Values[WorldCfg.MinPetName] > 12)
            {
                Log.outError(LogFilter.ServerLoading, "MinPetName ({0}) must be in range 1..{1}. Set to 2.", Values[WorldCfg.MinPetName], 12);
                Values[WorldCfg.MinPetName] = 2;
            }

            Values[WorldCfg.CharterCostGuild] = GetDefaultValue("Guild.CharterCost", 1000);
            Values[WorldCfg.CharterCostArena2v2] = GetDefaultValue("ArenaTeam.CharterCost.2v2", 800000);
            Values[WorldCfg.CharterCostArena3v3] = GetDefaultValue("ArenaTeam.CharterCost.3v3", 1200000);
            Values[WorldCfg.CharterCostArena5v5] = GetDefaultValue("ArenaTeam.CharterCost.5v5", 2000000);

            Values[WorldCfg.CharacterCreatingDisabled] = GetDefaultValue("CharacterCreating.Disabled", 0);
            Values[WorldCfg.CharacterCreatingDisabledRacemask] = GetDefaultValue("CharacterCreating.Disabled.RaceMask", 0);
            Values[WorldCfg.CharacterCreatingDisabledClassmask] = GetDefaultValue("CharacterCreating.Disabled.ClassMask", 0);

            Values[WorldCfg.CharactersPerRealm] = GetDefaultValue("CharactersPerRealm", 60);
            if ((int)Values[WorldCfg.CharactersPerRealm] < 1 || (int)Values[WorldCfg.CharactersPerRealm] > 200)
            {
                Log.outError(LogFilter.ServerLoading, "CharactersPerRealm ({0}) must be in range 1..200. Set to 200.", Values[WorldCfg.CharactersPerRealm]);
                Values[WorldCfg.CharactersPerRealm] = 200;
            }

            // must be after CharactersPerRealm
            Values[WorldCfg.CharactersPerAccount] = GetDefaultValue("CharactersPerAccount", 60);
            if ((int)Values[WorldCfg.CharactersPerAccount] < (int)Values[WorldCfg.CharactersPerRealm])
            {
                Log.outError(LogFilter.ServerLoading, "CharactersPerAccount ({0}) can't be less than CharactersPerRealm ({1}).", Values[WorldCfg.CharactersPerAccount], Values[WorldCfg.CharactersPerRealm]);
                Values[WorldCfg.CharactersPerAccount] = Values[WorldCfg.CharactersPerRealm];
            }

            Values[WorldCfg.CharacterCreatingEvokersPerRealm] = GetDefaultValue("CharacterCreating.EvokersPerRealm", 1);
            if ((int)Values[WorldCfg.CharacterCreatingEvokersPerRealm] < 0 || (int)Values[WorldCfg.CharacterCreatingEvokersPerRealm] > 10)
            {
                Log.outError(LogFilter.ServerLoading, $"CharacterCreating.EvokersPerRealm ({Values[WorldCfg.CharacterCreatingEvokersPerRealm]}) must be in range 0..10. Set to 1.");
                Values[WorldCfg.CharacterCreatingEvokersPerRealm] = 1;
            }

            Values[WorldCfg.CharacterCreatingMinLevelForDemonHunter] = GetDefaultValue("CharacterCreating.MinLevelForDemonHunter", 0);
            Values[WorldCfg.CharacterCreatingMinLevelForEvoker] = GetDefaultValue("CharacterCreating.MinLevelForEvoker", 50);
            Values[WorldCfg.CharacterCreatingDisableAlliedRaceAchievementRequirement] = GetDefaultValue("CharacterCreating.DisableAlliedRaceAchievementRequirement", false);

            Values[WorldCfg.SkipCinematics] = GetDefaultValue("SkipCinematics", 0);
            if ((int)Values[WorldCfg.SkipCinematics] < 0 || (int)Values[WorldCfg.SkipCinematics] > 2)
            {
                Log.outError(LogFilter.ServerLoading, "SkipCinematics ({0}) must be in range 0..2. Set to 0.", Values[WorldCfg.SkipCinematics]);
                Values[WorldCfg.SkipCinematics] = 0;
            }

            if (reload)
            {
                int val = GetDefaultValue("MaxPlayerLevel", SharedConst.DefaultMaxLevel);
                if (val != (int)Values[WorldCfg.MaxPlayerLevel])
                    Log.outError(LogFilter.ServerLoading, "MaxPlayerLevel option can't be changed at config reload, using current value ({0}).", Values[WorldCfg.MaxPlayerLevel]);
            }
            else
                Values[WorldCfg.MaxPlayerLevel] = GetDefaultValue("MaxPlayerLevel", SharedConst.DefaultMaxLevel);

            if ((int)Values[WorldCfg.MaxPlayerLevel] > SharedConst.MaxLevel)
            {
                Log.outError(LogFilter.ServerLoading, "MaxPlayerLevel ({0}) must be in range 1..{1}. Set to {1}.", Values[WorldCfg.MaxPlayerLevel], SharedConst.MaxLevel);
                Values[WorldCfg.MaxPlayerLevel] = SharedConst.MaxLevel;
            }

            Values[WorldCfg.MinDualspecLevel] = GetDefaultValue("MinDualSpecLevel", 40);

            Values[WorldCfg.StartPlayerLevel] = GetDefaultValue("StartPlayerLevel", 1);
            if ((int)Values[WorldCfg.StartPlayerLevel] < 1)
            {
                Log.outError(LogFilter.ServerLoading, "StartPlayerLevel ({0}) must be in range 1..MaxPlayerLevel({1}). Set to 1.", Values[WorldCfg.StartPlayerLevel], Values[WorldCfg.MaxPlayerLevel]);
                Values[WorldCfg.StartPlayerLevel] = 1;
            }
            else if ((int)Values[WorldCfg.StartPlayerLevel] > (int)Values[WorldCfg.MaxPlayerLevel])
            {
                Log.outError(LogFilter.ServerLoading, "StartPlayerLevel ({0}) must be in range 1..MaxPlayerLevel({1}). Set to {2}.", Values[WorldCfg.StartPlayerLevel], Values[WorldCfg.MaxPlayerLevel], Values[WorldCfg.MaxPlayerLevel]);
                Values[WorldCfg.StartPlayerLevel] = Values[WorldCfg.MaxPlayerLevel];
            }

            Values[WorldCfg.StartDeathKnightPlayerLevel] = GetDefaultValue("StartDeathKnightPlayerLevel", 8);
            if ((int)Values[WorldCfg.StartDeathKnightPlayerLevel] < 1)
            {
                Log.outError(LogFilter.ServerLoading, "StartDeathKnightPlayerLevel ({0}) must be in range 1..MaxPlayerLevel({1}). Set to 1.",
                    Values[WorldCfg.StartDeathKnightPlayerLevel], Values[WorldCfg.MaxPlayerLevel]);
                Values[WorldCfg.StartDeathKnightPlayerLevel] = 1;
            }
            else if ((int)Values[WorldCfg.StartDeathKnightPlayerLevel] > (int)Values[WorldCfg.MaxPlayerLevel])
            {
                Log.outError(LogFilter.ServerLoading, "StartDeathKnightPlayerLevel ({0}) must be in range 1..MaxPlayerLevel({1}). Set to {2}.",
                    Values[WorldCfg.StartDeathKnightPlayerLevel], Values[WorldCfg.MaxPlayerLevel], Values[WorldCfg.MaxPlayerLevel]);
                Values[WorldCfg.StartDeathKnightPlayerLevel] = Values[WorldCfg.MaxPlayerLevel];
            }

            Values[WorldCfg.StartDemonHunterPlayerLevel] = GetDefaultValue("StartDemonHunterPlayerLevel", 8);
            if ((int)Values[WorldCfg.StartDemonHunterPlayerLevel] < 1)
            {
                Log.outError(LogFilter.ServerLoading, "StartDemonHunterPlayerLevel ({0}) must be in range 1..MaxPlayerLevel({1}). Set to 1.",
                    Values[WorldCfg.StartDemonHunterPlayerLevel], Values[WorldCfg.MaxPlayerLevel]);
                Values[WorldCfg.StartDemonHunterPlayerLevel] = 1;
            }
            else if ((int)Values[WorldCfg.StartDemonHunterPlayerLevel] > (int)Values[WorldCfg.MaxPlayerLevel])
            {
                Log.outError(LogFilter.ServerLoading, "StartDemonHunterPlayerLevel ({0}) must be in range 1..MaxPlayerLevel({1}). Set to {2}.",
                    Values[WorldCfg.StartDemonHunterPlayerLevel], Values[WorldCfg.MaxPlayerLevel], Values[WorldCfg.MaxPlayerLevel]);
                Values[WorldCfg.StartDemonHunterPlayerLevel] = Values[WorldCfg.MaxPlayerLevel];
            }

            Values[WorldCfg.StartEvokerPlayerLevel] = GetDefaultValue("StartEvokerPlayerLevel", 10);
            if ((int)Values[WorldCfg.StartEvokerPlayerLevel] < 1)
            {
                Log.outError(LogFilter.ServerLoading, $"StartEvokerPlayerLevel ({Values[WorldCfg.StartEvokerPlayerLevel]}) must be in range 1..MaxPlayerLevel({Values[WorldCfg.MaxPlayerLevel]}). Set to 1.");
                Values[WorldCfg.StartEvokerPlayerLevel] = 1;
            }
            else if ((int)Values[WorldCfg.StartEvokerPlayerLevel] > (int)Values[WorldCfg.MaxPlayerLevel])
            {
                Log.outError(LogFilter.ServerLoading, $"StartEvokerPlayerLevel ({Values[WorldCfg.StartEvokerPlayerLevel]}) must be in range 1..MaxPlayerLevel({Values[WorldCfg.MaxPlayerLevel]}). Set to {Values[WorldCfg.MaxPlayerLevel]}.");
                Values[WorldCfg.StartEvokerPlayerLevel] = Values[WorldCfg.MaxPlayerLevel];
            }

            Values[WorldCfg.StartAlliedRaceLevel] = GetDefaultValue("StartAlliedRacePlayerLevel", 10);
            if ((int)Values[WorldCfg.StartAlliedRaceLevel] < 1)
            {
                Log.outError(LogFilter.ServerLoading, $"StartAlliedRaceLevel ({Values[WorldCfg.StartAlliedRaceLevel]}) must be in range 1..MaxPlayerLevel({Values[WorldCfg.MaxPlayerLevel]}). Set to 1.");
                Values[WorldCfg.StartAlliedRaceLevel] = 1;
            }
            else if ((int)Values[WorldCfg.StartAlliedRaceLevel] > (int)Values[WorldCfg.MaxPlayerLevel])
            {
                Log.outError(LogFilter.ServerLoading, $"StartAlliedRaceLevel ({Values[WorldCfg.StartAlliedRaceLevel]}) must be in range 1..MaxPlayerLevel({Values[WorldCfg.MaxPlayerLevel]}). Set to {Values[WorldCfg.MaxPlayerLevel]}.");
                Values[WorldCfg.StartAlliedRaceLevel] = Values[WorldCfg.MaxPlayerLevel];
            }

            Values[WorldCfg.StartPlayerMoney] = GetDefaultValue("StartPlayerMoney", 0);
            if ((int)Values[WorldCfg.StartPlayerMoney] < 0)
            {
                Log.outError(LogFilter.ServerLoading, "StartPlayerMoney ({0}) must be in range 0..{1}. Set to {2}.", Values[WorldCfg.StartPlayerMoney], PlayerConst.MaxMoneyAmount, 0);
                Values[WorldCfg.StartPlayerMoney] = 0;
            }
            else if ((int)Values[WorldCfg.StartPlayerMoney] > 0x7FFFFFFF - 1) // TODO: (See MaxMoneyAMOUNT)
            {
                Log.outError(LogFilter.ServerLoading, "StartPlayerMoney ({0}) must be in range 0..{1}. Set to {2}.",
                    Values[WorldCfg.StartPlayerMoney], 0x7FFFFFFF - 1, 0x7FFFFFFF - 1);
                Values[WorldCfg.StartPlayerMoney] = 0x7FFFFFFF - 1;
            }

            Values[WorldCfg.CurrencyResetHour] = GetDefaultValue("Currency.ResetHour", 3);
            if ((int)Values[WorldCfg.CurrencyResetHour] > 23)
            {
                Log.outError(LogFilter.ServerLoading, "StartPlayerMoney ({0}) must be in range 0..{1}. Set to {2}.", Values[WorldCfg.CurrencyResetHour] = 3);
            }
            Values[WorldCfg.CurrencyResetDay] = GetDefaultValue("Currency.ResetDay", 3);
            if ((int)Values[WorldCfg.CurrencyResetDay] > 6)
            {
                Log.outError(LogFilter.ServerLoading, "Currency.ResetDay ({0}) can't be load. Set to 3.", Values[WorldCfg.CurrencyResetDay]);
                Values[WorldCfg.CurrencyResetDay] = 3;
            }
            Values[WorldCfg.CurrencyResetInterval] = GetDefaultValue("Currency.ResetInterval", 7);
            if ((int)Values[WorldCfg.CurrencyResetInterval] <= 0)
            {
                Log.outError(LogFilter.ServerLoading, "Currency.ResetInterval ({0}) must be > 0, set to default 7.", Values[WorldCfg.CurrencyResetInterval]);
                Values[WorldCfg.CurrencyResetInterval] = 7;
            }

            Values[WorldCfg.MaxRecruitAFriendBonusPlayerLevel] = GetDefaultValue("RecruitAFriend.MaxLevel", 60);
            if ((int)Values[WorldCfg.MaxRecruitAFriendBonusPlayerLevel] > (int)Values[WorldCfg.MaxPlayerLevel])
            {
                Log.outError(LogFilter.ServerLoading, "RecruitAFriend.MaxLevel ({0}) must be in the range 0..MaxLevel({1}). Set to {2}.",
                    Values[WorldCfg.MaxRecruitAFriendBonusPlayerLevel], Values[WorldCfg.MaxPlayerLevel], 60);
                Values[WorldCfg.MaxRecruitAFriendBonusPlayerLevel] = 60;
            }

            Values[WorldCfg.MaxRecruitAFriendBonusPlayerLevelDifference] = GetDefaultValue("RecruitAFriend.MaxDifference", 4);
            Values[WorldCfg.AllTaxiPaths] = GetDefaultValue("AllFlightPaths", false);
            Values[WorldCfg.InstantTaxi] = GetDefaultValue("InstantFlightPaths", false);

            Values[WorldCfg.InstanceIgnoreLevel] = GetDefaultValue("Instance.IgnoreLevel", false);
            Values[WorldCfg.InstanceIgnoreRaid] = GetDefaultValue("Instance.IgnoreRaid", false);

            Values[WorldCfg.CastUnstuck] = GetDefaultValue("CastUnstuck", true);
            Values[WorldCfg.ResetScheduleWeekDay] = GetDefaultValue("ResetSchedule.WeekDay", 2);
            Values[WorldCfg.ResetScheduleHour] = GetDefaultValue("ResetSchedule.Hour", 8);
            Values[WorldCfg.InstanceUnloadDelay] = GetDefaultValue("Instance.UnloadDelay", 30 * Time.Minute * Time.InMilliseconds);
            Values[WorldCfg.DailyQuestResetTimeHour] = GetDefaultValue("Quests.DailyResetTime", 3);
            if ((int)Values[WorldCfg.DailyQuestResetTimeHour] > 23)
            {
                Log.outError(LogFilter.ServerLoading, $"Quests.DailyResetTime ({Values[WorldCfg.DailyQuestResetTimeHour]}) must be in range 0..23. Set to 3.");
                Values[WorldCfg.DailyQuestResetTimeHour] = 3;
            }

            Values[WorldCfg.WeeklyQuestResetTimeWDay] = GetDefaultValue("Quests.WeeklyResetWDay", 3);
            if ((int)Values[WorldCfg.WeeklyQuestResetTimeWDay] > 6)
            {
                Log.outError(LogFilter.ServerLoading, $"Quests.WeeklyResetDay ({Values[WorldCfg.WeeklyQuestResetTimeWDay]}) must be in range 0..6. Set to 3 (Wednesday).");
                Values[WorldCfg.WeeklyQuestResetTimeWDay] = 3;
            }

            Values[WorldCfg.MaxPrimaryTradeSkill] = GetDefaultValue("MaxPrimaryTradeSkill", 2);
            Values[WorldCfg.MinPetitionSigns] = GetDefaultValue("MinPetitionSigns", 4);
            if ((int)Values[WorldCfg.MinPetitionSigns] > 4)
            {
                Log.outError(LogFilter.ServerLoading, "MinPetitionSigns ({0}) must be in range 0..4. Set to 4.", Values[WorldCfg.MinPetitionSigns]);
                Values[WorldCfg.MinPetitionSigns] = 4;
            }

            Values[WorldCfg.GmLoginState] = GetDefaultValue("GM.LoginState", 2);
            Values[WorldCfg.GmVisibleState] = GetDefaultValue("GM.Visible", 2);
            Values[WorldCfg.GmChat] = GetDefaultValue("GM.Chat", 2);
            Values[WorldCfg.GmWhisperingTo] = GetDefaultValue("GM.WhisperingTo", 2);
            Values[WorldCfg.GmFreezeDuration] = GetDefaultValue("GM.FreezeAuraDuration", 0);

            Values[WorldCfg.GmLevelInGmList] = GetDefaultValue("GM.InGMList.Level", (int)AccountTypes.Administrator);
            Values[WorldCfg.GmLevelInWhoList] = GetDefaultValue("GM.InWhoList.Level", (int)AccountTypes.Administrator);
            Values[WorldCfg.StartGmLevel] = GetDefaultValue("GM.StartLevel", 1);
            if ((int)Values[WorldCfg.StartGmLevel] < (int)Values[WorldCfg.StartPlayerLevel])
            {
                Log.outError(LogFilter.ServerLoading, "GM.StartLevel ({0}) must be in range StartPlayerLevel({1})..{2}. Set to {3}.",
                    Values[WorldCfg.StartGmLevel], Values[WorldCfg.StartPlayerLevel], SharedConst.MaxLevel, Values[WorldCfg.StartPlayerLevel]);
                Values[WorldCfg.StartGmLevel] = Values[WorldCfg.StartPlayerLevel];
            }
            else if ((int)Values[WorldCfg.StartGmLevel] > SharedConst.MaxLevel)
            {
                Log.outError(LogFilter.ServerLoading, "GM.StartLevel ({0}) must be in range 1..{1}. Set to {1}.", Values[WorldCfg.StartGmLevel], SharedConst.MaxLevel);
                Values[WorldCfg.StartGmLevel] = SharedConst.MaxLevel;
            }
            Values[WorldCfg.AllowGmGroup] = GetDefaultValue("GM.AllowInvite", false);
            Values[WorldCfg.GmLowerSecurity] = GetDefaultValue("GM.LowerSecurity", false);
            Values[WorldCfg.ForceShutdownThreshold] = GetDefaultValue("GM.ForceShutdownThreshold", 30);

            Values[WorldCfg.GroupVisibility] = GetDefaultValue("Visibility.GroupMode", 1);

            Values[WorldCfg.MailDeliveryDelay] = GetDefaultValue("MailDeliveryDelay", Time.Hour);
            Values[WorldCfg.CleanOldMailTime] = GetDefaultValue("CleanOldMailTime", 4);
            if ((int)Values[WorldCfg.CleanOldMailTime] > 23)
            {
                Log.outError(LogFilter.ServerLoading, $"CleanOldMailTime ({Values[WorldCfg.CleanOldMailTime]}) must be an hour, between 0 and 23. Set to 4.");
                Values[WorldCfg.CleanOldMailTime] = 4;
            }

            Values[WorldCfg.UptimeUpdate] = GetDefaultValue("UpdateUptimeInterval", 10);
            if ((int)Values[WorldCfg.UptimeUpdate] <= 0)
            {
                Log.outError(LogFilter.ServerLoading, "UpdateUptimeInterval ({0}) must be > 0, set to default 10.", Values[WorldCfg.UptimeUpdate]);
                Values[WorldCfg.UptimeUpdate] = 10;
            }

            // log db cleanup interval
            Values[WorldCfg.LogdbClearinterval] = GetDefaultValue("LogDB.Opt.ClearInterval", 10);
            if ((int)Values[WorldCfg.LogdbClearinterval] <= 0)
            {
                Log.outError(LogFilter.ServerLoading, "LogDB.Opt.ClearInterval ({0}) must be > 0, set to default 10.", Values[WorldCfg.LogdbClearinterval]);
                Values[WorldCfg.LogdbClearinterval] = 10;
            }
            Values[WorldCfg.LogdbCleartime] = GetDefaultValue("LogDB.Opt.ClearTime", 1209600); // 14 days default
            Log.outInfo(LogFilter.ServerLoading, "Will clear `logs` table of entries older than {0} seconds every {1} minutes.", Values[WorldCfg.LogdbCleartime], Values[WorldCfg.LogdbClearinterval]);

            Values[WorldCfg.SkillChanceOrange] = GetDefaultValue("SkillChance.Orange", 100);
            Values[WorldCfg.SkillChanceYellow] = GetDefaultValue("SkillChance.Yellow", 75);
            Values[WorldCfg.SkillChanceGreen] = GetDefaultValue("SkillChance.Green", 25);
            Values[WorldCfg.SkillChanceGrey] = GetDefaultValue("SkillChance.Grey", 0);

            Values[WorldCfg.SkillChanceMiningSteps] = GetDefaultValue("SkillChance.MiningSteps", 75);
            Values[WorldCfg.SkillChanceSkinningSteps] = GetDefaultValue("SkillChance.SkinningSteps", 75);

            Values[WorldCfg.SkillProspecting] = GetDefaultValue("SkillChance.Prospecting", false);
            Values[WorldCfg.SkillMilling] = GetDefaultValue("SkillChance.Milling", false);

            Values[WorldCfg.SkillGainCrafting] = GetDefaultValue("SkillGain.Crafting", 1);

            Values[WorldCfg.SkillGainGathering] = GetDefaultValue("SkillGain.Gathering", 1);

            Values[WorldCfg.MaxOverspeedPings] = GetDefaultValue("MaxOverspeedPings", 2);
            if ((int)Values[WorldCfg.MaxOverspeedPings] != 0 && (int)Values[WorldCfg.MaxOverspeedPings] < 2)
            {
                Log.outError(LogFilter.ServerLoading, "MaxOverspeedPings ({0}) must be in range 2..infinity (or 0 to disable check). Set to 2.", Values[WorldCfg.MaxOverspeedPings]);
                Values[WorldCfg.MaxOverspeedPings] = 2;
            }

            Values[WorldCfg.Weather] = GetDefaultValue("ActivateWeather", true);

            Values[WorldCfg.DisableBreathing] = GetDefaultValue("DisableWaterBreath", (int)AccountTypes.Console);

            if (reload)
            {
                int val = GetDefaultValue("Expansion", (int)Expansion.WarlordsOfDraenor);
                if (val != (int)Values[WorldCfg.Expansion])
                    Log.outError(LogFilter.ServerLoading, "Expansion option can't be changed at worldserver.conf reload, using current value ({0}).", Values[WorldCfg.Expansion]);
            }
            else
                Values[WorldCfg.Expansion] = GetDefaultValue("Expansion", Expansion.WarlordsOfDraenor);

            Values[WorldCfg.ChatFloodMessageCount] = GetDefaultValue("ChatFlood.MessageCount", 10);
            Values[WorldCfg.ChatFloodMessageDelay] = GetDefaultValue("ChatFlood.MessageDelay", 1);
            Values[WorldCfg.ChatFloodMuteTime] = GetDefaultValue("ChatFlood.MuteTime", 10);

            Values[WorldCfg.EventAnnounce] = GetDefaultValue("Event.Announce", false);

            Values[WorldCfg.CreatureFamilyFleeAssistanceRadius] = GetDefaultValue("CreatureFamilyFleeAssistanceRadius", 30.0f);
            Values[WorldCfg.CreatureFamilyAssistanceRadius] = GetDefaultValue("CreatureFamilyAssistanceRadius", 10.0f);
            Values[WorldCfg.CreatureFamilyAssistanceDelay] = GetDefaultValue("CreatureFamilyAssistanceDelay", 1500);
            Values[WorldCfg.CreatureFamilyFleeDelay] = GetDefaultValue("CreatureFamilyFleeDelay", 7000);

            Values[WorldCfg.WorldBossLevelDiff] = GetDefaultValue("WorldBossLevelDiff", 3);

            Values[WorldCfg.QuestEnableQuestTracker] = GetDefaultValue("Quests.EnableQuestTracker", false);

            // note: disable value (-1) will assigned as 0xFFFFFFF, to prevent overflow at calculations limit it to max possible player Level MaxLevel(100)
            Values[WorldCfg.QuestLowLevelHideDiff] = GetDefaultValue("Quests.LowLevelHideDiff", 4);
            if ((int)Values[WorldCfg.QuestLowLevelHideDiff] > SharedConst.MaxLevel)
                Values[WorldCfg.QuestLowLevelHideDiff] = SharedConst.MaxLevel;
            Values[WorldCfg.QuestHighLevelHideDiff] = GetDefaultValue("Quests.HighLevelHideDiff", 7);
            if ((int)Values[WorldCfg.QuestHighLevelHideDiff] > SharedConst.MaxLevel)
                Values[WorldCfg.QuestHighLevelHideDiff] = SharedConst.MaxLevel;
            Values[WorldCfg.QuestIgnoreRaid] = GetDefaultValue("Quests.IgnoreRaid", false);
            Values[WorldCfg.QuestIgnoreAutoAccept] = GetDefaultValue("Quests.IgnoreAutoAccept", false);
            Values[WorldCfg.QuestIgnoreAutoComplete] = GetDefaultValue("Quests.IgnoreAutoComplete", false);

            Values[WorldCfg.RandomBgResetHour] = GetDefaultValue("Battleground.Random.ResetHour", 6);
            if ((int)Values[WorldCfg.RandomBgResetHour] > 23)
            {
                Log.outError(LogFilter.ServerLoading, "Battleground.Random.ResetHour ({0}) can't be load. Set to 6.", Values[WorldCfg.RandomBgResetHour]);
                Values[WorldCfg.RandomBgResetHour] = 6;
            }

            Values[WorldCfg.CalendarDeleteOldEventsHour] = GetDefaultValue("Calendar.DeleteOldEventsHour", 6);
            if ((int)Values[WorldCfg.CalendarDeleteOldEventsHour] > 23)
            {
                Log.outError(LogFilter.Misc, $"Calendar.DeleteOldEventsHour ({Values[WorldCfg.CalendarDeleteOldEventsHour]}) can't be load. Set to 6.");
                Values[WorldCfg.CalendarDeleteOldEventsHour] = 6;
            }

            Values[WorldCfg.GuildResetHour] = GetDefaultValue("Guild.ResetHour", 6);
            if ((int)Values[WorldCfg.GuildResetHour] > 23)
            {
                Log.outError(LogFilter.Server, "Guild.ResetHour ({0}) can't be load. Set to 6.", Values[WorldCfg.GuildResetHour]);
                Values[WorldCfg.GuildResetHour] = 6;
            }

            Values[WorldCfg.DetectPosCollision] = GetDefaultValue("DetectPosCollision", true);

            Values[WorldCfg.RestrictedLfgChannel] = GetDefaultValue("Channel.RestrictedLfg", true);
            Values[WorldCfg.TalentsInspecting] = GetDefaultValue("TalentsInspecting", 1);
            Values[WorldCfg.ChatFakeMessagePreventing] = GetDefaultValue("ChatFakeMessagePreventing", false);
            Values[WorldCfg.ChatStrictLinkCheckingSeverity] = GetDefaultValue("ChatStrictLinkChecking.Severity", 0);
            Values[WorldCfg.ChatStrictLinkCheckingKick] = GetDefaultValue("ChatStrictLinkChecking.Kick", 0);

            Values[WorldCfg.CorpseDecayNormal] = GetDefaultValue("Corpse.Decay.Normal", 60);
            Values[WorldCfg.CorpseDecayElite] = GetDefaultValue("Corpse.Decay.Elite", 300);
            Values[WorldCfg.CorpseDecayRareelite] = GetDefaultValue("Corpse.Decay.RareElite", 300);
            Values[WorldCfg.CorpseDecayObsolete] = GetDefaultValue("Corpse.Decay.Obsolete", 3600);
            Values[WorldCfg.CorpseDecayRare] = GetDefaultValue("Corpse.Decay.Rare", 300);
            Values[WorldCfg.CorpseDecayTrivial] = GetDefaultValue("Corpse.Decay.Trivial", 300);
            Values[WorldCfg.CorpseDecayMinusMob] = GetDefaultValue("Corpse.Decay.MinusMob", 150);

            Values[WorldCfg.DeathSicknessLevel] = GetDefaultValue("Death.SicknessLevel", 11);
            Values[WorldCfg.DeathCorpseReclaimDelayPvp] = GetDefaultValue("Death.CorpseReclaimDelay.PvP", true);
            Values[WorldCfg.DeathCorpseReclaimDelayPve] = GetDefaultValue("Death.CorpseReclaimDelay.PvE", true);
            Values[WorldCfg.DeathBonesWorld] = GetDefaultValue("Death.Bones.World", true);
            Values[WorldCfg.DeathBonesBgOrArena] = GetDefaultValue("Death.Bones.BattlegroundOrArena", true);

            Values[WorldCfg.DieCommandMode] = GetDefaultValue("Die.Command.Mode", true);

            Values[WorldCfg.ThreatRadius] = GetDefaultValue("ThreatRadius", 60.0f);

            Values[WorldCfg.DeclinedNamesUsed] = GetDefaultValue("DeclinedNames", false);
            Values[WorldCfg.ListenRangeSay] = GetDefaultValue("ListenRange.Say", 25.0f);
            Values[WorldCfg.ListenRangeTextemote] = GetDefaultValue("ListenRange.TextEmote", 25.0f);
            Values[WorldCfg.ListenRangeYell] = GetDefaultValue("ListenRange.Yell", 300.0f);

            Values[WorldCfg.BattlegroundCastDeserter] = GetDefaultValue("Battleground.CastDeserter", true);
            Values[WorldCfg.BattlegroundQueueAnnouncerEnable] = GetDefaultValue("Battleground.QueueAnnouncer.Enable", false);
            Values[WorldCfg.BattlegroundQueueAnnouncerPlayeronly] = GetDefaultValue("Battleground.QueueAnnouncer.PlayerOnly", false);
            Values[WorldCfg.BattlegroundStoreStatisticsEnable] = GetDefaultValue("Battleground.StoreStatistics.Enable", false);
            Values[WorldCfg.BattlegroundReportAfk] = GetDefaultValue("Battleground.ReportAFK", 3);
            if ((int)Values[WorldCfg.BattlegroundReportAfk] < 1)
            {
                Log.outError(LogFilter.ServerLoading, "Battleground.ReportAFK ({0}) must be >0. Using 3 instead.", Values[WorldCfg.BattlegroundReportAfk]);
                Values[WorldCfg.BattlegroundReportAfk] = 3;
            }
            if ((int)Values[WorldCfg.BattlegroundReportAfk] > 9)
            {
                Log.outError(LogFilter.ServerLoading, "Battleground.ReportAFK ({0}) must be <10. Using 3 instead.", Values[WorldCfg.BattlegroundReportAfk]);
                Values[WorldCfg.BattlegroundReportAfk] = 3;
            }
            Values[WorldCfg.BattlegroundInvitationType] = GetDefaultValue("Battleground.InvitationType", 0);
            Values[WorldCfg.BattlegroundPrematureFinishTimer] = GetDefaultValue("Battleground.PrematureFinishTimer", 5 * Time.Minute * Time.InMilliseconds);
            Values[WorldCfg.BattlegroundPremadeGroupWaitForMatch] = GetDefaultValue("Battleground.PremadeGroupWaitForMatch", 30 * Time.Minute * Time.InMilliseconds);
            Values[WorldCfg.BgXpForKill] = GetDefaultValue("Battleground.GiveXPForKills", false);
            Values[WorldCfg.ArenaMaxRatingDifference] = GetDefaultValue("Arena.MaxRatingDifference", 150);
            Values[WorldCfg.ArenaRatingDiscardTimer] = GetDefaultValue("Arena.RatingDiscardTimer", 10 * Time.Minute * Time.InMilliseconds);
            Values[WorldCfg.ArenaRatedUpdateTimer] = GetDefaultValue("Arena.RatedUpdateTimer", 5 * Time.InMilliseconds);
            Values[WorldCfg.ArenaQueueAnnouncerEnable] = GetDefaultValue("Arena.QueueAnnouncer.Enable", false);
            Values[WorldCfg.ArenaSeasonId] = GetDefaultValue("Arena.ArenaSeason.ID", 32);
            Values[WorldCfg.ArenaStartRating] = GetDefaultValue("Arena.ArenaStartRating", 0);
            Values[WorldCfg.ArenaStartPersonalRating] = GetDefaultValue("Arena.ArenaStartPersonalRating", 1000);
            Values[WorldCfg.ArenaStartMatchmakerRating] = GetDefaultValue("Arena.ArenaStartMatchmakerRating", 1500);
            Values[WorldCfg.ArenaSeasonInProgress] = GetDefaultValue("Arena.ArenaSeason.InProgress", false);
            Values[WorldCfg.ArenaLogExtendedInfo] = GetDefaultValue("ArenaLog.ExtendedInfo", false);
            Values[WorldCfg.ArenaWinRatingModifier1] = GetDefaultValue("Arena.ArenaWinRatingModifier1", 48.0f);
            Values[WorldCfg.ArenaWinRatingModifier2] = GetDefaultValue("Arena.ArenaWinRatingModifier2", 24.0f);
            Values[WorldCfg.ArenaLoseRatingModifier] = GetDefaultValue("Arena.ArenaLoseRatingModifier", 24.0f);
            Values[WorldCfg.ArenaMatchmakerRatingModifier] = GetDefaultValue("Arena.ArenaMatchmakerRatingModifier", 24.0f);

            if (reload)
            {
                Global.WorldStateMgr.SetValue(WorldStates.CurrentPvpSeasonId, GetBoolValue(WorldCfg.ArenaSeasonInProgress) ? GetIntValue(WorldCfg.ArenaSeasonId) : 0, false, null);
                Global.WorldStateMgr.SetValue(WorldStates.PreviousPvpSeasonId, GetIntValue(WorldCfg.ArenaSeasonId) - (GetBoolValue(WorldCfg.ArenaSeasonInProgress) ? 1 : 0), false, null);
            }

            Values[WorldCfg.OffhandCheckAtSpellUnlearn] = GetDefaultValue("OffhandCheckAtSpellUnlearn", true);

            Values[WorldCfg.CreaturePickpocketRefill] = GetDefaultValue("Creature.PickPocketRefillDelay", 10 * Time.Minute);
            Values[WorldCfg.CreatureStopForPlayer] = GetDefaultValue("Creature.MovingStopTimeForPlayer", 3 * Time.Minute * Time.InMilliseconds);

            int clientCacheId = GetDefaultValue("ClientCacheVersion", 0);
            if (clientCacheId != 0)
            {
                // overwrite DB/old value
                if (clientCacheId > 0)
                    Values[WorldCfg.ClientCacheVersion] = clientCacheId;
                else
                    Log.outError(LogFilter.ServerLoading, "ClientCacheVersion can't be negative {0}, ignored.", clientCacheId);
            }
            Log.outInfo(LogFilter.ServerLoading, "Client cache version set to: {0}", clientCacheId);

            Values[WorldCfg.GuildNewsLogCount] = GetDefaultValue("Guild.NewsLogRecordsCount", GuildConst.NewsLogMaxRecords);
            if ((int)Values[WorldCfg.GuildNewsLogCount] > GuildConst.NewsLogMaxRecords)
                Values[WorldCfg.GuildNewsLogCount] = GuildConst.NewsLogMaxRecords;

            Values[WorldCfg.GuildEventLogCount] = GetDefaultValue("Guild.EventLogRecordsCount", GuildConst.EventLogMaxRecords);
            if ((int)Values[WorldCfg.GuildEventLogCount] > GuildConst.EventLogMaxRecords)
                Values[WorldCfg.GuildEventLogCount] = GuildConst.EventLogMaxRecords;

            Values[WorldCfg.GuildBankEventLogCount] = GetDefaultValue("Guild.BankEventLogRecordsCount", GuildConst.BankLogMaxRecords);
            if ((int)Values[WorldCfg.GuildBankEventLogCount] > GuildConst.BankLogMaxRecords)
                Values[WorldCfg.GuildBankEventLogCount] = GuildConst.BankLogMaxRecords;

            // Load the CharDelete related config options
            Values[WorldCfg.ChardeleteMethod] = GetDefaultValue("CharDelete.Method", 0);
            Values[WorldCfg.ChardeleteMinLevel] = GetDefaultValue("CharDelete.MinLevel", 0);
            Values[WorldCfg.ChardeleteDeathKnightMinLevel] = GetDefaultValue("CharDelete.DeathKnight.MinLevel", 0);
            Values[WorldCfg.ChardeleteDemonHunterMinLevel] = GetDefaultValue("CharDelete.DemonHunter.MinLevel", 0);
            Values[WorldCfg.ChardeleteKeepDays] = GetDefaultValue("CharDelete.KeepDays", 30);

            // No aggro from gray mobs
            Values[WorldCfg.NoGrayAggroAbove] = GetDefaultValue("NoGrayAggro.Above", 0);
            Values[WorldCfg.NoGrayAggroBelow] = GetDefaultValue("NoGrayAggro.Below", 0);
            if ((int)Values[WorldCfg.NoGrayAggroAbove] > (int)Values[WorldCfg.MaxPlayerLevel])
            {
                Log.outError(LogFilter.ServerLoading, "NoGrayAggro.Above ({0}) must be in range 0..{1}. Set to {1}.", Values[WorldCfg.NoGrayAggroAbove], Values[WorldCfg.MaxPlayerLevel]);
                Values[WorldCfg.NoGrayAggroAbove] = Values[WorldCfg.MaxPlayerLevel];
            }
            if ((int)Values[WorldCfg.NoGrayAggroBelow] > (int)Values[WorldCfg.MaxPlayerLevel])
            {
                Log.outError(LogFilter.ServerLoading, "NoGrayAggro.Below ({0}) must be in range 0..{1}. Set to {1}.", Values[WorldCfg.NoGrayAggroBelow], Values[WorldCfg.MaxPlayerLevel]);
                Values[WorldCfg.NoGrayAggroBelow] = Values[WorldCfg.MaxPlayerLevel];
            }
            if ((int)Values[WorldCfg.NoGrayAggroAbove] > 0 && (int)Values[WorldCfg.NoGrayAggroAbove] < (int)Values[WorldCfg.NoGrayAggroBelow])
            {
                Log.outError(LogFilter.ServerLoading, "NoGrayAggro.Below ({0}) cannot be greater than NoGrayAggro.Above ({1}). Set to {1}.", Values[WorldCfg.NoGrayAggroBelow], Values[WorldCfg.NoGrayAggroAbove]);
                Values[WorldCfg.NoGrayAggroBelow] = Values[WorldCfg.NoGrayAggroAbove];
            }

            // Respawn Settings
            Values[WorldCfg.RespawnMinCheckIntervalMs] = GetDefaultValue("Respawn.MinCheckIntervalMS", 5000);
            Values[WorldCfg.RespawnDynamicMode] = GetDefaultValue("Respawn.DynamicMode", 0);
            if ((int)Values[WorldCfg.RespawnDynamicMode] > 1)
            {
                Log.outError(LogFilter.ServerLoading, $"Invalid value for Respawn.DynamicMode ({Values[WorldCfg.RespawnDynamicMode]}). Set to 0.");
                Values[WorldCfg.RespawnDynamicMode] = 0;
            }
            Values[WorldCfg.RespawnDynamicEscortNpc] = GetDefaultValue("Respawn.DynamicEscortNPC", false);
            Values[WorldCfg.RespawnGuidWarnLevel] = GetDefaultValue("Respawn.GuidWarnLevel", 12000000);
            if ((int)Values[WorldCfg.RespawnGuidWarnLevel] > 16777215)
            {
                Log.outError(LogFilter.ServerLoading, $"Respawn.GuidWarnLevel ({Values[WorldCfg.RespawnGuidWarnLevel]}) cannot be greater than maximum GUID (16777215). Set to 12000000.");
                Values[WorldCfg.RespawnGuidWarnLevel] = 12000000;
            }
            Values[WorldCfg.RespawnGuidAlertLevel] = GetDefaultValue("Respawn.GuidAlertLevel", 16000000);
            if ((int)Values[WorldCfg.RespawnGuidAlertLevel] > 16777215)
            {
                Log.outError(LogFilter.ServerLoading, $"Respawn.GuidWarnLevel ({Values[WorldCfg.RespawnGuidAlertLevel]}) cannot be greater than maximum GUID (16777215). Set to 16000000.");
                Values[WorldCfg.RespawnGuidAlertLevel] = 16000000;
            }
            Values[WorldCfg.RespawnRestartQuietTime] = GetDefaultValue("Respawn.RestartQuietTime", 3);
            if ((int)Values[WorldCfg.RespawnRestartQuietTime] > 23)
            {
                Log.outError(LogFilter.ServerLoading, $"Respawn.RestartQuietTime ({Values[WorldCfg.RespawnRestartQuietTime]}) must be an hour, between 0 and 23. Set to 3.");
                Values[WorldCfg.RespawnRestartQuietTime] = 3;
            }
            Values[WorldCfg.RespawnDynamicRateCreature] = GetDefaultValue("Respawn.DynamicRateCreature", 10.0f);
            if ((float)Values[WorldCfg.RespawnDynamicRateCreature] < 0.0f)
            {
                Log.outError(LogFilter.ServerLoading, $"Respawn.DynamicRateCreature ({Values[WorldCfg.RespawnDynamicRateCreature]}) must be positive. Set to 10.");
                Values[WorldCfg.RespawnDynamicRateCreature] = 10.0f;
            }
            Values[WorldCfg.RespawnDynamicMinimumCreature] = GetDefaultValue("Respawn.DynamicMinimumCreature", 10);
            Values[WorldCfg.RespawnDynamicRateGameobject] = GetDefaultValue("Respawn.DynamicRateGameObject", 10.0f);
            if ((float)Values[WorldCfg.RespawnDynamicRateGameobject] < 0.0f)
            {
                Log.outError(LogFilter.ServerLoading, $"Respawn.DynamicRateGameObject ({Values[WorldCfg.RespawnDynamicRateGameobject]}) must be positive. Set to 10.");
                Values[WorldCfg.RespawnDynamicRateGameobject] = 10.0f;
            }
            Values[WorldCfg.RespawnDynamicMinimumGameObject] = GetDefaultValue("Respawn.DynamicMinimumGameObject", 10);
            Values[WorldCfg.RespawnGuidWarningFrequency] = GetDefaultValue("Respawn.WarningFrequency", 1800);

            Values[WorldCfg.EnableMmaps] = GetDefaultValue("mmap.EnablePathFinding", true);
            Values[WorldCfg.VmapIndoorCheck] = GetDefaultValue("vmap.EnableIndoorCheck", false);

            Values[WorldCfg.MaxWho] = GetDefaultValue("MaxWhoListReturns", 49);
            Values[WorldCfg.StartAllSpells] = GetDefaultValue("PlayerStart.AllSpells", false);
            if ((bool)Values[WorldCfg.StartAllSpells])
                Log.outWarn(LogFilter.ServerLoading, "PlayerStart.AllSpells Enabled - may not function as intended!");

            Values[WorldCfg.HonorAfterDuel] = GetDefaultValue("HonorPointsAfterDuel", 0);
            Values[WorldCfg.ResetDuelCooldowns] = GetDefaultValue("ResetDuelCooldowns", false);
            Values[WorldCfg.ResetDuelHealthMana] = GetDefaultValue("ResetDuelHealthMana", false);
            Values[WorldCfg.StartAllExplored] = GetDefaultValue("PlayerStart.MapsExplored", false);
            Values[WorldCfg.StartAllRep] = GetDefaultValue("PlayerStart.AllReputation", false);
            Values[WorldCfg.PvpTokenEnable] = GetDefaultValue("PvPToken.Enable", false);
            Values[WorldCfg.PvpTokenMapType] = GetDefaultValue("PvPToken.MapAllowType", 4);
            Values[WorldCfg.PvpTokenId] = GetDefaultValue("PvPToken.ItemID", 29434);
            Values[WorldCfg.PvpTokenCount] = GetDefaultValue("PvPToken.ItemCount", 1);
            if ((int)Values[WorldCfg.PvpTokenCount] < 1)
                Values[WorldCfg.PvpTokenCount] = 1;

            Values[WorldCfg.NoResetTalentCost] = GetDefaultValue("NoResetTalentsCost", false);
            Values[WorldCfg.ShowKickInWorld] = GetDefaultValue("ShowKickInWorld", false);
            Values[WorldCfg.ShowMuteInWorld] = GetDefaultValue("ShowMuteInWorld", false);
            Values[WorldCfg.ShowBanInWorld] = GetDefaultValue("ShowBanInWorld", false);
            Values[WorldCfg.Numthreads] = GetDefaultValue("MapUpdate.Threads", 1);
            Values[WorldCfg.MaxResultsLookupCommands] = GetDefaultValue("Command.LookupMaxResults", 0);

            // Warden
            Values[WorldCfg.WardenEnabled] = GetDefaultValue("Warden.Enabled", false);
            Values[WorldCfg.WardenNumInjectChecks] = GetDefaultValue("Warden.NumInjectionChecks", 9);
            Values[WorldCfg.WardenNumLuaChecks] = GetDefaultValue("Warden.NumLuaSandboxChecks", 1);
            Values[WorldCfg.WardenNumClientModChecks] = GetDefaultValue("Warden.NumClientModChecks", 1);
            Values[WorldCfg.WardenClientBanDuration] = GetDefaultValue("Warden.BanDuration", 86400);
            Values[WorldCfg.WardenClientCheckHoldoff] = GetDefaultValue("Warden.ClientCheckHoldOff", 30);
            Values[WorldCfg.WardenClientFailAction] = GetDefaultValue("Warden.ClientCheckFailAction", 0);
            Values[WorldCfg.WardenClientResponseDelay] = GetDefaultValue("Warden.ClientResponseDelay", 600);

            // Feature System
            Values[WorldCfg.FeatureSystemBpayStoreEnabled] = GetDefaultValue("FeatureSystem.BpayStore.Enabled", false);
            Values[WorldCfg.FeatureSystemCharacterUndeleteEnabled] = GetDefaultValue("FeatureSystem.CharacterUndelete.Enabled", false);
            Values[WorldCfg.FeatureSystemCharacterUndeleteCooldown] = GetDefaultValue("FeatureSystem.CharacterUndelete.Cooldown", 2592000);
            Values[WorldCfg.FeatureSystemWarModeEnabled] = GetDefaultValue("FeatureSystem.WarMode.Enabled", false);

            // Dungeon finder
            Values[WorldCfg.LfgOptionsmask] = GetDefaultValue("DungeonFinder.OptionsMask", 1);

            // DBC_ItemAttributes
            Values[WorldCfg.DbcEnforceItemAttributes] = GetDefaultValue("DBC.EnforceItemAttributes", true);

            // Accountpassword Secruity
            Values[WorldCfg.AccPasschangesec] = GetDefaultValue("Account.PasswordChangeSecurity", 0);

            // Random Battleground Rewards
            Values[WorldCfg.BgRewardWinnerHonorFirst] = GetDefaultValue("Battleground.RewardWinnerHonorFirst", 27000);
            Values[WorldCfg.BgRewardWinnerConquestFirst] = GetDefaultValue("Battleground.RewardWinnerConquestFirst", 10000);
            Values[WorldCfg.BgRewardWinnerHonorLast] = GetDefaultValue("Battleground.RewardWinnerHonorLast", 13500);
            Values[WorldCfg.BgRewardWinnerConquestLast] = GetDefaultValue("Battleground.RewardWinnerConquestLast", 5000);
            Values[WorldCfg.BgRewardLoserHonorFirst] = GetDefaultValue("Battleground.RewardLoserHonorFirst", 4500);
            Values[WorldCfg.BgRewardLoserHonorLast] = GetDefaultValue("Battleground.RewardLoserHonorLast", 3500);

            // Max instances per hour
            Values[WorldCfg.MaxInstancesPerHour] = GetDefaultValue("AccountInstancesPerHour", 5);

            // Anounce reset of instance to whole party
            Values[WorldCfg.InstancesResetAnnounce] = GetDefaultValue("InstancesResetAnnounce", false);

            // Autobroadcast
            //AutoBroadcast.On
            Values[WorldCfg.AutoBroadcast] = GetDefaultValue("AutoBroadcast.On", false);
            Values[WorldCfg.AutoBroadcastCenter] = GetDefaultValue("AutoBroadcast.Center", 0);
            Values[WorldCfg.AutoBroadcastInterval] = GetDefaultValue("AutoBroadcast.Timer", 60000);

            // Guild save interval
            Values[WorldCfg.GuildSaveInterval] = GetDefaultValue("Guild.SaveInterval", 15);

            // misc
            Values[WorldCfg.PdumpNoPaths] = GetDefaultValue("PlayerDump.DisallowPaths", true);
            Values[WorldCfg.PdumpNoOverwrite] = GetDefaultValue("PlayerDump.DisallowOverwrite", true);

            // Wintergrasp battlefield
            Values[WorldCfg.WintergraspEnable] = GetDefaultValue("Wintergrasp.Enable", false);
            Values[WorldCfg.WintergraspPlrMax] = GetDefaultValue("Wintergrasp.PlayerMax", 100);
            Values[WorldCfg.WintergraspPlrMin] = GetDefaultValue("Wintergrasp.PlayerMin", 0);
            Values[WorldCfg.WintergraspPlrMinLvl] = GetDefaultValue("Wintergrasp.PlayerMinLvl", 77);
            Values[WorldCfg.WintergraspBattletime] = GetDefaultValue("Wintergrasp.BattleTimer", 30);
            Values[WorldCfg.WintergraspNobattletime] = GetDefaultValue("Wintergrasp.NoBattleTimer", 150);
            Values[WorldCfg.WintergraspRestartAfterCrash] = GetDefaultValue("Wintergrasp.CrashRestartTimer", 10);

            // Tol Barad battlefield
            Values[WorldCfg.TolbaradEnable] = GetDefaultValue("TolBarad.Enable", true);
            Values[WorldCfg.TolbaradPlrMax] = GetDefaultValue("TolBarad.PlayerMax", 100);
            Values[WorldCfg.TolbaradPlrMin] = GetDefaultValue("TolBarad.PlayerMin", 0);
            Values[WorldCfg.TolbaradPlrMinLvl] = GetDefaultValue("TolBarad.PlayerMinLvl", 85);
            Values[WorldCfg.TolbaradBattleTime] = GetDefaultValue("TolBarad.BattleTimer", 15);
            Values[WorldCfg.TolbaradBonusTime] = GetDefaultValue("TolBarad.BonusTime", 5);
            Values[WorldCfg.TolbaradNoBattleTime] = GetDefaultValue("TolBarad.NoBattleTimer", 150);
            Values[WorldCfg.TolbaradRestartAfterCrash] = GetDefaultValue("TolBarad.CrashRestartTimer", 10);

            // Stats limits
            Values[WorldCfg.StatsLimitsEnable] = GetDefaultValue("Stats.Limits.Enable", false);
            Values[WorldCfg.StatsLimitsDodge] = GetDefaultValue("Stats.Limits.Dodge", 95.0f);
            Values[WorldCfg.StatsLimitsParry] = GetDefaultValue("Stats.Limits.Parry", 95.0f);
            Values[WorldCfg.StatsLimitsBlock] = GetDefaultValue("Stats.Limits.Block", 95.0f);
            Values[WorldCfg.StatsLimitsCrit] = GetDefaultValue("Stats.Limits.Crit", 95.0f);

            //packet spoof punishment
            Values[WorldCfg.PacketSpoofPolicy] = GetDefaultValue("PacketSpoof.Policy", 1);//Kick
            Values[WorldCfg.PacketSpoofBanmode] = GetDefaultValue("PacketSpoof.BanMode", (int)BanMode.Account);
            if ((int)Values[WorldCfg.PacketSpoofBanmode] == 1 || (int)Values[WorldCfg.PacketSpoofBanmode] > 2)
                Values[WorldCfg.PacketSpoofBanmode] = (int)BanMode.Account;

            Values[WorldCfg.PacketSpoofBanduration] = GetDefaultValue("PacketSpoof.BanDuration", 86400);

            Values[WorldCfg.IpBasedActionLogging] = GetDefaultValue("Allow.IP.Based.Action.Logging", false);

            // AHBot
            Values[WorldCfg.AhbotUpdateInterval] = GetDefaultValue("AuctionHouseBot.Update.Interval", 20);

            Values[WorldCfg.CalculateCreatureZoneAreaData] = GetDefaultValue("Calculate.Creature.Zone.Area.Data", false);
            Values[WorldCfg.CalculateGameobjectZoneAreaData] = GetDefaultValue("Calculate.Gameoject.Zone.Area.Data", false);

            // Black Market
            Values[WorldCfg.BlackmarketEnabled] = GetDefaultValue("BlackMarket.Enabled", true);

            Values[WorldCfg.BlackmarketMaxAuctions] = GetDefaultValue("BlackMarket.MaxAuctions", 12);
            Values[WorldCfg.BlackmarketUpdatePeriod] = GetDefaultValue("BlackMarket.UpdatePeriod", 24);

            // prevent character rename on character customization
            Values[WorldCfg.PreventRenameCustomization] = GetDefaultValue("PreventRenameCharacterOnCustomization", false);

            // Allow 5-man parties to use raid warnings
            Values[WorldCfg.ChatPartyRaidWarnings] = GetDefaultValue("PartyRaidWarnings", false);

            // Allow to cache data queries
            Values[WorldCfg.CacheDataQueries] = GetDefaultValue("CacheDataQueries", true);

            // Check Invalid Position
            Values[WorldCfg.CreatureCheckInvalidPostion] = GetDefaultValue("Creature.CheckInvalidPosition", false);
            Values[WorldCfg.GameobjectCheckInvalidPostion] = GetDefaultValue("GameObject.CheckInvalidPosition", false);

            // Whether to use LoS from game objects
            Values[WorldCfg.CheckGobjectLos] = GetDefaultValue("CheckGameObjectLoS", true);

            // FactionBalance
            Values[WorldCfg.FactionBalanceLevelCheckDiff] = GetDefaultValue("Pvp.FactionBalance.LevelCheckDiff", 0);
            Values[WorldCfg.CallToArms5Pct] = GetDefaultValue("Pvp.FactionBalance.Pct5", 0.6f);
            Values[WorldCfg.CallToArms10Pct] = GetDefaultValue("Pvp.FactionBalance.Pct10", 0.7f);
            Values[WorldCfg.CallToArms20Pct] = GetDefaultValue("Pvp.FactionBalance.Pct20", 0.8f);

            // Specifies if IP addresses can be logged to the database
            Values[WorldCfg.AllowLogginIpAddressesInDatabase] = GetDefaultValue("AllowLoggingIPAddressesInDatabase", true);

            // Enable AE loot
            Values[WorldCfg.EnableAeLoot] = GetDefaultValue("Loot.EnableAELoot", true);

            // Loading of Locales
            Values[WorldCfg.LoadLocales] = GetDefaultValue("Load.Locales", true);

            // call ScriptMgr if we're reloading the configuration
            if (reload)
                Global.ScriptMgr.OnConfigLoad(reload);
        }

        public static uint GetUIntValue(WorldCfg confi)
        {
            return Convert.ToUInt32(Values.LookupByKey(confi));
        }

        public static int GetIntValue(WorldCfg confi)
        {
            return Convert.ToInt32(Values.LookupByKey(confi));
        }

        public static ulong GetUInt64Value(WorldCfg confi)
        {
            return Convert.ToUInt64(Values.LookupByKey(confi));
        }

        public static bool GetBoolValue(WorldCfg confi)
        {
            return Convert.ToBoolean(Values.LookupByKey(confi));
        }

        public static float GetFloatValue(WorldCfg confi)
        {
            return Convert.ToSingle(Values.LookupByKey(confi));
        }

        public static void SetValue(WorldCfg confi, object value)
        {
            Values[confi] = value;
        }

        static Dictionary<WorldCfg, object> Values = new();
    }
}
