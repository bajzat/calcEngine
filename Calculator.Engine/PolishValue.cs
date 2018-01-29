using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator.Engine
{
    public class PolishValue : IPolishValue
    {

        public PolishObjectType PolishObjectType { get; private set; }

        public double Value { get; private set; }

        public PolishValue(double value)
        {
            PolishObjectType = PolishObjectType.Operand;
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
