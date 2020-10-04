using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UL.Calculator.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> GetCalculation([FromQuery] string expression)
        {
            //var result = await _articleServices.CreateArticle(article);

            return Ok("Hello");
        }
    }
}