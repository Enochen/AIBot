#region

using AiBuddy.AI.Maps.HowlingAbyss.Brain;
using EloBuddy;
using EloBuddy.SDK;

#endregion

namespace AiBuddy.Champions.Aatrox.Modes
{
    internal class Combo
    {
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static void Execute()
        {
            if (ComboWinPrediction.Calculate() <= 50) return;

            var target = TargetSelector.GetTarget(Aatrox.E.Range, DamageType.Physical);
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

            if (Aatrox.W.IsReady() && _Player.HealthPercent <= 60 && _Player.HasBuff("AatroxWPower"))
            {
                Aatrox.W.Cast();
            }

            if (Aatrox.W.IsReady() && _Player.HealthPercent >= 90 && !_Player.HasBuff("AatroxWPower"))
            {
                Aatrox.W.Cast();
            }

            if (Aatrox.R.IsReady() && target.IsValidTarget(Aatrox.R.Range) &&
                _Player.CountEnemiesInRange(Aatrox.Q.Range) <= 2)
            {
                Aatrox.R.Cast(target);
            }
        }
    }
}