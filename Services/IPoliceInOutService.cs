using barangay_crime_compliant_api.DTOS;

namespace barangay_crime_compliant_api.Services
{
    public interface IPoliceInOutService
    {
        PoliceInOutDto CreatePoliceInOut(PoliceInOutDto policeInOutReq);
        List<PoliceInOutDto> GetPoliceInOutList(string keyword, int page, int pageSize);
    }
}