using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using Utils;

namespace Squares.Game
{
    public class ColorsManager : Singletone<ColorsManager>
    {
        private Color[] acceptableColors = new[] 
        {
            Color.blue,
            Color.red,
            Color.yellow,
            Color.green
        };

        private Subject<IEnumerable<Color>> newColors = new Subject<IEnumerable<Color>>();

        public Color[] Colors
        {
            get;
            private set;
        }

        public IObservable<IEnumerable<Color>> NewColors
        {
            get
            {
                return this.newColors.AsObservable();
            }
        }


        public Color[] NextColors
        {
            get;
            private set;
        }

        public ColorsManager()
        {
        }

        public void Next()
        {
            this.Colors = this.NextColors;
            this.NextColors = Enumerable.Range(0, 3).Select(_ => this.acceptableColors.Random()).ToArray();
            this.newColors.OnNext(this.Colors);
        }
    }
}
