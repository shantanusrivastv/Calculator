using System.Threading.Tasks;
using UL.Calculator.Core;
using UL.Calculator.Core.Model;
using UL.Calculator.Validators;

namespace UL.Calculator.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly IExpressionValidator _validator;
        private readonly IExpressionCalculator _calculator;

        public CalculatorService(IExpressionValidator validator,
                                 IExpressionCalculator calculator)
        {
            _validator = validator;
            _calculator = calculator;
        }

        public Task<bool> ValidateInput(InputModel input)
        {
            return Task.FromResult(_validator.IsValid(input.Expression));
        }

        public Task<double> CalculateExpression(InputModel input)
        {
            return Task.FromResult(_calculator.Calculate(input.Expression));
        }
    }
}