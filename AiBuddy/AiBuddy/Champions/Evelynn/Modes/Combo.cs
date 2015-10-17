#region

using EloBuddy;
using EloBuddy.SDK;

#endregion

namespace AiBuddy.Champions.Evelynn.Modes
{
    internal class Combo
    {
        private static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static void Execute()
        {
            var target = Utils.GetTarget.Target(Evelynn.W.Range, DamageType.Magical);
            if (target == null) return;

            if (Evelynn.Q.IsReady() && target.IsValidTarget(Evelynn.Q.Range))
            {
                Evelynn.Q.Cast();
            }

            if (Evelynn.W.IsReady() && target.IsValidTarget(Evelynn.W.Range) && !target.IsValidTarget(Evelynn.Q.Range))
            {
                Evelynn.W.Cast();
            }

            if (Evelynn.E.IsReady() && target.IsValidTarget(Evelynn.E.Range))
            {
                Evelynn.E.Cast(target);
            }

            if (Evelynn.R.IsReady())
            {
                var targetR = TargetSelector.GetTarget(Evelynn.R.Range, DamageType.Physical);
                if (targetR == null) return;

                if (target.IsValidTarget(Evelynn.R.Range) &&
                    target.Health <= _Player.GetSpellDamage(target, SpellSlot.R))
                {
                    Evelynn.R.Cast(target);
                }

                if (targetR.CountEnemiesInRange(Evelynn.R.Range) >= 2)
                {
                    Evelynn.R.Cast(target);
                }
            }
        }
    }
}