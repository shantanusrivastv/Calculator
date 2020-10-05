using System.Linq;
using System.Text.RegularExpressions;
using UL.Calculator.Common;

namespace UL.Calculator
{
    public class ExpressionValidator
    {
        private static readonly char[] AllowedOperators = OperatorBase.AllowedOperators;

        public bool Validate(string expression)
        {
            //Check for non-emty
            //Todo move into one Regx
            if (IsFirstCharacterValid(expression)) return false;
            if (IsConsecutiveOpertorExist(expression)) return false;
            if (IsLastCharacterValid(expression)) return false;
            var test = expression.ToCharArray().Except(AllowedOperators)
                                  .Where((x) => !char.IsDigit(x)).ToList();
            return !test.Any();
        }

        private bool IsFirstCharacterValid(string expression)
        {
            return !char.IsDigit(expression[0]);
        }

        private bool IsLastCharacterValid(string expression)
        {
            return !char.IsSymbol(expression[^1]);
        }

        private bool IsConsecutiveOpertorExist(string expression)
        {
            var regex = new Regex(@"[\W]{2,}");
            return regex.IsMatch(expression);
        }
    }
}