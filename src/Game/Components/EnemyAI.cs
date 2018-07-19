﻿//Copyright(c) 2018 Daniel Bramblett, Daniel Dupriest, Brandon Goldbeck
using System;
using System.Collections.Generic;
using System.Text;

using Ecs;
using IO;

namespace Game.Components
{
    class EnemyAI : Component
    {
        private int movementRate = 3;
        private int lastMoved = 0;
        public void SetRate(int rate)
        {
            movementRate = rate;
        }
        public override void Start()
        {
            base.Start();
        }

        public override void LateUpdate()
        {
            if (lastMoved >= movementRate)
            {
                base.LateUpdate();
                Think();
                lastMoved = 0;
            }
            else
            {
                ++lastMoved;
            }
        }

        public void Think()
        {
            Enemy puppet = (Enemy)base.gameObject.GetComponent<Enemy>();

            if (puppet == null)
            {
                Debug.LogError("EnemyAI component needs an enemy object");
                return;
            }
            if (puppet.Target != null)
            {
                SeekMove(puppet.Target, puppet.OnMove);
            }
            else
            {
                RandomMove(puppet.OnMove);
            }
        }

        /// <summary>
        /// This method moves the enemy towards a known player.
        /// </summary>
        private void SeekMove(Transform target, Action<int,int> Move)
        {
            if (target == null) return;
            Vec2i deltaMove;

            if (Vec2i.Heuristic(target.position, transform.position) == 1)
            {
                deltaMove = target.position - transform.position;
                Move(deltaMove.x, deltaMove.y);
            }
            else
            {
                NavigatorAgent navigation = (NavigatorAgent)base.gameObject.GetComponent<NavigatorAgent>();
                if (navigation == null)
                {
                    Debug.LogError("EnemyAI component needs an navigator agent object");
                    return;
                }

                List<Vec2i> path = navigation.targetPath;
                if (path != null && path.Count != 0)
                {
                    deltaMove = path[path.Count - 1] - transform.position;
                    Move(deltaMove.x, deltaMove.y);
                }
            }
            /*Random rand = new Random();

            //Figures out which direction on the it has to move to head towards the player.
            Vec2i deltaMove = target.position - transform.position;
            int dx = deltaMove.x;
            int dy = deltaMove.y;

            if (deltaMove.x != 0)
            {
                dx = deltaMove.x / Math.Abs(deltaMove.x);
            }
            if (deltaMove.y != 0)
            {
                dy = deltaMove.y / Math.Abs(deltaMove.y);
            }

            //It randomly decides whether to try to move on the X-axis or Y-axis.
            bool moveOnX = deltaMove.y == 0 ||(rand.Next() % 2 == 0 && deltaMove.x != 0);
            dx = moveOnX ? dx : 0;
            dy = moveOnX ? 0 : dy;

            //HUD.Append(dx + " " + dy);
            Move(dx, dy);*/
        }

        /// <summary>
        /// This method randomly move the enemy.
        /// </summary>
        private void RandomMove(Action<int, int> Move)
        {
            Random rand = new Random();
            int dx = 0, dy = 0;

            //The enemy can randomly move in 5 ways:left, up, right, down, and not move.
            switch (rand.Next() % 5)
            {
                case 0:
                    dx = 1;
                    break;
                case 1:
                    dx = -1;
                    break;
                case 2:
                    dy = 1;
                    break;
                case 3:
                    dy = -1;
                    break;
                default:
                    break;
            }
            Move(dx, dy);
        }
    }
}
