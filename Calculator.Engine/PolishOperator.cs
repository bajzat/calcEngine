using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator.Engine
{
    public class PolishOperator : IPolishOperator
    {
        public int Priority { get; private set; }
        private readonly Func<double, double, double> _evalFunc;
        public string OperatorSign { get; private set; }

        public PolishObjectType PolishObjectType { get; private set; }

        public int Piority { get; private set; }

        public PolishOperator(int priority, Func<double, double, double> evalFunc, string operatorSign)
        {
            if(evalFunc == null || operatorSign == null)
            {
                throw new ArgumentException("Cannot create operator without evalFunc or operatorSign");
            }

            PolishObjectType = PolishObjectType.Operator;
            Priority = priority;
            _evalFunc = evalFunc;
            OperatorSign = operatorSign;
        }

        public double Evaulate(double arg1, double arg2)
        {
            return _evalFunc(arg1, arg2);
        }

        public override string ToString()
        {
            return OperatorSign;
        }
    }
}
