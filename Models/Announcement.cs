using System;
using System.Collections.Generic;

namespace barangay_crime_complaint_api.Models
{
    public partial class Announcement
    {
        public long Id { get; set; }
        public string? Description { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeUpdated { get; set; }
        public long? UserId { get; set; }
        public bool? IsActive { get; set; }

        public virtual User? User { get; set; }
    }
}
