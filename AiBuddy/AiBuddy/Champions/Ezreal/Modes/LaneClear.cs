#region

using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

#endregion

namespace AiBuddy.Champions.Ezreal.Modes
{
    internal class LaneClear
    {
        public static void Execute()
        {
            Obj_AI_Base target =
                EntityManager.MinionsAndMonsters.EnemyMinions.Where(
                    minion =>
                        minion.Health <= Player.Instance.GetSpellDamage(minion, SpellSlot.Q) && !minion.IsInvulnerable
                        && minion.IsValidTarget(Ezreal.Q.Range))
                    .OrderByDescending(minion => minion.HealthPercent)
                    .First();

            if (Ezreal.Q.IsReady() && Ezreal.Q.GetPrediction(target).HitChance > HitChance.Low)
            {
                Ezreal.Q.Cast(target);
            }
        }
    }
}