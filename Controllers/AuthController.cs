using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using barangay_crime_complaint_api.Models;
using barangay_crime_compliant_api.DTOS;
using barangay_crime_compliant_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace barangay_crime_compliant_api.Controllers
{
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IAuthService _iAuthService;
        
        public AuthController(IAuthService iAuthService, IConfiguration configuration)
        {
            _iAuthService = iAuthService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("api/create-account")]
        public IActionResult CreateAccount([FromBody] UserDto userInfo)
        {
            try {

                var user = _iAuthService.CreateAccount(userInfo);
               
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = user.Token
                };

            }

            catch (Exception ex)
            {
                return new ContentResult
                {
                    StatusCode = 500,
                    ContentType = "text/html",
                    Content = Common.GetFormattedExceptionMessage(ex)
                };
            }

        }

        [HttpPost]
        [Route("api/login")]
        public IActionResult Login([FromBody] LoginDto loginInfo)
        {
            try {

                var loginRes = _iAuthService.Login(loginInfo);
               
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = loginRes
                };

            }

            catch (Exception ex)
            {
                return new ContentResult
                {
                    StatusCode = 500,
                    ContentType = "text/html",
                    Content = Common.GetFormattedExceptionMessage(ex)
                };
            }

        }


    }
}