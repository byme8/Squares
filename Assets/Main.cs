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
using Squares.Saving;

public class Main : MonoBehaviour
{
    public UserInputController UserInputController;
    public GridController GridController;
    public RestartView RestartView;

    private void Start()
    {
        if (!GameSaver.Instance.Load())
        {
            ColorsProvider.Instance.Next();
            ColorsProvider.Instance.Next();
            GameController.Instance.SetSize(5, 5);
        }

        this.UserInputController.StartSelection();
        var renderers = this.RestartView.GetComponentsInChildren<CanvasRenderer>();
        renderers.Select(o => o.Opacity(0, 0).StartCoroutine()).ToArray();
        this.GridController.CreateGrid();

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
        ColorsProvider.Instance.Next();
        ColorsProvider.Instance.Next();
        ScoreManager.Instance.Score.Value = 0;

        yield return this.RestartView.GetComponentsInChildren<CanvasRenderer>().Select(o => o.Opacity(0, 1f)).AsParallel();
        this.RestartView.Disable();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            GameSaver.Instance.Save();
    }

#if UNITY_EDITOR
    private void OnApplicationQuit()
    {
        GameSaver.Instance.Save();
    }
#endif
}
