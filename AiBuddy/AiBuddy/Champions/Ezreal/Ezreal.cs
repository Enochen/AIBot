#region



#endregion

namespace AiBuddy.Champions.Ezreal
{
    using System;

    using AiBuddy.Champions.Ezreal.Modes;

    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Enumerations;
    using EloBuddy.SDK.Events;

    // MarioGK
    internal class Ezreal
    {
        public static Spell.Skillshot Q;
        public static Spell.Skillshot W;
        public static Spell.Skillshot E;
        public static Spell.Skillshot R;

        public static void Initialize()
        {
            Bootstrap.Init(null);

            InitSpells();
            InitMisc();
        }

        public static void InitSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1100, SkillShotType.Circular, 300, 2000, 65);
            W = new Spell.Skillshot(SpellSlot.W, 1000, SkillShotType.Circular, 300, 1550, 80)
                    { AllowedCollisionCount = int.MaxValue };
            E = new Spell.Skillshot(SpellSlot.E, 475, SkillShotType.Circular, 600, int.MaxValue, 10);
            R = new Spell.Skillshot(SpellSlot.R, int.MaxValue, SkillShotType.Linear, 1, 2000, 160)
                    { AllowedCollisionCount = int.MaxValue };
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
                E.Cast(Player.Instance.Position.Shorten(sender.Position, E.Range));
            }
        }

        private static void OnGameUpdate(EventArgs args)
        {
            switch (Orbwalker.ActiveModesFlags)
            {
                case Orbwalker.ActiveModes.Combo:
                    Combo.Execute();
                    return;

                case Orbwalker.ActiveModes.LaneClear:
                    LaneClear.Execute();
                    return;
            }
        }
    }
}