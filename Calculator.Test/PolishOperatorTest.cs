using System;
using Calculator.Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.Test
{
    [TestClass]
    public class PolishOperatorTest
    {
        [TestMethod]
        public void EvaluateFunc()
        {
            var sumOperator = new PolishOperator(1, (d, d1) => d + d1, "+");
            var result = sumOperator.Evaulate(0, 1);

            Assert.AreEqual<double>(1, result);
        }

        [TestMethod]
        public void EvaluateFuncArgException()
        {
            try
            {
                var sumOperator = new PolishOperator(1, null, "+");
                var result = sumOperator.Evaulate(0, 1);
            }
            catch (ArgumentException argExp)
            {
                Assert.IsInstanceOfType(argExp, typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void ToStringTest()
        {

            var sumOperator = new PolishOperator(1, null, "+");
            Assert.AreEqual("+", sumOperator.OperatorSign);

        }
    }
}
