#region

using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;

#endregion

namespace AiBuddy.Champions.Soraka
{
    internal class Soraka
    {
        /*
        Spell Init Begins
        */

        public static Spell.Skillshot Q;
        public static Spell.Targeted W;
        public static Spell.Skillshot E;
        public static Spell.Active R;

        public static void Initialize()
        {
            Bootstrap.Init(null);

            InitSpells();
            InitMisc();
        }

        public static void InitSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 950, SkillShotType.Circular, 500, 1750, 300);
            W = new Spell.Targeted(SpellSlot.W, 550);
            E = new Spell.Skillshot(SpellSlot.E, 875, SkillShotType.Circular, 50, int.MaxValue, 250);
            R = new Spell.Active(SpellSlot.R, int.MaxValue);
            Q.AllowedCollisionCount = int.MaxValue;
            E.AllowedCollisionCount = int.MaxValue;

            //Spell Init Ends
        }

        private static void InitMisc()
        {
            //GameMenu.Initialize();
            Game.OnUpdate += OnGameUpdate;
            Interrupter.OnInterruptableSpell += Utils.Misc.OnInterruptableSpell;
        }

        private static void OnGameUpdate(EventArgs args)
        {
            //switch (Orbwalker.ActiveModesFlags)
            //{
            //    case Orbwalker.ActiveModes.Combo:
            Modes.Combo.Execute();
            //        break;
            //}
        }
    }
}