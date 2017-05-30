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

    private void Start()
    {
        this.UserInputController.StartSelection();
    }
}
