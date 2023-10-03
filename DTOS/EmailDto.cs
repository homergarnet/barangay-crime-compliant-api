using System.ComponentModel.DataAnnotations;

namespace barangay_crime_compliant_api.DTOS
{
    public class SmtpSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }

    public class ForgotDto
    {
        [Required]
        [EmailAddress]
        public string ToEmail { get; set; }

        // [Required]
        // public string Subject { get; set; }

        // [Required]
        // public string Body { get; set; }
    }

}