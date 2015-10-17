namespace AiBuddy.Champions.Blitzcrank.Utils
{
    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Constants;

    internal class Misc
    {
        public static void OnSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe)
            {
                return;
            }
            //Attack After Q
            if (args.Slot == SpellSlot.Q)
            {
                Player.IssueOrder(GameObjectOrder.AttackUnit, args.Target);
            }
            //Attack After E
            if (args.Slot == SpellSlot.E)
            {
                Player.IssueOrder(
                    GameObjectOrder.AttackUnit,
                    TargetSelector.GetTarget(Player.Instance.GetAutoAttackRange(), DamageType.Physical));
            }
            //E After Attack
            if (args.SData.IsAutoAttack() && Blitzcrank.E.IsReady()
                && TargetSelector.GetTarget(Player.Instance.GetAutoAttackRange(), DamageType.Physical)
                       .IsValidTarget())
            {
                Blitzcrank.E.Cast();
            }
        }
    }
}