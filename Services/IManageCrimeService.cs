using barangay_crime_compliant_api.DTOS;

namespace barangay_crime_compliant_api.Services
{

    public interface IManageCrimeService
    {
        // GetManageIncidentReport();
        List<ManageCrimeDto> GetManageCrimeList(string reportType, string status, long userId, string userType, string keyword, int page, int pageSize);
        long ManageCrimeCount(string reportType, long userId, string userType);
        ManageCrimeDto GetManageCrimeById(long id, string reportType);
        string UpdateCrimeStatus(long id, long responderId, string responderDescription, string status);
        string UpdateCrimeResolution(long id, long userId, string resolution);
    }

}