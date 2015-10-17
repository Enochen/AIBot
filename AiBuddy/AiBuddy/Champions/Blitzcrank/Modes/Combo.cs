#region

using AiBuddy.Utils;
using AiBuddy.Utils.DamageLib;
using EloBuddy.SDK;

#endregion

namespace AiBuddy.Champions.Blitzcrank.Modes
{
    internal class Combo
    {
        public static void Execute()
        {
            var target = GetTarget.Target(Blitzcrank.Q.Range, GetDamageType.Get());

            if (target == null)
            {
                return;
            }

            if (Blitzcrank.Q.IsReady() && target.IsValidTarget(Blitzcrank.Q.Range))
            {
                Blitzcrank.Q.Cast(target);
            }

            if (Blitzcrank.W.IsReady() && target.IsValidTarget())
            {
                Blitzcrank.W.Cast();
            }

            if (Blitzcrank.R.IsReady() && target.IsValidTarget(Blitzcrank.R.Width))
            {
                Blitzcrank.R.Cast(target);
            }
        }
    }
}