using System.Text.Json;
using barangay_crime_complaint_api.Models;
using barangay_crime_compliant_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace barangay_crime_compliant_api.Controllers
{
    public class ManageCrimeController : ControllerBase
    {

        private readonly IManageCrimeService _iManageCrimeService;
 
        public ManageCrimeController(IManageCrimeService iManageCrimeService)
        {
            _iManageCrimeService = iManageCrimeService;
        
        }

        [HttpGet]
        [Route("api/get-manage-crime")]
        public IActionResult GetManageCrimeList(
            [FromQuery] string keyword, [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10
        )
        {

            try {

                var getManageCrimeList = _iManageCrimeService.GetManageCrimeList(keyword, page, pageSize);
               
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = JsonSerializer.Serialize(getManageCrimeList)
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
        [Route("api/get-manage-crime-by-id")]
        public IActionResult GetManageCrimeById(
            [FromQuery] long id
        )
        {

            try {

                var getManageCrimeRes = _iManageCrimeService.GetManageCrimeById(id);
               
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = JsonSerializer.Serialize(getManageCrimeRes)
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
