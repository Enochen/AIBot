#region

using AiBuddy.Champions;
using EloBuddy;

#endregion

namespace AiBuddy.AI
{
    internal class InitBrain
    {
        // Load will be executed on Init.cs
        public static void Load()
        {
            //Parsing Maps
            switch (Game.MapId)
            {
                //Only ARAM is supported for now.
                case GameMapId.HowlingAbyss:
                    Maps.HowlingAbyss.MapHandler.Init();
                    FindChampion.FindAndSetChampion();
                    break;
                default:
                    return;
            }
        }
    }
}