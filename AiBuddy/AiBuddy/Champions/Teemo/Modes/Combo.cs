﻿#region

using AiBuddy.Utils;
using EloBuddy.SDK;

#endregion

namespace AiBuddy.Champions.Teemo.Modes
{
    internal class Combo
    {
        public static void Execute()
        {
            var target = GetTarget.GetComboTarget();

            if (target == null)
            {
                return;
            }

            if (Teemo.Q.IsReady() && target.IsValidTarget(Teemo.Q.Range))
            {
                Teemo.Q.Cast(target);
            }

            if (Teemo.W.IsReady() && target.IsValidTarget(Teemo.W.Range) && !target.IsValidTarget(Teemo.Q.Range))
            {
                Teemo.W.Cast();
            }

            if (Teemo.R.IsReady() && target.IsValidTarget(Teemo.R.Range))
            {
                Teemo.R.Cast(target);
            }
        }
    }
}