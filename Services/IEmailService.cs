using barangay_crime_compliant_api.DTOS;

namespace barangay_crime_compliant_api.Services
{
    public interface IEmailService
    {
         
        Task<string> SendForgotEmailAsync(string toEmail);

    }
}