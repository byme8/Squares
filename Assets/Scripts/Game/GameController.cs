using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class GameController
    {
        private readonly Subject<IEnumerable<Cell>> selectedCells;

        public GameController(int height, int width)
        {
            this.Cells = Enumerable.Range(0, height + 2).
                Select(row =>
                    Enumerable.Range(0, width + 2).
                        Select(column =>
                           new Cell(row, column)).
                    ToArray()).
                ToArray();

            this.selectedCells = new Subject<IEnumerable<Cell>>();
        }

        public IObservable<IEnumerable<Cell>> SelectedCells
        {
            get
            {
                return this.selectedCells.AsObservable();
            }
        }

        public Cell[][] Cells
        {
            get;
            private set;
        }

        public void Turn(IEnumerable<Cell> cells)
        {
            foreach (var cell in cells)
                this.Cells[cell.Row][cell.Column].Color = cell.Color;

            foreach (var cell in cells)
                this.CheckColors(cell, new[] { cell });
        }

        Vector2[] directions = new Vector2[] {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };

        private void CheckColors(Cell cell, IEnumerable<Cell> previouslyCells)
        {
            var cellsWithSameColor = new List<Cell>();
            foreach (var direction in this.directions)
            {
                var currentCell = this.Cells[cell.Row + (int)direction.y][cell.Column + (int)direction.x];
                if (currentCell.Color == cell.Color && !cellsWithSameColor.Contains(cell))
                {
                    cellsWithSameColor.Add(currentCell);
                }
            }

            var newCells = cellsWithSameColor.Except(previouslyCells);
            if (newCells.Any())
            {
                foreach (var newCell in newCells)
                {
                    this.CheckColors(newCell, new[] { newCell }.Union(previouslyCells));
                }
            }
            else if (previouslyCells.Count() > 2)
            {
                foreach (var selectedCell in previouslyCells)
                    this.Cells[selectedCell.Row][selectedCell.Column].Color = Color.black;
                this.selectedCells.OnNext(previouslyCells);
            }
        }
    }
}
