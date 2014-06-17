using System;
using System.Collections.Generic;
using System.IO;
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

        public Outputter(string[,] multipierGrid, int rows, int columns)
        {
            _multiplierGrid = multipierGrid;
            _rows = rows;
            _columns = columns;
        }
    }

    public class HtmlOutputter : Outputter, IOutput
    {
        private string _filePath;

        public HtmlOutputter(string[,] multipierGrid, int rows, int columns, string filePath)
            : base(multipierGrid, rows, columns)
        {
            this._filePath = filePath;
        }

        public void OutputGrid()
        {
            using (FileStream stream = new FileStream(_filePath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    writer.WriteLine("<table>");
                    for (int indexX = 0; indexX <= _rows; indexX++)
                    {
                        StringBuilder line = new StringBuilder();
                        line.Append("<tr>");
                        for (int indexY = 0; indexY <= _columns; indexY++)
                        {
                            line.Append("<td>" + _multiplierGrid[indexX, indexY] + "</td>");
                        }
                        line.Append("</tr>");
                        writer.WriteLine(line.ToString());
                        writer.Flush();
                    }
                    writer.WriteLine("</table>");
                    writer.Flush();
                }
            } 
        }
    }

    public class CvsOutputter : Outputter, IOutput
    {
        private string _filePath;

        public CvsOutputter(string[,] multipierGrid, int rows, int columns, string filePath)
            : base(multipierGrid, rows, columns)
        {
            this._filePath = filePath;
        }


        public void OutputGrid()
        {
            List<string> toWrite = new List<string>();
            for (int indexX = 0; indexX <= _rows; indexX++)
            {
                StringBuilder line = new StringBuilder();
                for (int indexY = 0; indexY <= _columns; indexY++)
                {
                   line.Append(_multiplierGrid[indexX, indexY] += ","); 
                }

                toWrite.Add(line.ToString());
            }

            File.WriteAllLines(_filePath, toWrite.ToArray());
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
                    if (indexY == 0)
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
