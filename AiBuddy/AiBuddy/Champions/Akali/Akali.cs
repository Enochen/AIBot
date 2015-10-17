#region

using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;

#endregion

namespace AiBuddy.Champions.Akali
{
    // MarioGK
    internal class Akali
    {
        public static Spell.Targeted Q;
        public static Spell.Skillshot W;
        public static Spell.Active E;
        public static Spell.Targeted R;

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
            Q = new Spell.Targeted(SpellSlot.Q, 600);
            W = new Spell.Skillshot(SpellSlot.W, 700, SkillShotType.Circular, 400, int.MaxValue, 400);
            E = new Spell.Active(SpellSlot.E, 325);
            R = new Spell.Targeted(SpellSlot.R, 700);
        }

        private static void InitMisc()
        {
            Game.OnUpdate += OnGameUpdate;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            Obj_AI_Base.OnLevelUp += Obj_AI_Base_OnLevelUp;
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsEnemy && sender.IsValidTarget(W.Range) && sender != null && e != null)
            {
                W.Cast(_Player);
            }
        }

        private static void Obj_AI_Base_OnLevelUp(Obj_AI_Base sender, Obj_AI_BaseLevelUpEventArgs args)
        {
            if (sender.IsMe)
            {
                if (_Player.Level == 11)
                {
                    R = new Spell.Targeted(SpellSlot.R, 800);
                }
                if (_Player.Level == 16)
                {
                    R = new Spell.Targeted(SpellSlot.R, 900);
                }
            }
        }

        private static void OnGameUpdate(EventArgs args)
        {
            Modes.Combo.Execute();
        }
    }
}