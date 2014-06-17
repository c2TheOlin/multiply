using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multiply.Model
{
    public interface IOutput
    {
        void OutputGrid();
    }

    public abstract class Outputter
    {
        protected string[,] _multiplierGrid;
        protected int _rows;
        protected int _columns;

        public Outputter(string [,] multipierGrid, int rows, int columns)
	    {
            _multiplierGrid = multipierGrid;
            _rows = rows;
            _columns = columns;
	    }
    }

    public class HtmlOutputter : IOutput
    {
        public void OutputGrid()
        {
            throw new NotImplementedException();
        }
    }

    public class CvsOutputter : IOutput
    {

        public void OutputGrid()
        {
            throw new NotImplementedException();
        }
    }

    public class ConsoleOutputter : Outputter, IOutput
    {
        private int BufferSize = 5;

        public ConsoleOutputter(string[,] multipierGrid, int rows, int columns)
            : base(multipierGrid, rows, columns)
        { 
        }

        public void OutputGrid()
        {
            for (int indexX = 0; indexX <= _rows; indexX++)
            {
                string line = string.Empty;

                for (int indexY = 0; indexY <= _columns; indexY++)
                {
                    if(indexY == 0)
                    {
                        string firstColumn = (_multiplierGrid[indexX, indexY] == string.Empty) ? " " : _multiplierGrid[indexX, indexY];
                        line = line + firstColumn.PadLeft(_rows.ToString().Length);
                    }
                    else
                    {
                        line = line + _multiplierGrid[indexX, indexY].PadLeft(BufferSize);
                    }
                }

                Console.WriteLine(line + System.Environment.NewLine);
            }
        }
    }
}
