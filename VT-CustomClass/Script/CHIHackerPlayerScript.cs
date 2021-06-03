﻿using VTCustomClass.Pouvoir;
using MEC;
using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;

namespace VTCustomClass.PlayerScript
{
    public class CHIHackerScript : BasePlayerScript
    {
        protected override string SpawnMessage => PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.CHIenemy;

        protected override List<int> FriendsList => Server.Get.FF ? new List<int> { } : TeamGroupe.CHIally;

        protected override RoleType RoleType => RoleType.ChaosInsurgency;

        protected override int RoleTeam => (int)TeamID.CHI;

        protected override int RoleId => (int)RoleID.ChiHacker;

        protected override string RoleName => PluginClass.ConfigCHIHacker.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigCHIHacker;

        public override bool CallPower(int power)
        {
            switch (power)
            {
                case (int)PowerType.DoorHack:
                    if ((DateTime.Now - lastPowerDoor).TotalSeconds > PluginClass.ConfigCHIHacker.CoolDownDoor)
                    {
                        Hack.Door(Player);
                        lastPowerDoor = DateTime.Now;
                    }
                    else
                        Reponse.Cooldown(Player, lastPowerDoor, PluginClass.ConfigCHIHacker.CoolDownDoor);
                    return true;

                case (int)PowerType.LightHack:

                    if ((DateTime.Now - lastPowerLight).TotalSeconds > PluginClass.ConfigCHIHacker.CoolDownDoor)
                    {
                        Hack.light();
                        lastPowerLight = DateTime.Now;
                    }
                    else
                        Reponse.Cooldown(Player, lastPowerLight, PluginClass.ConfigCHIHacker.CoolDownDoor);
                    return true;

                case (int)PowerType.CASSIEHack:
                    if ((DateTime.Now - lastPowerMessage).TotalSeconds > PluginClass.ConfigCHIHacker.CoolDownDoor)
                    {
                        Hack.Message();
                        lastPowerMessage = DateTime.Now;
                    }
                    else
                        Reponse.Cooldown(Player, lastPowerMessage, PluginClass.ConfigCHIHacker.CoolDownDoor);
                    return true;
                
                default:
                    return false;
            }
        }

        private DateTime lastPowerDoor = DateTime.Now.AddSeconds(-PluginClass.ConfigCHIHacker.CoolDownDoor);
        private DateTime lastPowerLight = DateTime.Now.AddSeconds(-PluginClass.ConfigCHIHacker.CoolDownDoor);
        private DateTime lastPowerMessage = DateTime.Now.AddSeconds(-PluginClass.ConfigCHIHacker.CoolDownDoor);
        protected override void Event()
        {
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress; 
        }

        public override void DeSpawn()
        {
            base.DeSpawn();
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPress;
        }

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player == Player)
            {
                switch (ev.KeyCode)
                {
                    case KeyCode.Alpha1: CallPower((int)PowerType.DoorHack);break;
                    case KeyCode.Alpha2: CallPower((int)PowerType.LightHack);break;
                    case KeyCode.Alpha3: CallPower((int)PowerType.CASSIEHack);break;
                }
            }
        }
    }
}
