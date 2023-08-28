using System;
using System.Collections.Generic;

namespace barangay_crime_complaint_api.Models
{
    public partial class PhProvZone
    {
        public decimal Ppzid { get; set; }
        public string? ProvCode { get; set; }
        public string? ZoneCode { get; set; }

        public virtual PhProvince? ProvCodeNavigation { get; set; }
    }
}
