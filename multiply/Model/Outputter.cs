using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace multiply.Model
{
    public interface IOutput
    {
        void OutputGrid();
    }

    public abstract class Outputter : IOutput
    {
        protected string[,] _multiplierGrid;
        protected int _rows;
        protected int _columns;

        public Outputter(string[,] multipierGrid, int rows, int columns)
        {
            if(multipierGrid == null)
            {
                throw new ArgumentNullException("Grid cannot be empty");
            }

            if (rows == 0)
            {
                throw new ArgumentException("Rows value cannot be 0");
            }
            if (columns == 0)
            {
                throw new ArgumentException("Columns value cannot be 0");
            }

            _multiplierGrid = multipierGrid;
            _rows = rows;
            _columns = columns;
        }

        public abstract void OutputGrid();
    }

    public class HtmlOutputter : Outputter
    {
        private string _filePath;

        public HtmlOutputter(string[,] multipierGrid, int rows, int columns, string filePath)
            : base(multipierGrid, rows, columns)
        {
            this._filePath = filePath;
        }

        public override void OutputGrid()
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

    public class CvsOutputter : Outputter
    {
        private string _filePath;

        public CvsOutputter(string[,] multipierGrid, int rows, int columns, string filePath)
            : base(multipierGrid, rows, columns)
        {
            this._filePath = filePath;
        }


        public override void OutputGrid()
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

    public class ConsoleOutputter : Outputter
    {
        private int BufferSize = 5;

        public ConsoleOutputter(string[,] multipierGrid, int rows, int columns)
            : base(multipierGrid, rows, columns)
        {
        }

        public override void OutputGrid()
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
