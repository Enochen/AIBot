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
                switch (cell.CalculateSafety())
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

        internal static NavigationSafety CalculateSafety(this NavigationControlCell cell)
        {
            if (cell.MeshCell.CollFlags.HasFlag(CollisionFlags.Grass))
            {
                return NavigationSafety.Grass;
            }
            //If cell is under turret and no ally under turret
            foreach (var ally in EntityManager.Heroes.Allies)
            {
                foreach (
                    var turret in
                        EntityManager.Turrets.Enemies.Where(
                            turret => cell.MeshCell.WorldPosition.IsInRange(turret, turret.GetAutoAttackRange())))
                {
                    return ally.IsInRange(turret, turret.GetAutoAttackRange())
                               ? NavigationSafety.Average
                               : NavigationSafety.Danger;
                }
            }
            return NavigationSafety.Safe;
        }
        public static Vector3 RandomizePosition(this Vector3 v)
        {
            var r = new Random(Environment.TickCount);
            var minRandBy = 30;
            var maxRandBy = 30;
            minRandBy *= r.Next(-1, 1);
            return new Vector2(v.X + r.Next(minRandBy, maxRandBy), v.Y + r.Next(minRandBy, maxRandBy)).To3D();
        }
    }
}