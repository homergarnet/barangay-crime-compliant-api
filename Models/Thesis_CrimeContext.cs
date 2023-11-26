using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace barangay_crime_complaint_api.Models
{
    public partial class Thesis_CrimeContext : DbContext
    {
        public Thesis_CrimeContext()
        {
        }

        public Thesis_CrimeContext(DbContextOptions<Thesis_CrimeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Announcement> Announcements { get; set; } = null!;
        public virtual DbSet<CrimeCompliant> CrimeCompliants { get; set; } = null!;
        public virtual DbSet<CrimeCompliantReport> CrimeCompliantReports { get; set; } = null!;
        public virtual DbSet<CrimeImage> CrimeImages { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<PhBrgy> PhBrgies { get; set; } = null!;
        public virtual DbSet<PhCity> PhCities { get; set; } = null!;
        public virtual DbSet<PhProvZone> PhProvZones { get; set; } = null!;
        public virtual DbSet<PhProvince> PhProvinces { get; set; } = null!;
        public virtual DbSet<PoliceInOut> PoliceInOuts { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=Thesis_Crime;User ID=sa;Password=123123Qq@; TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.ToTable("Announcement");

                entity.Property(e => e.DateTimeCreated).HasColumnType("datetime");

                entity.Property(e => e.DateTimeUpdated).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Announcements)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Announcement_User");
            });

            modelBuilder.Entity<CrimeCompliant>(entity =>
            {
                entity.ToTable("CrimeCompliant");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CrimeCompliantReport>(entity =>
            {
                entity.ToTable("CrimeCompliantReport");

                entity.Property(e => e.DateResolved).HasColumnType("datetime");

                entity.Property(e => e.DateTimeCreated).HasColumnType("datetime");

                entity.Property(e => e.DateTimeUpdated).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Resolution).IsUnicode(false);

                entity.Property(e => e.ResponderDescription).IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CrimeCompliant)
                    .WithMany(p => p.CrimeCompliantReports)
                    .HasForeignKey(d => d.CrimeCompliantId)
                    .HasConstraintName("FK_CrimeCompliantReport_CrimeCompliant");

                entity.HasOne(d => d.Responder)
                    .WithMany(p => p.CrimeCompliantReportResponders)
                    .HasForeignKey(d => d.ResponderId)
                    .HasConstraintName("FK_CrimeCompliantReport_Responder");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CrimeCompliantReportUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_CrimeCompliantReport_User");
            });

            modelBuilder.Entity<CrimeImage>(entity =>
            {
                entity.ToTable("CrimeImage");

                entity.Property(e => e.DateTimeCreated).HasColumnType("datetime");

                entity.Property(e => e.DateTimeUpdated).HasColumnType("datetime");

                entity.Property(e => e.FileName).IsUnicode(false);

                entity.Property(e => e.Image).IsUnicode(false);

                entity.HasOne(d => d.CrimeCompliantReport)
                    .WithMany(p => p.CrimeImages)
                    .HasForeignKey(d => d.CrimeCompliantReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CrimeImage_CrimeCompliantReport");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.DateTimeCreated).HasColumnType("datetime");

                entity.Property(e => e.DateTimeUpdated).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.HasOne(d => d.CrimeCompliantReport)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.CrimeCompliantReportId)
                    .HasConstraintName("FK_Location_CrimeCompliantReport");
            });

            modelBuilder.Entity<PhBrgy>(entity =>
            {
                entity.HasKey(e => e.BrgyCode);

                entity.ToTable("PhBrgy");

                entity.Property(e => e.BrgyCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BrgyName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CityCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.CityCodeNavigation)
                    .WithMany(p => p.PhBrgies)
                    .HasForeignKey(d => d.CityCode)
                    .HasConstraintName("FK_PhBrgy_PhCity");
            });

            modelBuilder.Entity<PhCity>(entity =>
            {
                entity.HasKey(e => e.CityCode);

                entity.ToTable("PhCity");

                entity.Property(e => e.CityCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CityDescription)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ProvCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProvCodeNavigation)
                    .WithMany(p => p.PhCities)
                    .HasForeignKey(d => d.ProvCode)
                    .HasConstraintName("FK_PhCity_PhProvince");
            });

            modelBuilder.Entity<PhProvZone>(entity =>
            {
                entity.HasKey(e => e.Ppzid);

                entity.ToTable("PhProvZone");

                entity.Property(e => e.Ppzid)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PPZID");

                entity.Property(e => e.ProvCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ZoneCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProvCodeNavigation)
                    .WithMany(p => p.PhProvZones)
                    .HasForeignKey(d => d.ProvCode)
                    .HasConstraintName("FK_PhProvZone_PhProvince");
            });

            modelBuilder.Entity<PhProvince>(entity =>
            {
                entity.HasKey(e => e.ProvCode);

                entity.ToTable("PhProvince");

                entity.Property(e => e.ProvCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ProvDesc)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("provDesc");

                entity.Property(e => e.RegionCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("regionCode");
            });

            modelBuilder.Entity<PoliceInOut>(entity =>
            {
                entity.ToTable("PoliceInOut");

                entity.Property(e => e.DateTimeCreated).HasColumnType("datetime");

                entity.Property(e => e.DateTimeUpdated).HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PoliceInOuts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_PoliceInOut_PoliceInOut_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.BrgyCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Building)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CityCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ForgotPasswordToken).IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HouseNo)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProvinceCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ResidencyType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Selfie).IsUnicode(false);

                entity.Property(e => e.Street)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UnitFloor)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UserType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValidId).IsUnicode(false);

                entity.Property(e => e.Village)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.BrgyCodeNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.BrgyCode)
                    .HasConstraintName("FK_User_PhBrgy");

                entity.HasOne(d => d.CityCodeNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CityCode)
                    .HasConstraintName("FK_User_PhCity");

                entity.HasOne(d => d.ProvinceCodeNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ProvinceCode)
                    .HasConstraintName("FK_User_PhProvince");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
