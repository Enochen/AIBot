#region

using System;
using System.Collections.Generic;
using EloBuddy;

#endregion

namespace AiBuddy.AI.Maps.HowlingAbyss.Brain
{
    using System.Linq;

    using EloBuddy.SDK;

    using AiBuddy.AI.Maps.HowlingAbyss.Brain.FollowBot;
    /// <summary>
    /// Started by KarmaPanda. Please Improved xD
    /// </summary>
    internal class Brain
    {
        /// <summary>
        /// Called whenever the Game Updates. TODO: Insert Brain.exe here please
        /// </summary>
        /// <param name="args"></param>
        public static void OnUpdate(EventArgs args)
        {
            // Initialize follow bot if there is more than one ally.
            if (EntityManager.Heroes.Allies.Count(t => !t.IsMe) > 1)
            {
                FollowBot();
            }
            else
            {
                AiBot();
            }
        }

        /// <summary>
        /// Called if there is more than 1 ally. (This is the follow bot)
        /// </summary>
        public static void FollowBot()
        {
            string[] ADC =
            {
                "Vayne", "Kalista", "Jinx", "Graves", "Urgot", "Corki",
                "Draven", "Tristana", "Ashe", "Miss Fortune", "Varus", "Lucian"
            };
            var GetFollow =
                EntityManager.Heroes.Allies.Find(
                    a =>
                        a.IsAlly && !a.IsMe && a.IsValid && a.CanCast && a.CanMove && a.Deaths < 10);
            var isADC = ADC.Contains(GetFollow.ChampionName);


        }



        /// <summary>
        /// Called if Follow Bot isn't called. (This is the automatic bot)
        /// </summary>
        public static void AiBot()
        {
            //Not implemented yet
        }
    }
}