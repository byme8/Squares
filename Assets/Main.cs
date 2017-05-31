using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Squares.Game;
using Squares;
using Squares.UserInput;

public class Main : MonoBehaviour
{
    public UserInputController UserInputController;
    public GridController GridController;

    private void Start()
    {
        this.UserInputController.StartSelection();
        ColorsPrpvider.Instance.Next();
        ColorsPrpvider.Instance.Next();

        GameController.Instance.GameOver.Subscribe(_ => this.Restart());

        //var game = GameController.Instance;
        //game.MergedCells.Subscribe(cells =>
        //{
        //    Debug.Log(string.Join(",", cells.
        //                    Select(o =>
        //                        string.Format("[{0}, {1}]", o.Row, o.Column)).ToArray()));
        //});

        //game.Cells[1][1].Color = Color.blue;
        //game.Cells[1][2].Color = Color.blue;
        //game.Cells[1][3].Color = Color.blue;
        //game.Cells[3][2].Color = Color.blue;
        //game.Turn(new[]
        //{
        //    new Cell(2, 2) { Color = Color.blue },
        //});
    }

    private void Restart()
    {
        this.UserInputController.StartSelection();
        this.GridController.Cleanup();
        ColorsPrpvider.Instance.Next();
        ColorsPrpvider.Instance.Next();
    }
}
