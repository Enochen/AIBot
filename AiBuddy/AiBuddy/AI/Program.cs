namespace AiBuddy.AI
{
    using System;
    using System.Linq;
    using System.Reflection;

    using AiBuddy.AI.Automation;

    using EloBuddy;
    using EloBuddy.SDK.Events;

    using TreeSharp;

    internal class Program
    {
        private static GameRoutine Routine;

        private static int lastTick;

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += delegate
                {
                    foreach (var routine in
                        Assembly.GetExecutingAssembly()
                            .GetTypes()
                            .Where(p => typeof(GameRoutine).IsAssignableFrom(p) && !p.IsAbstract)
                            .Select(Activator.CreateInstance)
                            .OfType<GameRoutine>()
                            .Where(routine => Game.MapId == routine.MapId))
                    {
                        Routine = routine;

                        Routine.OnLoad();
                    }

                    Game.OnTick += delegate
                        {
                            if (Environment.TickCount - lastTick >= 200)
                            {
                                return;
                            }
                            lastTick = Environment.TickCount;
                            Routine.ShopBehaviour.Tick(null);
                            Routine.MoveBehaviour.Tick(null);
                            if (Routine.ShopBehaviour.LastStatus != RunStatus.Running)
                            {
                                Routine.ShopBehaviour.Stop(null);
                                Routine.ShopBehaviour.Start(null);
                            }
                            if (Routine.MoveBehaviour.LastStatus != RunStatus.Running)
                            {
                                Routine.MoveBehaviour.Stop(null);
                                Routine.MoveBehaviour.Start(null);
                            }
                        };

                    Drawing.OnDraw += delegate { Routine.OnDraw(); };
                };
        }
    }
}