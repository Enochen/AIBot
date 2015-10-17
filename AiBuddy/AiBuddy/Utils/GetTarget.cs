#region

using EloBuddy;
using EloBuddy.SDK;

#endregion

namespace AiBuddy.Utils
{
    internal class GetTarget
    {
        /// <summary>
        ///     Gets the target using the input of the caller.
        /// </summary>
        /// <returns>
        ///     Obj_AI_Base as target
        /// </returns>
        public static AIHeroClient Target(float range, DamageType dtype)
        {
            return TargetSelector.GetTarget(range, dtype);
        }

        public static AIHeroClient GetComboTarget()
        {
            var hero = CurrentHero.Get();

            switch (hero)
            {
                case Champion.Aatrox:
                {
                    return TargetSelector.GetTarget(Champions.Aatrox.Aatrox.E.Range, DamageLib.GetDamageType.Get());
                }
                case Champion.Akali:
                {
                    return TargetSelector.GetTarget(Champions.Akali.Akali.R.Range, DamageLib.GetDamageType.Get());
                }
                case Champion.Caitlyn:
                {
                    return TargetSelector.GetTarget(Champions.Caitlyn.Caitlyn.Q.Range, DamageLib.GetDamageType.Get());
                }
                case Champion.Evelynn:
                {
                    return TargetSelector.GetTarget(Champions.Evelynn.Evelynn.W.Range, DamageLib.GetDamageType.Get());
                }
                case Champion.Ezreal:
                {
                    return TargetSelector.GetTarget(Champions.Ezreal.Ezreal.Q.Range, DamageLib.GetDamageType.Get());
                }
                case Champion.Soraka:
                {
                    return TargetSelector.GetTarget(Champions.Soraka.Soraka.Q.Range, DamageLib.GetDamageType.Get());
                }
                case Champion.Teemo:
                {
                    return TargetSelector.GetTarget(Champions.Teemo.Teemo.W.Range, DamageLib.GetDamageType.Get());
                }
                case Champion.Corki:
                {
                    return TargetSelector.GetTarget(Champions.Corki.Corki.Q.Range, DamageLib.GetDamageType.Get());
                }
                case Champion.Blitzcrank:
                {
                    return TargetSelector.GetTarget(Champions.Blitzcrank.Blitzcrank.Q.Range,
                        DamageLib.GetDamageType.Get());
                }
            }

            return TargetSelector.GetTarget(1000, DamageLib.GetDamageType.Get());
        }
    }
}