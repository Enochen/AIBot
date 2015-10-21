#region

using AiBuddy.Champions.Utils;
using AiBuddy.Utils;
using EloBuddy.SDK;

#endregion

namespace AiBuddy.Champions.Aatrox.Modes
{
    internal class Combo
    {
        public static void Execute()
        {

            var target = GetTarget.GetComboTarget();
            if (target == null) return;

            if (Aatrox.Q.IsReady() && target.IsValidTarget(Aatrox.Q.Range) &&
                target.CountEnemiesInRange(Aatrox.Q.Range) <= 2)
            {
                Aatrox.Q.Cast(target);
            }

            if (Aatrox.E.IsReady() && target.IsValidTarget(Aatrox.E.Range))
            {
                Aatrox.E.Cast(target);
            }

            if (Aatrox.W.IsReady() && Helper.Player.HealthPercent <= 60 && Helper.Player.HasBuff("AatroxWPower"))
            {
                Aatrox.W.Cast();
            }

            if (Aatrox.W.IsReady() && Helper.Player.HealthPercent >= 90 && !Helper.Player.HasBuff("AatroxWPower"))
            {
                Aatrox.W.Cast();
            }

            if (Aatrox.R.IsReady() && target.IsValidTarget(Aatrox.R.Range) &&
                Helper.Player.CountEnemiesInRange(Aatrox.Q.Range) <= 2)
            {
                Aatrox.R.Cast(target);
            }
        }
    }
}