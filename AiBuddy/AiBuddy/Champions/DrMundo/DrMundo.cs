#region

using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;

#endregion

namespace AiBuddy.Champions.DrMundo
{
    // MarioGK
    internal class DrMundo
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
            Q = new Spell.Skillshot(SpellSlot.Q, 990, SkillShotType.Linear, 400, 1850, 55);
            W = new Spell.Active(SpellSlot.W, 160);
            E = new Spell.Active(SpellSlot.E, (uint)_Player.AttackRange);
            R = new Spell.Active(SpellSlot.R, 500);
        }

        private static void InitMisc()
        {
            Game.OnUpdate += OnGameUpdate;
            Orbwalker.OnAttack += Orbwalker_OnAttack;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsEnemy && sender.IsValidTarget(Q.Range) && sender != null && e != null)
            {
                Q.Cast(sender);
            }
        }

        private static void Orbwalker_OnAttack(AttackableUnit target, EventArgs args)
        {
            if (target.IsEnemy)
            {
                E.Cast();
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