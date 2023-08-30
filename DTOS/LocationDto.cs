namespace barangay_crime_compliant_api.DTOS
{

    public class LocationDto
    {

        public long Id { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Long { get; set; }
        public string? Description { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeUpdated { get; set; }
        public long? UserId { get; set; }

    }

}