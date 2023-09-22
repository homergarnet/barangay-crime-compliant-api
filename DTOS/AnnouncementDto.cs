namespace barangay_crime_compliant_api.DTOS
{
    public class AnnouncementDto
    {

        public List<AnnouncementDescription> AnnouncementDescription { get; set; }
        public long? AnnouncementTotalCount { get;set; }
    }

    public class AnnouncementDescription
    {

        public long Id { get; set; }
        public string? Description { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeUpdated { get; set; }
        public long? UserId { get;set; }

    }
}