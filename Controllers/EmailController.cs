using barangay_crime_compliant_api.DTOS;
using barangay_crime_compliant_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace barangay_crime_compliant_api.Controllers
{
    public class EmailController : ControllerBase
    {


        private readonly IConfiguration _configuration;
        private readonly IEmailService _iEmailService;

        public EmailController(IEmailService iEmailService, IConfiguration configuration)
        {

            _iEmailService = iEmailService;
            _configuration = configuration;

        }

        [HttpPost("api/send")]
        public async Task<IActionResult> SendEmail([FromBody] ForgotDto model)
        {
            // Validate model and parameters
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Send the email
            try
            {
                var res = await _iEmailService.SendForgotEmailAsync(model.ToEmail);
                if (res == "Email sent successfully")
                {
                    return new ContentResult
                    {
                        StatusCode = 200,
                        ContentType = "application/json",
                        Content = res
                    };
                }
                else
                {
                    return new ContentResult
                    {
                        StatusCode = 500,
                        ContentType = "application/json",
                        Content = res
                    };
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}