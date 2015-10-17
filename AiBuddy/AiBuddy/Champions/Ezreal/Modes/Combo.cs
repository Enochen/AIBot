#region

using AiBuddy.Utils;
using EloBuddy;
using EloBuddy.SDK;

#endregion

namespace AiBuddy.Champions.Ezreal.Modes
{
    internal class Combo
    {
        private static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static void Execute()
        {
            var target = GetTarget.GetComboTarget();
            if (target == null) return;

            if (Ezreal.Q.IsReady() && target.IsValidTarget(Ezreal.Q.Range))
            {
                Ezreal.Q.Cast(target);
            }

            if (Ezreal.W.IsReady() && target.IsValidTarget(Ezreal.W.Range))
            {
                Ezreal.W.Cast();
            }

            if (Ezreal.R.IsReady())
            {
                var targetR = TargetSelector.GetTarget(Ezreal.R.Range, DamageType.Physical);
                if (targetR == null) return;

                if (target.IsValidTarget(Ezreal.R.Range) &&
                    target.Health <= _Player.GetSpellDamage(target, SpellSlot.R))
                {
                    Ezreal.R.Cast(target);
                }
            }
        }
    }
}