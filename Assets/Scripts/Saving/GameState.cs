using Squares.Game;
using UnityEngine;

namespace Squares.Saving
{
    public class GameState
    {
        public int BestScore
        {
            get;
            set;
        }

        public Cell[][] Cells
        {
            get;
            set;
        }

        public Color[] CurrentColors
        {
            get;
            set;
        }

        public Color[] NextColors
        {
            get;
            set;
        }

        public int Score
        {
            get;
            set;
        }
    }
}
