using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace barangay_crime_complaint_api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        private readonly string apkFilePath = "./uploads/app-debug.apk";

        [HttpGet]
        [Route("api/get")]
        public IActionResult Get()
        {
            return Ok("Hello, World!");
        }

        [HttpGet]
        [Route("api/get-computer-current-ip")]
        public IActionResult GetComputerCurrentIp()
        {
            string hostName = Dns.GetHostName();
            IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName);

            // Find the first IPv4 address
            IPAddress ipv4Address = null;
            foreach (var ipAddress in ipAddresses)
            {
                if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipv4Address = ipAddress;
                    break;
                }
            }

            if (ipv4Address != null)
            {
                return Ok(ipv4Address.ToString());
            }
            else
            {
                return NotFound("Unable to determine the server's IPv4 address.");
            }
        }



        [HttpGet]
        [Route("download")]
        public IActionResult GetApkFile()
        {
            // Validate if the APK file exists
            if (!System.IO.File.Exists(apkFilePath))
            {
                return NotFound("APK file not found");
            }

            // Read the APK file
            var apkFileStream = new FileStream(apkFilePath, FileMode.Open, FileAccess.Read);

            // Return the APK file as a FileStreamResult
            return File(apkFileStream, "application/vnd.android.package-archive", Path.GetFileName(apkFilePath));
        }
    }
}