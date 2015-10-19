namespace AiBuddy.AI
{
    using AiBuddy.AI.Maps.HowlingAbyss;
    using AiBuddy.Champions;

    using EloBuddy;

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
                    MapHandler.Init();
                    FindChampion.FindAndSetChampion();
                    break;
                default:
                    return;
            }
        }
    }
}