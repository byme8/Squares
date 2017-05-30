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
        private Vector2[] directions = new Vector2[] {
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

        public int Height { get; private set; }

        public IObservable<IEnumerable<Cell>> MergedCells
        {
            get
            {
                return this.mergedCells.AsObservable();
            }
        }

        public int Width { get; private set; }

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

            if (!allMergedCells.Any())
            {
                this.CheckGameOver();
                return allMergedCells;
            }

            var allUniqueMergedCells = allMergedCells.Distinct().ToArray();
            this.mergedCells.OnNext(allUniqueMergedCells);
            return allUniqueMergedCells;
        }

        private IEnumerable<Cell> CheckColors(Cell cell, Cell[] previouslyCells)
        {
            var cellsWithSameColor = new List<Cell>();
            foreach (var direction in this.directions)
            {
                var currentCell = this.Cells[cell.Row + (int)direction.y][cell.Column + (int)direction.x];
                if (currentCell.Color.HasValue && currentCell.Color == cell.Color && !cellsWithSameColor.Contains(cell))
                {
                    cellsWithSameColor.Add(currentCell);
                }
            }

            var newCells = cellsWithSameColor.Except(previouslyCells).ToArray();
            if (newCells.Any())
            {
                var nextCells = new List<Cell>();
                foreach (var newCell in newCells)
                    nextCells.AddRange(this.CheckColors(newCell, new[] { newCell }.Union(previouslyCells).ToArray()));

                return nextCells;
            }
            else
            {
                return previouslyCells;
            }
        }

        private void CheckGameOver()
        {
            // skip first and last row/column
            if (this.Cells.Skip(1).Take(this.Height).All(rows =>
                      rows.Skip(1).Take(this.Width).All(cell => cell.Color.HasValue)))
            {

                foreach (var row in this.Cells)
                    foreach (var cell in row)
                        cell.Color = null;

                this.gameOver.OnNext(Unit.Default);
            }
        }
    }
}