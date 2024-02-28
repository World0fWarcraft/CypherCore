SET @CGUID := 3000352;
SET @OGUID := 3006669;

-- Gameobject templates
UPDATE `gameobject_template` SET `ContentTuningId`=425, `VerifiedBuild`=51886 WHERE `entry`=208130; -- Candy Bucket

UPDATE `gameobject_template_addon` SET `faction`=1732 WHERE `entry`=208130; -- Candy Bucket

-- Quests
DELETE FROM `quest_offer_reward` WHERE `ID`=28978;
INSERT INTO `quest_offer_reward` (`ID`, `Emote1`, `Emote2`, `Emote3`, `Emote4`, `EmoteDelay1`, `EmoteDelay2`, `EmoteDelay3`, `EmoteDelay4`, `RewardText`, `VerifiedBuild`) VALUES
(28978, 0, 0, 0, 0, 0, 0, 0, 0, 'Candy buckets like this are located in inns throughout the realms. Go ahead... take some!', 51886); -- Candy Bucket

DELETE FROM `gameobject_queststarter` WHERE `id`=208130;
INSERT INTO `gameobject_queststarter` (`id`, `quest`, `VerifiedBuild`) VALUES
(208130, 28978, 51886);

UPDATE `gameobject_questender` SET `VerifiedBuild`=51886 WHERE (`id`=208130 AND `quest`=28978);

DELETE FROM `game_event_gameobject_quest` WHERE `id`=208130;

-- Creature spawns
DELETE FROM `creature` WHERE `guid`=@CGUID+0;
INSERT INTO `creature` (`guid`, `id`, `map`, `zoneId`, `areaId`, `spawnDifficulties`, `PhaseId`, `PhaseGroup`, `modelid`, `equipment_id`, `position_x`, `position_y`, `position_z`, `orientation`, `spawntimesecs`, `wander_distance`, `currentwaypoint`, `curhealth`, `curmana`, `MovementType`, `npcflag`, `unit_flags`, `unit_flags2`, `unit_flags3`, `VerifiedBuild`) VALUES
(@CGUID+0, 22816, 0, 4922, 5142, '0', 0, 0, 0, 0, -3235.9921875, -5009.970703125, 121.8743820190429687, 6.272729396820068359, 120, 0, 0, 1129, 0, 0, NULL, NULL, NULL, NULL, 51886); -- Black Cat (Area: Thundermar - Difficulty: 0) CreateObject1

