using barangay_crime_complaint_api.Models;
using barangay_crime_compliant_api.DTOS;
using Microsoft.EntityFrameworkCore;

namespace barangay_crime_compliant_api.Services
{
    public class ManageCrimeService : IManageCrimeService
    {

        private readonly IConfiguration configuration;
        private readonly Thesis_CrimeContext db;
        public ManageCrimeService(IConfiguration configuration, Thesis_CrimeContext db)
        {
            this.configuration = configuration;
            this.db = db;
        }
        public List<ManageCrimeDto> GetManageCrimeList(string reportType, string keyword, int page, int pageSize)
        {
            IQueryable<CrimeCompliantReport> query = db.CrimeCompliantReports;
            var locationQuery = db.Locations;
            var barangayQuery = db.PhBrgies;
            List<ManageCrimeDto> manageCrimeList = new List<ManageCrimeDto>();
            if(!string.IsNullOrEmpty(keyword))
            {

                if(reportType != null)
                {
                    if(!string.IsNullOrEmpty(reportType) && reportType.Equals("compliant types"))
                    {
                        query = query.Where(z => z.Description.Contains(keyword) || z.Status.Contains(keyword) || z.CrimeCompliant.Type.Equals("compliant types"));
                    }
                    if(reportType.Equals("index crime") || reportType.Equals("non index crime"))
                    {
                        query = query.Where(z => z.Description.Contains(keyword) || z.Status.Contains(keyword) || z.CrimeCompliant.Type.Equals("index crime") || z.CrimeCompliant.Type.Equals("non index crime"));
                    }
                    else
                    {
                        query = query.Where(z => z.Description.Contains(keyword) || z.Status.Contains(keyword));
                    }
                }

            }
            else 
            {

                if(reportType != null)
                {
                    if(!string.IsNullOrEmpty(reportType) && reportType.Equals("compliant types"))
                    {
                        query = query.Where(z => z.CrimeCompliant.Type.Equals(reportType));
                    }
                    if(reportType.Equals("index crime") || reportType.Equals("non index crime"))
                    {
                        query = query.Where(z => z.CrimeCompliant.Type.Equals("index crime") || z.CrimeCompliant.Type.Equals("non index crime"));
                    }
                }

            }

            var manageCrimeReportRes = query.Include(z => z.User).Include(z => z.CrimeCompliant)
            .Skip((page - 1) * pageSize).Take(pageSize).ToList();

            foreach(var manageCrime in manageCrimeReportRes)
            {

                var manageCrimeDto = new ManageCrimeDto();
                string formattedIdValue = manageCrime.Id.ToString("D3");
                manageCrimeDto.ReportId = manageCrime.Id;
                manageCrimeDto.ReportIdStr = formattedIdValue;
                manageCrimeDto.Barangay = barangayQuery.Where(z => z.BrgyCode == manageCrime.User.BrgyCode).Select(z => z.BrgyName).FirstOrDefault();
                manageCrimeDto.Date = manageCrime.DateTimeCreated.Value;
                manageCrimeDto.Time = manageCrime.DateTimeCreated.Value.TimeOfDay;
                manageCrimeDto.Lat =  locationQuery.Where(z => z.CrimeCompliantReportId == manageCrime.Id).Select(z => z.Lat).FirstOrDefault();
                manageCrimeDto.Long = locationQuery.Where(z => z.CrimeCompliantReportId == manageCrime.Id).Select(z => z.Long).FirstOrDefault();
                manageCrimeDto.Category = manageCrime.CrimeCompliant.Title;
                manageCrimeDto.Description = manageCrime.Description;
                manageCrimeDto.ReporterName = manageCrime.User.FirstName + " " + manageCrime.User.MiddleName + " " + manageCrime.User.LastName;
                manageCrimeDto.ReporterContact = manageCrime.User.Phone;
                manageCrimeDto.Status = manageCrime.Status;
                manageCrimeDto.Resolution = manageCrime.Resolution;
                manageCrimeList.Add(manageCrimeDto);
                
            }
            
            return manageCrimeList;
        }

