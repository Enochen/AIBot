#region

using EloBuddy;
using EloBuddy.SDK;

#endregion

namespace AiBuddy.Champions.Corki.Modes
{
    internal class Combo
    {
        private static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static void Execute()
        {
            var target = Utils.GetTarget.Target(Corki.Q.Range, DamageType.Physical);
            if (target == null) return;

            if (Corki.Q.IsReady() && target.IsValidTarget(Corki.Q.Range))
            {
                Corki.Q.Cast(target);
            }

            if (Corki.E.IsReady() && target.IsValidTarget(Corki.E.Range) && _Player.IsFacing(target))
            {
                Corki.E.Cast();
            }

            if (Corki.R.IsReady())
            {
                var targetR = TargetSelector.GetTarget(Corki.R.Range, DamageType.Physical);
                if (targetR == null) return;

                if (target.IsValidTarget(Corki.R.Range) && _Player.ManaPercent >= 50)
                {
                    Corki.R.Cast(target);
                }
            }
        }
    }
}