#region

using EloBuddy;
using EloBuddy.SDK;

#endregion

namespace AiBuddy.Champions.DrMundo.Modes
{
    internal class Combo
    {
        private static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static void Execute()
        {
            var target = Utils.GetTarget.Target(DrMundo.W.Range, DamageType.Magical);
            if (target == null) return;

            if (DrMundo.Q.IsReady() && target.IsValidTarget(DrMundo.Q.Range))
            {
                DrMundo.Q.Cast(target);
            }

            if (DrMundo.W.IsReady() && target.IsValidTarget(DrMundo.W.Range) && _Player.HealthPercent >= 90 &&
                !_Player.HasBuff("BurningAgony"))
            {
                DrMundo.W.Cast();
            }

            if (DrMundo.W.IsReady() && target.IsValidTarget(DrMundo.W.Range) && _Player.HealthPercent <= 20 &&
                _Player.HasBuff("BurningAgony"))
            {
                DrMundo.W.Cast();
            }

            if (DrMundo.R.IsReady() && target.IsValidTarget(DrMundo.R.Range) &&
                _Player.CountEnemiesInRange(DrMundo.R.Range) >= 2 && _Player.HealthPercent <= 25)
            {
                DrMundo.R.Cast();
            }
        }
    }
}