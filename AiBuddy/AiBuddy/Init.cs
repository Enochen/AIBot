#region

using System;
using EloBuddy.SDK.Events;

#endregion

namespace AiBuddy
{
    using System.Linq;
    using System.Reflection;

    using AiBuddy.AI.Automation;

    using EloBuddy;

    using TreeSharp;

    internal class Init
    {
        //public static Utils.AIWalker AIWalker;

        //private static void Main()
        //{
        //    // Once loading is complete we will now call Initialize()
        //    Loading.OnLoadingComplete += Initialize;
        //}

        //private static void Initialize(EventArgs args)
        //{
        //    // Champion Init
        //    //Switches between your current champion

        //    AIWalker = new Utils.AIWalker();
        //    AI.InitBrain.Load();
        //}

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

                    if (Routine.PrimaryBehaviour.LastStatus == RunStatus.Running)
                    {
                        return;
                    }
                    Routine.PrimaryBehaviour.Stop(null);
                    Routine.PrimaryBehaviour.Start(null);
                };

                Drawing.OnDraw += delegate { Routine.OnDraw(); };
            };
        }
    }
}