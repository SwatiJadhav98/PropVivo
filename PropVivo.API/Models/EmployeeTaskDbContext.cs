using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PropVivo.API.Models;

public partial class EmployeeTaskDbContext : DbContext
{
    public EmployeeTaskDbContext()
    {
    }

    public EmployeeTaskDbContext(DbContextOptions<EmployeeTaskDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<QuerieMaster> QuerieMasters { get; set; }

    public virtual DbSet<QueryResponse> QueryResponses { get; set; }

    public virtual DbSet<TaskMaster> TaskMasters { get; set; }

    public virtual DbSet<TimeLogMaster> TimeLogMasters { get; set; }

    public virtual DbSet<UserMaster> UserMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-VNBPVQS;Initial Catalog=EmployeeTaskDB;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__8B69263C9134DCBB");

            entity.ToTable("Attendance");

            entity.Property(e => e.AttendanceId).HasColumnName("AttendanceID");
            entity.Property(e => e.Completed).HasDefaultValue(false);
            entity.Property(e => e.TotalBreakTime).HasDefaultValue(0.0);
            entity.Property(e => e.TotalWorkHours).HasDefaultValue(0.0);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Attendanc__UserI__4CA06362");
        });

        modelBuilder.Entity<QuerieMaster>(entity =>
        {
            entity.HasKey(e => e.QueryId).HasName("PK__QuerieMa__5967F7DBF1A08528");

            entity.ToTable("QuerieMaster");

            entity.Property(e => e.AttachmentPath)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Open");
            entity.Property(e => e.Subject)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Task).WithMany(p => p.QuerieMasters)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__QuerieMas__TaskI__4D94879B");
        });

        modelBuilder.Entity<QueryResponse>(entity =>
        {
            entity.HasKey(e => e.ResponseId).HasName("PK__QueryRes__1AAA646C99D101A4");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Message).HasColumnType("text");

            entity.HasOne(d => d.Query).WithMany(p => p.QueryResponses)
                .HasForeignKey(d => d.QueryId)
                .HasConstraintName("FK__QueryResp__Query__4E88ABD4");

            entity.HasOne(d => d.Responder).WithMany(p => p.QueryResponses)
                .HasForeignKey(d => d.ResponderId)
                .HasConstraintName("FK__QueryResp__Respo__4F7CD00D");
        });

        modelBuilder.Entity<TaskMaster>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__TaskMast__7C6949B1057C32EB");

            entity.ToTable("TaskMaster");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.TaskMasterAssignedToNavigations)
                .HasForeignKey(d => d.AssignedTo)
                .HasConstraintName("FK__TaskMaste__Assig__5070F446");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TaskMasterCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__TaskMaste__Creat__5165187F");
        });

        modelBuilder.Entity<TimeLogMaster>(entity =>
        {
            entity.HasKey(e => e.TimeLogId).HasName("PK__TimeLogM__26E43757A174DAC2");

            entity.ToTable("TimeLogMaster");

            entity.Property(e => e.BreakReason)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.IsBreak).HasDefaultValue(false);
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.Task).WithMany(p => p.TimeLogMasters)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__TimeLogMa__TaskI__52593CB8");

            entity.HasOne(d => d.User).WithMany(p => p.TimeLogMasters)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__TimeLogMa__UserI__534D60F1");
        });

        modelBuilder.Entity<UserMaster>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserMast__1788CC4C3E973375");

            entity.ToTable("UserMaster");

            entity.HasIndex(e => e.Email, "UQ__UserMast__A9D10534342A0B5C").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
