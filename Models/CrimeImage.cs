using System;
using System.Collections.Generic;

namespace barangay_crime_complaint_api.Models
{
    public partial class CrimeImage
    {
        public long Id { get; set; }
        public long CrimeCompliantReportId { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeUpdated { get; set; }
        public string? FileName { get; set; }
        public string? Image { get; set; }

        public virtual CrimeCompliantReport CrimeCompliantReport { get; set; } = null!;
    }
}
