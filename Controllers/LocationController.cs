

using System.Text.Json;
using barangay_crime_compliant_api.DTOS;
using barangay_crime_compliant_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace barangay_crime_compliant_api.Controllers
{
    public class LocationController : ControllerBase
    {



        private readonly ILocationService _iLocationService;

        public LocationController(ILocationService iLocationService)
        {

            _iLocationService = iLocationService;

        }

        [HttpPost]
        [Route("api/create-location")]
        public IActionResult CreateLocation([FromBody] LocationDto locationInfo)
        {


            try {

                var createLocation = _iLocationService.CreateLocation(locationInfo);
            
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = JsonSerializer.Serialize(createLocation)
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
        [Route("api/get-location")]
        public IActionResult GetLocationList(
            [FromQuery] string keyword, [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10
        )
        {

            try {

                var getLocationList = _iLocationService.GetLocationList(keyword, page, pageSize);
               
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