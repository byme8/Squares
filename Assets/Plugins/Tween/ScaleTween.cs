using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coroutines;
using Coroutines.Abstractions;
using Tweens.Data;
using UnityEngine;

namespace Tweens
{
    public static class ScaleTween
    {
        public static IEnumerator Scale(this Transform transform,
                Vector3 to,
                float time,
                float delay = 0,
                Curve curve = null)
        {
            var start = transform.localScale;
            var delta = to - start;

            return Sequence.Create(
               Delay.Create(delay),
               TweenHelper.Process(
                   scale => transform.localScale = scale,
                   shift => start + delta * shift,
                   time,
                   curve));
        }
    }
}
