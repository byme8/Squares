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

        private Queue<Color> colorQueue = new Queue<Color>(ColorsManager.Instance.Colors);

        private IDisposable processor;

        private List<CellController> selectedCells = new List<CellController>();

        public void MakeHint()
        {

        }

        public void StartHummerProcesing()
        {

        }

        public void StartSelection()
        {
            if (this.processor != null)
                this.processor.Dispose();

            this.processor = this.GridController.CellSelection.Subscribe(cellController =>
            {
                if (!this.colorQueue.Any())
                    return;

                var color = this.colorQueue.Dequeue();
                Debug.Log(string.Format("Cell:{0}:{1} selected", cellController.Cell.Row, cellController.Cell.Column));
                cellController.Cell.Color = color;
                cellController.GetComponent<MeshRenderer>().material.
                    Color(color, 1, curve: Curves.ExponentialOut ).
                    StartCoroutine();

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

                    cellController.GetComponent<MeshRenderer>().material.
                        Color(Color.gray, 1, delay, Curves.ExponentialOut).
                        StartCoroutine();

                    delay += 0.1f;
                }

                this.selectedCells.Clear();

                if (!this.colorQueue.Any())
                {
                    ColorsManager.Instance.Next();
                    foreach (var color in ColorsManager.Instance.Colors)
                        this.colorQueue.Enqueue(color);
                }
            }
        }
    }
}
