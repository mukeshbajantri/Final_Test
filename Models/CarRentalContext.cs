using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CarRentals.Models;

public partial class CarRentalContext : DbContext
{
    public CarRentalContext()
    {
    }

    public CarRentalContext(DbContextOptions<CarRentalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BookingHistory> BookingHistories { get; set; }

    public virtual DbSet<CarDetail> CarDetails { get; set; }


    public virtual DbSet<UserDetail> UserDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=LAPTOP-996L0HTG;Database=Car_Rental;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookingHistory>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking___35AAE1F8DDE5C5A0");

            entity.ToTable("Booking_History");

            entity.Property(e => e.BookingId).HasColumnName("Booking_id");
            entity.Property(e => e.BookingDate)
                .HasMaxLength(50)
                .HasColumnName("Booking_date");
            entity.Property(e => e.CarId).HasColumnName("car_id");
            entity.Property(e => e.FromDate)
                .HasMaxLength(50)
                .HasColumnName("From_date");
            entity.Property(e => e.ToDate)
                .HasMaxLength(50)
                .HasColumnName("To_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Car).WithMany(p => p.BookingHistories)
                .HasForeignKey(d => d.CarId)
                .HasConstraintName("FK__Booking_H__car_i__4E88ABD4");

            entity.HasOne(d => d.User).WithMany(p => p.BookingHistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Booking_H__user___4D94879B");
        });

        modelBuilder.Entity<CarDetail>(entity =>
        {
            entity.HasKey(e => e.CarId).HasName("PK__carDetai__4C9A0DB348CEB98A");

            entity.ToTable("carDetails");

            entity.Property(e => e.CarId).HasColumnName("car_id");
            entity.Property(e => e.CarBrand)
                .HasMaxLength(50)
                .HasColumnName("car_brand");
            entity.Property(e => e.CarColor)
                .HasMaxLength(10)
                .HasColumnName("car_color");
            entity.Property(e => e.CarName)
                .HasMaxLength(50)
                .HasColumnName("car_Name");
            entity.Property(e => e.CarType)
                .HasMaxLength(10)
                .HasColumnName("car_type");
            entity.Property(e => e.Kilometers).HasColumnName("kilometers");
            entity.Property(e => e.ModelYear)
                .HasMaxLength(20)
                .HasColumnName("model_year");
            entity.Property(e => e.Amount).HasColumnName("Amount");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserDeta__B9BE370FC623550B");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.Admin).HasColumnName("admin");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.LicenseNumber)
                .HasMaxLength(20)
                .HasColumnName("license_number");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("user_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
