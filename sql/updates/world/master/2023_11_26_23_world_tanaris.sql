--
SET @CGUID := 3000462;
SET @OGUID := 3009692;

-- Old creature spawns
DELETE FROM `creature` WHERE `guid`=396415;
DELETE FROM `game_event_creature` WHERE `guid`=396415;

-- Creature spawns
DELETE FROM `creature` WHERE `guid`=@CGUID+0;
INSERT INTO `creature` (`guid`, `id`, `map`, `zoneId`, `areaId`, `spawnDifficulties`, `PhaseId`, `PhaseGroup`, `modelid`, `equipment_id`, `position_x`, `position_y`, `position_z`, `orientation`, `spawntimesecs`, `wander_distance`, `currentwaypoint`, `curhealth`, `curmana`, `MovementType`, `npcflag`, `unit_flags`, `unit_flags2`, `unit_flags3`, `VerifiedBuild`) VALUES
(@CGUID+0, 22816, 1, 440, 976, '0', 0, 0, 0, 0, -7147.916015625, -3883.333984375, 10.58909988403320312, 3.189942121505737304, 120, 10, 0, 188, 0, 1, NULL, NULL, NULL, NULL, 46366); -- Black Cat (Area: Gadgetzan - Difficulty: 0) (possible waypoints or random movement)

-- Old gameobject spawns
DELETE FROM `gameobject` WHERE `guid` BETWEEN 247898 AND 247991;
DELETE FROM `game_event_gameobject` WHERE `guid` BETWEEN 247898 AND 247991;

