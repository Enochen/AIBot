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
                            Routine.PrimaryBehaviour.Tick(null);

                            if (Routine.PrimaryBehaviour.LastStatus != RunStatus.Running)
                            {
                                Routine.PrimaryBehaviour.Stop(null);
                                Routine.PrimaryBehaviour.Start(null);
                            }
                        };

                    Drawing.OnDraw += delegate { Routine.OnDraw(); };
                };
        }
    }
}