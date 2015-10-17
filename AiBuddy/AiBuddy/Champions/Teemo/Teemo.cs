#region

using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

#endregion

namespace AiBuddy.Champions.Teemo
{
    using AiBuddy.Champions.Teemo.Shroom;

    /// <summary>
    /// MarioGK & KarmaPanda
    /// </summary>
    internal class Teemo
    {
        
        /// <summary>
        /// Initializes the Spell Q
        /// </summary>
        public static Spell.Targeted Q;
        
        /// <summary>
        /// Initializes the Spell W
        /// </summary>
        public static Spell.Active W;
        
        /// <summary>
        /// Initializes the Spell R
        /// </summary>
        public static Spell.Skillshot R;

        /// <summary>
        /// Initializes Teemo
        /// </summary>
        public static void Initialize()
        {
            Bootstrap.Init(null);

            InitSpells();
            InitMisc();
        }

        /// <summary>
        /// Initializes the Spells
        /// </summary>
        public static void InitSpells()
        {
            Q = new Spell.Targeted(SpellSlot.Q, 680);
            W = new Spell.Active(SpellSlot.W, 700);
            R = new Spell.Skillshot(SpellSlot.R, 300, SkillShotType.Circular, 500, 1000, 120);
        }

        /// <summary>
        /// Initializes the Events
        /// </summary>
        private static void InitMisc()
        {
            Game.OnUpdate += OnGameUpdate;
            Obj_AI_Base.OnLevelUp += Obj_AI_Base_OnLevelUp;
        }

        /// <summary>
        /// Called when a unit levels up.
        /// </summary>
        /// <param name="sender">The Unit that Leveled Up</param>
        /// <param name="args">The Args</param>
        private static void Obj_AI_Base_OnLevelUp(Obj_AI_Base sender, Obj_AI_BaseLevelUpEventArgs args)
        {
            if (!sender.IsMe)
            {
                return;
            }

            if (_Player.Level == 11)
            {
                R = new Spell.Skillshot(SpellSlot.R, 600, SkillShotType.Circular, 500, 1000, 120);
            }

            if (_Player.Level == 16)
            {
                R = new Spell.Skillshot(SpellSlot.R, 900, SkillShotType.Circular, 500, 1000, 120);
            }
        }

        /// <summary>
        /// Get's and Returns the Player Instance.
        /// </summary>
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        /// <summary>
        /// Called whenever the Game Updates.
        /// </summary>
        /// <param name="args">The Args</param>
        private static void OnGameUpdate(EventArgs args)
        {
            Modes.Combo.Execute();
            Init.Initialize();
        }
    }
}