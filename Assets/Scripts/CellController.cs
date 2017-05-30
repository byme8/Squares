using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Squares.Game;
using Tweens;
using UniRx;
using UnityEngine;

namespace Squares
{
    public class CellController : MonoBehaviour
    {
        public Cell Cell;
        public Subject<CellController> Selection;
        private Material material;

        private void Start()
        {
            this.material = this.GetComponent<MeshRenderer>().material;
        }

        public IEnumerator SetColor(Color color)
        {
            yield return this.material.Color(color, 1, curve: Curves.ExponentialOut);
        }

        private void OnMouseDown()
        {
            this.Selection.OnNext(this);
        }

        private void OnMouseEnter()
        {
            if (Input.GetMouseButton(0))
            {
                this.Selection.OnNext(this);
            }
        }
    }
}
