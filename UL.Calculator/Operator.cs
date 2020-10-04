using UL.Calculator.Common;

namespace UL.Calculator
{
    public partial class ExpressionCalculator
    {
        public class Operator
        {
            public OperatorPriority Priority { get; set; }
            public Precedence Precedence { get; set; } = Precedence.LeftToRight;
        }
    }
}