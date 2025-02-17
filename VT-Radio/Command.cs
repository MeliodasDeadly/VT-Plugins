﻿using MEC;
using Respawning;
using Synapse.Command;
using VT_Referance.Method;
using VT_Referance.Variable;

namespace VTRadio
{
    [CommandInformation(
        Name = "Avion",
        Aliases = new[] { "AirBomb" },
        Description = "A Command for Start air bombardmen",
        Permission = "",
        Platforms = new[] { Platform.ClientConsole },
        Usage = "Use .Avion and you must ave a radio in hand"
        )]
    public class Avion : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();
            if (context.Player.RoleID != (int)RoleID.NtfCommander && context.Player.RoleID != (int)RoleID.CdmCommander
             && context.Player.RoleID != (int)RoleID.NtfCaptain && context.Player.RoleID != (int)RoleID.NtfLieutenantColonel)
            {
                result.Message = "you do not have the accreditation for this order";
                result.State = CommandResultState.NoPermission;
            }
            else if (context.Player.ItemInHand.ID != (int)ItemType.Radio)
            {
                result.Message = "you need a radio !";
                result.State = CommandResultState.NoPermission;
            }
            else if (RespawnManager.Singleton.GetFieldValueorOrPerties<float>("_timeForNextSequence") <= 15)
            {
                result.State = CommandResultState.NoPermission;
                result.Message = "too close to a respawn";
            }
            else if (!Methods.isAirBombCurrently)
            {
                Timing.RunCoroutine(Methods.AirBomb(7, 5));
                result.State = CommandResultState.Ok;
                result.Message = "Air Bomb Start";
            }
            else
            {
                result.State = CommandResultState.NoPermission;
                result.Message = "there is already a bombardment";
            }
            return result;
        }
    }

}
