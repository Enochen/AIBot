#region

using AiBuddy.AI.Maps.HowlingAbyss.Shop;
using EloBuddy;

#endregion

namespace AiBuddy.AI.Maps.HowlingAbyss
{
    internal class MapHandler
    {
        public static void Init()
        {
            Game.OnUpdate += Brain.Brain.OnUpdate;
            ShopManager.Load();
        }
    }
}