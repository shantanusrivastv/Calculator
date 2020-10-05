using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UL.Calculator.WebAPI.Controllers
{/// <summary>
/// Calculator API
/// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        /// <summary>
        /// Get Mathematical Expression
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /CalculateExpression?expression
        ///     {
        ///         "4+5*2"
        ///     }
        /// Return Output: 14
        ///
        /// The input should be non-negative integers, valid mathematical expression only!
        /// </remarks>
        /// <param name="expression"></param>
        /// <returns>Mathematical Evaluation of expression</returns>
        /// <response code="200">Returns successfully Calculated value</response>
        /// <response code="400">For Invalid Input</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ApiConventionMethod(typeof(DefaultApiConventions),
        //             nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> CalculateExpression([FromBody] string expression)
        {
            //var result = await _articleServices.CreateArticle(article);

            return Ok("Hello");
        }
    }
}