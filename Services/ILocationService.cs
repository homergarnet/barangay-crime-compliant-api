

using barangay_crime_compliant_api.DTOS;

namespace barangay_crime_compliant_api.Services
{
    public interface ILocationService
    {
         
        LocationDto CreateLocation(LocationDto locationInfo);
        List<LocationDto> GetLocationList(long userId, string userType, string keyword, int page, int pageSize);
        LocationDto UpdateLocation(long id, long userId, LocationDto locationInfo);
    
    }
}