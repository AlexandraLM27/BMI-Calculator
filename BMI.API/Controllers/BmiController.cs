using Microsoft.AspNetCore.Mvc;
using BMI.API.Models;
using BMI.API.Services;

namespace BMI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BmiController : ControllerBase
    {
        [HttpPost]
        public IActionResult CalculateBmi([FromBody] BmiRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = BmiCalculator.Calculate(request.HeightCm, request.WeightKg);
            return Ok(result);
        }
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("API merge!");
        }

    }
}
