using System;
using System.Collections.Generic;

namespace barangay_crime_complaint_api.Models
{
    public partial class PhBrgy
    {
        public string BrgyCode { get; set; } = null!;
        public string? BrgyName { get; set; }
        public string? CityCode { get; set; }
        public string? ZipCode { get; set; }
        public bool? IsActive { get; set; }

        public virtual PhCity? CityCodeNavigation { get; set; }
    }
}
