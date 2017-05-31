using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using Utils;

namespace Squares.Game
{
    public class ScoreManager : Singletone<ScoreManager>
    {
        public ReactiveProperty<int> Score
        {
            get;
            set;
        }

        public ReactiveProperty<int> BestScore
        {
            get;
            set;
        }

        public ScoreManager()
        {
            this.Score = new ReactiveProperty<int>();
            this.BestScore = new ReactiveProperty<int>();

            this.Score.Subscribe(_ =>
            {
                if (this.Score.Value > this.BestScore.Value)
                    this.BestScore.Value = this.Score.Value;
            });

            GameController.Instance.MergedCells.Subscribe(cells =>
            {
                this.Score.Value += cells.Count() * 10;
            });
        }
    }
}
