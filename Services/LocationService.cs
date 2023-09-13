using barangay_crime_complaint_api.Models;
using barangay_crime_compliant_api.DTOS;


namespace barangay_crime_compliant_api.Services
{
    public class LocationService : ILocationService
    {

        private readonly IConfiguration configuration;
        private readonly Thesis_CrimeContext db;
        public LocationService(IConfiguration configuration, Thesis_CrimeContext db)
        {
            this.configuration = configuration;
            this.db = db;
        }

        public LocationDto CreateLocation(LocationDto locationInfo)
        {

            Location location = new Location();
            location.Lat = locationInfo.Lat;
            location.Long = locationInfo.Long;
            location.Description = locationInfo.Description;
            location.DateTimeCreated = DateTime.Now;
            location.CrimeCompliantReportId = locationInfo.CrimeCompliantReportId;
            db.Locations.Add(location);
            db.SaveChanges();
            return locationInfo;

        }

        public List<LocationDto> GetLocationList(string keyword, int page, int pageSize)
        {

            IQueryable<Location> query = db.Locations;
            List<LocationDto> locationList = new List<LocationDto>();
            if(!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(z => z.Description.Contains(keyword));
            }

            var locationRes = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            foreach(var location in locationRes)
            {

                var locationDto = new LocationDto();
                locationDto.Id = location.Id;
                locationDto.Lat = location.Lat;
                locationDto.Long = location.Long;
                locationDto.Description = location.Description;
                locationDto.DateTimeCreated = location.DateTimeCreated;
                locationDto.DateTimeUpdated = location.DateTimeUpdated;
                locationDto.CrimeCompliantReportId = location.CrimeCompliantReportId;
                locationList.Add(locationDto);
                
            }
            
            return locationList;

        }
    }
}