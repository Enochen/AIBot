#region

using EloBuddy;

#endregion

namespace AiBuddy.Utils
{
    internal class CurrentHero
    {
        private static readonly AIHeroClient Player = ObjectManager.Player;

        public static Champion Get()
        {
            return Player.Hero;
        }
    }
}