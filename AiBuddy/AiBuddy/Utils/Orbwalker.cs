#region

using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

#endregion

namespace AiBuddy.Utils
{
    /// <summary>
    ///     Very Basic Orbwalker
    /// </summary>
    internal class MyOrbwalker
    {
        public delegate void AfterAttackEvenH(AttackableUnit unit, AttackableUnit target);

        public delegate void BeforeAttackEvenH(Orbwalker.PreAttackArgs args);

        public delegate void OnAttackEvenH(AttackableUnit unit, AttackableUnit target);

        public delegate void OnNonKillableMinionH(AttackableUnit minion);

        public delegate void OnTargetChangeH(AttackableUnit oldTarget, AttackableUnit newTarget);

        public enum OrbwalkingMode
        {
            LastHit,
            Mixed,
            LaneClear,
            Combo,
            None
        }
    }

    public class AIWalker
    {
        private static readonly AIHeroClient Player = ObjectManager.Player;
        public static List<Orbwalker.AttackHandler> Instances = new List<Orbwalker.AttackHandler>();
        private Obj_AI_Base _forcedTarget;
        private Vector3 _orbwalkingPoint;
        private Obj_AI_Minion _prevMinion;

        private static bool IsInAARange(AttackableUnit target)
        {
            return Player.IsInAutoAttackRange(target);
        }

        private static bool CanAttack(AttackableUnit targer)
        {
            if (Orbwalker.CanAutoAttack)
            {
                return true;
            }
            return false;
        }

        public void SetAttack(bool b)
        {
            Orbwalker.DisableAttacking = b;
        }

        public void SetMovement(bool b)
        {
            Orbwalker.DisableMovement = b;
        }

        public void ForceTarget(Obj_AI_Base target)
        {
            Orbwalker.ForcedTarget = target;
        }

        public void MoveTo(Vector3 pos)
        {
            Orbwalker.MoveTo(pos);
        }

        public Vector3 GetOrbwalkingPoint()
        {
            return _orbwalkingPoint;
        }

        public static Obj_AI_Base GetKillableMinion(IEnumerable<Obj_AI_Minion> minionlist, bool shouldwait)
        {
            return Orbwalker.GetLastHitMinion(minionlist, out shouldwait);
        }
    }
}