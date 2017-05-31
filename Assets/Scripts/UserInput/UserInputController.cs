using System;
using System.Collections.Generic;
using System.Linq;
using CoroutinesEx;
using Squares.Game;
using UniRx;
using UnityEngine;

namespace Squares.UserInput
{
    public class UserInputController : MonoBehaviour
    {
        public GridController GridController;

        public ReactiveCollection<CellController> SelectedCells
            = new ReactiveCollection<CellController>();

        private int currentColorIndex;
        private Color[] currentColors;
        private IDisposable processor;
        public void StartHammerProcesing()
        {
            if (this.processor != null)
                this.processor.Dispose();

            this.processor = this.GridController.CellSelection.Subscribe(cellController =>
            {
                cellController.SetColor(CellColors.Empty).StartCoroutine();
                this.StartSelection();
            });
        }

        public void StartSelection()
        {
            if (this.processor != null)
                this.processor.Dispose();

            this.processor = this.GridController.CellSelection.Subscribe(cellController =>
            {
                if (cellController.Cell.Color != CellColors.Empty)
                    return;

                if (this.currentColorIndex >= this.currentColors.Length)
                    return;

                var lastSelection = this.SelectedCells.LastOrDefault();
                if (lastSelection == null || GameController.Instance.SupportedDirections.Any(direction =>
                    cellController.Cell.Row - direction.y == lastSelection.Cell.Row &&
                    cellController.Cell.Column - direction.x == lastSelection.Cell.Column))
                {
                    var color = this.currentColors[this.currentColorIndex++];
                    cellController.SetColor(color).StartCoroutine();

                    this.SelectedCells.Add(cellController);
                }
            });
        }

        private void Awake()
        {
            ColorsPropvider.Instance.NewColors.Subscribe(colors =>
            {
                this.currentColors = colors.ToArray();
            });
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0) && this.SelectedCells.Any())
            {
                if (this.currentColorIndex != this.currentColors.Length)
                {
                    foreach (var cellController in this.SelectedCells)
                    {
                        cellController.SetColor(CellColors.Empty).StartCoroutine();
                    }
                }
                else
                {
                    var delay = 0.1f;

                    foreach (var cell in GameController.Instance.Turn(this.SelectedCells.Select(o => o.Cell)))
                    {
                        var cellController = this.GridController.GetCellController(cell);
                        cellController.SetColor(CellColors.Empty).StartCoroutine();

                        delay += 0.1f;
                    }
                    ColorsPropvider.Instance.Next();
                }

                this.SelectedCells.Clear();
                this.currentColorIndex = 0;
            }
        }
    }
}
