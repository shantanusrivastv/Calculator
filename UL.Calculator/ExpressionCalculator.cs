using System;
using System.Collections.Generic;
using UL.Calculator.Common;
using UL.Calculator.Common.Extensions;

namespace UL.Calculator
{
    //Shunting-yard algorithm
    public class ExpressionCalculator
    {
        private readonly Dictionary<char, OperatorBase> _operatorsMapping;
        private readonly Stack<double> _numberStack;
        private readonly Stack<char> _operatorStack;

        public ExpressionCalculator()
        {
            _operatorsMapping = new OperatorMapper().GetMapping();
            _numberStack = new Stack<double>();
            _operatorStack = new Stack<char>();
        }

        public double Calculate(string expression)
        {
            expression = expression.Replace(" ", string.Empty); //Removing Whitespace
            //foreach (var expression[i] in expression.ToCharArray())
            for (int i = 0; i < expression.Length; i++)

            {
                if (char.IsDigit(expression[i]))
                {
                    var temp = expression.Substring(i);
                    int endIndex = temp.IndexOfAny(OperatorBase.AllowedOperators) == -1 ? 1 :
                                   temp.IndexOfAny(OperatorBase.AllowedOperators);

                    temp = expression.Substring(i, endIndex);
                    _numberStack.Push(Convert.ToDouble(temp));
                    i = endIndex == 1 ? i : i + endIndex - 1;
                }
                else
                {
                    if (_operatorStack.TryPeek(out char topOperator))
                    {
                        (_operatorsMapping[topOperator].Priority >= _operatorsMapping[expression[i]].Priority
                                                        ? new Action<char>(PushOperatorPostEvaluation)
                                                        : new Action<char>(PushOperator))(expression[i]);
                    }
                    else
                    {
                        PushOperator(expression[i]);
                    }
                }
            }

            //Input is completed, emptying stack and finalising calculation
            return GetFinalValue();
        }

        private void PushOperatorPostEvaluation(char item)
        {
            EvaluateExpression();
            _operatorStack.Push(item);
        }

        private void PushOperator(char item)
        {
            _operatorStack.Push(item);
        }

        private double GetFinalValue()
        {
            while (_operatorStack.Count > 0)
            {
                EvaluateExpression();
            }

            if (_numberStack.Count > 1)
            {
                throw new Exception("Something is wrong");
            }

            return _numberStack.Pop();
        }

        private void EvaluateExpression()
        {
            var rightOperand = _numberStack.Pop();
            var leftOperand = _numberStack.Pop();
            var @operator = _operatorStack.Pop();
            var result = OperatorBase.Evaluate(leftOperand, rightOperand, @operator);
            _numberStack.Push(result);
        }
    }
}