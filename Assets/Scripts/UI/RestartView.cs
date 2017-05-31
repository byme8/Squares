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
        private Subject<Unit> restart = new Subject<Unit>();

        public IObservable<Unit> Restart
        {
            get
            {
                return this.restart.AsObservable();
            }
        }

        public void RestartClick()
        {
            this.restart.OnNext(Unit.Default);
        }
    }
}
