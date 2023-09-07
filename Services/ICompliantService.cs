using barangay_crime_compliant_api.DTOS;

namespace barangay_crime_compliant_api.Services
{
    public interface ICompliantService
    {
         
        Task<List<long>> CreateCaseReport(List<IFormFile> CrimeImage, long UserId, string Description, DateTime DateTimeCreated, long CrimeCompliantId);
        List<CrimeCompliantDto> GetCrimeCompliantList(string keyword, int page, int pageSize);
        List<CrimeImageDto> GetCrimeImageList(long crimeCompliantReportId, string keyword, int page, int pageSize);
        string UpdateCrimeImage(long id, long userId, IFormFile CrimeImage);
    }
}