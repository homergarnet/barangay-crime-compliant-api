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
            [FromBody] ManageReportDto manageReportInfo
        )
        {

            try {

                var crimeCompliantList = db.CrimeCompliants.ToList();
                IQueryable<CrimeCompliantReport> crimeCompliantReport;
                if(manageReportInfo.TimeStart != null && manageReportInfo.TimeEnd != null)
                {
                    crimeCompliantReport = db.CrimeCompliantReports.Where(z => z.DateTimeCreated.Value.Date == manageReportInfo.Date.Date && z.DateTimeCreated.Value.Year == manageReportInfo.Year && z.DateTimeCreated.Value.TimeOfDay >= manageReportInfo.TimeStart && z.DateTimeCreated.Value.TimeOfDay <= manageReportInfo.TimeEnd);
                } 
                else 
                {
                    crimeCompliantReport = db.CrimeCompliantReports.Where(z => z.DateTimeCreated.Value.Date == manageReportInfo.Date.Date && z.DateTimeCreated.Value.Year == manageReportInfo.Year);
                }
                

                var incidentReportedList = new List<object>();
                var incidentResolvedList = new List<object>();
                var incidentUnderInvestigationList = new List<object>();
                
                var getManageIncidentReportList = new List<object>();

                foreach(var crimeCompliant in crimeCompliantList)
                {
                    var incidentReportedRes = crimeCompliantReport.Where(z => z.Id == crimeCompliant.Id).Count();
                    var incidentResolvedRes = crimeCompliantReport.Where(z => z.Id == crimeCompliant.Id && z.Status.Equals("resolved")).Count();
                    var incidentUnderInvestigationRes = crimeCompliantReport.Where(z => z.Id == crimeCompliant.Id && z.Status.Equals("investigation")).Count();
                    incidentReportedList.Add(incidentReportedRes);
                    incidentResolvedList.Add(incidentResolvedRes);
                    incidentUnderInvestigationList.Add(incidentUnderInvestigationRes);
                }

                foreach(var item in incidentReportedList)
                {
                    getManageIncidentReportList.Add(item);
                }

                foreach(var item in incidentResolvedList)
                {
                    getManageIncidentReportList.Add(item);
                }

                foreach(var item in incidentUnderInvestigationList)
                {
                    getManageIncidentReportList.Add(item);
                }

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