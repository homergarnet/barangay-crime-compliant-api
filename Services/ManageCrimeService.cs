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
        public List<ManageCrimeDto> GetManageCrimeList(string keyword, int page, int pageSize)
        {
            IQueryable<CrimeCompliantReport> query = db.CrimeCompliantReports;
            var locationQuery = db.Locations;
            var barangayQuery = db.PhBrgies;
            List<ManageCrimeDto> manageCrimeList = new List<ManageCrimeDto>();
            if(!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(z => z.Description.Contains(keyword) || z.Status.Contains(keyword));
            }

            var manageCrimeReportRes = query.Include(z => z.User).Include(z => z.CrimeCompliant)
            .Skip((page - 1) * pageSize).Take(pageSize).ToList();

            foreach(var manageCrime in manageCrimeReportRes)
            {

                var manageCrimeDto = new ManageCrimeDto();
                manageCrimeDto.ReportId = manageCrime.Id;
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
                manageCrimeList.Add(manageCrimeDto);
                
            }
            
            return manageCrimeList;
        }

        public ManageCrimeDto GetManageCrimeById(long id)
        {
            IQueryable<CrimeCompliantReport> query = db.CrimeCompliantReports;
            var locationQuery = db.Locations;
            var barangayQuery = db.PhBrgies;
            ManageCrimeDto manageCrimeRes = new ManageCrimeDto();

            var hasManageCrimeReportRes = db.CrimeCompliantReports.Any(z => z.Id == id);
            if(hasManageCrimeReportRes) 
            {
                
                var manageCrimeReportRes = query.Include(z => z.User).Include(z => z.CrimeCompliant).Where(z => z.Id == id).FirstOrDefault();
                manageCrimeRes.ReportId = manageCrimeReportRes.Id;
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

            }


            return manageCrimeRes;
        }

    }
}