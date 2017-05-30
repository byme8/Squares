using System;
using System.Collections;
using System.Collections.Generic;
using Squares.Game;
using UniRx;
using UnityEngine;

namespace Squares
{
    public class GridController : MonoBehaviour
    {
        public GameObject CellPrefab;
        public int Height;
        public int Width;
        const float Space = 1.1f;
        private Subject<Cell> cellSelection = new Subject<Cell>();

        public IObservable<Cell> CellSelection
        {
            get
            {
                return this.cellSelection.AsObservable();
            }
        }

        private void CreateGrid()
        {
            var verticalShift = this.Height / Space / 2.0f;
            var horizontalShift = this.Width / Space / 2.0f;
            var gridTransform = this.gameObject.transform;
            var game = GameController.Instance;

            for (int i = 0; i < this.Width; i++)
            {
                for (int j = 0; j < this.Height; j++)
                {
                    var cell = this.CellPrefab.Clone();
                    var cellTransform = cell.transform;
                    cellTransform.parent = gridTransform;
                    cellTransform.localPosition = new Vector3(Space * i - horizontalShift, Space * j - verticalShift, 0);
                    var cellController = cell.GetComponent<CellController>();
                    cellController.Cell = game.Cells[i + 1][j + 1];
                    cellController.Selection = this.cellSelection;
                }
            }
        }

        private void OnDestroy()
        {
            this.cellSelection.Dispose();
        }

        private void Start()
        {
            GameController.Instance.SetSize(this.Height, this.Width);
            this.CreateGrid();
        }
    }
}
