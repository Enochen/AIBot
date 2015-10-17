#region

using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

#endregion

namespace AiBuddy.AI.Maps.HowlingAbyss.Brain
{
    /// <summary>
    ///     Started by KarmaPanda. Please Improved xD
    /// </summary>
    internal class Brain
    {
        /// <summary>
        ///     Called whenever the Game Updates. TODO: Insert Brain.exe here please
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
        ///     Called if there is more than 1 ally. (This is the follow bot)
        /// </summary>
        public static void FollowBot()
        {
            var getFollow =
                EntityManager.Heroes.Allies.Find(
                    a =>
                        a.IsAlly && !a.IsMe && a.IsValid && a.CanCast && a.CanMove && a.Deaths < 10);

            if (getFollow != null)
            {
                Player.IssueOrder(GameObjectOrder.MoveTo, getFollow.Position);
            }
            else
            {
                var nexus =
                    ObjectManager.Get<Obj_HQ>()
                        .OrderBy(a => Player.Instance.Position.Distance(a.Position))
                        .FirstOrDefault();

                Player.IssueOrder(GameObjectOrder.MoveTo, nexus.Position);
                AiBot(); //will be using this if no one to follow

                Console.WriteLine("no one to follow");
            }
        }

        /// <summary>
        ///     Called if Follow Bot isn't called. (This is the automatic bot)
        /// </summary>
        public static void AiBot()
        {
            //Not implemented yet
        }
    }
}