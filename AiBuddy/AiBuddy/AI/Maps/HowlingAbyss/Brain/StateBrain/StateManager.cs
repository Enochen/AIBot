#region

using AiBuddy.Utils;
using EloBuddy;

#endregion

namespace AiBuddy.AI.Maps.HowlingAbyss.Brain.StateBrain
{
    internal class StateManager
    {
        public static void DoCombo()
        {
            var hero = CurrentHero.Get();
            switch (hero)
            {
                case Champion.Akali:
                {
                    Champions.Akali.Modes.Combo.Execute();
                    break;
                }
                case Champion.Caitlyn:
                {
                    Champions.Caitlyn.Modes.Combo.Execute();
                    break;
                }
                case Champion.Evelynn:
                {
                    Champions.Evelynn.Modes.Combo.Execute();
                    break;
                }
                case Champion.Ezreal:
                {
                    Champions.Ezreal.Modes.Combo.Execute();
                    break;
                }
                case Champion.Soraka:
                {
                    Champions.Soraka.Modes.Combo.Execute();
                    break;
                }
                case Champion.Teemo:
                {
                    Champions.Teemo.Modes.Combo.Execute();
                    break;
                }
                case Champion.Corki:
                {
                    Champions.Corki.Modes.Combo.Execute();
                    break;
                }
            }
        }
    }
}