using System;
using System.Collections;
using Coroutines.Abstractions;
using UnityEngine;

namespace Coroutines
{
    public static class CoroutinesFactory
    {
        static CoroutineHolder CoroutineHolder;

        static CoroutinesFactory()
        {
            var gameObject = new GameObject("~Coroutines");
            CoroutineHolder = gameObject.AddComponent<CoroutineHolder>();
        }

        public static CoroutineTask StartSuperFastCoroutine(IEnumerator enumerator)
        {
            var coroutine = new CoroutineTask(enumerator);
            CoroutineHolder.AddSuperFastCoroutine(coroutine);
            return coroutine;
        }

        public static Coroutine StartCoroutine(this IEnumerator coroutine)
        {
            return CoroutineHolder.AddCoroutine(coroutine);
        }

        public static void Stop(this Coroutine coroutine)
        {
            CoroutineHolder.StopCoroutine(coroutine);
        }
    }
}