

using barangay_crime_compliant_api.DTOS;

namespace barangay_crime_compliant_api.Services
{
    public interface ILocationService
    {
         
        LocationDto CreateLocation(LocationDto locationInfo);
        List<LocationDto> GetLocationList(string keyword, int page, int pageSize);

    }
}