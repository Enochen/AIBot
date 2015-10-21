#region

using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;

#endregion

namespace AiBuddy.Champions.Alistar
{
    // MarioGK
    internal class Alistar
    {
        public static Spell.Targeted Q;
        public static Spell.Skillshot W;
        public static Spell.Active E;
        public static Spell.Targeted R;

        public static void Initialize()
        {
            Bootstrap.Init(null);

            InitSpells();
            InitMisc();
        }

        public static void InitSpells()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 600);
            W = new Spell.Skillshot(SpellSlot.W, 700, SkillShotType.Circular, 400, int.MaxValue, 400);
            E = new Spell.Active(SpellSlot.E, 325);
            R = new Spell.Targeted(SpellSlot.R, 700);
        }

        private static void InitMisc()
        {
            Game.OnUpdate += OnGameUpdate;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsEnemy && sender.IsValidTarget(W.Range) && sender != null && e != null)
            {
                W.Cast(sender);
            }
        }

        private static void OnGameUpdate(EventArgs args)
        {
            Modes.Combo.Execute();
        }
    }
}