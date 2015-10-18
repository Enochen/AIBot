using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace AiBuddy.Utils.DamageLib
{
    class GetSummonerSpellDamage
    {
        public static float GetIgnite(Obj_AI_Base target)
        {
            var damage = 50 + (20 * Player.Instance.Level);

            return CalculateDamage(target, DamageType.True, damage);
        }

        public static float GetSmite(Obj_AI_Base target)
        {
            var levelplus = 0f;

            if (Player.Instance.Level >= 1 && Player.Instance.Level <= 4)
                levelplus = 20 * Player.Instance.Level;
            if (Player.Instance.Level >= 5 && Player.Instance.Level <= 9)
                levelplus = 30 * Player.Instance.Level;
            if (Player.Instance.Level >= 10 && Player.Instance.Level <= 14)
                levelplus = 40 * Player.Instance.Level;
            if (Player.Instance.Level >= 15 && Player.Instance.Level <= 18)
                levelplus = 50 * Player.Instance.Level;

            levelplus += 370;

            return CalculateDamage(target, DamageType.True, levelplus);
        }
        private static float CalculateDamage(Obj_AI_Base target, DamageType type, float rawdamage)
        {
            return Player.Instance.CalculateDamageOnUnit(target, type, rawdamage);
        }
    }
}