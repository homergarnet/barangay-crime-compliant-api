
using barangay_crime_complaint_api.Models;
using barangay_crime_compliant_api.DTOS;

namespace barangay_crime_compliant_api.Services
{

    public interface IBarangayService
    {
        List<BarangayDto> GetBarangayNameByCodeList(string keyword, int page, int pageSize);

    }
}