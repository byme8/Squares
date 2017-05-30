using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CoroutinesEx.Abstractions;
using UnityEngine;

namespace CoroutinesEx
{
    public static class Parallel
    {
        public static IEnumerator Create(string group, params IEnumerator[] coroutines)
        {
            var list = coroutines.Select(o => Coroutines.StartCoroutine(o, group)).ToArray();
            foreach (var coroutine in list)
                yield return coroutine;
        }

        public static IEnumerator AsParallel(this IEnumerable<IEnumerator> coroutines, string group = null)
        {
            yield return Create(group, coroutines.ToArray());
        }
    }
}
