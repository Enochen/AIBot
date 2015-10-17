#region

#endregion

namespace AiBuddy.Champions.Ryze.Modes
{
    using AiBuddy.Utils;

    using EloBuddy;
    using EloBuddy.SDK;

    internal class Combo
    {
        public static void Execute()
        {
            var target = GetTarget.GetComboTarget();
            if (target == null)
            {
                return;
            }

            var canKill = Player.Instance.CalculateDamageOnUnit(
                target,
                DamageType.Magical,
                Player.Instance.GetSpellDamage(target, SpellSlot.Q))
                          + Player.Instance.CalculateDamageOnUnit(
                              target,
                              DamageType.Magical,
                              Player.Instance.GetSpellDamage(target, SpellSlot.E)) > target.Health;


            if (Ryze.Q.IsReady() && target.IsValidTarget(Ryze.Q.Range))
            {
                Ryze.Q.Cast(Ryze.Q.GetPrediction(target).UnitPosition);
            }
            if (Ryze.W.IsReady() && target.IsValidTarget(Ryze.W.Range))
            {
                Ryze.W.Cast(target);
            }
            if (Ryze.E.IsReady() && target.IsValidTarget(Ryze.E.Range))
            {
                Ryze.E.Cast(target);
            }
            if (Ryze.R.IsReady() && target.IsValidTarget(Ryze.W.Range)
                && ((Player.Instance.GetBuffCount("RyzePassiveStack") > 3
                     || Player.Instance.HasBuff("ryzepassivecharged")) || canKill))
            {
                Ryze.R.Cast();
            }
        }
    }
}