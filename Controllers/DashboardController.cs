using System.Text.Json;
using barangay_crime_compliant_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace barangay_crime_compliant_api.Controllers
{
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _iDashboardService;
        private readonly IAuthService _iAuthService;
        public DashboardController(IDashboardService iDashboardService, IAuthService iAuthService)
        {
            _iDashboardService = iDashboardService;
            _iAuthService = iAuthService;
        }

        [Authorize]
        [HttpGet]
        [Route("api/total-dashboard-card-count")]
        public IActionResult TotalDashboardCardCount()
        {

            try {

                var userId = Convert.ToInt64(User.FindFirst("UserId").Value);
                
                var getUserPersonalInfo = _iAuthService.GetUserPersonalInfoById(userId);
        
                if(getUserPersonalInfo.UserType.Equals("admin") || getUserPersonalInfo.UserType.Equals("police")) 
                {
                    var totalDashboardCount = _iDashboardService.TotalDashboardCardCount(0);
                
                    return new ContentResult
                    {
                        StatusCode = 200,
                        ContentType = "application/json",
                        Content = JsonSerializer.Serialize(totalDashboardCount)
                    };
                }
                //barangay
                else
                {

                    var totalDashboardCount = _iDashboardService.TotalDashboardCardCount(userId,getUserPersonalInfo.BrgyCode);
                
                    return new ContentResult
                    {
                        StatusCode = 200,
                        ContentType = "application/json",
                        Content = JsonSerializer.Serialize(totalDashboardCount)
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
    }
}