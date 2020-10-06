using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UL.Calculator.Core.Model;
using UL.Calculator.Services;

namespace UL.Calculator.WebAPI.Controllers
{/// <summary>
/// Calculator API
/// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorController(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        /// <summary>
        /// Get Mathematical Expression
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Calculator
        ///     {
        ///        "Expression":  "4+5*2"
        ///     }
        /// Return Output: 14
        ///
        /// The input should be non-negative integers, valid mathematical expression only!
        /// </remarks>
        /// <param name="inputModel"></param>
        /// <returns>Mathematical Evaluation of expression</returns>
        /// <response code="200">Returns successfully Calculated value</response>
        /// <response code="400">For Invalid Input</response>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ApiConventionMethod(typeof(DefaultApiConventions),
        //             nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> CalculateExpression([FromBody] InputModel inputModel)
        {
            var isValidInput = await _calculatorService.ValidateInput(inputModel);

            if (isValidInput)
            {
                var result = await _calculatorService.CalculateExpression(inputModel);
                return Ok($"Calculation is executed successfully, the value is : {result} ");
            }

            return BadRequest("Invalid Input, Please check the official documentation ");
        }

        /// <summary>
        /// Premium Calculator
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Calculator/PremiumCalculator
        ///     {
        ///        "Expression":  "4+5*2"
        ///     }
        /// Return Output: 14
        ///
        /// </remarks>
        /// <param name="inputModel"></param>
        /// <returns>Mathematical Evaluation of expression</returns>
        /// <response code="200">Returns successfully Calculated value</response>
        /// <response code="400">For Invalid Input</response>
        /// <response code="401">For Invalid Users/ Token</response>
        /// <response code="401">For Non Premium Users</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Premium")]
        [HttpPost("/[action]")]
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage] //Since its not implemented yet
        public IActionResult PremiumCalculator([FromBody] InputModel inputModel)
        {
            return Ok($"The calculated value is {14d}, Thank you for using our {nameof(PremiumCalculator)} ");
        }
    }
}