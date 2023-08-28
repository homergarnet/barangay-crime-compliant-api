using System;
using System.Collections.Generic;

namespace barangay_crime_complaint_api.Models
{
    public partial class User
    {
        public long Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string Phone { get; set; } = null!;
        public string? HouseNo { get; set; }
        public string? Street { get; set; }
        public string? Village { get; set; }
        public string? UnitFloor { get; set; }
        public string? Building { get; set; }
        public string? ProvinceCode { get; set; }
        public string? CityCode { get; set; }
        public string? BrgyCode { get; set; }
        public string? ZipCode { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string? UserType { get; set; }
    }
}
