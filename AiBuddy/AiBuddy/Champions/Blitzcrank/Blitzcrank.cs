#region

using System;
using AiBuddy.Champions.Blitzcrank.Modes;
using AiBuddy.Champions.Blitzcrank.Utils;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

#endregion

namespace AiBuddy.Champions.Blitzcrank
{
    internal class Blitzcrank
    {
        public static Spell.Skillshot Q;
        public static Spell.Active W;
        public static Spell.Active E;
        public static Spell.Skillshot R;

        public static void Initialize()
        {
            Bootstrap.Init(null);

            InitSpells();
            InitMisc();
        }

        public static void InitSpells()
        {
            //Spell Definition
            Q = new Spell.Skillshot(SpellSlot.Q, 1050, SkillShotType.Linear, 250, 1800, 70);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Skillshot(SpellSlot.R, 0, SkillShotType.Circular, 250, int.MaxValue, 600);
            //More stuff
            Q.AllowedCollisionCount = 0;
            Q.MinimumHitChance = HitChance.High;
            R.AllowedCollisionCount = int.MaxValue;
        }

        private static void InitMisc()
        {
            Game.OnUpdate += OnGameUpdate;
            Obj_AI_Base.OnSpellCast += Misc.OnSpellCast;
        }

        private static void OnGameUpdate(EventArgs args)
        {
            Combo.Execute();
        }
    }
}