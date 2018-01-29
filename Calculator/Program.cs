using Calculator.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            //create a polish form object
            var polishForm = new PolishForm(("(10+2)*3/4+5*6^2"));
            //Print the original and polishform expression
            Console.WriteLine(polishForm);
            //Get the result
            Console.WriteLine("The result is " + polishForm.Evaluate());
        }
    }
}
