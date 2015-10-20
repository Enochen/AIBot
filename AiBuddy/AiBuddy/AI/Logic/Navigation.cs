namespace AiBuddy.AI.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Menu.Values;
    using EloBuddy.SDK.Rendering;

    using SharpDX;

    using Color = System.Drawing.Color;

    internal enum NavigationSafety
    {
        Safe,

        Average,

        Danger,

        VeryDangerous,

        Grass
    }

    internal class NavigationControlCell
    {
        public NavMeshCell MeshCell;

        public NavigationSafety Safety;

        public Vector3[] Vertices;
    }

    internal static class Navigation
    {
        internal const int ControlCellWh = 240;

        internal const int CalulcationControlArea = 1200;

        internal static List<NavMeshCell> WorldCell()
        {
            var sourceGrid = Player.Instance.Position;

            var navCells = new List<NavigationControlCell>();
            for (var x = sourceGrid.X - CalulcationControlArea;
                 x < sourceGrid.X + CalulcationControlArea;
                 x += ControlCellWh)
            {
                for (var y = sourceGrid.Y - CalulcationControlArea;
                     y < sourceGrid.Y + CalulcationControlArea;
                     y += ControlCellWh)
                {
                    var cell = new Vector2(x, y).ToNavMeshCell();

                    if (cell.CollFlags.HasFlag(CollisionFlags.Wall))
                    {
                        continue;
                    }

                    var world2D = cell.WorldPosition.To2D();

                    var vertices = new[]
                                       {
                                           cell.WorldPosition, cell.WorldPosition + new Vector3(ControlCellWh, 0, 0),
                                           cell.WorldPosition + new Vector3(ControlCellWh, ControlCellWh, 0),
                                           cell.WorldPosition + new Vector3(0, ControlCellWh, 0), cell.WorldPosition
                                       };

                    // check if borders are crossing Prop/Wall
                    if (vertices[0].ToNavMeshCell().CollFlags.HasFlag(CollisionFlags.Wall)
                        || vertices[1].ToNavMeshCell().CollFlags.HasFlag(CollisionFlags.Wall)
                        || vertices[2].ToNavMeshCell().CollFlags.HasFlag(CollisionFlags.Wall)
                        || vertices[3].ToNavMeshCell().CollFlags.HasFlag(CollisionFlags.Wall))
                    {
                        continue;
                    }

                    navCells.Add(new NavigationControlCell { MeshCell = cell, Vertices = vertices });
                }
            }

            foreach (var cell in navCells)
            {
                var color = Color.LightCoral;
                switch (cell.MeshCell.CalculateSafety())
                {
                    case NavigationSafety.Safe:
                        color = Color.SpringGreen;
                        break;
                    case NavigationSafety.Average:
                        color = Color.White;
                        break;
                    case NavigationSafety.Danger:
                        color = Color.Red;
                        break;
                    case NavigationSafety.VeryDangerous:
                        color = Color.DarkRed;
                        break;
                }

                Line.DrawLine(color, 1f, cell.Vertices);
            }

            return null;
        }

        internal static NavigationSafety CalculateSafety(this NavMeshCell cell)
        {
            if (cell.CollFlags.HasFlag(CollisionFlags.Grass))
            {
                return NavigationSafety.Grass;
            }
            var enemyTurret = EntityManager.Turrets.Enemies.Where(turret => !turret.IsDead).OrderBy(turret => turret.Distance(Player.Instance)).FirstOrDefault();
            if (cell.WorldPosition.IsInRange(enemyTurret, 800))
            {
                return NavigationSafety.Danger;
            }
            if(EntityManager.Heroes.Allies.Count(ally => ally.IsInRange(enemyTurret, 800)) > 1)
            {
                return NavigationSafety.Average;
            }
            //If cell is under turret and no ally under turret
            return NavigationSafety.Safe;
        }
        public static Vector3 Randomize(this Vector3 v)
        {
            var r = new Random(Environment.TickCount);
            var minRandBy = 0;
            var maxRandBy = 500;
            if (Player.Instance.Team == GameObjectTeam.Order)
            {
                minRandBy *= 1;
            }
            else
            {
                minRandBy *= 1;
            }
            return new Vector2(v.X + r.Next(minRandBy, maxRandBy), v.Y + r.Next(minRandBy, maxRandBy)).To3D();
        }
    }
}