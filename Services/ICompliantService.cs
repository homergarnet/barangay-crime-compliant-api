using barangay_crime_compliant_api.DTOS;

namespace barangay_crime_compliant_api.Services
{
    public interface ICompliantService
    {
         
        Task<List<long>> CreateCaseReport(List<IFormFile> CrimeImage, long UserId, string CompliantType, string Description, DateTime DateTimeCreated, string CaseType);

    }
}