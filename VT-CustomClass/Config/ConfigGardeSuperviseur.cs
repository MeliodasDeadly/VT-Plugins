﻿using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace VTCustomClass.Config
{
    public class ConfigGardeSuperviseur : AbstractConfigSection
    {
        [Description("The MapPoint where the class should Spawn")]
        public SerializedMapPoint SpawnPoint = new SerializedMapPoint("HCZ_EZ_Checkpoint", 7.9f, 2.143616f, 0.02f);

        [Description("The Amount of Health the class have")]
        public int Health = 120;

        [Description("The Inventory of the class")]
        public SerializedPlayerInventory inventory = new SerializedPlayerInventory()
        {
            Ammo = new SerializedAmmo(30, 70, 30, 30, 30),
            Items = new List<SerializedPlayerItem> ()
            { 
            new SerializedPlayerItem((int)ItemType.KeycardNTFOfficer, 1, 0, Vector3.one, 100, false), 
            new SerializedPlayerItem((int)ItemType.GunCrossvec, 50, 0, Vector3.one, 100, true), 
            new SerializedPlayerItem(50, 1, 0, Vector3.one, 100, false), 
            new SerializedPlayerItem((int)ItemType.Radio, 100, 0, Vector3.one, 100, false), 
            new SerializedPlayerItem((int)ItemType.ArmorCombat, 1, 0, Vector3.one, 100, false), 
            new SerializedPlayerItem(55, 1, 0, Vector3.one, 100, false), 
            new SerializedPlayerItem((int)ItemType.GrenadeFlash, 1, 0, Vector3.one, 100, false), 
            new SerializedPlayerItem((int)ItemType.GrenadeHE, 1, 0, Vector3.one, 100, false)
            }
        };

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 0;
        public int MaxArtificialHealth = 100;

        [Description("Shield of the class")]
        public int Shield = 15;
        public int MaxShield = 100;

        [Description("The Chance of which the class spawns")]
        public int SpawnChance = 100;

        [Description("Max alive at the same time")]
        public int MaxAlive = 1;

        [Description("The number of players required in the same role to have the chance for the class to appear")]
        public int RequiredPlayers = 0;

        [Description("The name of the class")]
        public string RoleName = "Garde Superviseur";
    }
}