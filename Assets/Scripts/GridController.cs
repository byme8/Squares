using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Coroutines;
using Squares.Game;
using Tweens;
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

        private Dictionary<Cell, CellController> cellControllers;

        private Subject<CellController> cellSelection = new Subject<CellController>();

        public IObservable<CellController> CellSelection
        {
            get
            {
                return this.cellSelection.AsObservable();
            }
        }

        public CellController GetCellController(Cell cell)
        {
            return this.cellControllers.First(o => o.Key == cell).Value;
        }

        private void CreateGrid()
        {
            var verticalShift = this.Height / Space / 2.0f;
            var horizontalShift = this.Width / Space / 2.0f;
            var gridTransform = this.gameObject.transform;
            var game = GameController.Instance;

            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    var cell = this.CellPrefab.Clone();
                    var cellTransform = cell.transform;
                    cellTransform.parent = gridTransform;
                    cellTransform.localPosition = new Vector3(Space * j - verticalShift, Space * i - horizontalShift, 0);
                    var cellController = cell.GetComponent<CellController>();
                    cellController.Cell = game.Cells[i + 1][j + 1];
                    cellController.Selection = this.cellSelection;

                    this.cellControllers.Add(cellController.Cell, cellController);
                }
            }
        }

        public void Cleanup()
        {
            foreach (var cellController in this.cellControllers.Values)
            {
                cellController.GetComponent<MeshRenderer>().material.
                    Color(CellColors.Empty, 0.5f, 1, Curves.ExponentialOut).
                    StartCoroutine();
            }
        }

        private void OnDestroy()
        {
            this.cellSelection.Dispose();
        }

        private void Start()
        {
            GameController.Instance.SetSize(this.Height, this.Width);
            this.cellControllers = new Dictionary<Cell, CellController>();
            this.CreateGrid();
        }
    }
}
