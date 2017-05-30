using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UniRx;
using Tweens;
using Coroutines;
using Squares.Game;

namespace Squares.UserInput
{
    public class UserInputController : MonoBehaviour
    {
        public GridController GridController;

        private Queue<Color> colorQueue;

        private IDisposable processor;

        private List<CellController> selectedCells = new List<CellController>();

        private void Awake()
        {
            this.colorQueue = new Queue<Color>();
            ColorsManager.Instance.NewColors.Subscribe(colors =>
            {
                this.colorQueue.Clear();
                foreach (var color in colors)
                    this.colorQueue.Enqueue(color);
            });
        }

        public void StartHammerProcesing()
        {
            if (this.processor != null)
                this.processor.Dispose();

            this.processor = this.GridController.CellSelection.Subscribe(cellController =>
            {
                cellController.Cell.Color = null;
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
                if (cellController.Cell.Color.HasValue)
                    return;

                if (!this.colorQueue.Any())
                    return;

                var color = this.colorQueue.Dequeue();
                cellController.Cell.Color = color;
                cellController.SetColor(color).StartCoroutine();

                this.selectedCells.Add(cellController);
            });
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0) && this.selectedCells.Any())
            {
                var delay = 0.1f;
                foreach (var cell in GameController.Instance.Turn(this.selectedCells.Select(o => o.Cell)))
                {
                    var cellController = this.GridController.GetCellController(cell);
                    cellController.Cell.Color = null;
                    cellController.SetColor(CellColors.Empty).StartCoroutine();

                    delay += 0.1f;
                }

                this.selectedCells.Clear();

                if (!this.colorQueue.Any())
                    ColorsManager.Instance.Next();
            }
        }
    }
}
