using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coroutines;
using Tweens;
using UnityEngine;
using UniRx;

namespace Squares.UI
{
    public class ColorsView : MonoBehaviour
    {
        public GameObject SquareTemplate;
        public GridController GridController;

        private Vector3 scale = new Vector3(50, 50, 1);
        private WaitForSeconds wait = new WaitForSeconds(0.5f);
        private Queue<Transform> squaresTransforms = new Queue<Transform>();

        public void StartSelectionMonitoring()
        {
            this.GridController.CellSelection.Subscribe(_ =>
            {
                if (!this.squaresTransforms.Any())
                    return;

                this.squaresTransforms.Dequeue().Scale(Vector3.zero, 0.5f).StartCoroutine();
            });
        }

        public IEnumerator SetColors(IEnumerable<Color> colors)
        {
            if (colors == null)
                yield break;

            yield return this.wait;

            foreach (Transform square in this.transform)
                GameObject.Destroy(square.gameObject);

            var delay = 0.1f;
            foreach (var color in colors)
            {
                var square = this.SquareTemplate.Clone();
                square.transform.SetParent(this.transform);
                square.transform.localScale = Vector3.zero;
                square.transform.Scale(this.scale, 1, delay, Curves.ElasticOut).StartCoroutine();
                square.GetComponent<MeshRenderer>().material.color = color;

                this.squaresTransforms.Enqueue(square.transform);

                delay += 0.1f;
            }
        }

        public IEnumerator Hide()
        {
            yield return this.transform.GetChilds().
                Select(o => o.Scale(Vector3.zero, 0.3f)).AsParallel();
        }
    }
}
