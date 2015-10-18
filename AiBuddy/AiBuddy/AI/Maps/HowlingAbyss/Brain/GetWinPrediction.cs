#region

using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

#endregion

namespace AiBuddy.AI.Maps.HowlingAbyss.Brain
{
    #region ComboWinPred
    [Obsolete("GetComboWinPrediction will soon become obsolete", false)]
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
    #endregion
    #region TeamFightWinPred

    internal class GetTeamFightWinPred
    {
        private static IEnumerable<AIHeroClient> AllyTeam = GetHeroes.TeamHeroes;
        private static IEnumerable<AIHeroClient> EnemyTeam = GetHeroes.EnemyHeroes;

        private class HealthPercent
        {
            #region AllyHealthPercent

            private static float AlyHealth1()
            {
                var h1 = AllyTeam.FirstOrDefault();
                if (h1 != null && h1.IsValid)
                {
                    return h1.HealthPercent/10;
                }
                return 0;
            }

            private static float AllyHealth2()
            {
                var h2 = AllyTeam.ElementAt(2);
                if (h2 != null && h2.IsValid)
                {
                    return h2.HealthPercent/10;
                }
                return 0;
            }

            private static float AllyHealth3()
            {
                var h3 = AllyTeam.ElementAt(3);
                if (h3 != null && h3.IsValid)
                {
                    return h3.HealthPercent/10;
                }
                return 0;
            }

            private static float AllyHealth4()
            {
                var h4 = AllyTeam.ElementAt(4);
                if (h4 != null && h4.IsValid)
                {
                    return h4.HealthPercent/10;
                }
                return 0;
            }

            private static float AllyHealth5()
            {
                var h5 = AllyTeam.ElementAt(4);
                if (h5 != null && h5.IsValid)
                {
                    return h5.HealthPercent/10;
                }
                return 0;
            }

            #endregion
            #region EnemyHealthPercent

            private static float EnemyHealth1()
            {
                var h1 = EnemyTeam.FirstOrDefault();
                if (h1 != null && h1.IsValid)
                {
                    return h1.HealthPercent/10;
                }
                return 0;
            }

            private static float EnemyHealth2()
            {
                var h2 = EnemyTeam.ElementAt(2);
                if (h2 != null && h2.IsValid)
                {
                    return h2.HealthPercent/10;
                }
                return 0;
            }

            private static float EnemyHealth3()
            {
                var h3 = EnemyTeam.ElementAt(3);
                if (h3 != null && h3.IsValid)
                {
                    return h3.HealthPercent/10;
                }
                return 0;
            }

            private static float EnemyHealth4()
            {
                var h4 = EnemyTeam.ElementAt(4);
                if (h4 != null && h4.IsValid)
                {
                    return h4.HealthPercent/10;
                }
                return 0;
            }

            private static float EnemyHealth5()
            {
                var h5 = EnemyTeam.ElementAt(5);
                if (h5 != null && h5.IsValid)
                {
                    return h5.HealthPercent/10;
                }
                return 0;
            }

            #endregion
        }
        private class Levels
        {
            #region AllyLevels

            private static float AllyLevel1()
            {
                var h = AllyTeam.FirstOrDefault();
                if (h != null && h.IsValid)
                {
                    return h.Level;
                }
                return 0;
            }

            private static float AllyLevel2()
            {
                var h = AllyTeam.ElementAt(2);
                if (h != null && h.IsValid)
                {
                    return h.Level;
                }
                return 0;
            }

            private static float AllyLevel3()
            {
                var h = AllyTeam.ElementAt(3);
                if (h != null && h.IsValid)
                {
                    return h.Level;
                }
                return 0;
            }

            private static float AllyLevel4()
            {
                var h = AllyTeam.ElementAt(4);
                if (h != null && h.IsValid)
                {
                    return h.Level;
                }
                return 0;
            }

            private static float AllyLevel5()
            {
                var h = AllyTeam.ElementAt(5);
                if (h != null && h.IsValid)
                {
                    return h.Level;
                }
                return 0;
            }

            #endregion
            #region EnemyLevels

            private static float EnemyLevel1()
            {
                var h = EnemyTeam.FirstOrDefault();
                if (h != null && h.IsValid)
                {
                    return h.Level;
                }
                return 0;
            }

            private static float EnemyLevel2()
            {
                var h = EnemyTeam.ElementAt(2);
                if (h != null && h.IsValid)
                {
                    return h.Level;
                }
                return 0;
            }

