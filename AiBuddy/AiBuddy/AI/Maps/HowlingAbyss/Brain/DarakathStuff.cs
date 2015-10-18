namespace AiBuddy.AI.Maps.HowlingAbyss.Brain
{
    using System.Linq;

    using EloBuddy;
    using EloBuddy.SDK;

    using SharpDX;

    internal class DarakathStuff
    {
        public static Vector3 HealPosition()
        {
            var heal =
                ObjectManager.Get<GameObject>()
                    .Where(o => o.Name.Contains("healingBuff"))
                    .OrderBy(buff => buff.Distance(Player.Instance))
                    .FirstOrDefault();

            return heal?.Position ?? new Vector3(1,2,3);
        }
    }
}