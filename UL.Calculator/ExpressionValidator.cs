using System.Linq;
using System.Text.RegularExpressions;

namespace UL.Calculator
{
    public class ExpressionValidator
    {
        private static readonly char[] allowedOperators = { '+', '-', '/', '*' };

        public bool Validate(string expression)
        {
            //Todo move into one Regx
            if (IsFirstCharacterValid(expression)) return false;
            if (IsConsecutiveOpertorExist(expression)) return false;
            if (IsLastCharacterValid(expression)) return false;
            var test = expression.ToCharArray().Except(allowedOperators)
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