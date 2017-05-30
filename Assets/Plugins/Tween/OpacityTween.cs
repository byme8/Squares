using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoroutinesEx;
using Tweens;
using Tweens.Data;
using UnityEngine;

namespace Tweens
{
    public static class OpacityTween
    {
        public static IEnumerator Opacity(this Material material, float to, float time, float delay = 0, Curve curve = null)
        {
            var color = material.color;
            var start = color.a;
            var delta = to - start;

            return Sequence.Create(
               Delay.Create(delay),
               TweenHelper.Process(
                   opacity => material.color = new Color(color.r, color.g, color.b, opacity),
                   shift => start + delta * shift,
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
