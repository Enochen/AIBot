#region

using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;

#endregion

namespace AiBuddy.Champions.Aatrox
{
    // MarioGK
    internal class Aatrox
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
            Q = new Spell.Skillshot(SpellSlot.Q, 650, SkillShotType.Circular, 450, int.MaxValue, 75);
            W = new Spell.Active(SpellSlot.W, (uint) _Player.AttackRange);
            E = new Spell.Skillshot(SpellSlot.E, 1000, SkillShotType.Linear, 350, int.MaxValue, 50);
            R = new Spell.Active(SpellSlot.R, 325);
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
                Q.Cast(_Player.Position.Shorten(sender.Position, Q.Range));
            }
        }

        private static void OnGameUpdate(EventArgs args)
        {
            Modes.Combo.Execute();
        }
    }
}