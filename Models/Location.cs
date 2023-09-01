using System;
using System.Collections.Generic;

namespace barangay_crime_complaint_api.Models
{
    public partial class Location
    {
        public long Id { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Long { get; set; }
        public string? Description { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeUpdated { get; set; }
        public long? UserId { get; set; }
        public long? CrimeCompliantReportId { get; set; }

        public virtual CrimeCompliantReport? CrimeCompliantReport { get; set; }
        public virtual User? User { get; set; }
    }
}
