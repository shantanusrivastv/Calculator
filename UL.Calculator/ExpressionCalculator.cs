using System;
using System.Collections.Generic;

namespace UL.Calculator
{
    //Shunting-yard algorithm
    public partial class ExpressionCalculator
    {
        private Dictionary<char, Operator> operatorsMapping;
        private readonly Stack<double> numberStack;
        private readonly Stack<char> operatorStack;

        public ExpressionCalculator()
        {
            SetUpMapping();
            numberStack = new Stack<double>();
            operatorStack = new Stack<char>();
        }

        public double Calculate(string expression)
        {
            expression = expression.Replace(" ", string.Empty); //Removing Whitespace
            foreach (var item in expression.ToCharArray())
            {
                if (char.IsDigit(item))
                {
                    var test = char.GetNumericValue(item);
                    var tes1 = item - '0';
                    numberStack.Push(tes1);
                }
                else
                {
                    if (operatorStack.TryPeek(out char topOperator))
                    {
                        (operatorsMapping[topOperator].Priority >= operatorsMapping[item].Priority
                                                        ? new Action<char>(EvaluateExpression)
                                                        : new Action<char>(PushOperator))(item);
                    }
                    else
                    {
                        PushOperator(item);
                    }
                }
            }

            var final = StartCalculation();
            return final;
        }

        private double StartCalculation()
        {
            while (operatorStack.Count > 0)
            {
                var rightOperand = numberStack.Pop();
                var leftOperand = numberStack.Pop();
                var oprator = operatorStack.Pop();
                var result = Evaluate(leftOperand, rightOperand, oprator);
                numberStack.Push(result);
            }

            if (numberStack.Count > 1)
            {
                throw new Exception("Something is wrong");
            }

            return numberStack.Pop();
        }

        private void PushOperator(char item)
        {
            operatorStack.Push(item);
        }

        private void EvaluateExpression(char item)
        {
            var rightOperand = numberStack.Pop();
            var leftOperand = numberStack.Pop();
            var oprator = operatorStack.Pop();
            var result = Evaluate(leftOperand, rightOperand, oprator);
            numberStack.Push(result);
            operatorStack.Push(item);
        }

        private double Evaluate(double leftOperand, double rightOperand, char oprator)
        {
            return oprator switch
            {
                '+' => leftOperand + rightOperand,
                '-' => leftOperand - rightOperand,
                '*' => leftOperand * rightOperand,
                '/' => leftOperand / rightOperand,
                _ => throw new Exception("invalid Operator"),//Added for safety case, validation will prevent this.
            };
        }

        public void SetUpMapping()
        {
            operatorsMapping = new Dictionary<char, Operator>()
            {
                {  '+' , new Operator() { Priority = OperatorPriority.Addition  } },
                {  '-' , new Operator() { Priority = OperatorPriority.Substraction  } },
                {  '/' , new Operator() { Priority = OperatorPriority.Division  } },
                {  '*' , new Operator() { Priority = OperatorPriority.Multiplication  } }
            };
        }
    }
}