#region

using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;

#endregion

namespace AiBuddy.Champions.Caitlyn
{
    // MarioGK
    internal class Caitlyn
    {
        public static Spell.Skillshot Q;
        public static Spell.Skillshot W;
        public static Spell.Skillshot E;
        public static Spell.Targeted R;

        public static void Initialize()
        {
            Bootstrap.Init(null);

            InitSpells();
            InitMisc();
        }

        public static void InitSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1300, SkillShotType.Linear, 400, int.MaxValue, 120);
            W = new Spell.Skillshot(SpellSlot.W, 800, SkillShotType.Circular, 400, int.MaxValue, 65);
            E = new Spell.Skillshot(SpellSlot.E, 950, SkillShotType.Circular, 50, int.MaxValue, 250);
            R = new Spell.Targeted(SpellSlot.R, 2000);
        }

        private static void InitMisc()
        {
            Game.OnUpdate += OnGameUpdate;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            Obj_AI_Base.OnLevelUp += Obj_AI_Base_OnLevelUp;

        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsEnemy && sender.IsValidTarget(E.Range) && sender != null && e != null)
            {
                E.Cast(sender);
            }
        }

        private static void Obj_AI_Base_OnLevelUp(Obj_AI_Base sender, Obj_AI_BaseLevelUpEventArgs args)
        {
            if (sender.IsMe)
            {
                if (_Player.Level == 11)
                {
                    R = new Spell.Targeted(SpellSlot.R, 2500);
                }
                if (_Player.Level == 16)
                {
                    R = new Spell.Targeted(SpellSlot.R, 3000);
                }
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