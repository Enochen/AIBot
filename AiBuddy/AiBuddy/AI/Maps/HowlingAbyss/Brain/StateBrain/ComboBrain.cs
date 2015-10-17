using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EloBuddy;

using AiBuddy.Utils;
using EloBuddy.SDK;

namespace AiBuddy.AI.Maps.HowlingAbyss.Brain.StateBrain
{
    //Not finished yet - Logic is like 5/100 xd
    class ComboBrain
    {
        /// <summary>
        /// Manages wheather or not to do combo
        /// </summary>
        public static bool IsDoingCombo;
        private static AIHeroClient _PlayerClient = ObjectManager.Player;
        public static void Init()
        {
            var target = GetTarget.GetComboTarget();
            var turrets = EntityManager.Turrets.Enemies;

            foreach (var turret in turrets)
            {
                if (target != null && target.IsValid && !target.IsDead && !target.IsInAutoAttackRange(turret) &&
                    target.Level <= _PlayerClient.Level && ComboWinPrediction.Calculate() > 65)
                {
                    //ComboWinPrediction still in Beta, and more logic needs to be taken in such as items, gold, position, etc.
                    IsDoingCombo = true;
                    StateManager.DoCombo();
                }

                else
                {
                    IsDoingCombo = false;
                }
            }

            Game.OnTick += Combo_ComboTickArgs;
        }

        private static void Combo_ComboTickArgs(EventArgs args)
        {
            if (IsDoingCombo)
            {
                FollowBot.FollowBot.ShouldFollow = false;
            }
        }
    }
}
