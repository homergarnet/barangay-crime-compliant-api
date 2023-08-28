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

        public virtual DbSet<PhBrgy> PhBrgies { get; set; } = null!;
        public virtual DbSet<PhCity> PhCities { get; set; } = null!;
        public virtual DbSet<PhProvZone> PhProvZones { get; set; } = null!;
        public virtual DbSet<PhProvince> PhProvinces { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-JA43BLA;Database=Thesis_Crime;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.BrgyCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Building)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CityCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HouseNo)
                    .HasMaxLength(150)
                    .IsUnicode(false);

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
                    .HasMaxLength(50)
                    .IsUnicode(false);

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

                entity.Property(e => e.Village)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
