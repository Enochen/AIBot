
    using System;
    using System.Linq;

    using EloBuddy;
    using EloBuddy.SDK;

namespace AiBuddy.AI.Maps.HowlingAbyss.Brain.FollowBot
{
    internal class FollowBot
    {
        public static bool ShouldFollow = true;

        public static void AutoFollow()
        {
            Game.OnTick += Game_OnTick;
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (ShouldFollow == false)
                return;

            var team = GetHeroes.AliveTeamHeroes;

            foreach (var hero in team)
            {
                Init.AIWalker.MoveTo(hero.Position);
            }
        }
    }
}
