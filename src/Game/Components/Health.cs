﻿#region copyright
// Copyright (C) 2018 "Daniel Bramblett" <bram4@pdx.edu>, "Daniel Dupriest" <kououken@gmail.com>, "Brandon Goldbeck" <bpg@pdx.edu>
// This software is licensed under the MIT License. See LICENSE file for the full text.
#endregion

using Ecs;
using Game.Interfaces;

namespace Game.Components
{
    public class Health : Component, IRegen
    {
        private bool inBattle = false;
        private float regenAfterSeconds = 10.0f;
        private float timeSinceLastRegen = 0.0f;

        public void SetHealthRegen(float regenInSeconds)
        {
            regenAfterSeconds = regenInSeconds;
        }

        public override void Update()
        {
            timeSinceLastRegen += ((float)Time.deltaMs / 1000.0f);

            if (regenAfterSeconds < timeSinceLastRegen)
            {
                if (inBattle)
                {
                    inBattle = false;
                }
                else
                {
                    gameObject.SendMessage<Actor>("RegenHP");
                }
                timeSinceLastRegen = 0;
            }
        }

        public void InBattle()
        {
            inBattle = true;
        }
    }
}
