using System.Text.Json;
using barangay_crime_compliant_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace barangay_crime_compliant_api.Controllers
{
    public class BarangayController : ControllerBase
    {

        private readonly IBarangayService _iBarangayService;
        
        public BarangayController(IBarangayService iBarangayService) 
        {

            _iBarangayService = iBarangayService;

        }

        [Authorize]
        [HttpGet]
        [Route("api/get-barangay-by-code")]
        public IActionResult GetBarangayList(
            [FromQuery] string keyword, [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10
        )
        {

            try {

                var getBarangayList = _iBarangayService.GetBarangayNameByCodeList(keyword, page, pageSize);
               
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = JsonSerializer.Serialize(getBarangayList)
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