using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UL.Calculator.Core;
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
        [HttpPost]
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
                return Ok($"Succss the value is : {result} ");
            }

            return BadRequest("Plese check the offcial documentation ");
        }
    }
}