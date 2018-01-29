using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator.Engine
{

    //Note: This class is not thread safe
    //Input limitations: it supports only positive integer values
    public class PolishForm
    {

        public string OriginalInput { get; set; }
        private readonly List<IPolishObject> _polishObjects;
        private readonly List<IPolishObject> _polishForm;
        private readonly Stack<IPolishOperator> _workingStack; 

        private readonly Dictionary<char, IPolishOperator> _operators;

        public PolishForm(string input)
        {
            OriginalInput = input;
            
            _workingStack = new Stack<IPolishOperator>();
            _polishObjects = new List<IPolishObject>();
            _polishForm = new List<IPolishObject>();

            //populate the operator store
            _operators = new Dictionary<char, IPolishOperator>
            {
                {'+', new PolishOperator(1, (d, d1) => d + d1, "+")},
                {'-', new PolishOperator(1, (d, d1) => d1 - d, "-")},
                {'/', new PolishOperator(2, (d, d1) => d1/d, "/")},
                {'*', new PolishOperator(2, (d, d1) => d1*d, "*")},
                {'^', new PolishOperator(3, (d, d1) => Math.Pow(d1, d),"^")},
                {'(', new PolishOperator(-1, (d, d1) => throw new NotSupportedException(),"(")},
                {')', new PolishOperator(-2,(d, d1) => throw new NotSupportedException(),")")}

            };
            Parse(input);
            ConvertToPolishForm();
            

        }


        //Parsing the string input
        private void Parse(string input)
        {
            var numberBuilder = new StringBuilder();

            foreach (var c in input)
            {
                //Number check, if is number then it will be placed in the number buffer
                if (Char.IsNumber(c))
                {
                    numberBuilder.Append(c);
                }
                else
                {
                    //operator is comming, closing the number buffer and add to the object store
                    if (numberBuilder.Length != 0)
                    {
                        _polishObjects.Add(new PolishValue(Convert.ToDouble(numberBuilder.ToString())));
                        numberBuilder.Clear();
                    }

                    //create the operator object
                    if (_operators.ContainsKey(c))
                    {
                        _polishObjects.Add(_operators[c]);
                    }

                }
            }
            //clean the number buffer
            if (numberBuilder.Length != 0)
            {
                _polishObjects.Add(new PolishValue(Convert.ToDouble(numberBuilder.ToString())));
                numberBuilder.Clear();
            }
        }

        private void ConvertToPolishForm()
        {
            _workingStack.Clear();
            foreach (var polishObject in _polishObjects)
            {
                if (polishObject.PolishObjectType.Equals(PolishObjectType.Operator))
                {
                    var op = polishObject as IPolishOperator;

                    //closing bracket
                    if (op.Priority == -2)
                    {
                        AppendToForm(op.Priority);
                        continue;
                    }
                    //normal operators
                    if (op.Priority > 0)
                    {
                        AppendToForm(op.Priority - 1);
                    }
                    // it will be executed for all operator except the )
                    _workingStack.Push(op);
                    
                }
                else
                {
                    //Numbers are going to the form immediately
                    _polishForm.Add(polishObject);
                }
            }
            //get the last operator
            AppendToForm();
        }

        //helper method
        private void AppendToForm(int priorityLimit = 0)
        {
            if (_workingStack.Count <= 0) return;

            IPolishOperator temp = null;
            var openingBracketFound = false;

            while (_workingStack.Count > 0 && (temp == null || temp.Priority != -1) && (temp = _workingStack.Peek()) != null &&
                   temp.Priority > priorityLimit)
            {
                _workingStack.Pop();
                if(temp.Priority != -1)
                {
                    _polishForm.Add(temp);
                }
                else
                {
                    openingBracketFound = true;
                }
                
            }

            if (priorityLimit == -2 && !openingBracketFound)
            {
                throw new FormatException("Irregular expression");
            }
        }

        //Evaluate the polish form
        public double Evaluate()
        {
            if (_workingStack.Count > 0)
                throw new FormatException("Irregular expression");

           var evalStack = new Stack<IPolishValue>();

            foreach (var polishObject in _polishForm)
            {
                if (polishObject.PolishObjectType.Equals(PolishObjectType.Operator))
                {
                    var op = polishObject as IPolishOperator;

                    if (evalStack.Count < 2)
                        throw new ArgumentException("Cannot evaluate operator with only one argument.");

                    var arg1 = evalStack.Pop();
                    var arg2 = evalStack.Pop();

                    evalStack.Push(new PolishValue(op.Evaulate(arg1.Value, arg2.Value)));
                }
                else
                {
                    evalStack.Push(polishObject as IPolishValue);
                }
            }

            return evalStack.Pop().Value;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder("Original expression:");

            foreach (var polishObject in _polishObjects)
            {
                stringBuilder.Append(polishObject);
            }

            stringBuilder.Append(" Expression in polishform:");

            foreach (var polishObject in _polishForm)
            {
                stringBuilder.Append(polishObject);
            }

            return stringBuilder.ToString();
        }
    }
}
