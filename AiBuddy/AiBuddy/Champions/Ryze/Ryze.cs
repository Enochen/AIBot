namespace AiBuddy.Champions.Ryze
{
    using System;
    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Enumerations;
    using EloBuddy.SDK.Events;
    internal class Ryze
    {
        public static Spell.Skillshot Q;
        public static Spell.Targeted W;
        public static Spell.Targeted E;
        public static Spell.Active R;

        public static void Initialize()
        {
            Bootstrap.Init(null);

            InitSpells();
            InitMisc();
        }

        public static void InitSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Linear, 250, 1700, 50);
            Q.AllowedCollisionCount = 0;
            W = new Spell.Targeted(SpellSlot.W, 600);
            E = new Spell.Targeted(SpellSlot.E, 600);
            R = new Spell.Active(SpellSlot.R);
        }

        private static void InitMisc()
        {
            Game.OnUpdate += OnGameUpdate;
            Gapcloser.OnGapcloser += OnGapCloser;
        }

        private static void OnGapCloser(Obj_AI_Base sender, Gapcloser.GapcloserEventArgs args)
        {
            if (sender.IsEnemy && sender is AIHeroClient && args.End.IsInRange(Player.Instance, W.Range) && W.IsReady())
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