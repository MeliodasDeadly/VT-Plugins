﻿using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace VTCustomClass.Config
{
    public class ConfigGardePrison : AbstractConfigSection
    {
        [Description("The MapPoint where the class should Spawn")]
        public SerializedMapPoint SpawnPoint = new SerializedMapPoint("LCZ_ClassDSpawn (1)", 13f, 1.33f, 1f);

        [Description("The Amount of Health the class have")]
        public int Health = 100;

        [Description("The Inventory of the class")]
        public SerializedPlayerInventory inventory = new SerializedPlayerInventory()
        {
            Ammo = new SerializedAmmo(30, 70, 30, 30, 30),
            Items = new List<SerializedPlayerItem> ()
            { 
                new SerializedPlayerItem((int)ItemType.GunCOM18, 50, 0, Vector3.one, 100, true),
                new SerializedPlayerItem(50, 1, 0, Vector3.one, 100, false),
                new SerializedPlayerItem((int)ItemType.Painkillers, 1, 0, Vector3.one, 100, false), 
                new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false), 
                new SerializedPlayerItem((int)ItemType.Flashlight, 1, 0, Vector3.one, 100, false),
                new SerializedPlayerItem((int)ItemType.ArmorLight, 1, 0, Vector3.one, 100, false),
            }
        };

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 0;
        public int MaxArtificialHealth = 100;

        [Description("Shield of the class")]
        public int Shield = 10;
        public int MaxShield = 100;

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 0;

        [Description("Max alive at the same time")]
        public int MaxAlive = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 7;

        [Description("The name of the class")]
        public string RoleName = "Garde Superviseur";
    }
}