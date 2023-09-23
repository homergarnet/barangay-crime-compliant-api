namespace barangay_crime_compliant_api.DTOS
{

    public class ManageCrimeDto
    {

        public long ReportId { get;set; }
        public string ReportIdStr { get;set; }
        public string Barangay { get;set; }
        public DateTime? Date { get;set; }
        public string? Time { get;set; }
        public float? Lat { get;set; }
        public float? Long { get;set; }
        public string Category { get;set; }
        public string Description { get;set; }
        public string ReporterName { get;set; }
        public string ReporterContact { get;set; }
        public string Status { get;set; }
        public string Resolution { get;set; }
        public DateTime? DateResolved { get;set; }
        public long? CrimeCompliantId { get;set; }

    }



}