#region

using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;

#endregion

namespace AiBuddy.Champions.Corki
{
    // MarioGK
    internal class Corki
    {
        public static Spell.Skillshot Q;
        public static Spell.Skillshot W;
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
            Q = new Spell.Skillshot(SpellSlot.Q, 825, SkillShotType.Circular, 400, int.MaxValue, 250);
            W = new Spell.Skillshot(SpellSlot.W, 800, SkillShotType.Circular, 400, 650, 65);
            E = new Spell.Active(SpellSlot.E, 600);
            R = new Spell.Skillshot(SpellSlot.R, 1200,SkillShotType.Linear, 400, 1950, 75);
        }

        private static void InitMisc()
        {
            Game.OnUpdate += OnGameUpdate;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsEnemy && sender.IsValidTarget(E.Range) && sender != null && e != null)
            {
                W.Cast(_Player.Position.Shorten(sender.Position, W.Range));
            }
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