            private static float EnemyLevel3()
            {
                var h = EnemyTeam.ElementAt(3);
                if (h != null && h.IsValid)
                {
                    return h.Level;
                }
                return 0;
            }

            private static float EnemyLevel4()
            {
                var h = EnemyTeam.ElementAt(4);
                if (h != null && h.IsValid)
                {
                    return h.Level;
                }
                return 0;
            }

            private static float EnemyLevel5()
            {
                var h = EnemyTeam.ElementAt(5);
                if (h != null && h.IsValid)
                {
                    return h.Level;
                }
                return 0;
            }

            #endregion
        }
        private class Dead
        {
            #region AllyDead

            private static bool AllyDead1()
            {
                var h = AllyTeam.FirstOrDefault();
                if (h != null && h.IsValid)
                {
                    if (h.IsDead)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }

            private static bool AllyDead2()
            {
                var h = AllyTeam.ElementAt(2);
                if (h != null && h.IsValid)
                {
                    if (h.IsDead)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }

            private static bool AllyDead3()
            {
                var h = AllyTeam.ElementAt(3);
                if (h != null && h.IsValid)
                {
                    if (h.IsDead)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }

            private static bool AllyDead4()
            {
                var h = AllyTeam.ElementAt(4);
                if (h != null && h.IsValid)
                {
                    if (h.IsDead)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }

            private static bool AllyDead5()
            {
                var h = AllyTeam.ElementAt(5);
                if (h != null && h.IsValid)
                {
                    if (h.IsDead)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }

            #endregion
            #region EnemyDead

            private static bool EnemyDead1()
            {
                var h = EnemyTeam.FirstOrDefault();
                if (h != null && h.IsValid)
                {
                    if (h.IsDead)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }

            private static bool EnemyDead2()
            {
                var h = EnemyTeam.ElementAt(2);
                if (h != null && h.IsValid)
                {
                    if (h.IsDead)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }

            private static bool EnemyDead3()
            {
                var h = EnemyTeam.ElementAt(3);
                if (h != null && h.IsValid)
                {
                    if (h.IsDead)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }

            private static bool EnemyDead4()
            {
                var h = EnemyTeam.ElementAt(4);
                if (h != null && h.IsValid)
                {
                    if (h.IsDead)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }

            private static bool EnemyDead5()
            {
                var h = EnemyTeam.ElementAt(5);
                if (h != null && h.IsValid)
                {
                    if (h.IsDead)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }

            #endregion
        }
        private class Kills
        {
            #region AllyKills

            private static float AllyKills1()
            {
                var h = AllyTeam.FirstOrDefault();
                if (h != null && h.IsValid)
                {
                    return h.ChampionsKilled;
                }
                return 0;
            }

            private static float AllyKills2()
            {
                var h = AllyTeam.ElementAt(2);
                if (h != null && h.IsValid)
                {
                    return h.ChampionsKilled;
                }
                return 0;
            }

            private static float AllyKills3()
            {
                var h = AllyTeam.ElementAt(3);
                if (h != null && h.IsValid)
                {
                    return h.ChampionsKilled;
                }
                return 0;
            }

            private static float AllyKills4()
            {
                var h = AllyTeam.ElementAt(4);
                if (h != null && h.IsValid)
                {
                    return h.ChampionsKilled;
                }
                return 0;
            }

            private static float AllyKills5()
            {
                var h = AllyTeam.ElementAt(5);
                if (h != null && h.IsValid)
                {
                    return h.ChampionsKilled;
                }
                return 0;
            }

            #endregion
            #region EnemyKills

            private static float EnemyKills1()
            {
                var h = EnemyTeam.FirstOrDefault();
                if (h != null && h.IsValid)
                {
                    return h.ChampionsKilled;
                }
                return 0;
            }

            private static float EnemyKills2()
            {
                var h = EnemyTeam.ElementAt(2);
                if (h != null && h.IsValid)
                {
                    return h.ChampionsKilled;
                }
                return 0;
            }

            private static float EnemyKills3()
            {
                var h = EnemyTeam.ElementAt(3);
                if (h != null && h.IsValid)
                {
                    return h.ChampionsKilled;
                }
                return 0;
            }

            private static float EnemyKills4()
            {
                var h = EnemyTeam.ElementAt(4);
                if (h != null && h.IsValid)
                {
                    return h.ChampionsKilled;
                }
                return 0;
            }

            private static float EnemyKills5()
            {
                var h = EnemyTeam.ElementAt(5);
                if (h != null && h.IsValid)
                {
                    return h.ChampionsKilled;
                }
                return 0;
            }

            #endregion
        }
        private class Deaths
        {
            #region AllyDeaths

            private static float AllyDeaths1()
            {
                var h = AllyTeam.FirstOrDefault();
                if (h != null && h.IsValid)
                {
                    return h.Deaths;
                }
                return 0;
            }

            private static float AllyDeaths2()
            {
                var h = AllyTeam.ElementAt(2);
                if (h != null && h.IsValid)
                {
                    return h.Deaths;
                }
                return 0;
            }

            private static float AllyDeaths3()
            {
                var h = AllyTeam.ElementAt(3);
                if (h != null && h.IsValid)
                {
                    return h.Deaths;
                }
                return 0;
            }

            private static float AllyDeaths4()
            {
                var h = AllyTeam.ElementAt(4);
                if (h != null && h.IsValid)
                {
                    return h.Deaths;
                }
                return 0;
            }

            private static float AllyDeaths5()
            {
                var h = AllyTeam.ElementAt(5);
                if (h != null && h.IsValid)
                {
                    return h.Deaths;
                }
                return 0;
            }

            #endregion
            #region EnemyDeaths

            private static float EnemyDeaths1()
            {
                var h = EnemyTeam.FirstOrDefault();
                if (h != null && h.IsValid)
                {
                    return h.Deaths;
                }
                return 0;
            }

            private static float EnemyDeaths2()
            {
                var h = EnemyTeam.ElementAt(2);
                if (h != null && h.IsValid)
                {
                    return h.Deaths;
                }
                return 0;
            }

            private static float EnemyDeaths3()
            {
                var h = EnemyTeam.ElementAt(3);
                if (h != null && h.IsValid)
                {
                    return h.Deaths;
                }
                return 0;
            }

            private static float EnemyDeaths4()
            {
                var h = EnemyTeam.ElementAt(4);
                if (h != null && h.IsValid)
                {
                    return h.Deaths;
                }
                return 0;
            }

            private static float EnemyDeaths5()
            {
                var h = EnemyTeam.ElementAt(5);
                if (h != null && h.IsValid)
                {
                    return h.Deaths;
                }
                return 0;
            }

            #endregion
        }
        private class Assists
        {
            #region AllyAssists

            private static float AllyAssists1()
            {
                var h = AllyTeam.FirstOrDefault();
                if (h != null && h.IsValid)
                {
                    return h.Assists;
                }
                return 0;
            }

            private static float AllyAssists2()
            {
                var h = AllyTeam.ElementAt(2);
                if (h != null && h.IsValid)
                {
                    return h.Deaths;
                }
                return 0;
            }

            private static float AllyAssists3()
            {
                var h = AllyTeam.ElementAt(3);
                if (h != null && h.IsValid)
                {
                    return h.Assists;
                }
                return 0;
            }

            private static float AllyAssists4()
            {
                var h = AllyTeam.ElementAt(4);
                if (h != null && h.IsValid)
                {
                    return h.Assists;
                }
                return 0;
            }

            private static float AllyAssists5()
            {
                var h = AllyTeam.ElementAt(5);
                if (h != null && h.IsValid)
                {
                    return h.Assists;
                }
                return 0;
            }

            #endregion
            #region EnemyAssists

            private static float EnemyAssists1()
            {
                var h = EnemyTeam.FirstOrDefault();
                if (h != null && h.IsValid)
                {
                    return h.Assists;
                }
                return 0;
            }

            private static float EnemyAssists2()
            {
                var h = EnemyTeam.ElementAt(2);
                if (h != null && h.IsValid)
                {
                    return h.Assists;
                }
                return 0;
            }

            private static float EnemyAssists3()
            {
                var h = EnemyTeam.ElementAt(3);
                if (h != null && h.IsValid)
                {
                    return h.Assists;
                }
                return 0;
            }

            private static float EnemyAssists4()
            {
                var h = EnemyTeam.ElementAt(4);
                if (h != null && h.IsValid)
                {
                    return h.Assists;
                }
                return 0;
            }

            private static float EnemyAssists5()
            {
                var h = EnemyTeam.ElementAt(5);
                if (h != null && h.IsValid)
                {
                    return h.Assists;
                }
                return 0;
            }

            #endregion
        }
        private class Champions
        {
            #region AllyChampions

            private static Champion AllyGet(int index)
            {
                var h = AllyTeam.ElementAt(index);
                return h.Hero;
            }
            #endregion
            #region EnemyChampions

            private static Champion EnemyGet(int index)
            {
                var h = EnemyTeam.ElementAt(index);
                return h.Hero;
            }
            #endregion
        }
    }
    #endregion
}