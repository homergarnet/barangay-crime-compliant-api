namespace barangay_crime_compliant_api.DTOS
{

    public class ManageCrimeDto
    {

        public long ReportId { get;set; }
        public string Barangay { get;set; }
        public DateTime? Date { get;set; }
        public TimeSpan? Time { get;set; }
        public decimal? Lat { get;set; }
        public decimal? Long { get;set; }
        public string Category { get;set; }
        public string Description { get;set; }
        public string ReporterName { get;set; }
        public string ReporterContact { get;set; }
        public string Status { get;set; }

    }



}