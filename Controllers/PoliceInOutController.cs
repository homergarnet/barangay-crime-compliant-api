using System.Text.Json;
using barangay_crime_complaint_api.Models;
using barangay_crime_compliant_api.DTOS;
using barangay_crime_compliant_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace barangay_crime_compliant_api.Controllers
{
    public class PoliceInOutController : ControllerBase
    {
        private readonly IPoliceInOutService _iPoliceInOutService;
        private readonly Thesis_CrimeContext db;

        public PoliceInOutController(IPoliceInOutService iPoliceInOutService, Thesis_CrimeContext db)
        {
            _iPoliceInOutService = iPoliceInOutService;
            this.db = db;
        }

        [Authorize]
        [HttpPost]
        [Route("api/create-police-in-out")]
        public IActionResult CreatePoliceInOut([FromBody] PoliceInOutDto policeInOutReq)
        {


            try
            {

                var userId = Convert.ToInt64(User.FindFirst("UserId").Value);
                var userType = User.FindFirst("UserType").Value;
                if (userType.Equals("police"))
                {
                    policeInOutReq.UserId = userId;
                    var createPoliceInOut = _iPoliceInOutService.CreatePoliceInOut(policeInOutReq);

                    return new ContentResult
                    {
                        StatusCode = 200,
                        ContentType = "application/json",
                        Content = JsonSerializer.Serialize(createPoliceInOut)
                    };
                }
                else
                {
                    return new ContentResult
                    {
                        StatusCode = 404,
                        ContentType = "application/json",
                        Content = "Not police account"
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
        [HttpGet]
        [Route("api/get-police-in-out")]
        public IActionResult GetPoliceInOutList(
            [FromQuery] string keyword, [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10
        )
        {

            try {

                var getPoliceInOutList = _iPoliceInOutService.GetPoliceInOutList(keyword, page, pageSize);
               
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = JsonSerializer.Serialize(getPoliceInOutList)
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