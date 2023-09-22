

using System.Text.Json;
using barangay_crime_compliant_api.DTOS;
using barangay_crime_compliant_api.Services;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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

        [Authorize]
        [HttpGet]
        [Route("api/get-location")]
        public IActionResult GetLocationList(
            [FromQuery] string keyword, [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10
        )
        {

            try {


                var userId = Convert.ToInt64(User.FindFirst("UserId").Value);
                var userType = Convert.ToString(User.FindFirst("UserType").Value);
                
                
                if(userType.Equals("barangay") || userType.Equals("compliant"))
                {

                    var getLocationList = _iLocationService.GetLocationList(userId, userType, keyword, page, pageSize);
                    return new ContentResult
                    {
                        StatusCode = 200,
                        ContentType = "application/json",
                        Content = JsonSerializer.Serialize(getLocationList)
                    };

                }
                else 
                {

                    var getLocationList = _iLocationService.GetLocationList(0L, userType, keyword, page, pageSize);
                    return new ContentResult
                    {
                        StatusCode = 200,
                        ContentType = "application/json",
                        Content = JsonSerializer.Serialize(getLocationList)
                    };
                    
                }


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
        [Route("api/update-location")]
        public IActionResult UpdateLocation(
            [FromQuery] long id,
            [FromBody] LocationDto locationInfo
        )
        {

            try {

                var userId = Convert.ToInt64(User.FindFirst("UserId").Value);

                var updateLocation = _iLocationService.UpdateLocation(id, userId, locationInfo);
               
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = JsonSerializer.Serialize(updateLocation)
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