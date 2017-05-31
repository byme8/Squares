using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Utils;

namespace Squares.Game
{
    public class GameController : Singletone<GameController>
    {
        public readonly Vector2[] SupportedDirections = new Vector2[] {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };

        private Subject<Unit> gameOver;
        private Subject<IEnumerable<Cell>> mergedCells;

        public Cell[][] Cells
        {
            get;
            private set;
        }

        public IObservable<Unit> GameOver
        {
            get
            {
                return this.gameOver.AsObservable();
            }
        }

        public int Height
        {
            get;
            private set;
        }

        public IObservable<IEnumerable<Cell>> MergedCells
        {
            get
            {
                return this.mergedCells.AsObservable();
            }
        }

        public int Width
        {
            get;
            private set;
        }

        public GameController()
        {
        }

        public void SetSize(int height, int width)
        {
            this.Height = height;
            this.Width = width;

            this.Cells = Enumerable.Range(0, height + 2).
                Select(row =>
                    Enumerable.Range(0, width + 2).
                        Select(column =>
                           new Cell(row, column)).
                        ToArray()).
                ToArray();

            this.mergedCells = new Subject<IEnumerable<Cell>>();
            this.gameOver = new Subject<Unit>();
        }

        public IEnumerable<Cell> Turn(IEnumerable<Cell> cells)
        {
            var allMergedCells = new List<Cell>();
            foreach (var cell in cells)
                this.Cells[cell.Row][cell.Column].Color = cell.Color;

            foreach (var cell in cells)
            {
                var allCells = this.CheckColors(cell, new[] { cell });
                var mergedCells = allCells.Distinct().ToArray();
                if (mergedCells.Count() > 2)
                    allMergedCells.AddRange(mergedCells);
            }


            var allUniqueMergedCells = allMergedCells.Distinct().ToArray();
            foreach (var cell in allUniqueMergedCells)
                cell.Color = CellColors.Empty;

            this.CheckGameOver();

            this.mergedCells.OnNext(allUniqueMergedCells);
            return allUniqueMergedCells;
        }

        private IEnumerable<Cell> CheckColors(Cell cell, IEnumerable<Cell> previouslyCells, int limit = 10, int limitValue = 0)
        {
            if (limitValue == limit)
                return previouslyCells;

            var newCells = new List<Cell>();
            foreach (var direction in this.SupportedDirections)
            {
                var currentCell = this.Cells[cell.Row + (int)direction.y][cell.Column + (int)direction.x];
                if (currentCell.Color.HasValue && currentCell.Color == cell.Color && !previouslyCells.Contains(currentCell))
                {
                    newCells.Add(currentCell);
                }
            }

            if (newCells.Any())
            {
                var nextCells = new List<Cell>();
                foreach (var newCell in newCells)
                {
                    var cells = this.CheckColors(newCell, new[] { newCell }.Union(previouslyCells), limit, limitValue++);

                    if (cells.Count() > limit)
                        return cells;

                    nextCells.AddRange(cells);
                }

                return nextCells;
            }
            else
            {
                return previouslyCells;
            }
        }

        private void CheckGameOver()
        {
            var emptyCells = this.Cells.SelectMany(row => row.Where(o => o.Color == CellColors.Empty)).ToArray();
            var limit = ColorsPropvider.Instance.NextColors.Length;
            foreach (var cell in emptyCells)
            {
                var mergedCells = this.CheckColors(cell, new[] { cell }, limit).Distinct().ToArray();
                if (mergedCells.Count() >= limit)
                    return;
            }

            this.gameOver.OnNext(Unit.Default);
        }
    }
}