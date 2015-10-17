namespace AiBuddy.Champions
{
    using System;
    using System.Runtime.CompilerServices;

    using EloBuddy;

    internal class FindChampion
    {
        public static void FindAndSetChampion()
        {
            switch (ObjectManager.Player.Hero)
            {
                case Champion.Akali:
                    Akali.Akali.Initialize();
                    break;
                case Champion.Soraka:
                    Soraka.Soraka.Initialize();
                    break;
                case Champion.Caitlyn:
                    Caitlyn.Caitlyn.Initialize();
                    break;
                case Champion.Corki:
                    Corki.Corki.Initialize();
                    break;
                case Champion.Evelynn:
                    Evelynn.Evelynn.Initialize();
                    break;
                case Champion.Ezreal:
                    Ezreal.Ezreal.Initialize();
                    break;
                case Champion.Teemo:
                    Teemo.Teemo.Initialize();
                    break;
                case Champion.Aatrox:
                    Aatrox.Aatrox.Initialize();
                    break;
                case Champion.Ahri:
                    break;
                case Champion.Alistar:
                    break;
                case Champion.Amumu:
                    Amumu.Amumu.Initialize();
                    break;
                case Champion.Anivia:
                    break;
                case Champion.Annie:
                    Annie.Annie.Initialize();
                    break;
                case Champion.Ashe:
                    break;
                case Champion.Azir:
                    break;
                case Champion.Bard:
                    break;
                case Champion.Blitzcrank:
                    Blitzcrank.Blitzcrank.Initialize();
                    break;
                case Champion.Brand:
                    break;
                case Champion.Braum:
                    break;
                case Champion.Cassiopeia:
                    break;
                case Champion.Chogath:
                    break;
                case Champion.Darius:
                    break;
                case Champion.Diana:
                    break;
                case Champion.DrMundo:
                    DrMundo.DrMundo.Initialize();
                    break;
                case Champion.Draven:
                    break;
                case Champion.Ekko:
                    break;
                case Champion.Elise:
                    break;
                case Champion.FiddleSticks:
                    break;
                case Champion.Fiora:
                    break;
                case Champion.Fizz:
                    break;
                case Champion.Galio:
                    break;
                case Champion.Gangplank:
                    break;
                case Champion.Garen:
                    break;
                case Champion.Gnar:
                    break;
                case Champion.Gragas:
                    break;
                case Champion.Graves:
                    break;
                case Champion.Hecarim:
                    break;
                case Champion.Heimerdinger:
                    break;
                case Champion.Irelia:
                    break;
                case Champion.Janna:
                    break;
                case Champion.JarvanIV:
                    break;
                case Champion.Jax:
                    break;
                case Champion.Jayce:
                    break;
                case Champion.Jinx:
                    break;
                case Champion.Kalista:
                    break;
                case Champion.Karma:
                    break;
                case Champion.Karthus:
                    break;
                case Champion.Kassadin:
                    break;
                case Champion.Katarina:
                    break;
                case Champion.Kayle:
                    break;
                case Champion.Kennen:
                    break;
                case Champion.Khazix:
                    break;
                case Champion.Kindred:
                    break;
                case Champion.KogMaw:
                    break;
                case Champion.Leblanc:
                    break;
                case Champion.LeeSin:
                    break;
                case Champion.Leona:
                    break;
                case Champion.Lissandra:
                    break;
                case Champion.Lucian:
                    break;
                case Champion.Lulu:
                    break;
                case Champion.Lux:
                    break;
                case Champion.Malphite:
                    break;
                case Champion.Malzahar:
                    break;
                case Champion.Maokai:
                    break;
                case Champion.MasterYi:
                    break;
                case Champion.MissFortune:
                    break;
                case Champion.Mordekaiser:
                    break;
                case Champion.Morgana:
                    break;
                case Champion.Nami:
                    break;
                case Champion.Nasus:
                    break;
                case Champion.Nautilus:
                    break;
                case Champion.Nidalee:
                    break;
                case Champion.Nocturne:
                    break;
                case Champion.Nunu:
                    break;
                case Champion.Olaf:
                    break;
                case Champion.Orianna:
                    break;
                case Champion.Pantheon:
                    break;
                case Champion.Poppy:
                    break;
                case Champion.Quinn:
                    break;
                case Champion.Rammus:
                    break;
                case Champion.RekSai:
                    break;
                case Champion.Renekton:
                    break;
                case Champion.Rengar:
                    break;
                case Champion.Riven:
                    break;
                case Champion.Rumble:
                    break;
                case Champion.Ryze:
                    break;
                case Champion.Sejuani:
                    break;
                case Champion.Shaco:
                    break;
                case Champion.Shen:
                    break;
                case Champion.Shyvana:
                    break;
                case Champion.Singed:
                    break;
                case Champion.Sion:
                    break;
                case Champion.Sivir:
                    break;
                case Champion.Skarner:
                    break;
                case Champion.Sona:
                    break;
                case Champion.Swain:
                    break;
                case Champion.Syndra:
                    break;
                case Champion.TahmKench:
                    break;
                case Champion.Talon:
                    break;
                case Champion.Taric:
                    break;
                case Champion.Thresh:
                    break;
                case Champion.Tristana:
                    break;
                case Champion.Trundle:
                    break;
                case Champion.Tryndamere:
                    break;
                case Champion.TwistedFate:
                    break;
                case Champion.Twitch:
                    break;
                case Champion.Udyr:
                    break;
                case Champion.Urgot:
                    break;
                case Champion.Varus:
                    break;
                case Champion.Vayne:
                    break;
                case Champion.Veigar:
                    break;
                case Champion.Velkoz:
                    break;
                case Champion.Vi:
                    break;
                case Champion.Viktor:
                    break;
                case Champion.Vladimir:
                    break;
                case Champion.Volibear:
                    break;
                case Champion.Warwick:
                    break;
                case Champion.MonkeyKing:
                    break;
                case Champion.Xerath:
                    break;
                case Champion.XinZhao:
                    break;
                case Champion.Yasuo:
                    break;
                case Champion.Yorick:
                    break;
                case Champion.Zac:
                    break;
                case Champion.Zed:
                    break;
                case Champion.Ziggs:
                    break;
                case Champion.Zilean:
                    break;
                case Champion.Zyra:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            //var myType = Type.GetType("AiBuddy.Champions." + Player.Instance.ChampionName + Player.Instance.ChampionName);
            //myType?.GetMethod("Initialize").Invoke(null, null);
        }
    }
}