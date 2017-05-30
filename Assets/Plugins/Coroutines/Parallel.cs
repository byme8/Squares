using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Coroutines.Abstractions;
using UnityEngine;

namespace Coroutines
{
    public static class Parallel
    {
        public static IEnumerator Create(params IEnumerator[] coroutines)
        {
            var list = coroutines.Select(o => CoroutinesFactory.StartCoroutine(o)).ToArray();
            foreach (var coroutine in list)
                yield return coroutine;
        }

        public static IEnumerator AsParallel(this IEnumerable<IEnumerator> coroutines)
        {
            yield return Create(coroutines.ToArray());
        }
    }
}
