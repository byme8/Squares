using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Coroutines.Abstractions;
using UnityEngine;

namespace Coroutines
{
    public class CoroutineTask : CustomYieldInstruction, ICoroutine
    {
        private bool _keepWaiting = true;

        public IEnumerator Enumerator
        {
            get;
            set;
        }

        public CoroutineTask(IEnumerator coroutine)
        {
            this.Enumerator = coroutine;
        }

        public override bool keepWaiting
        {
            get
            {
                return this._keepWaiting;
            }
        }

        public void Done()
        {
            this._keepWaiting = false;
        }
    }

    public static class CoroutinesExtentions
    {
        public static IEnumerator Wait(this IEnumerable<ICoroutine> tasks)
        {
            foreach (var task in tasks.ToArray())
                if (task.keepWaiting)
                    yield return task;
        }

        public static ICoroutine ToCoroutine(this IEnumerator enumerator)
        {
            return new CoroutineTask(enumerator);
        }
    }
}
