#region

using AiBuddy.Utils;
using EloBuddy.SDK;

#endregion

namespace AiBuddy.Champions.Akali.Modes
{
    internal class Combo
    {
        public static float ComboRange = Akali.R.Range;

        public static void Execute()
        {
            var target = GetTarget.GetComboTarget();
            if (target == null) return;

            if (Akali.Q.IsReady() && target.IsValidTarget(Akali.Q.Range))
            {
                Akali.Q.Cast(target);
            }

            if (Akali.E.IsReady() && target.IsValidTarget(Akali.E.Range))
            {
                Akali.E.Cast();
            }

            if (Akali.R.IsReady())
            {
                var targetR = GetTarget.Target(Akali.R.Range, Utils.DamageLib.GetDamageType.Get());
                if (targetR == null) return;

                if (target.IsValidTarget(Akali.R.Range) && target.CountEnemiesInRange(700) <= 2)
                {
                    Akali.R.Cast(target);
                }
            }
        }
    }
}