using System;
using System.Collections.Generic;
using UL.Calculator.Common;

namespace UL.Calculator
{
    //Shunting-yard algorithm
    public class ExpressionCalculator : IExpressionCalculator
    {
        private readonly Dictionary<char, OperatorBase> _operatorsMapping;
        private readonly IOperatorMapper _operatorMapper;
        private readonly Stack<double> _numberStack;
        private readonly Stack<char> _operatorStack;

        public ExpressionCalculator(IOperatorMapper operatorsMapping)
        {
            _operatorMapper = operatorsMapping;
            _operatorsMapping = _operatorMapper.GetMapping();
            _numberStack = new Stack<double>();
            _operatorStack = new Stack<char>();
        }

        public double Calculate(string expression)
        {
            expression = expression.Replace(" ", string.Empty); //Removing Whitespace
            StoreInput(expression);
            //Storing Input is completed, emptying the stack and finalising calculation
            return GetFinalValue();
        }

        private void StoreInput(string expression)
        {
            for (int i = 0; i < expression.Length; i++)
            {
                if (char.IsDigit(expression[i]))
                {
                    var temp = expression.Substring(i);
                    int endIndex = GetEndIndex(temp);
                    temp = expression.Substring(i, endIndex);
                    _numberStack.Push(Convert.ToDouble(temp));
                    i = endIndex == 1 ? i : (i + endIndex - 1);
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
        }

        private static int GetEndIndex(string temp)
        {
            //Last single digit
            if (temp.IndexOfAny(OperatorBase.AllowedOperators) == -1 && temp.Length == 1)
            {
                return 1;
            }
            else if (temp.IndexOfAny(OperatorBase.AllowedOperators) != -1)
            {
                return temp.IndexOfAny(OperatorBase.AllowedOperators);
            }
            //Last non-single digit

            return temp.Length;
        }

        private void PushOperatorPostEvaluation(char item)
        {
            while (_operatorStack.Count > 0 && _operatorsMapping[_operatorStack.Peek()].Priority >= _operatorsMapping[item].Priority)
            {
                EvaluateExpression();
            }

            _operatorStack.Push(item);
        }

        //Made this separate method as we might want to add some more logic before pushing item
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