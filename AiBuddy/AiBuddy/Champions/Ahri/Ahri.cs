#region

using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;

#endregion

namespace AiBuddy.Champions.Ahri
{
    // MarioGK
    internal class Ahri
    {
        public static Spell.Skillshot Q;
        public static Spell.Active W;
        public static Spell.Skillshot E;
        public static Spell.Active R;

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
            Q = new Spell.Skillshot(SpellSlot.Q, 880, SkillShotType.Linear, 350, 1700, 70);
            W = new Spell.Active(SpellSlot.W, 550);
            E = new Spell.Skillshot(SpellSlot.E, 975, SkillShotType.Linear, 350, 1600 , 55);
            R = new Spell.Active(SpellSlot.R, 450);
        }

        private static void InitMisc()
        {
            Game.OnUpdate += OnGameUpdate;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsEnemy && sender.IsValidTarget(Q.Range) && sender != null && e != null)
            {
                E.Cast(sender);
            }
        }

        private static void OnGameUpdate(EventArgs args)
        {
            Modes.Combo.Execute();
        }
    }
}