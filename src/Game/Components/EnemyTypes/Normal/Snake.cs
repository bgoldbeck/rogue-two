﻿#region copyright
// Copyright (C) 2018 "Daniel Bramblett" <bram4@pdx.edu>, "Daniel Dupriest" <kououken@gmail.com>, "Brandon Goldbeck" <bpg@pdx.edu>
// This software is licensed under the MIT License. See LICENSE file for the full text.
#endregion

using System;

namespace Game.Components.EnemyTypes
{
    class Snake : Enemy
    {
        public Snake(Random rand, int level, bool isShiny) 
            :base(      
                  "Snake",               //enemy's name
                  "Snake? SNAKE!!!!",    //enemy's description
                  level,                 //Level of the enemy
                  2 + (3 * level),       //Equation for the enemy's health.
                  level,                 //Equation for the enemy's armor.
                  level + 1,             //Equation for the enemy's attack.
                  11 * level,             //xp given by beating this enemy.
                  isShiny,
                  rand
                  )
        {
        }

        public override void Start()
        {
            base.Start();
            mapTile.character = 's';                //enemy's model
            if (isShiny)                            //Color
            {
                mapTile.color.Set(255, 215, 0);
            }
            else
            {
                mapTile.color.Set(255, 80, 80);        
            }
            ai.SetRate(0.5f / ((isShiny)? 2 : 1));                        //Time between each move.
            healthRegen.SetHealthRegen(12.0f / ((isShiny) ? 2 : 1));      //Health regen (seconds for 1 health regen).
            aggro.SetAggroPatience(5.0f);                                //Seconds before it gives up.
            aggro.SetAggroRange(8);                                     //Distance it can see the player at.
        }                           
    }
}
