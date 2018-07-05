﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ecs;
using Game.Interfaces;

namespace Game.Components
{
    class Enemy : Actor, IDamageable, IMovable
    {
        public Enemy()
        {
        }

        public Enemy(string name, string description, int level, int hp, int arm, int attack)
            :base(name, description, level, hp, arm, attack)
        {
        }

        public override void Start()
        {
            return;
        }

        public override void Update()
        {
            return;
        }

        public override void Render()
        {
            return;
        }

        public void ApplyDamage(int damage)
        {
            return;
        }

        public void OnDeath()
        {
            return;
        }

        public void Move(int dx, int dy)
        {
            return;
        }
    }
}