-- Gameobject spawns
DELETE FROM `gameobject` WHERE `guid` BETWEEN @OGUID+0 AND @OGUID+93;
INSERT INTO `gameobject` (`guid`, `id`, `map`, `zoneId`, `areaId`, `spawnDifficulties`, `PhaseId`, `PhaseGroup`, `position_x`, `position_y`, `position_z`, `orientation`, `rotation0`, `rotation1`, `rotation2`, `rotation3`, `spawntimesecs`, `animprogress`, `state`, `VerifiedBuild`) VALUES
(@OGUID+0, 180405, 1, 440, 976, '0', 0, 0, -7073.64013671875, -3842.669921875, 16.44319915771484375, 4.572763919830322265, 0, 0, -0.75470924377441406, 0.656059443950653076, 120, 255, 1, 46366), -- G_Pumpkin_01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+1, 180405, 1, 440, 976, '0', 0, 0, -7075.49853515625, -3879.43408203125, 21.4342041015625, 0.261798173189163208, 0, 0, 0.130525588989257812, 0.991444945335388183, 120, 255, 1, 46366), -- G_Pumpkin_01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+2, 180405, 1, 440, 976, '0', 0, 0, -7092.6630859375, -3792.529541015625, 20.41531562805175781, 3.001946926116943359, 0, 0, 0.997563362121582031, 0.069766148924827575, 120, 255, 1, 46366), -- G_Pumpkin_01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+3, 180405, 1, 440, 976, '0', 0, 0, -7074.1806640625, -3850.91845703125, 11.23122596740722656, 1.431168079376220703, 0, 0, 0.656058311462402343, 0.754710197448730468, 120, 255, 1, 46366), -- G_Pumpkin_01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+4, 180405, 1, 440, 976, '0', 0, 0, -7119.29541015625, -3798.0087890625, 21.16559028625488281, 1.343901276588439941, 0, 0, 0.622513771057128906, 0.78260880708694458, 120, 255, 1, 46366), -- G_Pumpkin_01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+5, 180405, 1, 440, 976, '0', 0, 0, -7149.79541015625, -3806.178955078125, 17.25362586975097656, 6.14356088638305664, 0, 0, -0.06975555419921875, 0.997564136981964111, 120, 255, 1, 46366), -- G_Pumpkin_01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+6, 180405, 1, 440, 976, '0', 0, 0, -7195.1181640625, -3857.942626953125, 21.74278640747070312, 6.195919513702392578, 0, 0, -0.04361915588378906, 0.999048233032226562, 120, 255, 1, 46366), -- G_Pumpkin_01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+7, 180405, 1, 440, 976, '0', 0, 0, -7204.67724609375, -3834.592041015625, 9.890347480773925781, 2.635444164276123046, 0, 0, 0.96814727783203125, 0.250381410121917724, 120, 255, 1, 46366), -- G_Pumpkin_01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+8, 180405, 1, 440, 976, '0', 0, 0, -7193.13525390625, -3810.23779296875, 10.90764808654785156, 2.076939344406127929, 0, 0, 0.861628532409667968, 0.50753939151763916, 120, 255, 1, 46366), -- G_Pumpkin_01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+9, 180405, 1, 440, 976, '0', 0, 0, -7128.02978515625, -3767.588623046875, 17.25362586975097656, 2.949595451354980468, 0, 0, 0.995395660400390625, 0.095851235091686248, 120, 255, 1, 46366), -- G_Pumpkin_01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+10, 180405, 1, 440, 976, '0', 0, 0, -7158.07275390625, -3776.173583984375, 17.25362586975097656, 4.59021615982055664, 0, 0, -0.74895572662353515, 0.662620067596435546, 120, 255, 1, 46366), -- G_Pumpkin_01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+11, 180405, 1, 440, 976, '0', 0, 0, -7193.01904296875, -3799.447998046875, 10.90303802490234375, 2.042035102844238281, 0, 0, 0.852640151977539062, 0.522498607635498046, 120, 255, 1, 46366), -- G_Pumpkin_01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+12, 180405, 1, 440, 976, '0', 0, 0, -7102.90283203125, -3717.882080078125, 20.61644935607910156, 2.268925428390502929, 0, 0, 0.906307220458984375, 0.422619491815567016, 120, 255, 1, 46366), -- G_Pumpkin_01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+13, 180405, 1, 440, 976, '0', 0, 0, -7139.32666015625, -3733.654541015625, 17.10546302795410156, 0.750490784645080566, 0, 0, 0.3665008544921875, 0.93041771650314331, 120, 255, 1, 46366), -- G_Pumpkin_01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+14, 180405, 1, 440, 976, '0', 0, 0, -7144.30224609375, -3735.34716796875, 17.11612892150878906, 0.767943859100341796, 0, 0, 0.374606132507324218, 0.927184045314788818, 120, 255, 1, 46366), -- G_Pumpkin_01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+15, 180405, 1, 440, 976, '0', 0, 0, -7184.62158203125, -3705.241455078125, 20.69564437866210937, 3.211419343948364257, 0, 0, -0.9993906021118164, 0.034906134009361267, 120, 255, 1, 46366), -- G_Pumpkin_01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+16, 180405, 1, 440, 976, '0', 0, 0, -7149.767578125, -3686.578125, 22.96961212158203125, 5.270895957946777343, 0, 0, -0.48480892181396484, 0.87462007999420166, 120, 255, 1, 46366), -- G_Pumpkin_01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+17, 180406, 1, 440, 976, '0', 0, 0, -7040.22900390625, -3823.935791015625, 21.22549629211425781, 1.902408957481384277, 0, 0, 0.814115524291992187, 0.580702960491180419, 120, 255, 1, 46366), -- G_Pumpkin_02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+18, 180406, 1, 440, 976, '0', 0, 0, -7105.33349609375, -3834.86279296875, 10.69816780090332031, 1.675513744354248046, 0, 0, 0.743144035339355468, 0.669131457805633544, 120, 255, 1, 46366), -- G_Pumpkin_02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+19, 180406, 1, 440, 976, '0', 0, 0, -7076.9306640625, -3868.210205078125, 16.44320106506347656, 5.742135047912597656, 0, 0, -0.26723766326904296, 0.96363067626953125, 120, 255, 1, 46366), -- G_Pumpkin_02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+20, 180406, 1, 440, 976, '0', 0, 0, -7075.27587890625, -3860.579833984375, 11.07569503784179687, 1.431168079376220703, 0, 0, 0.656058311462402343, 0.754710197448730468, 120, 255, 1, 46366), -- G_Pumpkin_02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+21, 180406, 1, 440, 976, '0', 0, 0, -7116.41650390625, -3786.458251953125, 21.16559028625488281, 1.972219824790954589, 0, 0, 0.83388519287109375, 0.55193793773651123, 120, 255, 1, 46366), -- G_Pumpkin_02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+22, 180406, 1, 440, 976, '0', 0, 0, -7138.5556640625, -3808.80908203125, 17.25362586975097656, 0.383971005678176879, 0, 0, 0.190808296203613281, 0.981627285480499267, 120, 255, 1, 46366), -- G_Pumpkin_02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+23, 180406, 1, 440, 976, '0', 0, 0, -7179.095703125, -3866.366455078125, 21.42614936828613281, 0.052358884364366531, 0, 0, 0.02617645263671875, 0.999657332897186279, 120, 255, 1, 46366), -- G_Pumpkin_02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+24, 180406, 1, 440, 976, '0', 0, 0, -7190.642578125, -3821.307373046875, 17.33078384399414062, 2.757613182067871093, 0, 0, 0.981626510620117187, 0.190812408924102783, 120, 255, 1, 46366), -- G_Pumpkin_02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+25, 180406, 1, 440, 976, '0', 0, 0, -7187.0244140625, -3825.170166015625, 17.34145545959472656, 2.652894020080566406, 0, 0, 0.970294952392578125, 0.241925001144409179, 120, 255, 1, 46366), -- G_Pumpkin_02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+26, 180406, 1, 440, 976, '0', 0, 0, -7224.3974609375, -3843.47216796875, 22.11511993408203125, 6.021387100219726562, 0, 0, -0.13052558898925781, 0.991444945335388183, 120, 255, 1, 46366), -- G_Pumpkin_02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+27, 180406, 1, 440, 976, '0', 0, 0, -7139.2724609375, -3764.822998046875, 17.25362586975097656, 3.473210096359252929, 0, 0, -0.98628520965576171, 0.165049895644187927, 120, 255, 1, 46366), -- G_Pumpkin_02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+28, 180406, 1, 440, 976, '0', 0, 0, -7161.01220703125, -3787.171875, 17.25362586975097656, 5.078907966613769531, 0, 0, -0.56640625, 0.824126183986663818, 120, 255, 1, 46366), -- G_Pumpkin_02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+29, 180406, 1, 440, 976, '0', 0, 0, -7208.6318359375, -3810.482666015625, 19.05033683776855468, 1.972219824790954589, 0, 0, 0.83388519287109375, 0.55193793773651123, 120, 255, 1, 46366), -- G_Pumpkin_02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+30, 180406, 1, 440, 976, '0', 0, 0, -7142.09716796875, -3734.447998046875, 14.480804443359375, 0.715584874153137207, 0, 0, 0.350207328796386718, 0.936672210693359375, 120, 255, 1, 46366), -- G_Pumpkin_02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+31, 180406, 1, 440, 976, '0', 0, 0, -7172.17529296875, -3708.12841796875, 20.57437896728515625, 3.874631166458129882, 0, 0, -0.93358039855957031, 0.358368009328842163, 120, 255, 1, 46366), -- G_Pumpkin_02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+32, 180406, 1, 440, 976, '0', 0, 0, -7110.38916015625, -3688.276123046875, 20.64831924438476562, 2.234017848968505859, 0, 0, 0.898793220520019531, 0.438372820615768432, 120, 255, 1, 46366), -- G_Pumpkin_02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+33, 180407, 1, 440, 976, '0', 0, 0, -7044.69091796875, -3855.4150390625, 20.85909843444824218, 1.85004889965057373, 0, 0, 0.798635482788085937, 0.60181504487991333, 120, 255, 1, 46366), -- G_Pumpkin_03 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+34, 180407, 1, 440, 976, '0', 0, 0, -7079.06005859375, -3857.7900390625, 19.03499984741210937, 5.183629035949707031, 0, 0, -0.52249813079833984, 0.852640450000762939, 120, 255, 1, 46366), -- G_Pumpkin_03 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+35, 180407, 1, 440, 976, '0', 0, 0, -7127.546875, -3806.04345703125, 19.52004241943359375, 0.959929943084716796, 0, 0, 0.461748123168945312, 0.887011110782623291, 120, 255, 1, 46366), -- G_Pumpkin_03 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+36, 180407, 1, 440, 976, '0', 0, 0, -7078.39013671875, -3853.02001953125, 19.03499984741210937, 3.94444584846496582, 0, 0, -0.92050457000732421, 0.3907318115234375, 120, 255, 1, 46366), -- G_Pumpkin_03 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+37, 180407, 1, 440, 976, '0', 0, 0, -7119.73779296875, -3775.302001953125, 19.52004241943359375, 2.513273954391479492, 0, 0, 0.951056480407714843, 0.309017121791839599, 120, 255, 1, 46366), -- G_Pumpkin_03 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+38, 180407, 1, 440, 976, '0', 0, 0, -7149.02978515625, -3872.532958984375, 22.06035232543945312, 0.24434557557106018, 0, 0, 0.121869087219238281, 0.9925462007522583, 120, 255, 1, 46366), -- G_Pumpkin_03 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+39, 180407, 1, 440, 976, '0', 0, 0, -7188.73291015625, -3823.291748046875, 14.71108341217041015, 2.757613182067871093, 0, 0, 0.981626510620117187, 0.190812408924102783, 120, 255, 1, 46366), -- G_Pumpkin_03 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+40, 180407, 1, 440, 976, '0', 0, 0, -7158.18408203125, -3798.3681640625, 19.52004241943359375, 5.672322273254394531, 0, 0, -0.3007049560546875, 0.953717231750488281, 120, 255, 1, 46366), -- G_Pumpkin_03 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+41, 180407, 1, 440, 976, '0', 0, 0, -7150.421875, -3767.645751953125, 19.52003860473632812, 3.996806621551513671, 0, 0, -0.90996074676513671, 0.414694398641586303, 120, 255, 1, 46366), -- G_Pumpkin_03 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+42, 180407, 1, 440, 976, '0', 0, 0, -7208.6162109375, -3799.328125, 18.74937629699707031, 1.989672422409057617, 0, 0, 0.838669776916503906, 0.544640243053436279, 120, 255, 1, 46366), -- G_Pumpkin_03 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+43, 180407, 1, 440, 976, '0', 0, 0, -7253.93408203125, -3795.036376953125, 20.78090858459472656, 5.253442287445068359, 0, 0, -0.49242305755615234, 0.870355963706970214, 120, 255, 1, 46366), -- G_Pumpkin_03 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+44, 180407, 1, 440, 976, '0', 0, 0, -7248.08154296875, -3822.265625, 20.63779640197753906, 5.515241622924804687, 0, 0, -0.37460613250732421, 0.927184045314788818, 120, 255, 1, 46366), -- G_Pumpkin_03 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+45, 180407, 1, 440, 976, '0', 0, 0, -7143.34912109375, -3675.4462890625, 26.45174598693847656, 3.892086982727050781, 0, 0, -0.93041706085205078, 0.366502493619918823, 120, 255, 1, 46366), -- G_Pumpkin_03 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+46, 180415, 1, 440, 976, '0', 0, 0, -7064.65625, -3858.001708984375, 10.75152587890625, 1.431168079376220703, 0, 0, 0.656058311462402343, 0.754710197448730468, 120, 255, 1, 46366), -- CandleBlack01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+47, 180425, 1, 440, 976, '0', 0, 0, -7063.5712890625, -3854.05029296875, 10.83474254608154296, 1.431168079376220703, 0, 0, 0.656058311462402343, 0.754710197448730468, 120, 255, 1, 46366), -- SkullCandle01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+48, 180426, 1, 440, 976, '0', 0, 0, -7145.484375, -3787.911376953125, 21.68546676635742187, 0.226892471313476562, 0, 0, 0.113203048706054687, 0.993571877479553222, 120, 255, 1, 46366), -- Bat01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+49, 180426, 1, 440, 976, '0', 0, 0, -7145.2724609375, -3788.2431640625, 21.70317268371582031, 6.213373661041259765, 0, 0, -0.03489875793457031, 0.999390840530395507, 120, 255, 1, 46366), -- Bat01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+50, 180426, 1, 440, 976, '0', 0, 0, -7145.1943359375, -3788.69091796875, 21.69566535949707031, 0.628316879272460937, 0, 0, 0.309016227722167968, 0.95105677843093872, 120, 255, 1, 46366), -- Bat01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+51, 180426, 1, 440, 976, '0', 0, 0, -7145.126953125, -3788.22216796875, 21.72626113891601562, 2.792518377304077148, 0, 0, 0.984807014465332031, 0.173652306199073791, 120, 255, 1, 46366), -- Bat01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+52, 180426, 1, 440, 976, '0', 0, 0, -7145.173828125, -3787.538330078125, 21.74870109558105468, 5.742135047912597656, 0, 0, -0.26723766326904296, 0.96363067626953125, 120, 255, 1, 46366), -- Bat01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+53, 180426, 1, 440, 976, '0', 0, 0, -7145.71875, -3788.3125, 21.63250160217285156, 2.879789113998413085, 0, 0, 0.991444587707519531, 0.130528271198272705, 120, 255, 1, 46366), -- Bat01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+54, 180426, 1, 440, 976, '0', 0, 0, -7145.33837890625, -3788.223876953125, 21.69394302368164062, 5.742135047912597656, 0, 0, -0.26723766326904296, 0.96363067626953125, 120, 255, 1, 46366), -- Bat01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+55, 180426, 1, 440, 976, '0', 0, 0, -7145.34228515625, -3788.11279296875, 21.69825172424316406, 1.308995485305786132, 0, 0, 0.608760833740234375, 0.793353796005249023, 120, 255, 1, 46366), -- Bat01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+56, 180426, 1, 440, 976, '0', 0, 0, -7145.34716796875, -3787.991455078125, 21.70297431945800781, 2.216565132141113281, 0, 0, 0.894933700561523437, 0.44619917869567871, 120, 255, 1, 46366), -- Bat01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+57, 180426, 1, 440, 976, '0', 0, 0, -7145.1962890625, -3788.484375, 21.70427894592285156, 2.809975385665893554, 0, 0, 0.986285209655761718, 0.165049895644187927, 120, 255, 1, 46366), -- Bat01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+58, 180427, 1, 440, 976, '0', 0, 0, -7145.48779296875, -3787.736083984375, 21.69252395629882812, 2.268925428390502929, 0, 0, 0.906307220458984375, 0.422619491815567016, 120, 255, 1, 46366), -- Bat02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+59, 180427, 1, 440, 976, '0', 0, 0, -7145.36279296875, -3787.84375, 21.70694160461425781, 2.72271275520324707, 0, 0, 0.978147506713867187, 0.207912087440490722, 120, 255, 1, 46366), -- Bat02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+60, 180427, 1, 440, 976, '0', 0, 0, -7145.35595703125, -3787.911376953125, 21.70493888854980468, 4.939284324645996093, 0, 0, -0.6225137710571289, 0.78260880708694458, 120, 255, 1, 46366), -- Bat02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+61, 180427, 1, 440, 976, '0', 0, 0, -7145.20849609375, -3788.17529296875, 21.715972900390625, 2.426007747650146484, 0, 0, 0.936672210693359375, 0.350207358598709106, 120, 255, 1, 46366), -- Bat02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+62, 180427, 1, 440, 976, '0', 0, 0, -7145.5537109375, -3788.12158203125, 21.66572189331054687, 0.994837164878845214, 0, 0, 0.477158546447753906, 0.878817260265350341, 120, 255, 1, 46366), -- Bat02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+63, 180427, 1, 440, 976, '0', 0, 0, -7145.35595703125, -3788.453125, 21.68145942687988281, 5.95157480239868164, 0, 0, -0.16504669189453125, 0.986285746097564697, 120, 255, 1, 46366), -- Bat02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+64, 180427, 1, 440, 976, '0', 0, 0, -7145.1005859375, -3787.84033203125, 21.74678421020507812, 2.600535154342651367, 0, 0, 0.963629722595214843, 0.26724100112915039, 120, 255, 1, 46366), -- Bat02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+65, 180427, 1, 440, 976, '0', 0, 0, -7145.3525390625, -3788.163330078125, 21.69445037841796875, 1.972219824790954589, 0, 0, 0.83388519287109375, 0.55193793773651123, 120, 255, 1, 46366), -- Bat02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+66, 180427, 1, 440, 976, '0', 0, 0, -7145.203125, -3788.1025390625, 21.71984100341796875, 6.265733242034912109, 0, 0, -0.00872611999511718, 0.999961912631988525, 120, 255, 1, 46366), -- Bat02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+67, 180427, 1, 440, 976, '0', 0, 0, -7145.33837890625, -3788.177001953125, 21.696136474609375, 5.969027042388916015, 0, 0, -0.1564340591430664, 0.987688362598419189, 120, 255, 1, 46366), -- Bat02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+68, 180471, 1, 440, 976, '0', 0, 0, -7049.9462890625, -3848.130126953125, 13.26397609710693359, 1.431168079376220703, 0, 0, 0.656058311462402343, 0.754710197448730468, 120, 255, 1, 46366), -- HangingSkullLight01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+69, 180471, 1, 440, 976, '0', 0, 0, -7056.3349609375, -3847.515625, 13.08644580841064453, 1.431168079376220703, 0, 0, 0.656058311462402343, 0.754710197448730468, 120, 255, 1, 46366), -- HangingSkullLight01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+70, 180471, 1, 440, 976, '0', 0, 0, -7055.65283203125, -3842.63720703125, 13.32686710357666015, 1.431168079376220703, 0, 0, 0.656058311462402343, 0.754710197448730468, 120, 255, 1, 46366), -- HangingSkullLight01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+71, 180471, 1, 440, 976, '0', 0, 0, -7071.611328125, -3844.932373046875, 13.08367824554443359, 1.431168079376220703, 0, 0, 0.656058311462402343, 0.754710197448730468, 120, 255, 1, 46366), -- HangingSkullLight01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+72, 180471, 1, 440, 976, '0', 0, 0, -7072.16650390625, -3839.8837890625, 12.37954425811767578, 1.431168079376220703, 0, 0, 0.656058311462402343, 0.754710197448730468, 120, 255, 1, 46366), -- HangingSkullLight01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+73, 180471, 1, 440, 976, '0', 0, 0, -7057.29150390625, -3873.623291015625, 12.34993743896484375, 1.431168079376220703, 0, 0, 0.656058311462402343, 0.754710197448730468, 120, 255, 1, 46366), -- HangingSkullLight01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+74, 180471, 1, 440, 976, '0', 0, 0, -7053.03125, -3869.21533203125, 13.27369117736816406, 1.431168079376220703, 0, 0, 0.656058311462402343, 0.754710197448730468, 120, 255, 1, 46366), -- HangingSkullLight01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+75, 180471, 1, 440, 976, '0', 0, 0, -7074.9619140625, -3871.380126953125, 13.31776809692382812, 1.431168079376220703, 0, 0, 0.656058311462402343, 0.754710197448730468, 120, 255, 1, 46366), -- HangingSkullLight01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+76, 180471, 1, 440, 976, '0', 0, 0, -7074.783203125, -3867.350830078125, 12.80379104614257812, 1.431168079376220703, 0, 0, 0.656058311462402343, 0.754710197448730468, 120, 255, 1, 46366), -- HangingSkullLight01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+77, 180471, 1, 440, 976, '0', 0, 0, -7056.6181640625, -3868.75, 12.1289072036743164, 1.431168079376220703, 0, 0, 0.656058311462402343, 0.754710197448730468, 120, 255, 1, 46366), -- HangingSkullLight01 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+78, 180472, 1, 440, 976, '0', 0, 0, -7064.1494140625, -3846.229248046875, 14.41779613494873046, 1.431168079376220703, 0, 0, 0.656058311462402343, 0.754710197448730468, 120, 255, 1, 46366), -- HangingSkullLight02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+79, 180472, 1, 440, 976, '0', 0, 0, -7056.392578125, -3858.942626953125, 14.46264553070068359, 1.431168079376220703, 0, 0, 0.656058311462402343, 0.754710197448730468, 120, 255, 1, 46366), -- HangingSkullLight02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+80, 180472, 1, 440, 976, '0', 0, 0, -7076.47216796875, -3844.2431640625, 14.70294952392578125, 1.431168079376220703, 0, 0, 0.656058311462402343, 0.754710197448730468, 120, 255, 1, 46366), -- HangingSkullLight02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+81, 180472, 1, 440, 976, '0', 0, 0, -7070.02978515625, -3793.93408203125, 19.380767822265625, 1.658061861991882324, 0, 0, 0.737277030944824218, 0.67559051513671875, 120, 255, 1, 46366), -- HangingSkullLight02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+82, 180472, 1, 440, 976, '0', 0, 0, -7067.19775390625, -3867.444580078125, 14.47201728820800781, 1.431168079376220703, 0, 0, 0.656058311462402343, 0.754710197448730468, 120, 255, 1, 46366), -- HangingSkullLight02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+83, 180472, 1, 440, 976, '0', 0, 0, -7079.51904296875, -3866.625, 14.80517292022705078, 1.431168079376220703, 0, 0, 0.656058311462402343, 0.754710197448730468, 120, 255, 1, 46366), -- HangingSkullLight02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+84, 180472, 1, 440, 976, '0', 0, 0, -7095.3818359375, -3754.880126953125, 18.10312843322753906, 6.108653545379638671, 0, 0, -0.08715534210205078, 0.996194720268249511, 120, 255, 1, 46366), -- HangingSkullLight02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+85, 180472, 1, 440, 976, '0', 0, 0, -7162.080078125, -3851.482666015625, 14.84703254699707031, 2.565631866455078125, 0, 0, 0.958819389343261718, 0.284016460180282592, 120, 255, 1, 46366), -- HangingSkullLight02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+86, 180472, 1, 440, 976, '0', 0, 0, -7148.33349609375, -3851.6650390625, 14.68859577178955078, 6.248279094696044921, 0, 0, -0.01745223999023437, 0.999847710132598876, 120, 255, 1, 46366), -- HangingSkullLight02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+87, 180472, 1, 440, 976, '0', 0, 0, -7173.486328125, -3851.157958984375, 13.91457271575927734, 1.204277276992797851, 0, 0, 0.56640625, 0.824126183986663818, 120, 255, 1, 46366), -- HangingSkullLight02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+88, 180472, 1, 440, 976, '0', 0, 0, -7232.7568359375, -3786.588623046875, 14.70685291290283203, 2.740161895751953125, 0, 0, 0.979924201965332031, 0.199370384216308593, 120, 255, 1, 46366), -- HangingSkullLight02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+89, 180472, 1, 440, 976, '0', 0, 0, -7216.486328125, -3801.3056640625, 3.651153564453125, 0.209439441561698913, 0, 0, 0.104528427124023437, 0.994521915912628173, 120, 255, 1, 46366), -- HangingSkullLight02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+90, 180472, 1, 440, 976, '0', 0, 0, -7216.580078125, -3806.435791015625, 3.642531633377075195, 0.122172988951206207, 0, 0, 0.061048507690429687, 0.998134791851043701, 120, 255, 1, 46366), -- HangingSkullLight02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+91, 180472, 1, 440, 976, '0', 0, 0, -7184.48291015625, -3727.638916015625, 15.91984939575195312, 4.782202720642089843, 0, 0, -0.68199825286865234, 0.731353819370269775, 120, 255, 1, 46366), -- HangingSkullLight02 (Area: Gadgetzan - Difficulty: 0)
(@OGUID+92, 180523, 1, 440, 976, '0', 0, 0, -7070.1005859375, -3849.84033203125, 10.19074726104736328, 1.431168079376220703, 0, 0, 0.656058311462402343, 0.754710197448730468, 120, 255, 1, 46366), -- Apple Bob (Area: Gadgetzan - Difficulty: 0)
(@OGUID+93, 190105, 1, 440, 976, '0', 0, 0, -7073.9306640625, -3853.085205078125, 9.764927864074707031, 1.431168079376220703, 0, 0, 0.656058311462402343, 0.754710197448730468, 120, 255, 1, 46366); -- Candy Bucket (Area: Gadgetzan - Difficulty: 0)

