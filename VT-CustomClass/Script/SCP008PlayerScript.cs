﻿using VTCustomClass.Pouvoir;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Referance.Variable;
using VT_Referance.Method;
using VT_Referance.PlayerScript;
using static VT_Referance.Variable.Data;
using System;

namespace VTCustomClass.PlayerScript
{
    public class SCP008Script : BasePlayerScript
    {
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.SCPenemy;

        protected override List<int> FriendsList => TeamGroupe.SCPally;

        protected override RoleType RoleType => RoleType.Scp0492;

        protected override int RoleTeam => (int)TeamID.SCP;

        protected override int RoleId => (int)RoleID.SCP008;

        protected override string RoleName => Plugin.ConfigSCP008.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigSCP008;
        Aura aura;
        protected override void AditionalInit()
        {
            aura = ActiveComponent<Aura>();
            {
                aura.PlayerEffect = Effect.ArtificialRegen;
                aura.TargetEffect = Effect.Poisoned;
                aura.HerIntencty = 6;
                aura.HerTime = 5;
                aura.MyHp = Plugin.ConfigSCP008.HealHp;
                aura.HerHp = -Plugin.ConfigSCP008.DomageHp;
                aura.Distance = Plugin.ConfigSCP008.Distance;
            }
        }

        public override void DeSpawn()
        {
            base.DeSpawn();
            KillComponent<Aura>();
            Server.Get.Events.Player.PlayerDamageEvent -= OnAttack;
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPress;
            Server.Get.Events.Player.PlayerDeathEvent -= OnDeath;
        }

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnAttack;
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
            Server.Get.Events.Player.PlayerDeathEvent += OnDeath;
        }

        private void OnDeath(PlayerDeathEventArgs ev)
        {
            if (Player == ev.Victim && !Server.Get.Players.Where(p => p.RoleID == (int)RoleID.SCP008).Any())
                Map.Get.GlitchedCassie("ALL SCP 0 0 8 SUCCESSFULLY TERMINATED . NOSCPSLEFT");
        }

        private void OnAttack(PlayerDamageEventArgs ev)
        {
            if (ev.Allow && ev.Killer == Player && ev.HitInfo.Tool == DamageTypes.Scp0492)
            {
                if (!ev.Victim.IsUTR())
                    ev.Victim.GiveEffect(Effect.Bleeding, 2, 4);
                ev.DamageAmount = 50;
            }
        }


        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player == Player && ev.KeyCode == KeyCode.Alpha1)
                CallPower((int)PowerType.Zombifaction);
        }

        public override bool CallPower(int power)
        {
            if (power == (int)PowerType.Zombifaction)
            {
                Player corpseowner = Methods.GetPlayercoprs(Player, 4);
                if (Methods.IsWasScpRole(corpseowner) == false)
                {
                    corpseowner.RoleID = (int)RoleID.SCP008;
                    Player.Health += 100;
                    corpseowner.Position = Player.Position;
                }
                return true;
            }
            return false;
        }
    }
}
