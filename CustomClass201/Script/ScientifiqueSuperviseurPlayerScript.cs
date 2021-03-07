﻿using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using VT_Referance.Variable;

namespace CustomClass.PlayerScript
{
    public class ScientifiqueSuperviseurScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.CHI, (int)Team.SCP };

        protected override List<int> FriendsList => new List<int> { (int)Team.MTF, (int)Team.RSC };

        protected override RoleType RoleType => RoleType.Scientist;

        protected override int RoleTeam => (int)Team.RSC;

        protected override int RoleId => (int)RoleID.ScientifiqueSuperviseur;

        protected override string RoleName => PluginClass.ConfigScientifiqueSuperviseur.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigScientifiqueSuperviseur;
    }
}
