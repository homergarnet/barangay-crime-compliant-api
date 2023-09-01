using System;
using System.Collections.Generic;

namespace barangay_crime_complaint_api.Models
{
    public partial class CrimeCompliantReport
    {
        public CrimeCompliantReport()
        {
            CrimeImages = new HashSet<CrimeImage>();
            Locations = new HashSet<Location>();
        }

        public long Id { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Resolution { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeUpdated { get; set; }
        public DateTime? DateResolved { get; set; }
        public long? UserId { get; set; }
        public long? CrimeCompliantId { get; set; }

        public virtual CrimeCompliant? CrimeCompliant { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<CrimeImage> CrimeImages { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}
