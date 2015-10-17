#region

using EloBuddy;
using EloBuddy.SDK;

#endregion

namespace AiBuddy.Champions.Amumu.Modes
{
    internal class Combo
    {
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static void Execute()
        {
            var target = TargetSelector.GetTarget(Amumu.Q.Range, DamageType.Magical);
            if (target == null) return;

            if (Amumu.Q.IsReady() && target.IsValidTarget(Amumu.Q.Range) && target.CountEnemiesInRange(Amumu.Q.Range) <= 2)
            {
                Amumu.Q.Cast(target);
            }

            if (Amumu.E.IsReady() && target.IsValidTarget(Amumu.E.Range))
            {
                Amumu.E.Cast();
            }

            if (Amumu.W.IsReady() && target.IsValidTarget(Amumu.W.Range) && _Player.ManaPercent >= 30 && !_Player.HasBuff("AuraofDespair"))
            {
                Amumu.W.Cast();
            }

            if (Amumu.W.IsReady() && !target.IsValidTarget(Amumu.W.Range) && _Player.ManaPercent <= 30 && _Player.HasBuff("AuraofDespair"))
            {
                Amumu.W.Cast();
            }

            if (Amumu.R.IsReady() && target.IsValidTarget(Amumu.R.Range) && target.HealthPercent <= 50 && _Player.CountEnemiesInRange(Amumu.R.Range) <= 2)
            {
                Amumu.R.Cast();
            }
        }
    }
}