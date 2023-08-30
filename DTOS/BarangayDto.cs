namespace barangay_crime_compliant_api.DTOS
{

    public class CompliantCrimeDto
    {

        public long Id { get; set; }
        public string? CompliantType { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Resolution { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeUpdated { get; set; }
        public DateTime? DateResolved { get; set; }
        public string? CaseType { get; set; }

    }

    public class CrimeImageDto
    {

        public long Id { get; set; }
        public long CrimeCompliantReportId { get; set; }
        public byte[]? Image { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeUpdated { get; set; }
        public string? FileName { get;set; }

    }

}