using Microsoft.AspNetCore.Mvc;

namespace barangay_crime_complaint_api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {


        [HttpGet]
        [Route("api/get")]
        public IActionResult Get()
        {
            return Ok("Hello, World!");
        }
    }
}