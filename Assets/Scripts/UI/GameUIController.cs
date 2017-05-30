using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Coroutines;
using Squares;
using Squares.Game;
using Tweens;
using UniRx;
using UnityEngine;

namespace Squares.UI
{
    public class GameUIController : MonoBehaviour
    {
        public ColorsView MainColors;
        public ColorsView HintColors;
        private bool hintShowed;

        private void Awake()
        {
            ColorsManager.Instance.NewColors.Subscribe(colors =>
            {
                this.HintColors.Hide().StartCoroutine();
                this.MainColors.SetColors(colors).StartCoroutine();
            });

            this.MainColors.StartSelectionMonitoring();
        }

        public void ShowHint()
        {
            this.hintShowed = true;
            this.HintColors.SetColors(ColorsManager.Instance.NextColors).StartCoroutine();
        }
    }
}