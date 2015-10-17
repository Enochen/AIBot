#region

using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

#endregion

namespace AiBuddy.AI.Maps.HowlingAbyss.Shop
{
    internal class ShopManager
    {
        private static ItemId lastPurchasedItem = ItemId.Ichor_of_Illumination;
        // We will be using that to remember the last purchased Item

        private static readonly List<Item> Items = new List<Item>(); // Current Item List

        public static void Load()
        {
            var n = ObjectManager.Player.ChampionName;
            switch (n)
            {
                case "Aatrox":
                    ShopList.List = ShopList.FIGHTER;
                    break;
                case "Ahri":
                    ShopList.List = ShopList.APC;
                    break;
                case "Akali":
                    ShopList.List = ShopList.Akali;
                    break;
                case "Alistar":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Amumu":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Anivia":
                    ShopList.List = ShopList.Anivia;
                    break;
                case "Annie":
                    ShopList.List = ShopList.APC;
                    break;
                case "Ashe":
                    ShopList.List = ShopList.ADC;
                    break;
                case "Azir":
                    ShopList.List = ShopList.Azir;
                    break;
                case "Bard":
                    ShopList.List = ShopList.SUP;
                    break;
                case "Blitzcrank":
                    ShopList.List = ShopList.SUP;
                    break;
                case "Brand":
                    ShopList.List = ShopList.APC;
                    break;
                case "Braum":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Caitlyn":
                    ShopList.List = ShopList.ADC;
                    break;
                case "Cassiopeia":
                    ShopList.List = ShopList.Cassiopeia;
                    break;
                case "ChoGath":
                    ShopList.List = ShopList.APC;
                    break;
                case "Corki":
                    ShopList.List = ShopList.ADC;
                    break;
                case "Darius":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Diana":
                    ShopList.List = ShopList.APC;
                    break;
                case "DrMundo":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Draven":
                    ShopList.List = ShopList.ADC;
                    break;
                case "Ekko":
                    ShopList.List = ShopList.APC;
                    break;
                case "Elise":
                    ShopList.List = ShopList.APC;
                    break;
                case "Evelynn":
                    ShopList.List = ShopList.APC;
                    break;
                case "Ezreal":
                    ShopList.List = ShopList.ADC;
                    break;
                case "Fiddlesticks":
                    ShopList.List = ShopList.APC;
                    break;
                case "Fiora":
                    ShopList.List = ShopList.FIGHTER;
                    break;
                case "Fizz":
                    ShopList.List = ShopList.APC;
                    break;
                case "Galio":
                    ShopList.List = ShopList.APC;
                    break;
                case "Gangplank":
                    ShopList.List = ShopList.ADC;
                    break;
                case "Garen":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Gnar":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Gragas":
                    ShopList.List = ShopList.APC;
                    break;
                case "Graves":
                    ShopList.List = ShopList.ADC;
                    break;
                case "Hecarim":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Heimerdinger":
                    ShopList.List = ShopList.APC;
                    break;
                case "Irelia":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Janna":
                    ShopList.List = ShopList.SUP;
                    break;
                case "Jarvan IV":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Jax":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Jayce":
                    ShopList.List = ShopList.ADC;
                    break;
                case "Jinx":
                    ShopList.List = ShopList.ADC;
                    break;
                case "Kalista":
                    ShopList.List = ShopList.ADC;
                    break;
                case "Karma":
                    ShopList.List = ShopList.SUP;
                    break;
                case "Kassadin":
                    ShopList.List = ShopList.Karthus;
                    break;
                case "Katarina":
                    ShopList.List = ShopList.Karthus;
                    break;
                case "Kayle":
                    ShopList.List = ShopList.Karthus;
                    break;
                case "Kennen":
                    ShopList.List = ShopList.Karthus;
                    break;
                case "KhaZix":
                    ShopList.List = ShopList.Karthus;
                    break;
                case "KogMaw":
                    ShopList.List = ShopList.Karthus;
                    break;
                case "LeBlanc":
                    ShopList.List = ShopList.Karthus;
                    break;
                case "LeeSin":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Leona":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Lissandra":
                    ShopList.List = ShopList.APC;
                    break;
                case "Lucian":
                    ShopList.List = ShopList.ADC;
                    break;
                case "Lulu":
                    ShopList.List = ShopList.SUP;
                    break;
                case "Lux":
                    ShopList.List = ShopList.APC;
                    break;
                case "Malphite":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Malzahar":
                    ShopList.List = ShopList.APC;
                    break;
                case "Maokai":
                    ShopList.List = ShopList.TANK;
                    break;
                case "MasterYi":
                    ShopList.List = ShopList.Karthus;
                    break;
                case "MissFortune":
                    ShopList.List = ShopList.Karthus;
                    break;
                case "Mordekaiser":
                    ShopList.List = ShopList.Karthus;
                    break;
                case "Morgana":
                    ShopList.List = ShopList.SUP;
                    break;
                case "Nami":
                    ShopList.List = ShopList.SUP;
                    break;
                case "Nasus":
                    ShopList.List = ShopList.FIGHTER;
                    break;
                case "Nautilus":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Nidalee":
                    ShopList.List = ShopList.APC;
                    break;
                case "Nocturne":
                    ShopList.List = ShopList.FIGHTER;
                    break;
                case "Nunu":
                    ShopList.List = ShopList.SUP;
                    break;
                case "Olaf":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Orianna":
                    ShopList.List = ShopList.APC;
                    break;
                case "Pantheon":
                    ShopList.List = ShopList.FIGHTER;
                    break;
                case "Poppy":
                    ShopList.List = ShopList.FIGHTER;
                    break;
                case "Quinn":
                    ShopList.List = ShopList.ADC;
                    break;
                case "Rammus":
                    ShopList.List = ShopList.TANK;
                    break;
                case "RekSai":
                    ShopList.List = ShopList.FIGHTER;
                    break;
                case "Renekton":
                    ShopList.List = ShopList.FIGHTER;
                    break;
                case "Rengar":
                    ShopList.List = ShopList.FIGHTER;
                    break;
                case "Riven":
                    ShopList.List = ShopList.FIGHTER;
                    break;
                case "Rumble":
                    ShopList.List = ShopList.APC;
                    break;
                case "Ryze":
                    ShopList.List = ShopList.APC;
                    break;
                case "Sejuani":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Shaco":
                    ShopList.List = ShopList.FIGHTER;
                    break;
                case "Shen":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Shyvana":
                    ShopList.List = ShopList.FIGHTER;
                    break;
                case "Singed":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Sion":
                    ShopList.List = ShopList.FIGHTER;
                    break;
                case "Sivir":
                    ShopList.List = ShopList.ADC;
                    break;
                case "Skarner":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Sona":
                    ShopList.List = ShopList.SUP;
                    break;
                case "Soraka":
                    ShopList.List = ShopList.SUP;
                    break;
                case "Swain":
                    ShopList.List = ShopList.APC;
                    break;
                case "Syndra":
                    ShopList.List = ShopList.APC;
                    break;
                case "TahmKench":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Talon":
                    ShopList.List = ShopList.FIGHTER;
                    break;
                case "Taric":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Teemo":
                    ShopList.List = ShopList.APC;
                    break;
                case "Thresh":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Tristana":
                    ShopList.List = ShopList.ADC;
                    break;
                case "Trundle":
                    ShopList.List = ShopList.FIGHTER;
                    break;
                case "Tryndamere":
                    ShopList.List = ShopList.FIGHTER;
                    break;
                case "Twisted Fate":
                    ShopList.List = ShopList.APC;
                    break;
                case "Twitch":
                    ShopList.List = ShopList.ADC;
                    break;
                case "Udyr":
                    ShopList.List = ShopList.FIGHTER;
                    break;
                case "Urgot":
                    ShopList.List = ShopList.ADC;
                    break;
                case "Varus":
                    ShopList.List = ShopList.ADC;
                    break;
                case "Vayne":
                    ShopList.List = ShopList.ADC;
                    break;
                case "Veigar":
                    ShopList.List = ShopList.APC;
                    break;
                case "VelKoz":
                    ShopList.List = ShopList.APC;
                    break;
                case "Vi":
                    ShopList.List = ShopList.FIGHTER;
                    break;
                case "Viktor":
                    ShopList.List = ShopList.APC;
                    break;
                case "Vladimir":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Volibear":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Warwick":
                    ShopList.List = ShopList.FIGHTER;
                    break;
                case "Wukong":
                    ShopList.List = ShopList.FIGHTER;
                    break;
                case "Xerath":
                    ShopList.List = ShopList.APC;
                    break;
                case "XinZhao":
                    ShopList.List = ShopList.FIGHTER;
                    break;
                case "Yasuo":
                    ShopList.List = ShopList.ADC;
                    break;
                case "Yorick":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Zac":
                    ShopList.List = ShopList.TANK;
                    break;
                case "Zed":
                    ShopList.List = ShopList.ADC;
                    break;
                case "Ziggs":
                    ShopList.List = ShopList.APC;
                    break;
                case "Zilean":
                    ShopList.List = ShopList.SUP;
                    break;
                case "Zyra":
                    ShopList.List = ShopList.SUP;
                    break;
                default:
                    return;
            }
        }

        /*
        Buy Item
        */

        public static void BuyItem()
        {
            if (!Player.Instance.IsInShopRange() || !Player.Instance.IsDead)
            {
                return;
            }
            if (lastPurchasedItem == ItemId.Ichor_of_Illumination &&
                new ItemData((uint) ShopList.List.First()).BasePrice <= Player.Instance.Gold)
            {
                EloBuddy.Shop.BuyItem(ShopList.List.First());
                lastPurchasedItem = ShopList.List.First();
            }
            for (var x = 0; x < ShopList.List.Length; x++)
            {
                var data = new ItemData((uint) (ShopList.List[x + 1]));
                if (lastPurchasedItem != ShopList.List[x] || !(data.BasePrice <= Player.Instance.Gold))
                {
                    continue;
                }
                EloBuddy.Shop.BuyItem(ShopList.List[x*1]);
                lastPurchasedItem = ShopList.List[x + 1];
            }
        }
    }
}