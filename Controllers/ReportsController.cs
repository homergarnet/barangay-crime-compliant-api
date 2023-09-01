using System.Text.Json;
using barangay_crime_compliant_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace barangay_crime_compliant_api.Controllers
{
    public class ReportsController : ControllerBase
    {

        private readonly IReportsService _iReportsService;
 
        public ReportsController(IReportsService iReportsService)
        {
            _iReportsService = iReportsService;
        
        }

    }
}
