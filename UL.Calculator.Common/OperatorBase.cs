using System;
using System.Diagnostics.CodeAnalysis;

namespace UL.Calculator.Common
{
    [ExcludeFromCodeCoverage]
    public class OperatorBase
    {
        public OperatorPriority Priority { get; set; }

        public OperatorPrecedence Precedence { get; set; } = OperatorPrecedence.LeftToRight; //for future use

        public static char[] AllowedOperators => new[] { '+', '-', '/', '*' };

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