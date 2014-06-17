using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using multiply.Model;
using multiply.Types;

namespace multiply.test
{
    /// <summary>
    /// Test the Validation Criteria on the Mutliplier Argument
    /// </summary>
    [TestClass]
    public class MultiplierArgumentsValidationTests
    {
        private Argument arguments;
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            arguments = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArguments_ThrowsArgumentNullException()
        {
            arguments = new MultiplierArgument(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmptyArguments_ThrowsArgumentNullException()
        {
            arguments = new MultiplierArgument(new string[] { });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void RowValue_IsNotInteger_ThrowsInvalidCastException()
        {
            arguments = new MultiplierArgument(new string[] { "@" });
        }

        [TestMethod]
        public void RowValue_IsValidInteger_SetsRowValue()
        {
            arguments = new MultiplierArgument(new string[] { "5" });
            Assert.AreEqual(arguments.Rows, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RowValue_IsNotInValidRange_ArgumentOutOfRangeException()
        {
            arguments = new MultiplierArgument(new string[] { "21" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ColumnValue_IsNotInValidRange_ArgumentOutOfRangeException()
        {
            arguments = new MultiplierArgument(new string[] { "20", "0" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ColumnValue_IsNotInteger_ArgumentException()
        {
            arguments = new MultiplierArgument(new string[] { "20", "&", "console" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OutputFormat_NotValid_ArgumentException()
        {
            arguments = new MultiplierArgument(new string[] { "2", "5", "json" });
        }

        [TestMethod]
        public void NoColumnValueInput_ColumnValueSetToRowValue()
        {
            arguments = new MultiplierArgument(new string[] { "20", "console" });

            Assert.IsNotNull(arguments.Columns);
            Assert.AreEqual(arguments.Rows, arguments.Columns);
        }

        [TestMethod]
        public void NoOutValueInput_OutputValueSetToConsole()
        {
            arguments = new MultiplierArgument(new string[] { "20", "5" });
            Assert.AreEqual(arguments.Columns, 5);
            Assert.AreEqual(arguments.Rows, 20);
            Assert.AreEqual(arguments.OutputType, OutputType.console);
        }

        [TestMethod]
        public void OutputFormatEntered_OutputFormatSetToValue()
        {
            arguments = new MultiplierArgument(new string[] { "20", "5","html" });
            Assert.AreEqual(arguments.OutputType, OutputType.html);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ColumnandFormatEnteredInWrongOrder_ThrowsArgumentExcpetion()
        {
            arguments = new MultiplierArgument(new string[] { "20", "html", "6" });
            Assert.AreEqual(arguments.OutputType, OutputType.html);
        }
    }

}
