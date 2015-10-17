#region

using System;
using EloBuddy;
using EloBuddy.SDK.Events;

#endregion

namespace AiBuddy
{
    internal class Init
    {
        public static Utils.AIWalker AIWalker;
        private static void Main()
        {
            // Once loading is complete we will now call Initialize()
            Loading.OnLoadingComplete += Initialize;
        }

        private static void Initialize(EventArgs args)
        {
            // Champion Init
            //Switches between your current champion

            AIWalker = new Utils.AIWalker();
            AI.InitBrain.Load();
            
        }
    }
}