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
            Argument mulitpleArgumentValidator;
            try
            {
                mulitpleArgumentValidator = new MultiplierArgument(args);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException || ex is ArgumentNullException 
                    || ex is ArgumentOutOfRangeException || ex is ArgumentOutOfRangeException 
                    || ex is InvalidCastException)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

                throw;
            }

            // Pass Values of column and/or row into a programme to produce the multiplication table.
            // Produce standard output table.
            // Pass this into and output formatter.

        }
    }
}
