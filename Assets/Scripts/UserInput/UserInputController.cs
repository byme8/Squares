using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UniRx;

namespace Squares.UserInput
{
    public class UserInputController : MonoBehaviour
    {
        public GridController GridController;
        private IDisposable processor;

        public void StartSelection()
        {
            if (this.processor != null)
                this.processor.Dispose();

            this.processor = this.GridController.CellSelection.Subscribe(cell =>
            {
                Debug.Log(string.Format("Cell:{0}:{1} selected", cell.Row, cell.Column));
                cell.Color = Color.blue;
            });
        }

        public void StartHummerProcesing()
        {

        }

        public void MakeHint()
        {

        }
    }
}
