using barangay_crime_complaint_api.Models;
using barangay_crime_compliant_api.DTOS;
using Microsoft.EntityFrameworkCore;

namespace barangay_crime_compliant_api.Services
{
    public class ManageReportService : IManageReportService
    {

        private readonly IConfiguration configuration;
        private readonly Thesis_CrimeContext db;

        
        public ManageReportService(IConfiguration configuration, Thesis_CrimeContext db)
        {
            this.configuration = configuration;
            this.db = db;

        }


    }
}
