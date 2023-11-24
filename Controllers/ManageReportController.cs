using System.Text.Json;
using barangay_crime_complaint_api.Models;
using barangay_crime_compliant_api.DTOS;
using barangay_crime_compliant_api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
namespace barangay_crime_compliant_api.Controllers
{
    public class ManageReportController : ControllerBase 
    {

        private readonly IManageReportService _iManageReportService;
        private readonly Thesis_CrimeContext db;
        public ManageReportController(IManageReportService iManageReportService, Thesis_CrimeContext db)
        {
            _iManageReportService = iManageReportService;
            this.db = db;
        }

        [Authorize]
        [HttpGet]
        [Route("api/get-manage-incident-report")]
        public IActionResult GetManageIncidentReport(

            [FromQuery] DateTime date, [FromQuery] int year, 
            [FromQuery] TimeSpan timeStart,
            [FromQuery] TimeSpan timeEnd
            
        )
        {

            try {

                var crimeCompliantList = db.CrimeCompliants.ToList();
                IQueryable<CrimeCompliantReport> crimeCompliantReport;
                if(timeStart != TimeSpan.Zero && timeEnd != TimeSpan.Zero)
                {
                    crimeCompliantReport = db.CrimeCompliantReports.Where(z => z.DateTimeCreated.Value.Date == date.Date && z.DateTimeCreated.Value.Year == year && z.DateTimeCreated.Value.TimeOfDay >= timeStart && z.DateTimeCreated.Value.TimeOfDay <= timeEnd);
                } 
                else 
                {
                    crimeCompliantReport = db.CrimeCompliantReports.Where(z => z.DateTimeCreated.Value.Date == date.Date && z.DateTimeCreated.Value.Year == year);
                }
                

                var incidentReportedList = new List<int>();
                var incidentResolvedList = new List<int>();
                var incidentUnderInvestigationList = new List<int>();
                
                var getManageIncidentReportList = new ManageReportDto();

                foreach(var crimeCompliant in crimeCompliantList)
                {
                    var incidentReportedRes = crimeCompliantReport.Where(z => z.CrimeCompliantId == crimeCompliant.Id).Count();
                    var incidentResolvedRes = crimeCompliantReport.Where(z => z.CrimeCompliantId == crimeCompliant.Id && z.Status.Equals("resolved")).Count();
                    var incidentUnderInvestigationRes = crimeCompliantReport.Where(z => z.CrimeCompliantId == crimeCompliant.Id && z.Status.Equals("investigation")).Count();
                    incidentReportedList.Add(incidentReportedRes);
                    incidentResolvedList.Add(incidentResolvedRes);
                    incidentUnderInvestigationList.Add(incidentUnderInvestigationRes);
                }

                getManageIncidentReportList.IncidentsReported = incidentReportedList;
                getManageIncidentReportList.IncidentsResolved = incidentResolvedList;
                getManageIncidentReportList.IncidentsUnderInvestigation = incidentUnderInvestigationList;

                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = JsonSerializer.Serialize(getManageIncidentReportList)
                };

            }

            catch (Exception ex)
            {
                return new ContentResult
                {
                    StatusCode = 500,
                    ContentType = "text/html",
                    Content = Common.GetFormattedExceptionMessage(ex)
                };
            }

        }

    }
}