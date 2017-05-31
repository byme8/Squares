using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using Utils;

namespace Squares.Game
{
    public class ColorsPrpvider : Singletone<ColorsPrpvider>
    {
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

        public ColorsPrpvider()
        {
        }

        private ValueWithProbability<int>[] sizeProbabilities = new[] 
        {
            new ValueWithProbability<int>(0.15f, 1),
            new ValueWithProbability<int>(0.35f, 2),
            new ValueWithProbability<int>(0.35f, 3),
            new ValueWithProbability<int>(0.15f, 4),
        };

        public void Next()
        {
            this.Colors = this.NextColors;
            this.NextColors = Enumerable.Range(0, this.sizeProbabilities.Random()).Select(_ => CellColors.GridColors.Random()).ToArray();

            if (this.Colors == null)
                return;

            this.newColors.OnNext(this.Colors);
        }
    }
}
