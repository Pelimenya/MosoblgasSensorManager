using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MosoblgasSensorManager.Models;

namespace MosoblgasSensorManager.Context;

public partial class ContextDB : DbContext
{
    public ContextDB()
    {
    }

    public ContextDB(DbContextOptions<ContextDB> options)
        : base(options)
    {
    }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Maintenance> Maintenances { get; set; }

    public virtual DbSet<Qrcode> Qrcodes { get; set; }

    public virtual DbSet<Reading> Readings { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Sensor> Sensors { get; set; }

    public virtual DbSet<Technician> Technicians { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("host=localhost; port=5432; database=MosoblgasSensorManagerDB; username=Pelimenya; password=root");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("Location_pkey");

            entity.ToTable("Location");

            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Latitude)
                .HasPrecision(9, 6)
                .HasColumnName("latitude");
            entity.Property(e => e.Longitude)
                .HasPrecision(9, 6)
                .HasColumnName("longitude");
        });

        modelBuilder.Entity<Maintenance>(entity =>
        {
            entity.HasKey(e => e.MaintenanceId).HasName("Maintenance_pkey");

            entity.ToTable("Maintenance");

            entity.Property(e => e.MaintenanceId).HasColumnName("maintenance_id");
            entity.Property(e => e.MaintenanceDate).HasColumnName("maintenance_date");
            entity.Property(e => e.MaintenanceType)
                .HasMaxLength(50)
                .HasColumnName("maintenance_type");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.SensorId).HasColumnName("sensor_id");
            entity.Property(e => e.TechnicianId).HasColumnName("technician_id");

            entity.HasOne(d => d.Sensor).WithMany(p => p.Maintenances)
                .HasForeignKey(d => d.SensorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Maintenance_sensor_id_fkey");

            entity.HasOne(d => d.Technician).WithMany(p => p.Maintenances)
                .HasForeignKey(d => d.TechnicianId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Maintenance_technician_id_fkey");
        });

        modelBuilder.Entity<Qrcode>(entity =>
        {
            entity.HasKey(e => e.QrCodeId).HasName("QRCode_pkey");

            entity.ToTable("QRCode");

            entity.Property(e => e.QrCodeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("qr_code_id");
            entity.Property(e => e.QrCodeData).HasColumnName("qr_code_data");

            entity.HasOne(d => d.QrCode).WithOne(p => p.Qrcode)
                .HasForeignKey<Qrcode>(d => d.QrCodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("QRCode_qr_code_id_fkey");
        });

        modelBuilder.Entity<Reading>(entity =>
        {
            entity.HasKey(e => e.ReadingId).HasName("Reading_pkey");

            entity.ToTable("Reading");

            entity.Property(e => e.ReadingId).HasColumnName("reading_id");
            entity.Property(e => e.ReadingDate).HasColumnName("reading_date");
            entity.Property(e => e.SensorId).HasColumnName("sensor_id");
            entity.Property(e => e.Value)
                .HasPrecision(10, 2)
                .HasColumnName("value");

            entity.HasOne(d => d.Sensor).WithMany(p => p.Readings)
                .HasForeignKey(d => d.SensorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Reading_sensor_id_fkey");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("Report_pkey");

            entity.ToTable("Report");

            entity.Property(e => e.ReportId).HasColumnName("report_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.GeneratedDate).HasColumnName("generated_date");
            entity.Property(e => e.ReportType)
                .HasMaxLength(50)
                .HasColumnName("report_type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Reports)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Report_user_id_fkey");
        });

        modelBuilder.Entity<Sensor>(entity =>
        {
            entity.HasKey(e => e.SensorId).HasName("Sensor_pkey");

            entity.ToTable("Sensor");

            entity.HasIndex(e => e.SerialNumber, "idx_sensor_serial_number");

            entity.Property(e => e.SensorId).HasColumnName("sensor_id");
            entity.Property(e => e.InstallationDate).HasColumnName("installation_date");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.SerialNumber)
                .HasMaxLength(50)
                .HasColumnName("serial_number");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");

            entity.HasOne(d => d.Location).WithMany(p => p.Sensors)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("Sensor_location_id_fkey");
        });

        modelBuilder.Entity<Technician>(entity =>
        {
            entity.HasKey(e => e.TechnicianId).HasName("Technician_pkey");

            entity.ToTable("Technician");

            entity.Property(e => e.TechnicianId)
                .ValueGeneratedOnAdd()
                .HasColumnName("technician_id");
            entity.Property(e => e.ContactInfo)
                .HasMaxLength(255)
                .HasColumnName("contact_info");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Qualification)
                .HasMaxLength(100)
                .HasColumnName("qualification");

            entity.HasOne(d => d.TechnicianNavigation).WithOne(p => p.Technician)
                .HasForeignKey<Technician>(d => d.TechnicianId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Technician_technician_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.Username, "User_username_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
