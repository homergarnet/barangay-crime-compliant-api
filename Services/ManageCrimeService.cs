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
        public List<ManageCrimeDto> GetManageCrimeList(string reportType, string status, long userId, string userType, string keyword, int page, int pageSize)
        {

            IQueryable<CrimeCompliantReport> query = db.CrimeCompliantReports;
            var barangayInfo = db.Users.Where(z => z.Id == userId).FirstOrDefault();
            if (userId != 0L && userType.Equals("barangay"))
            {
                query = query.Where(z => z.User.BrgyCode == barangayInfo.BrgyCode);
            }

            if (userId != 0L && userType.Equals("compliant"))
            {
                query = query.Where(z => z.UserId == userId);
            }

            var locationQuery = db.Locations;
            var barangayQuery = db.PhBrgies;
            List<ManageCrimeDto> manageCrimeList = new List<ManageCrimeDto>();
            if (!string.IsNullOrEmpty(keyword))
            {

                if (reportType != null)
                {
                    //compliant types
                    if (!string.IsNullOrEmpty(reportType) && reportType.Contains("compliant"))
                    {
                        query = query.Where(z => z.Description.Contains(keyword) || z.Status.Contains(keyword) || z.CrimeCompliant.Type.Equals("compliant types"));
                    }
                    //non index crime, index crime
                    if (reportType.Contains("crime"))
                    {
                        query = query.Where(z => z.Description.Contains(keyword) || z.Status.Contains(keyword) || z.CrimeCompliant.Type.Contains("crime"));
                    }
                    else
                    {
                        query = query.Where(z => z.Description.Contains(keyword) || z.Status.Contains(keyword));
                    }
                }

                if (status != null)
                {

                    if (!string.IsNullOrEmpty(status) && !status.Equals("completed") && !status.Equals("closed"))
                    {
                        query = query.Where(z => !z.Status.Equals("completed") && !z.Status.Equals("closed"));
                    }

                    else if (!string.IsNullOrEmpty(status) && status.Equals("completed"))
                    {
                        query = query.Where(z => z.Status.Equals(status));
                    }

                    else if (!string.IsNullOrEmpty(status) && status.Equals("closed"))
                    {
                        query = query.Where(z => z.Status.Equals(status));
                    }

                }

            }
            else
            {

                if (reportType != null)
                {
                    //compliant types
                    if (!string.IsNullOrEmpty(reportType) && reportType.Contains("compliant"))
                    {
                        query = query.Where(z => z.Description.Contains(keyword) || z.Status.Contains(keyword) || z.CrimeCompliant.Type.Equals("compliant types"));
                    }
                    //non index crime, index crime
                    if (reportType.Contains("crime"))
                    {
                        query = query.Where(z => z.Description.Contains(keyword) || z.Status.Contains(keyword) || z.CrimeCompliant.Type.Contains("crime"));
                    }
                }

                if (status != null)
                {

                    if (!string.IsNullOrEmpty(status) && !status.Equals("completed") && !status.Equals("closed"))
                    {
                        query = query.Where(z => !z.Status.Equals("completed") && !z.Status.Equals("closed"));
                    }

                    else if (!string.IsNullOrEmpty(status) && status.Equals("completed"))
                    {
                        query = query.Where(z => z.Status.Equals(status));
                    }

                    else if (!string.IsNullOrEmpty(status) && status.Equals("closed"))
                    {
                        query = query.Where(z => z.Status.Equals(status));
                    }

                }

            }

            var manageCrimeReportRes = query.Include(z => z.User).Include(z => z.CrimeCompliant)
            .Skip((page - 1) * pageSize).Take(pageSize).ToList();

            foreach (var manageCrime in manageCrimeReportRes)
            {
                DateTime dateTimeCreated = manageCrime.DateTimeCreated.Value;

                var manageCrimeDto = new ManageCrimeDto();
                string formattedIdValue = manageCrime.Id.ToString("D3");
                manageCrimeDto.ReportId = manageCrime.Id;
                manageCrimeDto.ReportIdStr = formattedIdValue;
                manageCrimeDto.CrimeCompliantId = manageCrime.CrimeCompliantId;
                manageCrimeDto.Barangay = barangayQuery.Where(z => z.BrgyCode == manageCrime.User.BrgyCode).Select(z => z.BrgyName).FirstOrDefault();
                manageCrimeDto.Date = manageCrime.DateTimeCreated.Value;
                manageCrimeDto.Time = dateTimeCreated.ToString("HH:mm:ss");
                manageCrimeDto.Lat = locationQuery.Where(z => z.CrimeCompliantReportId == manageCrime.Id).Select(z => z.Lat).FirstOrDefault();
                manageCrimeDto.Long = locationQuery.Where(z => z.CrimeCompliantReportId == manageCrime.Id).Select(z => z.Long).FirstOrDefault();
                manageCrimeDto.Category = manageCrime.CrimeCompliant.Title;
                manageCrimeDto.Description = manageCrime.Description;
                manageCrimeDto.ReporterName = manageCrime.User.FirstName + " " + manageCrime.User.MiddleName + " " + manageCrime.User.LastName;
                manageCrimeDto.ReporterContact = manageCrime.User.Phone;
                manageCrimeDto.Status = manageCrime.Status;
                manageCrimeDto.Resolution = manageCrime.Resolution;
                manageCrimeDto.DateResolved = manageCrime.DateResolved != null ? manageCrime.DateResolved.Value : null;
                manageCrimeList.Add(manageCrimeDto);

            }

            return manageCrimeList;
        }

        public long ManageCrimeCount(string reportType, long userId, string userType)
        {

            IQueryable<CrimeCompliantReport> query = db.CrimeCompliantReports;
            var barangayInfo = db.Users.Where(z => z.Id == userId).FirstOrDefault();
            if (userId != 0L && userType.Equals("barangay"))
            {
                query = query.Where(z => z.User.BrgyCode == barangayInfo.BrgyCode);
            }

            //compliant types
            if (!string.IsNullOrEmpty(reportType) && reportType.Contains("compliant"))
            {
                query = query.Where(z => z.CrimeCompliant.Type.Equals("compliant types"));
            }
            //non index crime, index crime
            if (reportType.Contains("crime"))
            {
                query = query.Where(z => z.CrimeCompliant.Type.Contains("crime"));
            }
            return query.Count();

        }


        public ManageCrimeDto GetManageCrimeById(long id, string reportType)
        {
            IQueryable<CrimeCompliantReport> query = db.CrimeCompliantReports;
            if (reportType != null)
            {

                if (!string.IsNullOrEmpty(reportType) && reportType.Equals("compliant types"))
                {
                    query = query.Where(z => z.CrimeCompliant.Type.Equals(reportType));
                }
                if (reportType.Equals("index crime") || reportType.Equals("non index crime"))
                {
                    query = query.Where(z => z.CrimeCompliant.Type.Equals("index crime") && z.CrimeCompliant.Type.Equals("non index crime"));
                }

            }

            var locationQuery = db.Locations;
            var barangayQuery = db.PhBrgies;
            ManageCrimeDto manageCrimeRes = new ManageCrimeDto();

            var hasManageCrimeReportRes = db.CrimeCompliantReports.Any(z => z.Id == id);
            if (hasManageCrimeReportRes)
            {


                var manageCrimeReportRes = query.Include(z => z.User).Include(z => z.CrimeCompliant).Where(z => z.Id == id).FirstOrDefault();
                DateTime dateTimeCreated = manageCrimeReportRes.DateTimeCreated.Value;
                string formattedIdValue = manageCrimeReportRes.Id.ToString("D3");
                manageCrimeRes.ReportId = manageCrimeReportRes.Id;
                manageCrimeRes.ReportIdStr = formattedIdValue;
                manageCrimeRes.Barangay = barangayQuery.Where(z => z.BrgyCode == manageCrimeReportRes.User.BrgyCode).Select(z => z.BrgyName).FirstOrDefault();
                manageCrimeRes.Date = manageCrimeReportRes.DateTimeCreated.Value;
                manageCrimeRes.Time = dateTimeCreated.ToString("HH:mm:ss");
                manageCrimeRes.Lat = locationQuery.Where(z => z.CrimeCompliantReportId == manageCrimeReportRes.Id).Select(z => z.Lat).FirstOrDefault();
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

        public string UpdateCrimeStatus(long id, string status)
        {
            var hasCrimeStatus = db.CrimeCompliantReports.Any(z => z.Id == id);

            if (hasCrimeStatus)
            {
                var crimeStatus = db.CrimeCompliantReports.Where(z => z.Id == id).First();
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

            if (hasCrimeStatus)
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