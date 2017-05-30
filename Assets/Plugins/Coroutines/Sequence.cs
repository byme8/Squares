using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoroutinesEx.Abstractions;
using UnityEngine;

namespace CoroutinesEx
{
    public static class Sequence
    {
        public static IEnumerator Create(params IEnumerator[] coroutines)
        {
            foreach (var coroutine in coroutines)
                yield return coroutine;
        }
    }
}
