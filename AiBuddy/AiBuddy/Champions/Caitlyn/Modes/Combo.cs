#region

using EloBuddy;
using EloBuddy.SDK;

#endregion

namespace AiBuddy.Champions.Caitlyn.Modes
{
    internal class Combo
    {
        private static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static void Execute()
        {
            var target = Utils.GetTarget.Target(Caitlyn.Q.Range, DamageType.Physical);
            if (target == null) return;

            if (Caitlyn.Q.IsReady() && target.IsValidTarget(Caitlyn.Q.Range))
            {
                Caitlyn.Q.Cast(target);
            }

            if (Caitlyn.W.IsReady() && target.IsValidTarget(Caitlyn.W.Range) && target.HasBuffOfType(BuffType.Stun) ||
                target.HasBuffOfType(BuffType.Snare))
            {
                Caitlyn.W.Cast(target);
            }

            if (Caitlyn.R.IsReady())
            {
                var targetR = TargetSelector.GetTarget(Caitlyn.R.Range, DamageType.Physical);
                if (targetR == null) return;

                if (target.IsValidTarget(Caitlyn.R.Range) &&
                    target.Health <= _Player.GetSpellDamage(target, SpellSlot.R))
                {
                    Caitlyn.R.Cast(target);
                }
            }
        }
    }
}