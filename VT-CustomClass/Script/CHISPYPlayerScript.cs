﻿using VTCustomClass.Pouvoir;
using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using UnityEngine;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;
using static VT_Referance.Variable.Data;

namespace VTCustomClass.PlayerScript
{
    public class CHISPYScript : BasePlayerScript
    {
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.CHIenemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.CHIally;

        protected override RoleType RoleType => RoleType.NtfPrivate;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.ChaosSpy;

        protected override string RoleName => Plugin.ConfigCHISPY.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigCHISPY;

        protected override bool SetDisplayInfo => false;

        public override bool CallPower(int power)
        {
            if (power == (int)PowerType.SwitchRole && Player.RoleType == RoleType.NtfPrivate)
            {
                Server.Get.Map.SpawnGrenade(Player.Position, Vector3.zero, 0.1f, GrenadeType.Flashbang);
                Player.ChangeRoleAtPosition(RoleType.ChaosConscript);
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
                CallPower((int)PowerType.SwitchRole);
            }
        }

        private void OnDeath(PlayerDeathEventArgs ev)
        {
            if (ev.Victim == Player)
                Player.ChangeRoleAtPosition(RoleType.ChaosConscript);
            else if (ev.Killer == Player)
            {
                string message = Plugin.PluginTranslation.ActiveTranslation.KilledMessage.Replace( "%RoleName%", RoleName);
                ev.Victim.OpenReportWindow(message);
            }
        }

    }
}
