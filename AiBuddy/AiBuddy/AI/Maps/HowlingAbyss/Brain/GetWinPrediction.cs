#region

using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

#endregion

namespace AiBuddy.AI.Maps.HowlingAbyss.Brain
{
    internal class ComboWinPrediction
    {
        private static readonly AIHeroClient PlayerClient = ObjectManager.Player;

        /// <summary>
        ///     Win Prexdiction BETA - Predicts win chance.
        /// </summary>
        /// <returns>1-100 Depending on Win Chance</returns>
        private static float GetLevelMatchup()
        {
            var clientlvl = PlayerClient.Level;
            var combotarget = Utils.GetTarget.GetComboTarget();

            if (combotarget != null || combotarget.IsValid)

            {
                if (clientlvl >= 1 && clientlvl <= 6)
                {
                    if (combotarget.Level >= 1 && combotarget.Level <= 6)
                    {
                        return 1;
                    }
                }

                if (clientlvl >= 7 && clientlvl <= 11)
                {
                    if (combotarget.Level >= 7 && combotarget.Level <= 11)
                    {
                        return 1;
                    }
                }

                if (clientlvl >= 12 && clientlvl <= 16)
                {
                    if (combotarget.Level >= 1 && combotarget.Level <= 6)
                    {
                        return 1;
                    }
                }

                if (clientlvl >= 17 && clientlvl <= 18)
                {
                    if (combotarget.Level >= 17 && combotarget.Level <= 18)
                    {
                        return 1;
                    }
                }

                if (clientlvl == combotarget.Level)
                {
                    return 2;
                }

                if (clientlvl - 3 > combotarget.Level)
                {
                    return 4;
                }

                if (clientlvl - 6 > combotarget.Level)
                {
                    return 5;
                }

                if (clientlvl > combotarget.Level)
                {
                    return 3;
                }
            }
            return 0;
        }

        private static float GetAllyCountMatchup()
        {
            var team = GetHeroes.AliveTeamHeroes
                .Where(h => h.IsInRange(PlayerClient, 1500));

            //var enemyteam = GetHeroes.AliveEnemyHeroes
            //.Where(h => h.IsInRange(_playerClient, 1500));

            if (team
                .Count(h => !h.IsMe) == 1)
            {
                return 1;
            }

            if (team
                .Count(h => !h.IsMe) == 2)
            {
                return 2;
            }

            if (team
                .Count(h => !h.IsMe) == 3)
            {
                return 3;
            }

            if (team
                .Count(h => !h.IsMe) == 4)
            {
                return 4;
            }

            if (team
                .Count(h => !h.IsMe) == 5)
            {
                return 5;
            }

            return 1;
        }

        private static float GetEnemyCountMatchup()
        {
            var team = GetHeroes.AliveEnemyHeroes
                .Where(h => h.IsInRange(PlayerClient, 1500));

            //var enemyteam = GetHeroes.AliveEnemyHeroes
            //.Where(h => h.IsInRange(_playerClient, 1500));

            if (team
                .Count(h => !h.IsMe) == 1)
            {
                return 5;
            }

            if (team
                .Count(h => !h.IsMe) == 2)
            {
                return 4;
            }

            if (team
                .Count(h => !h.IsMe) == 3)
            {
                return 3;
            }

            if (team
                .Count(h => !h.IsMe) == 4)
            {
                return 2;
            }

            if (team
                .Count(h => !h.IsMe) == 5)
            {
                return 1;
            }

            return 1;
        }

        public static float Calculate()
        {
            var teamcount = GetAllyCountMatchup();
            var enemycount = GetEnemyCountMatchup();
            var levelmatchup = GetLevelMatchup();
            var rating = 0f;

            if (PlayerClient.IsDead)
            {
                return 0;
            }

            rating = (teamcount*7) + (enemycount*7) + (levelmatchup*9);

            if (rating > 100)
            {
                return 100;
            }

            return 0;
        }
    }
}