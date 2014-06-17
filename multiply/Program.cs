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
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            // Validate Input
            IArgumentValidator _multiplierArgumentValidator;
            IMultiplier _multiplier;
            IOutput _outputter = null;
            string[,] _multiplierGrid;

            try
            {
                _multiplierArgumentValidator = new MultiplierArgumentValidtor(args);
                 // Pass Values of column and/or row into a programme to produce the multiplication table.
                _multiplier = new LoopMultiplier(_multiplierArgumentValidator.Rows, _multiplierArgumentValidator.Columns);
                _multiplierGrid = _multiplier.GenerateMultiplicationGrid();

                // Pass this into and output formatter.
                switch(_multiplierArgumentValidator.OutputType)
                {
                    case Types.OutputType.html:
                        break;
                    case Types.OutputType.csv:
                        break;
                    default:
                        _outputter = new ConsoleOutputter(_multiplierGrid, _multiplierArgumentValidator.Rows, _multiplierArgumentValidator.Columns);
                        break;
                }

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
