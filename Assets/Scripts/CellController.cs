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
        public Subject<CellController> Selection;

        private Cell cell;

        private Material material;

        public Cell Cell
        {
            get
            {
                return this.cell;
            }
            set
            {
                this.cell = value;
                this.StartCoroutine(SetColor(this.cell.Color));
            }
        }

        public IEnumerator SetColor(Color? color, float time = 0.2f)
        {
            if (!color.HasValue)
                yield break;

            this.Cell.Color = color;
            yield return this.material.Color(color.Value, time, curve: Curves.SinusoidalIn);
        }

        private void Awake()
        {
            this.material = this.GetComponent<MeshRenderer>().material;
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
