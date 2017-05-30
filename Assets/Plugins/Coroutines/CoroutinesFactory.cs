using System;
using System.Collections;
using CoroutinesEx.Abstractions;
using UnityEngine;

namespace CoroutinesEx
{
    public static class Coroutines
    {
        static CoroutineHolder CoroutineHolder;

        static Coroutines()
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

        public static Coroutine StartCoroutine(this IEnumerator coroutine, string group = null)
        {
            if (string.IsNullOrEmpty(group))
                return CoroutineHolder.AddCoroutine(coroutine);

            return CoroutineHolder.AddCoroutine(coroutine, group);
        }

        public static void StopGroup(string group)
        {
            CoroutineHolder.StopGroup(group);
        }

        public static bool IsGroupActive(string group)
        {
            return CoroutineHolder.IsGroupActive(group);
        }

        public static void Stop(this Coroutine coroutine)
        {
            CoroutineHolder.StopCoroutine(coroutine);
        }
    }
}