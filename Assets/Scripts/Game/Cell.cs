using UniRx;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class Cell
    {
        public Color Color { get; set; }

        public int Column { get; set; }

        public int Row { get; set; }

        public Cell(int row, int column)
        {
            this.Row = row;
            this.Column = column;
            this.Color = Color.black;
        }

        public override bool Equals(object obj)
        {
            var cell = obj as Cell;
            if (cell == null)
                return base.Equals(obj);

            return cell.Column == this.Column && cell.Row == this.Row && cell.Color == this.Color;
        }

        public override int GetHashCode()
        {
            return Tuple.Create(this.Row, this.Column, this.Color).GetHashCode();
        }
    }
}