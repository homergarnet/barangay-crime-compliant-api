using System;
using System.Collections.Generic;

namespace barangay_crime_complaint_api.Models
{
    public partial class PhProvince
    {
        public PhProvince()
        {
            PhCities = new HashSet<PhCity>();
            PhProvZones = new HashSet<PhProvZone>();
            Users = new HashSet<User>();
        }

        public string ProvCode { get; set; } = null!;
        public string? ProvDesc { get; set; }
        public string? RegionCode { get; set; }

        public virtual ICollection<PhCity> PhCities { get; set; }
        public virtual ICollection<PhProvZone> PhProvZones { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
