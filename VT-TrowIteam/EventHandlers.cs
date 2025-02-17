﻿using InventorySystem.Items.ThrowableProjectiles;
using MEC;
using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using UnityEngine;

namespace VTTrowItem
{
    internal class EventHandlers
    {
        // https://github.com/sanyae2439/SanyaPlugin_Exiled
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerShootEvent += OnShootEvent;
        }

        private void OnShootEvent(PlayerShootEventArgs ev)
        {
            if (ev.TargetPosition != Vector3.zero
                && Physics.Linecast(ev.Player.Position, ev.TargetPosition, out RaycastHit raycastHit, 1049088))
            {
                if (Plugin.Config.ShootMouve)
                {
                    var pickup = raycastHit.transform.GetComponentInParent<ItemPickupBase>();
                    if (pickup != null && pickup.Rb != null)
                    {
                        pickup.Rb.AddExplosionForce(Vector3.Distance(ev.TargetPosition, ev.Player.Position), ev.Player.Position, 500f, 3f, ForceMode.Impulse);
                    }
                }

                if (Plugin.Config.ShootInstantFuse)
                {
                    var grenade = raycastHit.transform.GetComponentInParent<ExplosionGrenade>();
                    if (grenade != null)
                    {
                        grenade.NetworkfuseTime -= grenade.NetworkfuseTime;
                    }
                }
            }
        }
    }
}