        public ManageCrimeDto GetManageCrimeById(long id, string reportType)
        {
            IQueryable<CrimeCompliantReport> query = db.CrimeCompliantReports;
            if(reportType != null)
            {

                if(!string.IsNullOrEmpty(reportType) && reportType.Equals("compliant types"))
                {
                    query = query.Where(z => z.CrimeCompliant.Type.Equals(reportType));
                }
                if(reportType.Equals("index crime") || reportType.Equals("non index crime"))
                {
                    query = query.Where(z => z.CrimeCompliant.Type.Equals("index crime") && z.CrimeCompliant.Type.Equals("non index crime"));
                }
                
            }

            var locationQuery = db.Locations;
            var barangayQuery = db.PhBrgies;
            ManageCrimeDto manageCrimeRes = new ManageCrimeDto();

            var hasManageCrimeReportRes = db.CrimeCompliantReports.Any(z => z.Id == id);
            if(hasManageCrimeReportRes) 
            {
                
                var manageCrimeReportRes = query.Include(z => z.User).Include(z => z.CrimeCompliant).Where(z => z.Id == id).FirstOrDefault();
                string formattedIdValue = manageCrimeReportRes.Id.ToString("D3");
                manageCrimeRes.ReportId = manageCrimeReportRes.Id;
                manageCrimeRes.ReportIdStr = formattedIdValue;
                manageCrimeRes.Barangay = barangayQuery.Where(z => z.BrgyCode == manageCrimeReportRes.User.BrgyCode).Select(z => z.BrgyName).FirstOrDefault();
                manageCrimeRes.Date = manageCrimeReportRes.DateTimeCreated.Value;
                manageCrimeRes.Time = manageCrimeReportRes.DateTimeCreated.Value.TimeOfDay;
                manageCrimeRes.Lat =  locationQuery.Where(z => z.CrimeCompliantReportId == manageCrimeReportRes.Id).Select(z => z.Lat).FirstOrDefault();
                manageCrimeRes.Long = locationQuery.Where(z => z.CrimeCompliantReportId == manageCrimeReportRes.Id).Select(z => z.Long).FirstOrDefault();
                manageCrimeRes.Category = manageCrimeReportRes.CrimeCompliant.Title;
                manageCrimeRes.Description = manageCrimeReportRes.Description;
                manageCrimeRes.ReporterName = manageCrimeReportRes.User.FirstName + " " + manageCrimeReportRes.User.MiddleName + " " + manageCrimeReportRes.User.LastName;
                manageCrimeRes.ReporterContact = manageCrimeReportRes.User.Phone;
                manageCrimeRes.Status = manageCrimeReportRes.Status;
                manageCrimeRes.Resolution = manageCrimeReportRes.Resolution;

            }


            return manageCrimeRes;
        }

        public string UpdateCrimeStatus(long id, long userId, string status)
        {
            var hasCrimeStatus = db.CrimeCompliantReports.Any(z => z.Id == id && z.UserId == userId);

            if(hasCrimeStatus) 
            {
                var crimeStatus = db.CrimeCompliantReports.Where(z => z.Id == id && z.UserId == userId).First();
                crimeStatus.Status = status;
                crimeStatus.DateTimeUpdated = DateTime.Now;
                db.SaveChanges();
                return "updated Crime Status";
            }

            return "No updated Crime Status";


        }

        public string UpdateCrimeResolution(long id, long userId, string resolution)
        {
            var hasCrimeStatus = db.CrimeCompliantReports.Any(z => z.Id == id && z.UserId == userId);

            if(hasCrimeStatus) 
            {
                var crimeStatus = db.CrimeCompliantReports.Where(z => z.Id == id && z.UserId == userId).First();
                crimeStatus.Status = "resolved";
                crimeStatus.Resolution = resolution;
                crimeStatus.DateResolved = DateTime.Now;
                db.SaveChanges();
                return "updated Crime Resolution";
            }

            return "No updated Crime Resolution";

        }

    }
}