-- Gameobject spawns
DELETE FROM `gameobject` WHERE `guid` BETWEEN @OGUID+0 AND @OGUID+54;
INSERT INTO `gameobject` (`guid`, `id`, `map`, `zoneId`, `areaId`, `spawnDifficulties`, `PhaseId`, `PhaseGroup`, `position_x`, `position_y`, `position_z`, `orientation`, `rotation0`, `rotation1`, `rotation2`, `rotation3`, `spawntimesecs`, `animprogress`, `state`, `VerifiedBuild`) VALUES
(@OGUID+0, 180405, 0, 4922, 5142, '0', 0, 0, -3213.75341796875, -5046.18408203125, 120.4340667724609375, 2.879789113998413085, 0, 0, 0.991444587707519531, 0.130528271198272705, 120, 255, 1, 51886), -- G_Pumpkin_01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+1, 180405, 0, 4922, 5142, '0', 0, 0, -3185.19091796875, -4993.5380859375, 120.5458297729492187, 5.84685373306274414, 0, 0, -0.21643924713134765, 0.976296067237854003, 120, 255, 1, 51886), -- G_Pumpkin_01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+2, 180406, 0, 4922, 5142, '0', 0, 0, -3228.345458984375, -5006.9912109375, 120.54296875, 2.44346022605895996, 0, 0, 0.939692497253417968, 0.34202045202255249, 120, 255, 1, 51886), -- G_Pumpkin_02 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+3, 180406, 0, 4922, 5142, '0', 0, 0, -3186.842041015625, -5005.16845703125, 121.6221694946289062, 4.345870018005371093, 0, 0, -0.82412624359130859, 0.566406130790710449, 120, 255, 1, 51886), -- G_Pumpkin_02 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+4, 180406, 0, 4922, 5142, '0', 0, 0, -3194.420166015625, -5063.81103515625, 121.6938095092773437, 4.1538848876953125, 0, 0, -0.8746194839477539, 0.484810054302215576, 120, 255, 1, 51886), -- G_Pumpkin_02 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+5, 180407, 0, 4922, 5142, '0', 0, 0, -3192.060791015625, -5006.1005859375, 120.6062698364257812, 5.305802345275878906, 0, 0, -0.46947097778320312, 0.882947921752929687, 120, 255, 1, 51886), -- G_Pumpkin_03 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+6, 180407, 0, 4922, 5142, '0', 0, 0, -3182.401123046875, -5044.1650390625, 120.5376815795898437, 4.625123500823974609, 0, 0, -0.73727703094482421, 0.67559051513671875, 120, 255, 1, 51886), -- G_Pumpkin_03 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+7, 180408, 0, 4922, 5142, '0', 0, 0, -3213.842041015625, -5046.267578125, 122.0939102172851562, 2.70525527000427246, 0, 0, 0.97629547119140625, 0.216442063450813293, 120, 255, 1, 51886), -- G_WitchHat_01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+8, 180411, 0, 4922, 5142, '0', 0, 0, -3226.4619140625, -5002.15283203125, 125.9590225219726562, 3.665196180343627929, 0, 0, -0.96592521667480468, 0.258821308612823486, 120, 255, 1, 51886), -- G_Ghost_01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+9, 180414, 0, 4922, 5142, '0', 0, 0, -3215.600830078125, -5040.8056640625, 120.4995803833007812, 6.003933906555175781, 0, 0, -0.13917255401611328, 0.990268170833587646, 120, 255, 1, 51886), -- Cauldron (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+10, 180415, 0, 4922, 5142, '0', 0, 0, -3181.467041015625, -5026.34033203125, 122.8794326782226562, 0, 0, 0, 0, 1, 120, 255, 1, 51886), -- CandleBlack01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+11, 180415, 0, 4922, 5142, '0', 0, 0, -3181.960205078125, -5018.97900390625, 123.3023300170898437, 0, 0, 0, 0, 1, 120, 255, 1, 51886), -- CandleBlack01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+12, 180415, 0, 4922, 5142, '0', 0, 0, -3181.923583984375, -5019.99853515625, 122.9446792602539062, 0, 0, 0, 0, 1, 120, 255, 1, 51886), -- CandleBlack01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+13, 180415, 0, 4922, 5142, '0', 0, 0, -3181.505126953125, -5027.7744140625, 122.8810501098632812, 0, 0, 0, 0, 1, 120, 255, 1, 51886), -- CandleBlack01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+14, 180415, 0, 4922, 5142, '0', 0, 0, -3181.838623046875, -5021.5400390625, 122.8860702514648437, 0, 0, 0, 0, 1, 120, 255, 1, 51886), -- CandleBlack01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+15, 180415, 0, 4922, 5142, '0', 0, 0, -3181.498291015625, -5028.77587890625, 123.2391891479492187, 0, 0, 0, 0, 1, 120, 255, 1, 51886), -- CandleBlack01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+16, 180425, 0, 4922, 5142, '0', 0, 0, -3219.204833984375, -4996.33349609375, 124.8443984985351562, 0.15707901120185852, 0, 0, 0.078458786010742187, 0.996917366981506347, 120, 255, 1, 51886), -- SkullCandle01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+17, 180425, 0, 4922, 5142, '0', 0, 0, -3194.189208984375, -5034.86474609375, 124.8492813110351562, 2.67034769058227539, 0, 0, 0.972369194030761718, 0.233448356389999389, 120, 255, 1, 51886), -- SkullCandle01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+18, 180425, 0, 4922, 5142, '0', 0, 0, -3206.645751953125, -5050.064453125, 124.5228805541992187, 5.602506637573242187, 0, 0, -0.33380699157714843, 0.942641437053680419, 120, 255, 1, 51886), -- SkullCandle01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+19, 180426, 0, 4922, 5142, '0', 0, 0, -3207.4462890625, -5000.35791015625, 124.0326309204101562, 0, 0, 0, 0, 1, 120, 255, 1, 51886), -- Bat01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+20, 180426, 0, 4922, 5142, '0', 0, 0, -3204.255126953125, -5007.611328125, 124.23980712890625, 0, 0, 0, 0, 1, 120, 255, 1, 51886), -- Bat01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+21, 180426, 0, 4922, 5142, '0', 0, 0, -3204.098876953125, -5029.048828125, 124.1910171508789062, 0, 0, 0, 0, 1, 120, 255, 1, 51886), -- Bat01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+22, 180426, 0, 4922, 5142, '0', 0, 0, -3203.07470703125, -5030.98974609375, 124.1128921508789062, 0, 0, 0, 0, 1, 120, 255, 1, 51886), -- Bat01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+23, 180426, 0, 4922, 5142, '0', 0, 0, -3204.779541015625, -5015.15283203125, 124.3357162475585937, 0, 0, 0, 0, 1, 120, 255, 1, 51886), -- Bat01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+24, 180427, 0, 4922, 5142, '0', 0, 0, -3208.26904296875, -5007.01025390625, 124.230133056640625, 0, 0, 0, 0, 1, 120, 255, 1, 51886), -- Bat02 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+25, 180427, 0, 4922, 5142, '0', 0, 0, -3203.75341796875, -5028.2099609375, 124.1800918579101562, 0, 0, 0, 0, 1, 120, 255, 1, 51886), -- Bat02 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+26, 180427, 0, 4922, 5142, '0', 0, 0, -3204.592041015625, -5014.85791015625, 124.3387832641601562, 0, 0, 0, 0, 1, 120, 255, 1, 51886), -- Bat02 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+27, 180427, 0, 4922, 5142, '0', 0, 0, -3204.303955078125, -5016.81103515625, 124.2758636474609375, 0, 0, 0, 0, 1, 120, 255, 1, 51886), -- Bat02 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+28, 180428, 0, 4922, 5142, '0', 0, 0, -3218.20654296875, -5041.71337890625, 120.2840728759765625, 3.298687219619750976, 0, 0, -0.99691677093505859, 0.078466430306434631, 120, 255, 1, 51886), -- G_WitchBroom_01 scale 0.5 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+29, 180471, 0, 4922, 5142, '0', 0, 0, -3214, -5039.27783203125, 127.6022567749023437, 1.85004889965057373, 0, 0, 0.798635482788085937, 0.60181504487991333, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+30, 180471, 0, 4922, 5142, '0', 0, 0, -3231.84716796875, -4999.58837890625, 124.1406631469726562, 1.500982880592346191, 0, 0, 0.681998252868652343, 0.731353819370269775, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+31, 180471, 0, 4922, 5142, '0', 0, 0, -3231.842041015625, -5004.37158203125, 124.1608734130859375, 1.85004889965057373, 0, 0, 0.798635482788085937, 0.60181504487991333, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+32, 180471, 0, 4922, 5142, '0', 0, 0, -3211.79345703125, -5044.59033203125, 128.5394744873046875, 1.937312245368957519, 0, 0, 0.824125289916992187, 0.566407561302185058, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+33, 180471, 0, 4922, 5142, '0', 0, 0, -3211.154541015625, -5046.05712890625, 127.7040786743164062, 1.954769015312194824, 0, 0, 0.829037666320800781, 0.559192776679992675, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+34, 180471, 0, 4922, 5142, '0', 0, 0, -3213.4150390625, -5040.921875, 128.56036376953125, 1.884953022003173828, 0, 0, 0.809016227722167968, 0.587786316871643066, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+35, 180471, 0, 4922, 5142, '0', 0, 0, -3183.560791015625, -5049.9150390625, 124.0442581176757812, 3.90954136848449707, 0, 0, -0.92718315124511718, 0.37460830807685852, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+36, 180471, 0, 4922, 5142, '0', 0, 0, -3199.822998046875, -5066.17724609375, 128.67291259765625, 3.054326534271240234, 0, 0, 0.999048233032226562, 0.043619260191917419, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+37, 180471, 0, 4922, 5142, '0', 0, 0, -3204.857666015625, -5064.96337890625, 128.7720184326171875, 3.211419343948364257, 0, 0, -0.9993906021118164, 0.034906134009361267, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+38, 180471, 0, 4922, 5142, '0', 0, 0, -3207.26904296875, -5064.92529296875, 127.7363662719726562, 3.298687219619750976, 0, 0, -0.99691677093505859, 0.078466430306434631, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+39, 180471, 0, 4922, 5142, '0', 0, 0, -3197.704833984375, -5067.171875, 127.6429824829101562, 2.984498262405395507, 0, 0, 0.996916770935058593, 0.078466430306434631, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+40, 180471, 0, 4922, 5142, '0', 0, 0, -3181.875, -5047.361328125, 124.1390380859375, 4.241150379180908203, 0, 0, -0.85264015197753906, 0.522498607635498046, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+41, 180471, 0, 4922, 5142, '0', 0, 0, -3209.107666015625, -5065.32470703125, 126.1587600708007812, 3.089183330535888671, 0, 0, 0.99965667724609375, 0.026201646775007247, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+42, 180471, 0, 4922, 5142, '0', 0, 0, -3196.310791015625, -5068.298828125, 125.930572509765625, 2.932138919830322265, 0, 0, 0.994521141052246093, 0.104535527527332305, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+43, 180472, 0, 4922, 5142, '0', 0, 0, -3231.732666015625, -5002.0400390625, 126.2105865478515625, 0.331610709428787231, 0, 0, 0.16504669189453125, 0.986285746097564697, 120, 255, 1, 51886), -- HangingSkullLight02 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+44, 180472, 0, 4922, 5142, '0', 0, 0, -3183.038330078125, -5027.4130859375, 125.3242034912109375, 3.124123096466064453, 0, 0, 0.99996185302734375, 0.008734640665352344, 120, 255, 1, 51886), -- HangingSkullLight02 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+45, 180472, 0, 4922, 5142, '0', 0, 0, -3183.385498046875, -5020.46875, 125.2332077026367187, 2.617989301681518554, 0, 0, 0.965925216674804687, 0.258821308612823486, 120, 255, 1, 51886), -- HangingSkullLight02 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+46, 180472, 0, 4922, 5142, '0', 0, 0, -3212.56591796875, -5042.73291015625, 129.7841644287109375, 1.117009282112121582, 0, 0, 0.529918670654296875, 0.84804844856262207, 120, 255, 1, 51886), -- HangingSkullLight02 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+47, 180472, 0, 4922, 5142, '0', 0, 0, -3202.2900390625, -5065.46533203125, 129.6165924072265625, 1.343901276588439941, 0, 0, 0.622513771057128906, 0.78260880708694458, 120, 255, 1, 51886), -- HangingSkullLight02 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+48, 180523, 0, 4922, 5142, '0', 0, 0, -3226.69970703125, -5045.26025390625, 120.9619827270507812, 0, 0, 0, 0, 1, 120, 255, 1, 51886), -- Apple Bob (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+49, 185436, 0, 4922, 5142, '0', 0, 0, -3215.9306640625, -5040.9375, 121.005859375, 5.986480236053466796, 0, 0, -0.14780902862548828, 0.989015936851501464, 120, 255, 1, 51886), -- Sitting Skeleton 03 (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+50, 208063, 0, 4922, 5142, '0', 0, 0, -3217.939208984375, -5045.28466796875, 122.3961029052734375, 5.986480236053466796, 0, 0, -0.14780902862548828, 0.989015936851501464, 120, 255, 1, 51886), -- Web Hanging (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+51, 208063, 0, 4922, 5142, '0', 0, 0, -3182.998291015625, -5048.5224609375, 121.9560394287109375, 2.635444164276123046, 0, 0, 0.96814727783203125, 0.250381410121917724, 120, 255, 1, 51886), -- Web Hanging (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+52, 208064, 0, 4922, 5142, '0', 0, 0, -3231.611083984375, -5007.19970703125, 121.7464370727539062, 3.176533222198486328, 0, 0, -0.999847412109375, 0.017469281330704689, 120, 255, 1, 51886), -- Web Corner (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+53, 208064, 0, 4922, 5142, '0', 0, 0, -3182.354248046875, -5029.8818359375, 122.4231033325195312, 3.368495941162109375, 0, 0, -0.99357128143310546, 0.113208353519439697, 120, 255, 1, 51886), -- Web Corner (Area: Thundermar - Difficulty: 0) CreateObject1
(@OGUID+54, 208130, 0, 4922, 5142, '0', 0, 0, -3223.255126953125, -5051.986328125, 120.662872314453125, 3.141592741012573242, 0, 0, -1, 0, 120, 255, 1, 51886); -- Candy Bucket (Area: Thundermar - Difficulty: 0) CreateObject1

