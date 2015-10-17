#region

using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

#endregion

namespace AiBuddy.Champions.Amumu
{
    // MarioGK
    internal class Amumu
    {
        public static Spell.Skillshot Q;
        public static Spell.Active W;
        public static Spell.Active E;
        public static Spell.Active R;

        public static void Initialize()
        {
            Bootstrap.Init(null);

            InitSpells();
            InitMisc();
        }

        public static void InitSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1090, SkillShotType.Linear, 650, 2000, 70);
            W = new Spell.Active(SpellSlot.W, 295);
            E = new Spell.Active(SpellSlot.E, 340);
            R = new Spell.Active(SpellSlot.R, 540);
        }

        private static void InitMisc()
        {
            Game.OnUpdate += OnGameUpdate;
        }

        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        private static void OnGameUpdate(EventArgs args)
        {
            Modes.Combo.Execute();
        }
    }
}