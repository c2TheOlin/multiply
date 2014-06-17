using Microsoft.VisualStudio.TestTools.UnitTesting;
using multiply.Model;
using multiply.Types;
using System;

namespace multiply.test
{
    /// <summary>
    /// Test the Validation Criteria on the Mutliplier IArgumentValidator
    /// </summary>
    [TestClass]
    public class MultiplierArgumentsValidationTests
    {
        private IArgumentValidator _arguments;

        [TestCleanup()]
        public void TestCleanup()
        {
            _arguments = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArguments_ThrowsArgumentNullException()
        {
            _arguments = new MultiplierArgumentValidtor(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmptyArguments_ThrowsArgumentNullException()
        {
            _arguments = new MultiplierArgumentValidtor(new string[] { });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void RowValue_IsNotInteger_ThrowsInvalidCastException()
        {
            _arguments = new MultiplierArgumentValidtor(new string[] { "@" });
        }

        [TestMethod]
        public void RowValue_IsValidInteger_SetsRowValue()
        {
            _arguments = new MultiplierArgumentValidtor(new string[] { "5" });
            Assert.AreEqual(_arguments.Rows, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RowValue_IsNotInValidRange_ArgumentOutOfRangeException()
        {
            _arguments = new MultiplierArgumentValidtor(new string[] { "21" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ColumnValue_IsNotInValidRange_ArgumentOutOfRangeException()
        {
            _arguments = new MultiplierArgumentValidtor(new string[] { "20", "0" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ColumnValue_IsNotInteger_ArgumentException()
        {
            _arguments = new MultiplierArgumentValidtor(new string[] { "20", "&", "console" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OutputFormat_NotValid_ArgumentException()
        {
            _arguments = new MultiplierArgumentValidtor(new string[] { "2", "5", "json" });
        }

        [TestMethod]
        public void NoColumnValueInput_ColumnValueSetToRowValue()
        {
            _arguments = new MultiplierArgumentValidtor(new string[] { "20", "console" });

            Assert.IsNotNull(_arguments.Columns);
            Assert.AreEqual(_arguments.Rows, _arguments.Columns);
        }

        [TestMethod]
        public void NoOutValueInput_OutputValueSetToConsole()
        {
            _arguments = new MultiplierArgumentValidtor(new string[] { "20", "5" });
            Assert.AreEqual(_arguments.Columns, 5);
            Assert.AreEqual(_arguments.Rows, 20);
            Assert.AreEqual(_arguments.OutputType, OutputType.console);
        }

        [TestMethod]
        public void OutputFormatEntered_OutputFormatSetToValue()
        {
            _arguments = new MultiplierArgumentValidtor(new string[] { "20", "5","html" });
            Assert.AreEqual(_arguments.OutputType, OutputType.html);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ColumnandFormatEnteredInWrongOrder_ThrowsArgumentExcpetion()
        {
            _arguments = new MultiplierArgumentValidtor(new string[] { "20", "html", "6" });
            Assert.AreEqual(_arguments.OutputType, OutputType.html);
        }
    }
}
