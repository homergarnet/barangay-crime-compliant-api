using System.Text.Json;
using barangay_crime_compliant_api.DTOS;
using barangay_crime_compliant_api.Services;
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

        [HttpPost]
        [Route("api/create-announcement")]
        public IActionResult CreateAccount([FromBody] AnnouncementDto announcementInfo)
        {


            try {

                var createAnnouncement = _iAnnouncementService.CreateAnnouncement(announcementInfo);
               
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

        [HttpGet]
        [Route("api/get-announcement")]
        public IActionResult GetAnnouncementList(
            [FromQuery] string keyword, [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10
        )
        {

            try {

                var getAnnouncement = _iAnnouncementService.GetAnnouncementList(keyword, page, pageSize);
               
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

        [HttpPut]
        [Route("api/update-announcement")]
        public IActionResult UpdateAnnouncement(
            [FromQuery] long id,
            [FromQuery] long userId,
            [FromBody] AnnouncementDto announcementInfo
        )
        {

            try {

                var updateAnnouncement = _iAnnouncementService.UpdateAnnouncement(id, userId, announcementInfo);
               
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

        [HttpPut]
        [Route("api/soft-remove-announcement")]
        public IActionResult SoftRemoveAnnouncement(
            [FromQuery] long id,
            [FromQuery] long userId
            
        )
        {

            try {

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