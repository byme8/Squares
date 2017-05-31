using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Squares.Game;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Squares.UI
{
    public class ScoreView : MonoBehaviour
    {
        public Text Score;
        public Text BestScore;

        private void Start()
        {
            ScoreManager.Instance.Score.Subscribe(score => this.Score.text = score.ToString());
            ScoreManager.Instance.BestScore.Subscribe(score => this.BestScore.text = score.ToString());
        }
    }
}
