﻿using VTCustomClass.Pouvoir;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using UnityEngine;
using VT_Referance.Variable;
using VT_Referance.Method;
using System;
using VT_Referance.PlayerScript;
using static VT_Referance.Variable.Data;

namespace VTCustomClass.PlayerScript
{
    public class CHIInfirmierScript : BasePlayerScript
    {
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.CHIenemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.CHIally;

        protected override RoleType RoleType => RoleType.ChaosRepressor;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.ChaosInfirmier;

        protected override string RoleName => Plugin.ConfigCHIInfirmier.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigCHIInfirmier;

        private DateTime lastPower = DateTime.Now.AddSeconds(-Plugin.ConfigCHIInfirmier.Cooldown);

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnDammage;
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
        }
        
        public override void DeSpawn()
        {
            base.DeSpawn();
            Server.Get.Events.Player.PlayerDamageEvent -= OnDammage;
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPress;
        }

        private void OnDammage(PlayerDamageEventArgs ev)
        {
            if (ev.Killer == Player)
                ev.HollowBullet(Player);
        }

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player == Player && ev.KeyCode == KeyCode.Alpha1)
                CallPower((int)PowerType.Defibrillation);
        }

        public override bool CallPower(int power)
        {
            if (power == (int)PowerType.Defibrillation)
            { 
                if((DateTime.Now - lastPower).TotalSeconds > Plugin.ConfigCHIInfirmier.Cooldown)
                {
                    Player corpseowner = Methods.GetPlayercoprs(Player, 2.5f);
                    if (Methods.IsWasScpRole(corpseowner) == false)
                    { 
                        corpseowner.RoleID = Data.PlayerRole[corpseowner];
                        corpseowner.Position = corpseowner.DeathPosition;
                        corpseowner.Inventory.Clear();
                        lastPower = DateTime.Now;
                    }
                }
                else Reponse.Cooldown(Player, lastPower, Plugin.ConfigCHIInfirmier.Cooldown);
            }
            else return false;
            return false;
        }
    }
}
