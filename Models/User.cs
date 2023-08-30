using System;
using System.Collections.Generic;

namespace barangay_crime_complaint_api.Models
{
    public partial class User
    {
        public User()
        {
            Announcements = new HashSet<Announcement>();
            CrimeCompliantReports = new HashSet<CrimeCompliantReport>();
            Locations = new HashSet<Location>();
            Reports = new HashSet<Report>();
        }

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
        public byte[]? ValidId { get; set; }
        public byte[]? Selfie { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Announcement> Announcements { get; set; }
        public virtual ICollection<CrimeCompliantReport> CrimeCompliantReports { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
