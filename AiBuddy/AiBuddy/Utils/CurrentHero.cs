using EloBuddy;

namespace AiBuddy.Utils
{
    class CurrentHero
    {
        private static readonly AIHeroClient Player = ObjectManager.Player;
        public static Champion Get()
        {
            return Player.Hero;
        }
    }
}
