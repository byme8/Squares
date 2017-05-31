using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoroutinesEx;
using Tweens;
using UnityEngine;
using UniRx;
using Squares.UserInput;

namespace Squares.UI
{
    public class ColorsView : MonoBehaviour
    {
        public GameObject SquareTemplate;
        public UserInputController UserInputController;

        private Vector3 scale = new Vector3(50, 50, 1);
        private List<Transform> Squares = new List<Transform>();
        private int SquaresIndex;

        public void StartSelectionMonitoring()
        {
            this.UserInputController.SelectedCells.ObserveAdd().Subscribe(addSelection =>
            {
                if (this.SquaresIndex >= this.Squares.Count)
                    return;

                this.Squares[this.SquaresIndex++].Scale(Vector3.zero, 0.2f, curve: Curves.SinusoidalIn).StartCoroutine();
            });

            this.UserInputController.SelectedCells.ObserveReset().Subscribe(resetSelection =>
            {
                if (this.SquaresIndex >= this.Squares.Count)
                    return;

                this.ShowSquares();
            });
        }

        public void SetColors(IEnumerable<Color> colors)
        {
            foreach (var square in this.Squares)
            {
                square.gameObject.Disable();
                GameObject.Destroy(square.gameObject, 1);
            }

            this.Squares.Clear();

            foreach (var color in colors)
            {
                var square = this.SquareTemplate.Clone();
                square.transform.SetParent(this.transform);
                square.transform.localScale = Vector3.zero;
                square.GetComponent<MeshRenderer>().material.color = color;
                this.Squares.Add(square.transform);
            }

            this.ShowSquares();
        }

        private void ShowSquares()
        {
            var delay = 0.1f;
            foreach (var square in this.Squares)
            {
                square.transform.Scale(this.scale, 0.2f, delay, Curves.SinusoidalIn).StartCoroutine();
                delay += 0.1f;
            }

            this.SquaresIndex = 0;
        }

        public IEnumerator Hide()
        {
            yield return this.Squares.
                Select(o => o.Scale(Vector3.zero, 0.2f, curve: Curves.SinusoidalIn)).AsParallel();
        }
    }
}
