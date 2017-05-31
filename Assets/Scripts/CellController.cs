using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoroutinesEx;
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

        private void Awake()
        {
            this.material = this.GetComponent<MeshRenderer>().material;
        }

        public IEnumerator SetColor(Color color, float time = 0.2f)
        {
            this.Cell.Color = color;
            yield return this.material.Color(color, time, curve: Curves.SinusoidalIn);
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
