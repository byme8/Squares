using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Squares
{
    public class GridController : MonoBehaviour
    {
        public GameObject CellPrefab;

        public int Height;
        public int Width;

        private void Start()
        {
            this.CreateGrid();
        }

        private void CreateGrid()
        {
            const float spacing = 1.1f;
            var verticalShift = this.Height / spacing / 2.0f;
            var horizontalShift = this.Width / spacing / 2.0f;
            var gridTransform = this.gameObject.transform;

            for (int i = 0; i < this.Width; i++)
            {
                for (int j = 0; j < this.Height; j++)
                {
                    var cell = this.CellPrefab.Clone();
                    var cellTransform = cell.transform;
                    cellTransform.parent = gridTransform;
                    cellTransform.localPosition = new Vector3(spacing * i - horizontalShift, spacing * j - verticalShift, 0);
                }
            }
        }
    }
}
