#region

using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

#endregion

namespace AiBuddy.Champions.Evelynn
{
    // MarioGK
    internal class Evelynn
    {
        public static Spell.Active Q;
        public static Spell.Active W;
        public static Spell.Targeted E;
        public static Spell.Skillshot R;

        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static void Initialize()
        {
            Bootstrap.Init(null);

            InitSpells();
            InitMisc();
        }

        public static void InitSpells()
        {
            Q = new Spell.Active(SpellSlot.Q, 500);
            W = new Spell.Active(SpellSlot.W, 650);
            E = new Spell.Targeted(SpellSlot.E, 220);
            R = new Spell.Skillshot(SpellSlot.R, 650, SkillShotType.Circular, 350, int.MaxValue, 250);
        }

        private static void InitMisc()
        {
            Game.OnUpdate += OnGameUpdate;
        }

        private static void OnGameUpdate(EventArgs args)
        {
            if (_Player.HasBuffOfType(BuffType.Slow))
            {
                W.Cast();
            }

            Modes.Combo.Execute();
        }
    }
}