using System.Linq;
using System.Text.RegularExpressions;
using UL.Calculator.Common;

namespace UL.Calculator.Validators
{
    public class ExpressionValidator : IExpressionValidator
    {
        private static readonly char[] AllowedOperators = OperatorBase.AllowedOperators;

        public bool IsValid(string expression)
        {
            expression = expression.Replace(" ", string.Empty); //Removing Whitespace
            var IsNullOrEmpty = string.IsNullOrEmpty(expression);
            var startsAndEndWithDigit = new Regex(@"^[\d](.*[\d])?$");
            var hasConsecutiveOperators = new Regex(@"[\W]{2,}");
            var containsUnSupportedOperators =
                                        expression.ToCharArray().Except(AllowedOperators)
                                       .Where((x) => !char.IsDigit(x));

            //If any of the prior validation fails we do not have to check next validation
            if (IsNullOrEmpty ||
               !startsAndEndWithDigit.IsMatch(expression) ||
               hasConsecutiveOperators.IsMatch(expression) ||
               containsUnSupportedOperators.Any())

            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}