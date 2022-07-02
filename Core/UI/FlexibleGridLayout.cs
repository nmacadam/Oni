// Original work Copyright (c) 2020 Game Dev Guide
// CC-BY License
// https://www.youtube.com/watch?v=CGsEJToeXmA

// Modified for ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;
using UnityEngine.UI;

namespace Oni.UI
{
    /// <summary>
    /// Grid layout with support for auto-scaling and tiling
    /// </summary>
    public class FlexibleGridLayout : LayoutGroup
    {
		public enum FitType 
		{
			Uniform,
			Width,
			Height,
			FixedRows,
			FixedColumns
		}

		[SerializeField] private FitType _fitType = default;
		[SerializeField] private int _rows = default;
		[SerializeField] private int _columns = default;
		[SerializeField] private Vector2 _cellSize = default;
		[SerializeField] private Vector2 _spacing = default;
		[SerializeField] private bool _fitX = default;
		[SerializeField] private bool _fitY = default;
		[SerializeField] private bool _ignoreInactive = true;

        public FitType Fit { get => _fitType; set => _fitType = value; }
        public int Rows { get => _rows; set => _rows = value; }
        public int Columns { get => _columns; set => _columns = value; }
        public Vector2 CellSize { get => _cellSize; set => _cellSize = value; }
        public Vector2 Spacing { get => _spacing; set => _spacing = value; }
        public bool FitX { get => _fitX; set => _fitX = value; }
        public bool FitY { get => _fitY; set => _fitY = value; }
        public bool IgnoreInactive { get => _ignoreInactive; set => _ignoreInactive = value; }

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();

			if (_fitType == FitType.Uniform || _fitType == FitType.Width || _fitType == FitType.Height)
			{
				_fitX = _fitY = true;
				float sqrt = Mathf.Sqrt(transform.childCount);
				_rows = _columns = Mathf.CeilToInt(sqrt);
			}

			if (_fitType == FitType.Width || _fitType == FitType.FixedColumns)
			{
				_rows = Mathf.CeilToInt(transform.childCount / (float)_columns);
			}
			if (_fitType == FitType.Height || _fitType == FitType.FixedRows)
			{
				_columns = Mathf.CeilToInt(transform.childCount / (float)_rows);
			}

			float parentWidth = rectTransform.rect.width;
			float parentHeight = rectTransform.rect.height;

			float cellWidth = parentWidth / (float)_columns - (_spacing.x / (float)_columns * (_columns - 1)) - (padding.left / (float)_columns) - (padding.right / (float)_columns);
			float cellHeight = parentHeight / (float)_rows - (_spacing.y / (float)_rows * (_rows - 1)) - (padding.top / (float)_rows) - (padding.bottom / (float)_rows);

			_cellSize.x = _fitX ? cellWidth : _cellSize.x;
			_cellSize.y = _fitY ? cellHeight : _cellSize.y;

			int columnCount = 0;
			int rowCount = 0;

			for (int i = 0; i < rectChildren.Count; i++)
			{
				if (_ignoreInactive && !rectChildren[i].gameObject.activeSelf)
				{
					continue;
				}

				rowCount = i / _columns;
				columnCount = i % _columns;

				var item = rectChildren[i];
				var xPos = (_cellSize.x * columnCount) + (_spacing.x * columnCount) + padding.left;
				var yPos = (_cellSize.y * rowCount) + (_spacing.y * rowCount) + padding.top;

				SetChildAlongAxis(item, 0, xPos, _cellSize.x);
				SetChildAlongAxis(item, 1, yPos, _cellSize.y);
			}
        }

        public override void CalculateLayoutInputVertical()
        {
            
        }

        public override void SetLayoutHorizontal()
        {
            
        }

        public override void SetLayoutVertical()
        {
            
        }
    }
}