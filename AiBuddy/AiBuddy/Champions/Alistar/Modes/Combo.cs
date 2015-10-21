#region

using System.Linq;
using AiBuddy.Champions.Utils;
using AiBuddy.Utils;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;

#endregion

namespace AiBuddy.Champions.Alistar.Modes
{
    internal class Combo
    {
        public static void Execute()
        {
            var target = GetTarget.GetComboTarget();
            if (target == null) return;

            if (Alistar.W.IsReady() && target.IsValidTarget(Alistar.W.Range))
            {
                Alistar.W.Cast(target);
            }

            if (Alistar.Q.IsReady() && Helper.Player.IsDashing())
            {
                Alistar.Q.Cast();
            }

            if (Alistar.E.IsReady() && Helper.Player.ManaPercent >= 30)
            {
                var ally = Helper.TeamHeroes.FirstOrDefault(a => a.IsValidTarget(Alistar.E.Range));
                if (Helper.Player.HealthPercent <= 30 || ally.HealthPercent <= 30)
                {
                    Alistar.E.Cast();
                } 
            }

            if (Alistar.R.IsReady())
            {
                var ally = Helper.TeamHeroes.FirstOrDefault(a => a.IsValidTarget(600));
                if (target.CountEnemiesInRange(Alistar.E.Range) <= 2 && ally != null)
                {
                    Alistar.R.Cast();
                }
            }
        }
    }
}