-- Event spawns
DELETE FROM `game_event_creature` WHERE `eventEntry`=12 AND `guid`=@CGUID+0;
INSERT INTO `game_event_creature` (`eventEntry`, `guid`) VALUES
(12, @CGUID+0);

DELETE FROM `game_event_gameobject` WHERE `eventEntry`=12 AND `guid` BETWEEN @OGUID+0 AND @OGUID+54;
INSERT INTO `game_event_gameobject` (`eventEntry`, `guid`) VALUES
(12, @OGUID+0),
(12, @OGUID+1),
(12, @OGUID+2),
(12, @OGUID+3),
(12, @OGUID+4),
(12, @OGUID+5),
(12, @OGUID+6),
(12, @OGUID+7),
(12, @OGUID+8),
(12, @OGUID+9),
(12, @OGUID+10),
(12, @OGUID+11),
(12, @OGUID+12),
(12, @OGUID+13),
(12, @OGUID+14),
(12, @OGUID+15),
(12, @OGUID+16),
(12, @OGUID+17),
(12, @OGUID+18),
(12, @OGUID+19),
(12, @OGUID+20),
(12, @OGUID+21),
(12, @OGUID+22),
(12, @OGUID+23),
(12, @OGUID+24),
(12, @OGUID+25),
(12, @OGUID+26),
(12, @OGUID+27),
(12, @OGUID+28),
(12, @OGUID+29),
(12, @OGUID+30),
(12, @OGUID+31),
(12, @OGUID+32),
(12, @OGUID+33),
(12, @OGUID+34),
(12, @OGUID+35),
(12, @OGUID+36),
(12, @OGUID+37),
(12, @OGUID+38),
(12, @OGUID+39),
(12, @OGUID+40),
(12, @OGUID+41),
(12, @OGUID+42),
(12, @OGUID+43),
(12, @OGUID+44),
(12, @OGUID+45),
(12, @OGUID+46),
(12, @OGUID+47),
(12, @OGUID+48),
(12, @OGUID+49),
(12, @OGUID+50),
(12, @OGUID+51),
(12, @OGUID+52),
(12, @OGUID+53),
(12, @OGUID+54);
