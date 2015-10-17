#region

using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

#endregion

namespace AiBuddy.Champions.Annie
{
    // MarioGK
    internal class Annie
    {
        public static Spell.Targeted Q;
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
            Q = new Spell.Targeted(SpellSlot.Q, 620);
            W = new Spell.Skillshot(SpellSlot.W, 620, SkillShotType.Cone, 400, int.MaxValue, 50);
            E = new Spell.Active(SpellSlot.E, 10);
            R = new Spell.Skillshot(SpellSlot.R, 600, SkillShotType.Circular, 500, int.MaxValue, 290);
        }

        private static void InitMisc()
        {
            Game.OnUpdate += OnGameUpdate;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsEnemy && args.SData.Name.ToLowerInvariant().Contains("attack") && sender is AIHeroClient && args.Target != null && args.Target.IsMe)
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