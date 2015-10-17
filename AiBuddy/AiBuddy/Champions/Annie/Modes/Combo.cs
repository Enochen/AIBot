#region

using EloBuddy;
using EloBuddy.SDK;

#endregion

namespace AiBuddy.Champions.Annie.Modes
{
    internal class Combo
    {
        private static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static void Execute()
        {
            var target = Utils.GetTarget.Target(Annie.Q.Range, DamageType.Magical);
            if (target == null) return;

            if (Annie.Q.IsReady() && target.IsValidTarget(Annie.Q.Range))
            {
                Annie.Q.Cast(target);
            }

            if (Annie.W.IsReady() && target.IsValidTarget(Annie.W.Range))
            {
                Annie.W.Cast(target);
            }

            if (Annie.R.IsReady() && target.IsValidTarget(Annie.R.Range) && target.CountEnemiesInRange(290) >= 2 &&
                _Player.HasBuff("Energized"))
            {
                Annie.R.Cast(target);
            }
        }
    }
}