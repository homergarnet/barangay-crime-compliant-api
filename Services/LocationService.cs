using barangay_crime_complaint_api.Models;
using barangay_crime_compliant_api.DTOS;
using Microsoft.EntityFrameworkCore;


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

        public List<LocationDto> GetLocationList(long userId, string userType, string status, string keyword, int page, int pageSize)
        {

            IQueryable<Location> query = db.Locations;

            var barangayInfo = db.Users.Where(z => z.Id == userId).FirstOrDefault();
            if(userId != 0L && userType.Equals("barangay")) 
            {
                query = query.Where(z => z.CrimeCompliantReport.User.BrgyCode == barangayInfo.BrgyCode);
            }

            if(userId != 0L && userType.Equals("compliant")) 
            {
                query = query.Where(z => z.CrimeCompliantReport.User.Id == userId);
            }

            List<LocationDto> locationList = new List<LocationDto>();
            if(!string.IsNullOrEmpty(keyword))
            {

                query = query.Where(z => z.CrimeCompliantReportId == Convert.ToInt64(keyword));
                if (status != null)
                {

                    if (!string.IsNullOrEmpty(status) && !status.Equals("completed") && !status.Equals("closed"))
                    {
                        query = query.Where(z => !z.CrimeCompliantReport.Status.Equals("completed") && !z.CrimeCompliantReport.Status.Equals("closed"));
                    }

                    else if (!string.IsNullOrEmpty(status) && status.Equals("completed"))
                    {
                        query = query.Where(z => z.CrimeCompliantReport.Status.Equals(status));
                    }

                    else if (!string.IsNullOrEmpty(status) && status.Equals("closed"))
                    {
                        query = query.Where(z => z.CrimeCompliantReport.Status.Equals(status));
                    }

                }

            }
            else 
            {

                if (status != null)
                {

                    if (!string.IsNullOrEmpty(status) && !status.Equals("completed") && !status.Equals("closed"))
                    {
                        query = query.Where(z => !z.CrimeCompliantReport.Status.Equals("completed") && !z.CrimeCompliantReport.Status.Equals("closed"));
                    }

                    else if (!string.IsNullOrEmpty(status) && status.Equals("completed"))
                    {
                        query = query.Where(z => z.CrimeCompliantReport.Status.Equals(status));
                    }

                    else if (!string.IsNullOrEmpty(status) && status.Equals("closed"))
                    {
                        query = query.Where(z => z.CrimeCompliantReport.Status.Equals(status));
                    }

                }

            }

            var locationRes = query.Include(z => z.CrimeCompliantReport).Skip((page - 1) * pageSize).Take(pageSize).ToList();

            foreach(var location in locationRes)
            {

                var locationDto = new LocationDto();
                locationDto.Id = location.Id;
                locationDto.Lat = location.Lat;
                locationDto.Long = location.Long;
                locationDto.Description = location.CrimeCompliantReport.Description;
                locationDto.DateTimeCreated = location.DateTimeCreated;
                locationDto.DateTimeUpdated = location.DateTimeUpdated;
                locationDto.CrimeCompliantReportId = location.CrimeCompliantReportId;
                locationList.Add(locationDto);
                
            }
            
            return locationList;

        }

        public LocationDto UpdateLocation(long id, long userId, LocationDto locationInfo)
        {

            var hasLocation = db.Locations.Any(z => z.CrimeCompliantReport.User.Id == userId && z.CrimeCompliantReportId == locationInfo.CrimeCompliantReportId);
            if(hasLocation) 
            {

                var location = db.Locations.Where(z => z.CrimeCompliantReport.User.Id == userId && z.CrimeCompliantReportId == locationInfo.CrimeCompliantReportId).First();
                
                location.Lat = locationInfo.Lat;
                location.Long = locationInfo.Long;

                // location.Description = locationInfo.Description;
                location.DateTimeUpdated = DateTime.Now;
                db.SaveChanges();

            }
            
            
            return locationInfo;
        }

    }
}