using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace barangay_crime_compliant_api.Controllers
{
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _iDashboardService;
        public DashboardController(IDashboardService iDashboardService)
        {
            _iDashboardService = iDashboardService;
        }

        [HttpGet]
        [Route("api/total-dashboard-card-count")]
        public IActionResult TotalDashboardCardCount()
        {

            try {

                var totalDashboardCount = _iDashboardService.TotalDashboardCardCount();
               
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = JsonSerializer.Serialize(totalDashboardCount)
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