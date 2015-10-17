#region

using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

#endregion

namespace AiBuddy.AI.Maps.HowlingAbyss.Brain
{
    internal class GetHeroes
    {
        /// <summary>
        ///     Gets the valid heroes ingame
        /// </summary>
        public static IEnumerable<AIHeroClient> TeamHeroes
        {
            get
            {
                return EntityManager.Heroes.Allies
                    .Where(h => h.IsValid);
            }
        }

        public static IEnumerable<AIHeroClient> AliveTeamHeroes
        {
            get
            {
                return EntityManager.Heroes.Allies
                    .Where(h => h.IsValid && !h.IsDead);
            }
        }

        public static IEnumerable<AIHeroClient> DeadTeamHeroes
        {
            get
            {
                return EntityManager.Heroes.Allies
                    .Where(h => h.IsValid && h.IsDead);
            }
        }

        public static IEnumerable<AIHeroClient> EnemyHeroes
        {
            get
            {
                return EntityManager.Heroes.Enemies
                    .Where(h => h.IsValid);
            }
        }

        public static IEnumerable<AIHeroClient> AliveEnemyHeroes
        {
            get
            {
                return EntityManager.Heroes.Enemies
                    .Where(h => h.IsValid && !h.IsDead);
            }
        }

        public static IEnumerable<AIHeroClient> DeadEnemyHeroes
        {
            get
            {
                return EntityManager.Heroes.Enemies
                    .Where(h => h.IsValid && h.IsDead);
            }
        }

        public static IEnumerable<AIHeroClient> AllHeroes
        {
            get
            {
                return EntityManager.Heroes.AllHeroes
                    .Where(h => h.IsValid);
            }
        }

        public static IEnumerable<AIHeroClient> AllAliveHeroes
        {
            get
            {
                return EntityManager.Heroes.AllHeroes
                    .Where(h => h.IsValid && !h.IsDead);
            }
        }

        public static IEnumerable<AIHeroClient> AllDeadHeroes
        {
            get
            {
                return EntityManager.Heroes.AllHeroes
                    .Where(h => h.IsValid && h.IsDead);
            }
        }
    }
}