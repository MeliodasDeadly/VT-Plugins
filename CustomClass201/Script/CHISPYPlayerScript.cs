﻿using CustomClass.Pouvoir;
using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CustomClass.PlayerScript
{
    public class ICSPYScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.MTF };

        protected override List<int> FriendsList => new List<int> { (int)Team.CHI, (int)Team.CDP };

        protected override RoleType RoleType => RoleType.NtfCadet;

        protected override int RoleTeam => (int)Team.CHI;

        protected override int RoleId => (int)MoreClasseID.CHISPY;

        protected override string RoleName => PluginClass.ConfigCHISPY.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigCHISPY;

        protected override bool SetDisplayInfo => false;

        public override bool CallPower(PowerType power)
        {
            if (power == PowerType.ChangeRole && Player.RoleType == RoleType.NtfCadet)
            {
                Server.Get.Map.SpawnGrenade(Player.Position, Vector3.zero, 0.1f, GrenadeType.Flashbang);
                Player.ChangeRoleAtPosition(RoleType.ChaosInsurgency);
                Player.MaxHealth = Config.GetConfigValue("Health", 120);
                Player.GiveEffect(Effect.Blinded, 0, 0);
                return true;
            }
            return false;
        }

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerDeathEvent += OnDeath;
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
        }

        public override void DeSpawn()
        {
            base.DeSpawn();
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPress;
            Server.Get.Events.Player.PlayerDeathEvent -= OnDeath;
        }

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player == Player && ev.KeyCode == UnityEngine.KeyCode.Alpha1)
            {
                CallPower(PowerType.ChangeRole);
            }
        }

        private void OnDeath(PlayerDeathEventArgs ev)
        {
            if (ev.Victim == Player)
            {
                Player.ChangeRoleAtPosition(RoleType.ChaosInsurgency);
            }
        }

    }
}
