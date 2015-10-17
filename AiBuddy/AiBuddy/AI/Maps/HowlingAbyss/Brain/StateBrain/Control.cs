using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace AiBuddy.AI.Maps.HowlingAbyss.Brain.StateBrain
{
    internal class Control
    {
        private static readonly AIHeroClient Me = ObjectManager.Player;

        public static bool Heal()
        {
            var HealBuff =
                ObjectManager.Get<GameObject>()
                    .Where(a => a.Name.ToLower().Contains("healingbuff"))
                    .OrderBy(a => Player.Instance.Position.Distance(a.Position))
                    .First();

            if (Me.IsDead || Me.HealthPercent < 65 || !HealBuff.IsValid) return false;

            if (Me.Position.Distance(HealBuff.Position) <= 600 && Me.CountEnemiesInRange(800) < 0 || (Me.Position.Distance(HealBuff.Position) <= 600 && ComboWinPrediction.Calculate() > 55))
            {
                Player.IssueOrder(GameObjectOrder.MoveTo, HealBuff.Position);
                return true;
            }

            return false;
        }

        public static bool NoAlly()
        {
            if (EntityManager.Heroes.Allies.All(a => a.IsDead))
            {
                var turret =
                    ObjectManager.Get<Obj_Turret>()
                        .Where(a => a.IsValid)
                        .OrderBy(a => Player.Instance.Position.Distance(a.Position))
                        .FirstOrDefault();
                Player.IssueOrder(GameObjectOrder.MoveTo, turret.Position);
                return true;
            }
            return false;
        }
    }
}