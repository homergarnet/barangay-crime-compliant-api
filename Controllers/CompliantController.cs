using System.Text.Json;
using barangay_crime_compliant_api.DTOS;
using barangay_crime_compliant_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace barangay_crime_compliant_api.Controllers
{
    public class CompliantController : ControllerBase
    {
        private readonly ICompliantService _iCompliantService;
        public CompliantController(ICompliantService iCompliantService)
        {
            _iCompliantService = iCompliantService;
        }

        [Authorize]
        [HttpPost]
        [Route("api/create-case-report")]
        public async Task<IActionResult> CreateCaseReport([FromForm] List<IFormFile> CrimeImage, [FromForm] long UserId, [FromForm] string Description, [FromForm] DateTime DateTimeCreated, [FromForm] long CrimeCompliantId)
        {
            if (CrimeImage == null || CrimeImage.Count == 0)
            {
                return new ContentResult
                {
                    StatusCode = 500,
                    ContentType = "application/json",
                    Content = "No Image Uploaded"
                };
            }


            try {

                var user = await _iCompliantService.CreateCaseReport(CrimeImage, UserId, Description, DateTimeCreated, CrimeCompliantId);
               
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = JsonSerializer.Serialize(user)
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
        [Route("api/get-crime-compliant")]
        public IActionResult GetCrimeCompliantList(
            [FromQuery] string keyword, [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10
        )
        {

            try {

                var getLocationList = _iCompliantService.GetCrimeCompliantList(keyword, page, pageSize);
               
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

        [Authorize]
        [HttpGet]
        [Route("api/get-crime-image")]
        public IActionResult GetCrimeImageList(
            [FromQuery] long crimeCompliantReportId,
            [FromQuery] string keyword, [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10
        )
        {

            try {

                var getLocationList = _iCompliantService.GetCrimeImageList(crimeCompliantReportId, keyword, page, pageSize);
               
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

        [Authorize]
        [HttpPut]
        [Route("api/update-crime-image")]
        public IActionResult UpdateCrimeImage(
            [FromQuery] long id,
            [FromQuery] long userId,
 
            [FromForm] IFormFile CrimeImage
        )
        {

            try {

                var updateCrimeImage = _iCompliantService.UpdateCrimeImage(id, userId, CrimeImage);
               
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



        // [HttpGet("api/image/{imageName}")]
        // public IActionResult GetImage(string imageName)
        // {
        //     var imagePath = Path.Combine("uploads", imageName); // Change the path as needed

        //     if (!System.IO.File.Exists(imagePath))
        //     {
        //         return NotFound("Image not found");
        //     }
        //     var contentType = GetContentTypeImage(imageName);
        //     var imageBytes = System.IO.File.ReadAllBytes(imagePath);
        //     return File(imageBytes, contentType); // Adjust the content type as needed
        // }
        // private string GetContentTypeImage(string fileName)
        // {
        //     var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();

        //     switch (fileExtension)
        //     {
        //         case ".jpg":
        //             return "image/jpeg";
        //         case ".jpeg":
        //             return "image/jpeg";
        //         case ".png":
        //             return "image/png";
        //         case ".gif":
        //             return "image/gif";
        //         case ".bmp":
        //             return "image/bmp";
        //         case ".svg":
        //             return "image/svg+xml";
        //         // Add more image formats as needed
        //         default:
        //             return "application/octet-stream"; // Default to binary data if the format is not recognized
        //     }
        // }
    
        // private string GetContentTypeVideo(string fileName)
        // {
        //     var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();

        //     switch (fileExtension)
        //     {
        //         case ".mp4":
        //             return "video/mp4";
        //         case ".webm":
        //             return "video/webm";
        //         case ".ogg":
        //             return "video/ogg";
        //         default:
        //             return "application/octet-stream"; // Default to binary data if the format is not recognized
        //     }
        // }

    }

}