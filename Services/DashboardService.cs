using barangay_crime_complaint_api.Models;
using barangay_crime_compliant_api.DTOS;
using Microsoft.EntityFrameworkCore;

namespace barangay_crime_compliant_api.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IConfiguration configuration;
        private readonly Thesis_CrimeContext db;
        public DashboardService(IConfiguration configuration, Thesis_CrimeContext db)
        {
            this.configuration = configuration;
            this.db = db;
        }

        public DashboardDto TotalDashboardCardCount(long userId, string barangayCode = "")
        {

            IQueryable<CrimeCompliantReport> query = db.CrimeCompliantReports;
            IQueryable<User> usersQuery = db.Users;

            if(!string.IsNullOrEmpty(barangayCode))
            {
                query = query.Where(z => z.User.BrgyCode == barangayCode);
                usersQuery = usersQuery.Where(z => z.BrgyCode == barangayCode);
            }

            
            var dashboardRes = new DashboardDto();
            dashboardRes.TotalOfCrimes = query.Include(z => z.CrimeCompliant).Where(z => z.CrimeCompliant.Type.Contains("crime")).Count();
            dashboardRes.TotalOfCompliant = query.Include(z => z.CrimeCompliant).Where(z => z.CrimeCompliant.Type.Contains("compliant")).Count();
            dashboardRes.TotalOfUsers = usersQuery.Count();
            return dashboardRes;

        }
    }
}