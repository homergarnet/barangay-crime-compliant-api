namespace barangay_crime_compliant_api.DTOS
{

    public class ManageReportDto
    {

       public DateTime Date { get;set; }
       public int Year { get;set; }
       public TimeSpan? TimeStart { get;set; }
       public TimeSpan? TimeEnd { get;set; }
       public List<int> IncidentsReported { get; set; }
       public List<int> IncidentsResolved { get;set; }
       public List<int> IncidentsUnderInvestigation { get;set; }

    }



}