using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace AiBuddy.Champions.Utils
{
    public class Helper
    {
        public static AIHeroClient Player
        {
            get { return ObjectManager.Player; }
        }

        public static IEnumerable<AIHeroClient> TeamHeroes
        {
            get
            {
                return EntityManager.Heroes.Allies
                    .Where(h => h.IsValid);
            }
        }
    }
}
