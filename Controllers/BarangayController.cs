using Microsoft.AspNetCore.Mvc;

namespace barangay_crime_compliant_api.Controllers
{
    public class BarangayController : ControllerBase
    {
        
        public BarangayController() 
        {
            
        }

        // [HttpPost]
        // [Route("api/create-case-report")]
        // public async Task<IActionResult> CreateCaseReport([FromForm] ImageUploadModel model)
        // {
        //     if (model.Image == null || model.Image.Length == 0)
        //     {
        //         return BadRequest("Invalid image data");
        //     }

        //     using (var memoryStream = new MemoryStream())
        //     {
        //         await model.Image.CopyToAsync(memoryStream);
        //         var image = new Image
        //         {
        //             Title = model.Title,
        //             Data = memoryStream.ToArray()
        //         };

        //         await _dbContext.Images.AddAsync(image);
        //         await _dbContext.SaveChangesAsync();

        //         return Ok(image.Id);
        //     }
        // }   

        // [HttpPost]
        // [Route("api/create-case-report")]
        // public IActionResult CreateCaseReport([FromBody] UserDto userInfo)
        // {
        //     try {

        //         var user = _iAuthService.CreateAccount(userInfo);
               
        //         return new ContentResult
        //         {
        //             StatusCode = 200,
        //             ContentType = "application/json",
        //             Content = user.Token
        //         };

        //     }

        //     catch (Exception ex)
        //     {
        //         return new ContentResult
        //         {
        //             StatusCode = 500,
        //             ContentType = "text/html",
        //             Content = Common.GetFormattedExceptionMessage(ex)
        //         };
        //     }

        // }

    }
}