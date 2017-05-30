using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CoroutinesEx.Abstractions;
using UnityEngine;

namespace CoroutinesEx
{
    class CoroutineHolder : MonoBehaviour
    {
        List<ICoroutine> superFastCoroutines
            = new List<ICoroutine>();

        Dictionary<string, List<Coroutine>> coroutinesGroups
            = new Dictionary<string, List<Coroutine>>();

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

        public Coroutine AddCoroutine(IEnumerator coroutine, string group)
        {
            return this.StartCoroutine(this.StartCoroutineInGroup(coroutine, group));
        }

        public void StopGroup(string group)
        {
            if (!this.coroutinesGroups.ContainsKey(group))
                return;

            foreach (var coroutine in this.coroutinesGroups[group])
                this.StopCoroutine(coroutine);
        }

        public bool IsGroupActive(string group)
        {
            return this.coroutinesGroups.ContainsKey(group) && this.coroutinesGroups[group].Any();
        }

        private IEnumerator StartCoroutineInGroup(IEnumerator coroutine, string group)
        {
            Debug.Log("Group " + group + " started");
            var startedCoroutine = this.StartCoroutine(coroutine);

            if (this.coroutinesGroups.ContainsKey(group))
                this.coroutinesGroups[group].Add(startedCoroutine);
            else
                this.coroutinesGroups.Add(group, new List<Coroutine>() { startedCoroutine });

            yield return startedCoroutine;

            this.coroutinesGroups[group].Remove(startedCoroutine);
            Debug.Log("Group " + group + " finished");
        }

        public Coroutine AddCoroutine(IEnumerator coroutine)
        {
            return this.StartCoroutine(coroutine);
        }
    }
}