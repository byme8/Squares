using System.Collections;
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
        public ColorsView HintColors;
        public ColorsView MainColors;

        public void ShowHint()
        {
            this.HintColors.SetColors(ColorsProvider.Instance.NextColors);
        }

        public void Skip()
        {
            ColorsProvider.Instance.Next();
        }

        private void Awake()
        {
            ColorsProvider.Instance.NewColors.Subscribe(colors =>
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
    }
}