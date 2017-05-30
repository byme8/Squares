using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Coroutines.Abstractions;
using UnityEngine;

namespace Coroutines
{
	class CoroutineHolder : MonoBehaviour
	{
		List<ICoroutine> superFastCoroutines 
			= new List<ICoroutine>();

		public void AddSuperFastCoroutine(ICoroutine coroutine)
		{
			this.superFastCoroutines.Add(coroutine);
		}

		void Update()
		{
			this.superFastCoroutines.RemoveAll(this.ProcessFastCoroutine);
		}

        private bool ProcessFastCoroutine(ICoroutine coroutine)
        {
            var nextMissing = !coroutine.Enumerator.MoveNext();
            if (nextMissing)
            {
                coroutine.Done();
            }

            return nextMissing;
        }

        public Coroutine AddCoroutine(IEnumerator coroutine)
        {
            return this.StartCoroutine(coroutine);
        }
    }
}