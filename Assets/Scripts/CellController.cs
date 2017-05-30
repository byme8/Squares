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