-- Event spawns
DELETE FROM `game_event_creature` WHERE `eventEntry`=12 AND `guid`=@CGUID+0;
INSERT INTO `game_event_creature` (`eventEntry`, `guid`) VALUES
(12, @CGUID+0);

DELETE FROM `game_event_gameobject` WHERE `eventEntry`=12 AND `guid` BETWEEN @OGUID+0 AND @OGUID+93;
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
(12, @OGUID+54),
(12, @OGUID+55),
(12, @OGUID+56),
(12, @OGUID+57),
(12, @OGUID+58),
(12, @OGUID+59),
(12, @OGUID+60),
(12, @OGUID+61),
(12, @OGUID+62),
(12, @OGUID+63),
(12, @OGUID+64),
(12, @OGUID+65),
(12, @OGUID+66),
(12, @OGUID+67),
(12, @OGUID+68),
(12, @OGUID+69),
(12, @OGUID+70),
(12, @OGUID+71),
(12, @OGUID+72),
(12, @OGUID+73),
(12, @OGUID+74),
(12, @OGUID+75),
(12, @OGUID+76),
(12, @OGUID+77),
(12, @OGUID+78),
(12, @OGUID+79),
(12, @OGUID+80),
(12, @OGUID+81),
(12, @OGUID+82),
(12, @OGUID+83),
(12, @OGUID+84),
(12, @OGUID+85),
(12, @OGUID+86),
(12, @OGUID+87),
(12, @OGUID+88),
(12, @OGUID+89),
(12, @OGUID+90),
(12, @OGUID+91),
(12, @OGUID+92),
(12, @OGUID+93);

