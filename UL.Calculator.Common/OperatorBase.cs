using System;

namespace UL.Calculator.Common
{
    public class OperatorBase
    {
        public OperatorPriority Priority { get; set; }
        public Precedence Precedence { get; set; } = Precedence.LeftToRight; //for future use

        public static double Evaluate(double leftOperand, double rightOperand, char @operator)
        {
            return @operator switch
            {
                '+' => leftOperand + rightOperand,
                '-' => leftOperand - rightOperand,
                '*' => leftOperand * rightOperand,
                '/' => leftOperand / rightOperand,
                _ => throw new Exception("invalid Operator"),//Added for safety case, validation will prevent this.
            };
        }

    }
}