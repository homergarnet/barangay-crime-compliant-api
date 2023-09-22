using System.Text.Json;
using barangay_crime_compliant_api.DTOS;
using barangay_crime_compliant_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace barangay_crime_compliant_api.Controllers
{
    public class AnnouncementController : ControllerBase
    {


        private readonly IConfiguration _configuration;
        private readonly IAnnouncementService _iAnnouncementService;

        public AnnouncementController(IAnnouncementService iAnnouncementService, IConfiguration configuration)
        {

            _iAnnouncementService = iAnnouncementService;
            _configuration = configuration;

        }

        [Authorize]
        [HttpPost]
        [Route("api/create-announcement")]
        public IActionResult CreateAccount([FromBody] AnnouncementDescription announcementDescriptionInfo)
        {


            try {

                var userId = Convert.ToInt64(User.FindFirst("UserId").Value);
                announcementDescriptionInfo.UserId = userId;
                var createAnnouncement = _iAnnouncementService.CreateAnnouncement(announcementDescriptionInfo);
               
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = JsonSerializer.Serialize(createAnnouncement)
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
        [Route("api/get-announcement")]
        public IActionResult GetAnnouncement(
            [FromQuery] string keyword, [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10
        )
        {

            try {

                var getAnnouncement = _iAnnouncementService.GetAnnouncement(keyword, page, pageSize);
               
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = JsonSerializer.Serialize(getAnnouncement)
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
        [Route("api/update-announcement")]
        public IActionResult UpdateAnnouncement(
            [FromQuery] long id,

            [FromBody] AnnouncementDescription announcementDescriptionInfo
        )
        {

            try {

                var userId = Convert.ToInt64(User.FindFirst("UserId").Value);

                var updateAnnouncement = _iAnnouncementService.UpdateAnnouncement(id, userId, announcementDescriptionInfo);
               
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = JsonSerializer.Serialize(updateAnnouncement)
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
        [Route("api/soft-remove-announcement")]
        public IActionResult SoftRemoveAnnouncement(
            [FromQuery] long id
            
        )
        {

            try {

                var userId = Convert.ToInt64(User.FindFirst("UserId").Value);
                var softRemoveAnnouncement = _iAnnouncementService.SoftRemoveAnnouncement(id, userId);
               
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = softRemoveAnnouncement
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