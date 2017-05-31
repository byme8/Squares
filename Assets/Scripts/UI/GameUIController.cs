﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CoroutinesEx;
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

        private void Awake()
        {
            ColorsManager.Instance.NewColors.Subscribe(colors =>
            {
                this.NewColorsCoroutine(colors).StartCoroutine();
            });

            this.MainColors.StartSelectionMonitoring();
        }

        private IEnumerator NewColorsCoroutine(IEnumerable<Color> colors)
        {
            yield return this.HintColors.Hide();

            this.MainColors.SetColors(colors);
        }

        public void ShowHint()
        {
            this.HintColors.SetColors(ColorsManager.Instance.NextColors);
        }

        public void Skip()
        {
            ColorsManager.Instance.Next();
        }
    }
}