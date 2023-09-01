using System;
using System.Collections.Generic;

namespace barangay_crime_complaint_api.Models
{
    public partial class PhCity
    {
        public PhCity()
        {
            PhBrgies = new HashSet<PhBrgy>();
            Users = new HashSet<User>();
        }

        public string CityCode { get; set; } = null!;
        public string? CityDescription { get; set; }
        public string? ProvCode { get; set; }
        public string? ZipCode { get; set; }
        public bool? IsActive { get; set; }

        public virtual PhProvince? ProvCodeNavigation { get; set; }
        public virtual ICollection<PhBrgy> PhBrgies { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
