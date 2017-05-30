using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Squares.Game;
using UniRx;
using UnityEngine;

namespace Squares
{
    public class CellController : MonoBehaviour
    {
        public Cell Cell;
        public Subject<CellController> Selection;

        public bool Filled
        {
            get
            {
                return this.Cell.Color.HasValue;
            }
        }

        private void OnMouseEnter()
        {
            if (Input.GetMouseButton(0) && !this.Filled)
            {
                this.Selection.OnNext(this);
            }
        }

        private void OnMouseDown()
        {
            if (!this.Filled)
            {
                this.Selection.OnNext(this);
            }
        }
    }
}
