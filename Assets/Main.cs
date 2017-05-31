using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Squares.Game;
using Squares;
using Squares.UserInput;
using Squares.UI;
using Tweens;
using CoroutinesEx;

public class Main : MonoBehaviour
{
    public UserInputController UserInputController;
    public GridController GridController;
    public RestartView RestartView;

    private void Start()
    {
        this.UserInputController.StartSelection();
        ColorsPropvider.Instance.Next();
        ColorsPropvider.Instance.Next();
        var renderers = this.RestartView.GetComponentsInChildren<CanvasRenderer>();
        renderers.Select(o => o.Opacity(0, 0).StartCoroutine()).ToArray();

        GameController.Instance.GameOver.Subscribe(_ =>
        {
            this.RestartView.Enable();
            this.RestartView.GetComponentsInChildren<CanvasRenderer>().Select(o => o.Opacity(1, 1f)).AsParallel().StartCoroutine();
            this.UserInputController.StopSelection();
        });

        this.RestartView.Restart.Subscribe(_ => this.Restart().StartCoroutine());
    }

    public IEnumerator Restart()
    {
        this.UserInputController.StartSelection();
        this.GridController.Cleanup();
        ColorsPropvider.Instance.Next();
        ColorsPropvider.Instance.Next();
        ScoreManager.Instance.Score.Value = 0;

        yield return this.RestartView.GetComponentsInChildren<CanvasRenderer>().Select(o => o.Opacity(0, 1f)).AsParallel();
        this.RestartView.Disable();
    }
}
