using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tweens;
using Tweens.Data;
using UnityEngine;

namespace Tweens
{
    public static class TweenHelper
    {
        public static IEnumerator Process<TValue>(
            Action<TValue> setValue,
            Func<float, TValue> valueCalculator,
            float time, 
            Curve curve)
        {
            if (curve == null)
                curve = Curves.BackIn;

            var timeSpent = 0.0f;
            while (timeSpent < time)
            {
                var shift = curve.Caclculate(timeSpent / time);
                var currentValue = valueCalculator(shift);
                setValue(currentValue);
                timeSpent += Time.deltaTime;
                yield return null;
            }
        }
    }
}
