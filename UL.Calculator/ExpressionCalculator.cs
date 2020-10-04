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
            foreach (var item in expression.ToCharArray())
            {
                if (char.IsDigit(item))
                {
                    _numberStack.Push(item.ToDouble());
                }
                else
                {
                    if (_operatorStack.TryPeek(out char topOperator))
                    {
                        (_operatorsMapping[topOperator].Priority >= _operatorsMapping[item].Priority
                                                        ? new Action<char>(PushOperatorPostEvaluation)
                                                        : new Action<char>(PushOperator))(item);
                    }
                    else
                    {
                        PushOperator(item);
                    }
                }
            }

            //Input is completed, emptying stack and finalising calculation 
            return  GetFinalValue();
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