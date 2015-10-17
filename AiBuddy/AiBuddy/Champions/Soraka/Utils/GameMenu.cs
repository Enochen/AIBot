#region

using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

#endregion

namespace AiBuddy.Champions.Soraka.Utils
{
    internal class GameMenu
    {
        public static Menu Menu, ComboMenu;
        public static readonly AIHeroClient Me = ObjectManager.Player;

        public static void Initialize()
        {
            Menu = MainMenu.AddMenu("AI" + Me.ChampionName, Me.ChampionName.ToLower());
            Menu.AddLabel("AI" + Me.ChampionName);
            Menu.AddLabel("something something Darakath");

            ComboMenu = Menu.AddSubMenu("Combo", "combo");
            ComboMenu.AddLabel("Combo Settings");
            ComboMenu.Add("useQ", new CheckBox("Use Q"));
            ComboMenu.Add("useW", new CheckBox("Use W"));
            ComboMenu.Add("useE", new CheckBox("Use E"));
            ComboMenu.Add("useR", new CheckBox("Use R"));
        }
    }
}