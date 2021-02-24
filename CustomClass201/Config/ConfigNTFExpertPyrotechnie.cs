﻿using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace CustomClass.Config
{
    public class ConfigNTFExpertPyrotechnie : AbstractConfigSection
    {
        [Description("The Amount of Health the class have")]
        public int Health = 150;

        [Description("The Items the class spawns with")]
        public List<SerializedItem> Items = new List<SerializedItem>() { new SerializedItem((int)ItemType.KeycardNTFLieutenant, 1, 0, 0, 0, Vector3.one), new SerializedItem((int)ItemType.GunE11SR, 40, 0, 0, 0, Vector3.one), new SerializedItem(51, 4, 0, 0, 0, Vector3.one), new SerializedItem((int)ItemType.GrenadeFrag, 1, 0, 0, 0, Vector3.one), new SerializedItem((int)ItemType.GrenadeFrag, 1, 0, 0, 0, Vector3.one), new SerializedItem((int)ItemType.Medkit, 1, 0, 0, 0, Vector3.one), new SerializedItem((int)ItemType.Radio, 100, 0, 0, 0, Vector3.one), new SerializedItem((int)ItemType.WeaponManagerTablet, 1, 0, 0, 0, Vector3.one) };

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 16;

        [Description("Max alive at the same time")]
        public int MaxAlive = 100;

        [Description("max of this role which can spawn into a respawn")]
        public int MaxRespawn = 100;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 0;

        [Description("The name of the class")]
        public string RoleName = "Expert en explosif";
    }
}