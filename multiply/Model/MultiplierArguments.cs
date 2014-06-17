using multiply.Types;
using System;

namespace multiply.Model
{
    public interface IArgumentValidator
    {
        int Rows { get; }
        int Columns { get; }
        OutputType OutputType { get; }
    }

    // Implementation of the IMultiplier Argumentss
    public class MultiplierArgumentValidtor : IArgumentValidator
    {
        private readonly string nullArgsException = "Null Arguments: A row value must be provided. " + System.Environment.NewLine +
               "You can provide the following when running the programme" + System.Environment.NewLine +
               "rows - (1-20) - required" + System.Environment.NewLine +
               "columns - (1-20) - optional" + System.Environment.NewLine +
               "output format - (Console, Csv, Html) optional";

        private readonly string invalidCastException = "[value] is not a valid numeric number. Please enter a value from 1 - 20";
        private readonly string outofRangeException = "[value] is not a valid entry. Values must be between 1 - 20";

        private string[] arguments;
        private int _row;
        private int _column;
        private OutputType _outputType = OutputType.console;

        public MultiplierArgumentValidtor(string[] args)
        {
            arguments = args;
            ValidateArguments();
        }

        public int Rows
        {
            get { return _row; }
        }

        public int Columns
        {
            get { return _column; }
        }

        public OutputType OutputType
        {
            get { return _outputType; }
        }

        private void ValidateArguments()
        {
            if (arguments == null || arguments.Length == 0)
            {
                throw new ArgumentNullException("args", nullArgsException);
            }

            int validatedArg;
            bool isInt = int.TryParse(arguments[0], out validatedArg);

            if (!isInt)
            {
                throw new InvalidCastException(invalidCastException.Replace("[value]", arguments[0]));
            }

            if (!IsIntInRange(validatedArg))
            {
                throw new ArgumentOutOfRangeException("rows", outofRangeException.Replace("[value]", validatedArg.ToString()));
            }

            _row = validatedArg;

            if(arguments.Length <= 1)
            {
                return;
            }

            isInt = int.TryParse(arguments[1], out validatedArg);
            bool isOutput = Enum.IsDefined(typeof(OutputType), arguments[1].ToLower());

            if (isInt)
            {
                if (IsIntInRange(validatedArg))
                {
                    _column = validatedArg;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("columns", outofRangeException.Replace("[value]", validatedArg.ToString()));
                }
            }

            if(isOutput)
            {
                _outputType = GetOutputType(arguments[1]);
                _column = _row;
            } 

            if(!isInt && !isOutput)
            {
                throw new ArgumentException("someerror");
            }

            if (arguments.Length <= 2)
            {
                return;
            }

            isOutput = Enum.IsDefined(typeof(OutputType), arguments[2].ToLower());

            if (isOutput)
            {
                _outputType = GetOutputType(arguments[2]);
                return;
            }
            else
            {
                throw new ArgumentException("Not a valid output");
            }
        }

        private bool IsIntInRange(int inputtedValue)
        {
            return inputtedValue > 0 && inputtedValue <= 20;
        }

        private OutputType GetOutputType(string inputtedValue)
        {
            OutputType validatedArg;

            if(string.IsNullOrWhiteSpace(inputtedValue))
            {
               throw new ArgumentNullException("Some Error");
            }

            try
            {
                validatedArg = (OutputType)Enum.Parse(typeof(OutputType), inputtedValue, true);
            }
            catch(InvalidCastException ex)
            {
                throw new InvalidCastException("Some message", ex);
            }

            return validatedArg;
        }
    }
}