--
SET @OGUID := 3009786;

-- Old gameobject spawns
DELETE FROM `gameobject` WHERE `guid` BETWEEN 247992 AND 248028;
DELETE FROM `game_event_gameobject` WHERE `guid` BETWEEN 247992 AND 248028;

-- Gameobject spawns
DELETE FROM `gameobject` WHERE `guid` BETWEEN @OGUID+0 AND @OGUID+36;
INSERT INTO `gameobject` (`guid`, `id`, `map`, `zoneId`, `areaId`, `spawnDifficulties`, `PhaseId`, `PhaseGroup`, `position_x`, `position_y`, `position_z`, `orientation`, `rotation0`, `rotation1`, `rotation2`, `rotation3`, `spawntimesecs`, `animprogress`, `state`, `VerifiedBuild`) VALUES
(@OGUID+0, 180405, 1, 440, 5062, '0', 0, 0, -8692.076171875, -4080.00341796875, 40.27819061279296875, 2.251473426818847656, 0, 0, 0.902585029602050781, 0.430511653423309326, 120, 255, 1, 46366), -- G_Pumpkin_01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+1, 180406, 1, 440, 5062, '0', 0, 0, -8693.7236328125, -4068.37841796875, 40.42780685424804687, 1.989672422409057617, 0, 0, 0.838669776916503906, 0.544640243053436279, 120, 255, 1, 46366), -- G_Pumpkin_02 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+2, 180406, 1, 440, 5062, '0', 0, 0, -8694.1630859375, -4088.757080078125, 40.47179794311523437, 3.211419343948364257, 0, 0, -0.9993906021118164, 0.034906134009361267, 120, 255, 1, 46366), -- G_Pumpkin_02 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+3, 180407, 1, 440, 5062, '0', 0, 0, -8678.435546875, -4041.463623046875, 43.67625808715820312, 0.366517573595046997, 0, 0, 0.182234764099121093, 0.98325502872467041, 120, 255, 1, 46366), -- G_Pumpkin_03 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+4, 180410, 1, 440, 5062, '0', 0, 0, -8701.1357421875, -4075.017333984375, 41.59624099731445312, 0.052358884364366531, 0, 0, 0.02617645263671875, 0.999657332897186279, 120, 255, 1, 46366), -- G_HangingSkeleton_01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+5, 180415, 1, 440, 5062, '0', 0, 0, -8690.498046875, -4090.579833984375, 41.64896774291992187, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- CandleBlack01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+6, 180415, 1, 440, 5062, '0', 0, 0, -8688.3544921875, -4091.260498046875, 41.60406494140625, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- CandleBlack01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+7, 180415, 1, 440, 5062, '0', 0, 0, -8689.4482421875, -4090.354248046875, 41.47544097900390625, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- CandleBlack01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+8, 180415, 1, 440, 5062, '0', 0, 0, -8695.8681640625, -4088.026123046875, 41.52730941772460937, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- CandleBlack01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+9, 180415, 1, 440, 5062, '0', 0, 0, -8692.154296875, -4089.689208984375, 41.11940383911132812, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- CandleBlack01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+10, 180415, 1, 440, 5062, '0', 0, 0, -8696.794921875, -4087.295166015625, 40.47179794311523437, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- CandleBlack01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+11, 180415, 1, 440, 5062, '0', 0, 0, -8677.2158203125, -4043.017333984375, 43.583343505859375, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- CandleBlack01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+12, 180415, 1, 440, 5062, '0', 0, 0, -8673.6044921875, -4043.083251953125, 45.49809646606445312, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- CandleBlack01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+13, 180415, 1, 440, 5062, '0', 0, 0, -8677.1875, -4039.130126953125, 46.61213302612304687, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- CandleBlack01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+14, 180415, 1, 440, 5062, '0', 0, 0, -8675.6474609375, -4041.885498046875, 46.47837448120117187, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- CandleBlack01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+15, 180415, 1, 440, 5062, '0', 0, 0, -8681.701171875, -4042.401123046875, 42.83848190307617187, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- CandleBlack01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+16, 180415, 1, 440, 5062, '0', 0, 0, -8679.7392578125, -4038.104248046875, 44.92335128784179687, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- CandleBlack01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+17, 180426, 1, 440, 5062, '0', 0, 0, -8672.158203125, -4066.444580078125, 47.80141448974609375, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- Bat01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+18, 180426, 1, 440, 5062, '0', 0, 0, -8632.685546875, -4053.52783203125, 45.95989990234375, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- Bat01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+19, 180426, 1, 440, 5062, '0', 0, 0, -8662.7275390625, -4070.640625, 47.48949432373046875, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- Bat01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+20, 180426, 1, 440, 5062, '0', 0, 0, -8651.70703125, -4065.826416015625, 46.93803024291992187, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- Bat01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+21, 180426, 1, 440, 5062, '0', 0, 0, -8646.140625, -4090.225830078125, 42.43922042846679687, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- Bat01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+22, 180426, 1, 440, 5062, '0', 0, 0, -8640.1455078125, -4069.569580078125, 43.80381393432617187, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- Bat01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+23, 180427, 1, 440, 5062, '0', 0, 0, -8653.4970703125, -4078.657958984375, 38.92481613159179687, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- Bat02 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+24, 180427, 1, 440, 5062, '0', 0, 0, -8648.25390625, -4081.75, 43.91880416870117187, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- Bat02 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+25, 180427, 1, 440, 5062, '0', 0, 0, -8646.876953125, -4091.548583984375, 36.0577392578125, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- Bat02 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+26, 180427, 1, 440, 5062, '0', 0, 0, -8655.734375, -4068.36279296875, 41.03221511840820312, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- Bat02 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+27, 180427, 1, 440, 5062, '0', 0, 0, -8639.2158203125, -4065.73779296875, 37.85643386840820312, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- Bat02 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+28, 180471, 1, 440, 5062, '0', 0, 0, -8695.658203125, -4067.507080078125, 44.84920120239257812, 2.146752834320068359, 0, 0, 0.878816604614257812, 0.477159708738327026, 120, 255, 1, 46366), -- HangingSkullLight01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+29, 180471, 1, 440, 5062, '0', 0, 0, -8694.0068359375, -4081.013916015625, 45.167572021484375, 1.099556446075439453, 0, 0, 0.522498130798339843, 0.852640450000762939, 120, 255, 1, 46366), -- HangingSkullLight01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+30, 180471, 1, 440, 5062, '0', 0, 0, -8693.4462890625, -4080.09375, 45.180572509765625, 1.134462952613830566, 0, 0, 0.537299156188964843, 0.843391716480255126, 120, 255, 1, 46366), -- HangingSkullLight01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+31, 180471, 1, 440, 5062, '0', 0, 0, -8694.904296875, -4068.197998046875, 44.83955764770507812, 2.251473426818847656, 0, 0, 0.902585029602050781, 0.430511653423309326, 120, 255, 1, 46366), -- HangingSkullLight01 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+32, 180472, 1, 440, 5062, '0', 0, 0, -8694.2294921875, -4074.15625, 46.11717605590820312, 0.15707901120185852, 0, 0, 0.078458786010742187, 0.996917366981506347, 120, 255, 1, 46366), -- HangingSkullLight02 (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+33, 180523, 1, 440, 5062, '0', 0, 0, -8698.6826171875, -4084.744873046875, 40.90895462036132812, 0, 0, 0, 0, 1, 120, 255, 1, 46366), -- Apple Bob (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+34, 208056, 1, 440, 5062, '0', 0, 0, -8706.109375, -4082.12158203125, 44.41543197631835937, 3.665196180343627929, 0, 0, -0.96592521667480468, 0.258821308612823486, 120, 255, 1, 46366), -- G_Ghost_01 (Scale 0.5) (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+35, 208064, 1, 440, 5062, '0', 0, 0, -8706.7080078125, -4082.40966796875, 43.11758804321289062, 3.211419343948364257, 0, 0, -0.9993906021118164, 0.034906134009361267, 120, 255, 1, 46366), -- Web Corner (Area: Bootlegger Outpost - Difficulty: 0)
(@OGUID+36, 208177, 1, 440, 5062, '0', 0, 0, -8702.171875, -4080.111083984375, 40.45697784423828125, 2.076939344406127929, 0, 0, 0.861628532409667968, 0.50753939151763916, 120, 255, 1, 46366); -- Candy Bucket (Area: Bootlegger Outpost - Difficulty: 0)

-- Event spawns
DELETE FROM `game_event_gameobject` WHERE `eventEntry`=12 AND `guid` BETWEEN @OGUID+0 AND @OGUID+36;
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
(12, @OGUID+36);