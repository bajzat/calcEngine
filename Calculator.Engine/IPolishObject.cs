using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator.Engine
{
    public enum PolishObjectType
    {
        Operand = 1,
        Operator = 2
    }

    public interface IPolishObject
    {
        PolishObjectType PolishObjectType { get; }
    }

    public interface IPolishValue : IPolishObject
    {
        double Value { get; }
    }

    public interface IPolishOperator: IPolishObject
    {
        int Priority { get; }
        double Evaulate(double arg1, double arg2);
    }
}
