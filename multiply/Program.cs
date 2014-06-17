using multiply.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multiply
{
    public class Program
    {
        /// <summary>
        /// Produce multiplication tables and output in required format.
        /// </summary> 
        /// <param name="args">console arguments</param>
        public static void Main(string[] args)
        {
            IArgumentValidator _argValidator;
            IMultiplier _multiplier;
            IOutput _outputter = null;
            string[,] _multiplierGrid;
            var _pathFormat = @"~\..\multiply_{0}_{1}.{2}";
            string _filePath;

            try
            {
                // Validate Input
                _argValidator = new MultiplierArgumentValidtor(args);
                
                // Pass Values of column and/or row into a programme to produce the multiplication table.
                _multiplier = new LoopMultiplier(_argValidator.Rows, _argValidator.Columns);
                _multiplierGrid = _multiplier.GenerateMultiplicationGrid();

                // Pass this into and output formatter.
                _filePath = string.Format(_pathFormat, _argValidator.Rows, _argValidator.Columns, _argValidator.OutputType.ToString());

                switch(_argValidator.OutputType)
                {
                    case Types.OutputType.html:
                        _outputter = new HtmlOutputter(_multiplierGrid, _argValidator.Rows, _argValidator.Columns, _filePath);
                        break;
                    case Types.OutputType.csv:
                        _outputter = new CvsOutputter(_multiplierGrid, _argValidator.Rows, _argValidator.Columns, _filePath);
                        break;
                    default:
                        _outputter = new ConsoleOutputter(_multiplierGrid, _argValidator.Rows, _argValidator.Columns);
                        break;
                }

                // Output to desired format
                _outputter.OutputGrid();
            }
            catch (ArgumentNullException ex)
            {
                OutputErrorMessage(ex);
                return;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                OutputErrorMessage(ex);
                return;
            }
            catch (InvalidCastException ex)
            {
                OutputErrorMessage(ex);
                return;
            }
            catch (ArgumentException ex)
            {
                OutputErrorMessage(ex);
                return;
            }
        }

        private static void OutputErrorMessage(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
