#region

using EloBuddy;
using EloBuddy.SDK.Events;

#endregion

namespace AiBuddy.Champions.Soraka.Utils
{
    internal class Misc
    {
        public static void OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {
            if (sender.IsEnemy && !sender.IsMinion && args.EndTime > Soraka.E.CastDelay)
            {
                Soraka.E.Cast(sender);
            }
        }
    }
}