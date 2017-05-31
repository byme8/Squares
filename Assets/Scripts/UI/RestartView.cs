using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;

namespace Squares.UI
{
    public class RestartView : MonoBehaviour
    {
        private Subject<Unit> restart;

        public IObservable<Unit> Restart
        {
            get
            {
                return this.restart.AsObservable();
            }
        }

        private void Start()
        {
            this.restart = new Subject<Unit>();
        }

        public void RestartClick()
        {
            this.restart.OnNext(Unit.Default);
        }
    }
}
