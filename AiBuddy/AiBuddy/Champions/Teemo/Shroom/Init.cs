namespace AiBuddy.Champions.Teemo.Shroom
{
    using System;
    using System.Linq;

    using EloBuddy;
    using EloBuddy.SDK;

    using SharpDX;

    /// <summary>
    /// Made by KarmaPanda
    /// </summary>
    internal class Init
    {
        #region Fields

        /// <summary>
        /// Stores Time of Last R Cast
        /// </summary>
        private static int lastR;

        /// <summary>
        /// Initializes Shroom Tables
        /// </summary>
        private static ShroomTables shroomTables;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if there is shroom in location
        /// </summary>
        /// <param name="position">The location of check</param>
        /// <returns>If that location has a shroom.</returns>
        private static bool IsShroomed(Vector3 position)
        {
            return ObjectManager.Get<Obj_AI_Base>().Where(obj => obj.Name == "Noxious Trap").Any(obj => position.Distance(obj.Position) <= 250);
        }

        /// <summary>
        /// Creates Shroom Tables and Calls Events.
        /// </summary>
        public static void Initialize()
        {
            shroomTables = new ShroomTables();
            Game.OnTick += Game_OnTick;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
        }

        #endregion

        #region Events

        /// <summary>
        /// Called whenever a Spell is Casted
        /// </summary>
        /// <param name="sender">The Sender of the Spell</param>
        /// <param name="args">The Spell Data.</param>
        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe)
            {
                return;
            }

            if (args.SData.Name.ToLower() == "teemorcast")
            {
                lastR = Environment.TickCount;
            }
        }

        /// <summary>
        /// Called whenever the game ticks.
        /// </summary>
        /// <param name="args">The Args</param>
        private static void Game_OnTick(EventArgs args)
        {
            if (!Teemo.R.IsReady() || Player.Instance.HasBuff("Recall"))
            {
                return;
            }

            switch (Game.MapId)
            {
                case GameMapId.SummonersRift:
                    if (!shroomTables.SummonersRift.Any())
                    {
                        return;
                    }
                    foreach (var place in shroomTables.SummonersRift.Where(pos => pos.Distance(ObjectManager.Player.Position) <= Teemo.R.Range && !IsShroomed(pos)).Where(place => Environment.TickCount - lastR > 5000))
                    {
                        Teemo.R.Cast(place);
                    }
                    break;
                case GameMapId.HowlingAbyss:
                    if (!shroomTables.HowlingAbyss.Any())
                    {
                        return;
                    }
                    foreach (var place in shroomTables.HowlingAbyss.Where(pos => pos.Distance(ObjectManager.Player.Position) <= Teemo.R.Range && !IsShroomed(pos)).Where(place => Environment.TickCount - lastR > 5000))
                    {
                        Teemo.R.Cast(place);
                    }
                    break;
                case GameMapId.CrystalScar:
                    if (!shroomTables.CrystalScar.Any())
                    {
                        return;
                    }
                    foreach (var place in shroomTables.CrystalScar.Where(pos => pos.Distance(ObjectManager.Player.Position) <= Teemo.R.Range && !IsShroomed(pos)).Where(place => Environment.TickCount - lastR > 5000))
                    {
                        Teemo.R.Cast(place);
                    }
                    break;
                case GameMapId.TwistedTreeline:
                    if (!shroomTables.TwistedTreeline.Any())
                    {
                        return;
                    }
                    foreach (var place in shroomTables.TwistedTreeline.Where(pos => pos.Distance(ObjectManager.Player.Position) <= Teemo.R.Range && !IsShroomed(pos)).Where(place => Environment.TickCount - lastR > 5000))
                    {
                        Teemo.R.Cast(place);
                    }
                    break;
            }
        }

        #endregion
    }
}
