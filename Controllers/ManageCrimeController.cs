using System.Text.Json;
using barangay_crime_complaint_api.Models;
using barangay_crime_compliant_api.Services;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        [Route("api/get-manage-crime")]
        public IActionResult GetManageCrimeList(
            [FromQuery] string reportType,
            [FromQuery] string keyword, [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10
        )
        {

            try {

                var getManageCrimeList = _iManageCrimeService.GetManageCrimeList(reportType, keyword, page, pageSize);
               
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


        [Authorize]
        [HttpGet]
        [Route("api/get-manage-crime-by-id")]
        public IActionResult GetManageCrimeById(
            [FromQuery] long id,
            [FromQuery] string reportType
        )
        {

            try {

                var getManageCrimeRes = _iManageCrimeService.GetManageCrimeById(id, reportType);
               
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


        [Authorize]
        [HttpPut]
        [Route("api/update-crime-status")]
        public IActionResult UpdateCrimeStatus(
            [FromQuery] long id,
            [FromQuery] string status
        )
        {

            try {
                var userId = Convert.ToInt64(User.FindFirst("UserId").Value);
                var updateCrimeImage = _iManageCrimeService.UpdateCrimeStatus(id, userId, status);
               
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = updateCrimeImage
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


        [Authorize]
        [HttpPut]
        [Route("api/update-crime-resolution")]
        public IActionResult UpdateCrimeResolution(
            [FromQuery] long id,
            [FromQuery] string resolution
        )
        {

            try {
                
                var userId = Convert.ToInt64(User.FindFirst("UserId").Value);
                var updateCrimeImage = _iManageCrimeService.UpdateCrimeResolution(id, userId, resolution);
               
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = updateCrimeImage
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
