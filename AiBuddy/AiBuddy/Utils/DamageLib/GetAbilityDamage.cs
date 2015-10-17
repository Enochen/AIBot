#region

using EloBuddy;
using EloBuddy.SDK;

#endregion

namespace AiBuddy.Utils.DamageLib
{
    internal class GetAbilityDamage
    {
        /// <summary>
        ///     Gets the abilities' damage using the SDK Damagelib. If any damage isn't working,
        ///     I will manually add it after it is reported.
        /// </summary>
        private static readonly AIHeroClient _playerClient = ObjectManager.Player;

        public static float GetSingle(Obj_AI_Base target, SpellSlot slot)
        {
            var damage = 0f;

            damage += _playerClient.GetSpellDamage(target, slot);
            return damage;
        }

        public static float GetCombo(Obj_AI_Base target)
        {
            var damage = 0f;

            damage += _playerClient.GetSpellDamage(target, SpellSlot.Q);
            damage += _playerClient.GetSpellDamage(target, SpellSlot.W);
            damage += _playerClient.GetSpellDamage(target, SpellSlot.E);
            damage += _playerClient.GetSpellDamage(target, SpellSlot.R);

            return damage;
        }
    }
}