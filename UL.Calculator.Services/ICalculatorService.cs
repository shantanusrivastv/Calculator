using System.Threading.Tasks;
using UL.Calculator.WebAPI.Model;

namespace UL.Calculator.Services
{
    public interface ICalculatorService
    {
        Task<double> CalculateExpression(InputModel inputModel);

        Task<bool> ValidateInput(InputModel inputModel);
    }
}