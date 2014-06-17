using System;

namespace multiply.Model
{
    public interface IMultiplier
    {
        string[,] GenerateMultiplicationGrid();
    }

    public class LoopMultiplier : IMultiplier
    {
        private int _rows;
        private int _columns;
        private string [,] _grid;

        public LoopMultiplier(int rows, int columns)
        {
            if(rows == 0)
            {
                throw new ArgumentException("Row value cannot be 0");
            }

            if(columns == 0)
            {
                throw new ArgumentException("Column value cannot be 0");
            }

            this._rows = rows + 1;
            this._columns = columns + 1;
            this._grid  = new string[_rows, _columns];
        }

        public string[,] GenerateMultiplicationGrid()
        {
            _grid[0, 0] = string.Empty;
            for (int indexX = 1; indexX < _columns; indexX ++)
            {
                _grid[0, indexX] = indexX.ToString();
            }

            for (int indexY = 1; indexY < _rows; indexY ++)
            {
                _grid[indexY, 0] = indexY.ToString();
            }

            for (int indexX = 1; indexX < _columns; indexX++)
            {
                for (int indexY = 1; indexY < _rows; indexY++)
                {
                    _grid[indexY, indexX] = (indexY * indexX).ToString();
                }
            }

            return _grid;
        }
    }
}
