SET @OGUID := 3005442;

-- Gameobject templates
UPDATE `gameobject_template` SET `ContentTuningId`=425, `VerifiedBuild`=51886 WHERE `entry`=190084; -- Candy Bucket

UPDATE `gameobject_template_addon` SET `faction`=1735 WHERE `entry`=190084; -- Candy Bucket

-- Quests
UPDATE `quest_offer_reward` SET `VerifiedBuild`=51886 WHERE `ID`=12382;

DELETE FROM `gameobject_queststarter` WHERE `id`=190084;
INSERT INTO `gameobject_queststarter` (`id`, `quest`, `VerifiedBuild`) VALUES
(190084, 12382, 51886);

UPDATE `gameobject_questender` SET `VerifiedBuild`=51886 WHERE (`id`=190084 AND `quest`=12382);

DELETE FROM `game_event_gameobject_quest` WHERE `id`=190084;

-- Old gameobject spawns
DELETE FROM `gameobject` WHERE `guid` BETWEEN 231617 AND 231661;
DELETE FROM `game_event_gameobject` WHERE `guid` BETWEEN 231617 AND 231661;

-- Gameobject spawns
DELETE FROM `gameobject` WHERE `guid` BETWEEN @OGUID+0 AND @OGUID+46;
INSERT INTO `gameobject` (`guid`, `id`, `map`, `zoneId`, `areaId`, `spawnDifficulties`, `PhaseId`, `PhaseGroup`, `position_x`, `position_y`, `position_z`, `orientation`, `rotation0`, `rotation1`, `rotation2`, `rotation3`, `spawntimesecs`, `animprogress`, `state`, `VerifiedBuild`) VALUES
(@OGUID+0, 180405, 0, 33, 117, '0', 0, 0, -12428.009765625, 132.83447265625, 3.73997807502746582, 3.577930212020874023, 0, 0, -0.97629547119140625, 0.216442063450813293, 120, 255, 1, 51886), -- G_Pumpkin_01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+1, 180405, 0, 33, 117, '0', 0, 0, -12330.658203125, 169.2863006591796875, 16.24791336059570312, 2.007128477096557617, 0, 0, 0.84339141845703125, 0.537299633026123046, 120, 255, 1, 51886), -- G_Pumpkin_01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+2, 180405, 0, 33, 117, '0', 0, 0, -12429.7822265625, 225.4612579345703125, 1.217295050621032714, 3.176533222198486328, 0, 0, -0.999847412109375, 0.017469281330704689, 120, 255, 1, 51886), -- G_Pumpkin_01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+3, 180405, 0, 33, 117, '0', 0, 0, -12383.953125, 229.4755096435546875, 15.22707271575927734, 3.52557229995727539, 0, 0, -0.98162651062011718, 0.190812408924102783, 120, 255, 1, 51886), -- G_Pumpkin_01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+4, 180406, 0, 33, 117, '0', 0, 0, -12432.5986328125, 138.9429473876953125, 3.183878898620605468, 1.466075778007507324, 0, 0, 0.669130325317382812, 0.74314504861831665, 120, 255, 1, 51886), -- G_Pumpkin_02 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+5, 180406, 0, 33, 117, '0', 0, 0, -12397.3681640625, 129.2810821533203125, 16.65046119689941406, 0.314158439636230468, 0, 0, 0.156434059143066406, 0.987688362598419189, 120, 255, 1, 51886), -- G_Pumpkin_02 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+6, 180406, 0, 33, 117, '0', 0, 0, -12423.0478515625, 223.0655975341796875, 1.349156022071838378, 3.141592741012573242, 0, 0, -1, 0, 120, 255, 1, 51886), -- G_Pumpkin_02 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+7, 180406, 0, 33, 117, '0', 0, 0, -12331.6357421875, 179.3895111083984375, 16.12927055358886718, 1.972219824790954589, 0, 0, 0.83388519287109375, 0.55193793773651123, 120, 255, 1, 51886), -- G_Pumpkin_02 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+8, 180407, 0, 33, 117, '0', 0, 0, -12432.46875, 134.4552459716796875, 3.509543895721435546, 2.548179388046264648, 0, 0, 0.956304550170898437, 0.292372345924377441, 120, 255, 1, 51886), -- G_Pumpkin_03 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+9, 180407, 0, 33, 117, '0', 0, 0, -12386.4052734375, 128.4357147216796875, 16.494964599609375, 0.349065244197845458, 0, 0, 0.173647880554199218, 0.984807789325714111, 120, 255, 1, 51886), -- G_Pumpkin_03 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+10, 180407, 0, 33, 117, '0', 0, 0, -12373.7890625, 230.0304718017578125, 15.5079193115234375, 3.508116960525512695, 0, 0, -0.98325443267822265, 0.182238012552261352, 120, 255, 1, 51886), -- G_Pumpkin_03 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+11, 180411, 0, 33, 117, '0', 0, 0, -12425.2119140625, 184.6666717529296875, 22.937408447265625, 1.884953022003173828, 0, 0, 0.809016227722167968, 0.587786316871643066, 120, 255, 1, 51886), -- G_Ghost_01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+12, 180411, 0, 33, 117, '0', 0, 0, -12452.228515625, 220.15972900390625, 23.67788124084472656, 6.03883981704711914, 0, 0, -0.12186908721923828, 0.9925462007522583, 120, 255, 1, 51886), -- G_Ghost_01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+13, 180411, 0, 33, 117, '0', 0, 0, -12449.138671875, 191.111114501953125, 23.39250373840332031, 0.24434557557106018, 0, 0, 0.121869087219238281, 0.9925462007522583, 120, 255, 1, 51886), -- G_Ghost_01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+14, 180411, 0, 33, 117, '0', 0, 0, -12406.53515625, 208.8246612548828125, 23.42061805725097656, 3.019413232803344726, 0, 0, 0.998134613037109375, 0.061051756143569946, 120, 255, 1, 51886), -- G_Ghost_01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+15, 180411, 0, 33, 117, '0', 0, 0, -12425, 230.3975677490234375, 23.87989616394042968, 4.45059061050415039, 0, 0, -0.79335308074951171, 0.608761727809906005, 120, 255, 1, 51886), -- G_Ghost_01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+16, 180471, 0, 33, 117, '0', 0, 0, -12427.46875, 200.4340362548828125, 12.97709465026855468, 3.665196180343627929, 0, 0, -0.96592521667480468, 0.258821308612823486, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+17, 180471, 0, 33, 117, '0', 0, 0, -12443.12890625, 197.7309112548828125, 8.0163421630859375, 5.113816738128662109, 0, 0, -0.55193614959716796, 0.833886384963989257, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+18, 180471, 0, 33, 117, '0', 0, 0, -12421.8095703125, 200.453125, 5.834054946899414062, 4.084071159362792968, 0, 0, -0.8910064697265625, 0.453990638256072998, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+19, 180471, 0, 33, 117, '0', 0, 0, -12415.2080078125, 195.2621612548828125, 7.879192829132080078, 0.925023794174194335, 0, 0, 0.446197509765625, 0.894934535026550292, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+20, 180471, 0, 33, 117, '0', 0, 0, -12436.388671875, 188.8020782470703125, 7.177947044372558593, 5.89921426773071289, 0, 0, -0.19080829620361328, 0.981627285480499267, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+21, 180471, 0, 33, 117, '0', 0, 0, -12425.65625, 191.9270782470703125, 7.739860057830810546, 0.453785061836242675, 0, 0, 0.224950790405273437, 0.974370121955871582, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+22, 180471, 0, 33, 117, '0', 0, 0, -12436.7470703125, 225.0850677490234375, 7.963707923889160156, 3.281238555908203125, 0, 0, -0.99756336212158203, 0.069766148924827575, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+23, 180471, 0, 33, 117, '0', 0, 0, -12423.0068359375, 208.5243072509765625, 11.03492355346679687, 4.433136463165283203, 0, 0, -0.79863548278808593, 0.60181504487991333, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+24, 180471, 0, 33, 117, '0', 0, 0, -12415.7783203125, 207.154510498046875, 7.966198921203613281, 1.588248729705810546, 0, 0, 0.713250160217285156, 0.700909554958343505, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+25, 180471, 0, 33, 117, '0', 0, 0, -12449.5830078125, 207.2621612548828125, 8.5053863525390625, 4.852017402648925781, 0, 0, -0.65605831146240234, 0.754710197448730468, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+26, 180471, 0, 33, 117, '0', 0, 0, -12416.044921875, 217.8246612548828125, 8.443134307861328125, 2.129300594329833984, 0, 0, 0.874619483947753906, 0.484810054302215576, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+27, 180471, 0, 33, 117, '0', 0, 0, -12437.2919921875, 212.060760498046875, 7.178095817565917968, 1.727874636650085449, 0, 0, 0.760405540466308593, 0.649448513984680175, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+28, 180471, 0, 33, 117, '0', 0, 0, -12428.65625, 215.578125, 9.139922142028808593, 2.897245407104492187, 0, 0, 0.99254608154296875, 0.121869951486587524, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+29, 180471, 0, 33, 117, '0', 0, 0, -12443.2880859375, 216.361114501953125, 8.265881538391113281, 4.101525306701660156, 0, 0, -0.88701057434082031, 0.461749136447906494, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+30, 180471, 0, 33, 117, '0', 0, 0, -12427.3193359375, 221.6979217529296875, 8.162608146667480468, 2.775068521499633789, 0, 0, 0.983254432678222656, 0.182238012552261352, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+31, 180471, 0, 33, 117, '0', 0, 0, -12440.580078125, 207.0885467529296875, 5.970573902130126953, 1.727874636650085449, 0, 0, 0.760405540466308593, 0.649448513984680175, 120, 255, 1, 51886), -- HangingSkullLight01 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+32, 180472, 0, 33, 117, '0', 0, 0, -12432.6318359375, 197.814239501953125, 7.392722129821777343, 1.291541695594787597, 0, 0, 0.60181427001953125, 0.798636078834533691, 120, 255, 1, 51886), -- HangingSkullLight02 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+33, 180472, 0, 33, 117, '0', 0, 0, -12439.9140625, 193.9031829833984375, 8.770547866821289062, 3.822272777557373046, 0, 0, -0.94264125823974609, 0.333807557821273803, 120, 255, 1, 51886), -- HangingSkullLight02 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+34, 180472, 0, 33, 117, '0', 0, 0, -12421.2431640625, 193.4652862548828125, 8.942568778991699218, 5.009094715118408203, 0, 0, -0.59482288360595703, 0.80385679006576538, 120, 255, 1, 51886), -- HangingSkullLight02 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+35, 180472, 0, 33, 117, '0', 0, 0, -12431.2158203125, 190.2777862548828125, 8.79494476318359375, 5.270895957946777343, 0, 0, -0.48480892181396484, 0.87462007999420166, 120, 255, 1, 51886), -- HangingSkullLight02 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+36, 180472, 0, 33, 117, '0', 0, 0, -12415.544921875, 201.2934112548828125, 9.187487602233886718, 0.24434557557106018, 0, 0, 0.121869087219238281, 0.9925462007522583, 120, 255, 1, 51886), -- HangingSkullLight02 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+37, 180472, 0, 33, 117, '0', 0, 0, -12421.0517578125, 219.435760498046875, 9.466778755187988281, 1.361356139183044433, 0, 0, 0.629320144653320312, 0.77714616060256958, 120, 255, 1, 51886), -- HangingSkullLight02 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+38, 180472, 0, 33, 117, '0', 0, 0, -12439.9443359375, 220.4913177490234375, 9.350820541381835937, 2.67034769058227539, 0, 0, 0.972369194030761718, 0.233448356389999389, 120, 255, 1, 51886), -- HangingSkullLight02 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+39, 180472, 0, 33, 117, '0', 0, 0, -12432.2880859375, 216.51910400390625, 7.256028175354003906, 5.375615119934082031, 0, 0, -0.4383707046508789, 0.898794233798980712, 120, 255, 1, 51886), -- HangingSkullLight02 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+40, 180472, 0, 33, 117, '0', 0, 0, -12422.763671875, 212.8090362548828125, 7.253457069396972656, 4.188792228698730468, 0, 0, -0.86602497100830078, 0.50000077486038208, 120, 255, 1, 51886), -- HangingSkullLight02 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+41, 180472, 0, 33, 117, '0', 0, 0, -12433.076171875, 223.7256927490234375, 9.326802253723144531, 1.535889506340026855, 0, 0, 0.694658279418945312, 0.719339847564697265, 120, 255, 1, 51886), -- HangingSkullLight02 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+42, 180472, 0, 33, 117, '0', 0, 0, -12445.8798828125, 212.2630767822265625, 9.529207229614257812, 2.652894020080566406, 0, 0, 0.970294952392578125, 0.241925001144409179, 120, 255, 1, 51886), -- HangingSkullLight02 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+43, 180472, 0, 33, 117, '0', 0, 0, -12445.77734375, 201.7794342041015625, 9.364244461059570312, 3.804818391799926757, 0, 0, -0.94551849365234375, 0.325568377971649169, 120, 255, 1, 51886), -- HangingSkullLight02 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+44, 180472, 0, 33, 117, '0', 0, 0, -12415.90625, 212.548614501953125, 9.490024566650390625, 0.24434557557106018, 0, 0, 0.121869087219238281, 0.9925462007522583, 120, 255, 1, 51886), -- HangingSkullLight02 (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+45, 180523, 0, 33, 117, '0', 0, 0, -12423.06640625, 216.5520782470703125, 2.649869918823242187, 0.069811686873435974, 0, 0, 0.034898757934570312, 0.999390840530395507, 120, 255, 1, 51886), -- Apple Bob (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1
(@OGUID+46, 190084, 0, 33, 117, '0', 0, 0, -12431.958984375, 211.2803497314453125, 2.36514902114868164, 2.984498262405395507, 0, 0, 0.996916770935058593, 0.078466430306434631, 120, 255, 1, 51886); -- Candy Bucket (Area: Grom'gol Base Camp - Difficulty: 0) CreateObject1

-- Event spawns
DELETE FROM `game_event_gameobject` WHERE `eventEntry`=12 AND `guid` BETWEEN @OGUID+0 AND @OGUID+46;
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
(12, @OGUID+46);