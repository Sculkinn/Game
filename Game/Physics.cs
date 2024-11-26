﻿using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    internal class Physics
    {
        public Game Game;

        public Physics(Game game)
        {
            this.Game = game;
        }

        public void ApplyGravity(List<Object> objects)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].Size == (10f, 0.1f, 10f))
                {
                }
                else
                {
                    for (int j = i + 1; j < objects.Count; j++)
                    {
                        if (objects[j].Size == (10f, 0.1f, 10f))
                        {
                            if (CheckCollision(objects[i], objects[j]))
                            {
                                Object obj = objects[i];
                                Object obj2 = objects[i];
                                obj.GravityAffected = false;
                                obj.Velocity = new Vector3(0, 0, 0);

                                //obj.Move(0, 1, 0);
                            }
                            else
                            {
                                objects[i].GravityAffected = true;
                            }
                        }
                        else
                        {
                            Object obj = objects[i]; //obj being pushed
                            Object obj2 = objects[i]; //obj pushing
                            if (CheckCollision(objects[i], objects[j]))
                            {
                                obj2.CanMove = false;
                            }
                            else
                            {
                                obj2.CanMove = true;
                            }
                        }
                    }
                }
            }
            foreach (var obj in objects)
            {
                if (obj.GravityAffected)
                {
                    if (obj.Weight != 0)
                    {
                        obj.Translation *= Matrix4.CreateTranslation(obj.Velocity);
                        obj.SetModelMatrix(obj.Translation);
                        obj.Velocity += new Vector3(0,-0.0075f,0);
                        //0.147105
                    }
                }
            }
        }

        public static bool CheckCollision(Object obj1, Object obj2)
        {
            Vector3 size1 = obj1.Size;
            Vector3 size2 = obj2.Size;

            Vector3 min1 = obj1.Position - size1;
            Vector3 max1 = obj1.Position + size1;

            Vector3 min2 = obj2.Position - size2;
            Vector3 max2 = obj2.Position + size2;

            return (min1.X <= max2.X && max1.X >= min2.X) &&
                   (min1.Y <= max2.Y && max1.Y >= min2.Y) &&
                   (min1.Z <= max2.Z && max1.Z >= min2.Z);
        }
    }
}
