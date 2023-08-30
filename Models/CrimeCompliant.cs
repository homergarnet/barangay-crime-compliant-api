using System;
using System.Collections.Generic;

namespace barangay_crime_complaint_api.Models
{
    public partial class CrimeCompliant
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
    }
}
