using Microsoft.VisualStudio.TestTools.UnitTesting;
using multiply.Model;
using System;
using System.IO;

namespace multiply.test
{
    [TestClass]
    public class OutputterTests
    {
        private Outputter _outputter;
        private TextWriter sw;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenGivenEmptyGrid_ThrowsError()
        {
            _outputter = new HtmlOutputter(null, 5, 5, "path");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenCsvOutputterGivenEmptyGrid_ThrowsError()
        {
            _outputter = new CvsOutputter(null, 5, 5, "path");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenConsoleOutputterGivenEmptyGrid_ThrowsError()
        {
            _outputter = new ConsoleOutputter(null, 5, 5);
        }

        [TestMethod]
        public void WhenConsoleGivenGrid_GeneratesOutput()
        {

            LoopMultiplier mult = new LoopMultiplier(4, 4);

            _outputter = new ConsoleOutputter(mult.GenerateMultiplicationGrid(), 4, 4);

            using (StringWriter writer = new StringWriter())
            {
                Console.SetOut(writer);
                _outputter.OutputGrid();

                Assert.AreNotEqual(string.Empty, writer.ToString());
                Assert.IsTrue(writer.ToString().Contains("16"));
            }
        }
    }
}
