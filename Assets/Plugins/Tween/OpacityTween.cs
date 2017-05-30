using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coroutines;
using Tweens;
using Tweens.Data;
using UnityEngine;

namespace Tweens
{
    public static class OpacityTween
    {
        public static IEnumerator Opacity(this CanvasRenderer render, float to, float time, float delay = 0, Curve curve = null)
        {
            return Sequence.Create(
               Delay.Create(delay),
               ProcessOpacity(
                   render,
                   to,
                   time,
                   curve));
        }

        private static IEnumerator ProcessOpacity(
            CanvasRenderer render,
            float to,
            float time,
            Curve curve)
        {
            if (curve == null)
                curve = Curves.BackIn;

            var timeSpent = 0.0f;
            var start = render.GetAlpha();
            var delta = to - start;
            while (timeSpent < time)
            {
                var shift = curve.Caclculate(timeSpent / time);
                var currentValue = start + delta * shift;
                render.SetAlpha(currentValue);
                timeSpent += Time.deltaTime;
                yield return null;
            }
        }
    }
}
