using System;
using Calculator.Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.Test
{
    [TestClass]
    public class PolishFormTest
    {
        [TestMethod]
        public void EvaluateWithCorrectInput()
        {
            var polishForm = new PolishForm(("(10+2)*3/4+5*6^2"));
            var result = polishForm.Evaluate();

            Assert.AreEqual(189, result);
            
        }

        [TestMethod]
        public void EvaluateWithDoubledOperatorInput()
        {
            try
            {
                var polishForm = new PolishForm(("(10++2)*3/4+5*6^2"));
                var result = polishForm.Evaluate();

               
            }
            catch(ArgumentException aExp)
            {
                Assert.IsInstanceOfType(aExp, typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void EvaluateWithIrregularBrackets()
        {
            try
            {
                var polishForm = new PolishForm(("(10+2)*3/4+(5*6^2"));
                var result = polishForm.Evaluate();


            }
            catch (FormatException fExp)
            {
                Assert.IsInstanceOfType(fExp, typeof(FormatException));
            }
        }
    }
}
