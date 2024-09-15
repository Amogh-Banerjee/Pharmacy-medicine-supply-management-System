using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PharmacyMedicineSupplyManagementAPI.Models;

public partial class MedDbContext : DbContext
{
    public MedDbContext()
    {
    }

    public MedDbContext(DbContextOptions<MedDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MedicalRep> MedicalReps { get; set; }

    public virtual DbSet<MedicineDemand> MedicineDemands { get; set; }

    public virtual DbSet<MedicineStock> MedicineStocks { get; set; }

    public virtual DbSet<Pharmacy> Pharmacies { get; set; }

    public virtual DbSet<PharmacyMedicineSupply> PharmacyMedicineSupplies { get; set; }

    public virtual DbSet<RepSchedule> RepSchedules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-6DIUOQ0T\\SQLEXPRESS;Database=MedDb;Trusted_Connection=True;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MedicalRep>(entity =>
        {
            entity.HasKey(e => e.MedRepId).HasName("PK__MedicalR__55BF6ACD07EEEB3F");

            entity.ToTable("MedicalRep");

            entity.Property(e => e.MedRepId).HasColumnName("MedRepID");
            entity.Property(e => e.MedRepName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MedicineDemand>(entity =>
        {
            entity.HasKey(e => e.MedId).HasName("PK__Medicine__EB77FC369F90CF8B");

            entity.ToTable("MedicineDemand");

            entity.Property(e => e.MedId)
                .ValueGeneratedNever()
                .HasColumnName("MedID");

            entity.HasOne(d => d.Med).WithOne(p => p.MedicineDemand)
                .HasForeignKey<MedicineDemand>(d => d.MedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicineD__MedID__06CD04F7");
        });

        modelBuilder.Entity<MedicineStock>(entity =>
        {
            entity.HasKey(e => e.MedId).HasName("PK__Medicine__EB77FC367D838B92");

            entity.ToTable("MedicineStock");

            entity.Property(e => e.MedId).HasColumnName("MedID");
            entity.Property(e => e.ChemicalComposition).HasColumnType("text");
            entity.Property(e => e.MedName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TargetAilment)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Pharmacy>(entity =>
        {
            entity.HasKey(e => e.PharmacyId).HasName("PK__Pharmaci__BD9D2A8E029A54BF");

            entity.Property(e => e.PharmacyId).HasColumnName("PharmacyID");
            entity.Property(e => e.PharmacyName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PharmacyMedicineSupply>(entity =>
        {
            entity.HasKey(e => e.SupplyId).HasName("PK__Pharmacy__7CDD6C8E240B1724");

            entity.ToTable("PharmacyMedicineSupply");

            entity.HasIndex(e => new { e.PharmacyId, e.MedId }, "UQ_Pharmacy_Medicine").IsUnique();

            entity.Property(e => e.SupplyId).HasColumnName("SupplyID");
            entity.Property(e => e.MedId).HasColumnName("MedID");
            entity.Property(e => e.PharmacyId).HasColumnName("PharmacyID");

            entity.HasOne(d => d.Med).WithMany(p => p.PharmacyMedicineSupplies)
                .HasForeignKey(d => d.MedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PharmacyM__MedID__1F98B2C1");

            entity.HasOne(d => d.Pharmacy).WithMany(p => p.PharmacyMedicineSupplies)
                .HasForeignKey(d => d.PharmacyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PharmacyM__Pharm__1EA48E88");
        });

        modelBuilder.Entity<RepSchedule>(entity =>
        {
            entity.HasKey(e => e.SchId).HasName("PK__RepSched__CAD9872B36EA5211");

            entity.ToTable("RepSchedule");

            entity.Property(e => e.SchId).HasColumnName("SchID");
            entity.Property(e => e.DoctorContact)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DoctorName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MedRepId).HasColumnName("MedRepID");
            entity.Property(e => e.Medicine)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MeetingEndTime).HasDefaultValue(new TimeOnly(14, 0, 0));
            entity.Property(e => e.MeetingStartTime).HasDefaultValue(new TimeOnly(13, 0, 0));
            entity.Property(e => e.TreatingAilment)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.MedRep).WithMany(p => p.RepSchedules)
                .HasForeignKey(d => d.MedRepId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RepSchedu__MedRe__797309D9");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
