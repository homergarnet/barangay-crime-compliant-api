using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace barangay_crime_complaint_api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        private readonly string _apkFolderPath;
        public TestController()
        {
            _apkFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        }

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
        [Route("api/download")]
        public IActionResult GetApkFile()
        {
            var fileName = "app-debug.apk";
            var filePath = Path.Combine(_apkFolderPath, fileName);
            if (System.IO.File.Exists(filePath))
            {
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var contentType = "application/vnd.android.package-archive"; // APK MIME type
                return File(fileStream, contentType, fileName);
            }
            else
            {
                return NotFound(); // File not found
            }
        }
    }
}