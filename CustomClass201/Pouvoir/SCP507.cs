﻿using Mirror;
using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VT_Referance.Behaviour;

namespace CustomClass.Pouvoir
{
    class SCP507 : BaseRepeatingBehaviour
    {
        private Player player;
        private int _timer;
        private int duraction;
        private int rnd;
        public int minduraction { get; set; }
        public int maxduraction { get; set; }
        public List<Vector3> rooms = new List<Vector3>();
        private void Start()
        {
            minduraction = PluginClass.ConfigSCP507.MinTPower;
            maxduraction = PluginClass.ConfigSCP507.MaxTPower;
            foreach (var mapPoint in PluginClass.ConfigSCP507.ListRoom)
                rooms.Add(mapPoint.Parse().Position);

            player = gameObject.GetPlayer();
            duraction = UnityEngine.Random.Range(minduraction, maxduraction);
        }

        protected override void BehaviourAction()
        {
            _timer += 1;
            if (_timer > duraction)
            {
                rnd = UnityEngine.Random.Range(0, rooms.Count() - 1);
                player.MapPoint = PluginClass.ConfigSCP507.ListRoom[rnd].Parse();
                duraction = UnityEngine.Random.Range(minduraction, maxduraction);
            }
            if (_timer > duraction)
                _timer = 0;
        }
    }
}

