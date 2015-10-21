#region

using AiBuddy.AI.Maps.HowlingAbyss.Brain;
using AiBuddy.Champions.Utils;
using AiBuddy.Utils;
using EloBuddy.SDK;

#endregion

namespace AiBuddy.Champions.Ahri.Modes
{
    internal class Combo
    {
        public static void Execute()
        {

            var target = GetTarget.GetComboTarget();
            if (target == null) return;

            if (Ahri.E.IsReady() && target.IsValidTarget(Ahri.E.Range))
            {
                Ahri.E.Cast(target);
            }

            if (Ahri.Q.IsReady() && target.IsValidTarget(Ahri.Q.Range))
            {
                Ahri.Q.Cast(target);
            }

            if (Ahri.W.IsReady() && target.IsValidTarget(Ahri.W.Range))
            {
                Ahri.W.Cast(target);
            }

            if (Ahri.R.IsReady() && target.IsValidTarget(Ahri.R.Range) &&
                Helper.Player.CountEnemiesInRange(Ahri.Q.Range) <= 2 && target.HealthPercent <= 30 && ComboWinPrediction.Calculate() <= 70)
            {
                Ahri.R.Cast(target);
            }
        }
    }
}