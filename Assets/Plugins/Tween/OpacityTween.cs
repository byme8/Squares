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
        public static IEnumerator Opacity(this CanvasRenderer canvasRenderer, float to, float time, float delay = 0, Curve curve = null)
        {
            var start = canvasRenderer.GetAlpha();
            var delta = to - start;

            return Sequence.Create(
               Delay.Create(delay),
               TweenHelper.Process(
                   opacity => canvasRenderer.SetAlpha(opacity),
                   shift => start + delta * shift,
                   time,
                   curve));
        }
    }
}
