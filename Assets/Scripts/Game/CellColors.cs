using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Squares.Game
{
    public class CellColors
    {
        public static readonly Color Red = Color.red;
        public static readonly Color Blue = Color.blue;
        public static readonly Color Yellow = Color.yellow;
        public static readonly Color Green = Color.green;
        public static readonly Color Empty = new Color32(218, 218, 218, 255);

        public static readonly Color[] GridColors = new[]
        {
            Red,
            Blue,
            Yellow,
            Green
        };
    }
}
