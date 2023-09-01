using barangay_crime_compliant_api.DTOS;

namespace barangay_crime_compliant_api.Services
{

    public interface IManageCrimeService
    {
        // GetManageIncidentReport();
        List<ManageCrimeDto> GetManageCrimeList(string keyword, int page, int pageSize);
        ManageCrimeDto GetManageCrimeById(long id);
    }

}