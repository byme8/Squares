using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coroutines.Abstractions;
using UnityEngine;

namespace Coroutines
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
