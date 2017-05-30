using System.Collections;
using CoroutinesEx;
using CoroutinesEx.Abstractions;
using Tweens.Data;
using UnityEngine;

namespace Tweens
{
    public static class MoveTween
    {
        public static IEnumerator Move(this Transform transform,
                Vector3 to,
                float time,
                float delay = 0,
                Curve curve = null)
        {
            var start = transform.localPosition;
            var delta = to - start;

            return Sequence.Create(
                   Delay.Create(delay),
                   TweenHelper.Process(
                       position => transform.localPosition = position,
                       shift => start + delta * shift,
                       time,
                       curve));
        }
    }
}