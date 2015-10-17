#region

using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

#endregion

namespace AiBuddy.Champions.Soraka.Modes
{
    internal class Combo
    {
        private static AIHeroClient Me
        {
            get { return ObjectManager.Player; }
        }

        public static void Execute()
        {
            var enemy = AiBuddy.Utils.GetTarget.Target(Soraka.Q.Range, DamageType.Magical);
            var ally =
                EntityManager.Heroes.Allies.Where(
                    x =>
                        !x.IsMe && x.IsValid && !x.IsZombie && x.IsInRange(Player.Instance, Soraka.W.Range)
                        && !x.IsInShopRange()).OrderBy(x => x.HealthPercent).First();

            if (Soraka.R.IsReady() && ally.HealthPercent < 20)
            {
                Soraka.R.Cast();
            }

            if (Soraka.W.IsReady() && ally.HealthPercent < 70)
            {
                Soraka.W.Cast(ally);
            }

            if (Soraka.Q.IsReady())
            {
                Soraka.Q.Cast(enemy);
            }
        }
    }
}