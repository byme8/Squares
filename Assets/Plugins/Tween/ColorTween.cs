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
    public static class ColorTween
    {
        public static IEnumerator Color(this Material material, Color to, float time, float delay = 0, Curve curve = null)
        {
            var start = material.color;
            var delta = to - start;

            return Sequence.Create(
               Delay.Create(delay),
               TweenHelper.Process(
                   color => material.color = color,
                   shift => start + delta * shift,
                   time,
                   curve));
        }
    }
}
