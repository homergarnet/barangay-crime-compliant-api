using System.Text.Json;
using barangay_crime_compliant_api.DTOS;
using barangay_crime_compliant_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace barangay_crime_compliant_api.Controllers
{
    public class CompliantController : ControllerBase
    {
        private readonly ICompliantService _iCompliantService;
        public CompliantController(ICompliantService iCompliantService)
        {
            _iCompliantService = iCompliantService;
        }

        [HttpPost]
        [Route("api/create-case-report")]
        public async Task<IActionResult> CreateCaseReport([FromForm] List<IFormFile> CrimeImage, [FromForm] long UserId, [FromForm] string Description, [FromForm] DateTime DateTimeCreated, [FromForm] long CrimeCompliantId)
        {
            if (CrimeImage == null || CrimeImage.Count == 0)
            {
                return new ContentResult
                {
                    StatusCode = 500,
                    ContentType = "application/json",
                    Content = "No Image Uploaded"
                };
            }


            try {

                var user = await _iCompliantService.CreateCaseReport(CrimeImage, UserId, Description, DateTimeCreated, CrimeCompliantId);
               
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = JsonSerializer.Serialize(user)
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

        [HttpGet]
        [Route("api/get-crime-compliant")]
        public IActionResult GetCrimeCompliantList(
            [FromQuery] string keyword, [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10
        )
        {

            try {

                var getLocationList = _iCompliantService.GetCrimeCompliantList(keyword, page, pageSize);
               
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = JsonSerializer.Serialize(getLocationList)
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