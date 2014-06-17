using Microsoft.VisualStudio.TestTools.UnitTesting;
using multiply.Model;
using System;

namespace multiply.test
{
    [TestClass]
    public class MultiplierTests
    {
        private IMultiplier _multiplier;

        [TestCleanup()]
        public void TestCleanup()
        {
            _multiplier = null;
        }

        [TestMethod]
        public void WithRowAndColumnValue_ReturnsGridWithHeaders()
        {
            _multiplier = new LoopMultiplier(3, 5);
            string [,] grid = _multiplier.GenerateMultiplicationGrid();
            Assert.AreEqual(grid[0, 5], "5");
            Assert.AreEqual(grid[3, 0], "3");
            Assert.AreEqual(grid[0,0], string.Empty);
        }

        [TestMethod]
        public void WithRowAndColumnValue_ReturnsFullGrid()
        {
            _multiplier = new LoopMultiplier(3, 5);
            string[,] grid = _multiplier.GenerateMultiplicationGrid();
            Assert.AreEqual(grid[3, 5], "15");
            Assert.AreEqual(grid[2, 4], "8");
            Assert.AreEqual(grid[3, 2], "6");
            Assert.AreEqual(grid[1, 4], "4");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WithZeroRowAndColumnValue_ReturnsFullGrid()
        {
            _multiplier = new LoopMultiplier(0, 0);
            string[,] grid = _multiplier.GenerateMultiplicationGrid();
        }
    }
}
