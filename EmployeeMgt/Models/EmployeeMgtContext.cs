using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMgt.Models;

public partial class EmployeeMgtContext : DbContext
{
    public EmployeeMgtContext()
    {
    }

    public EmployeeMgtContext(DbContextOptions<EmployeeMgtContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Designation> Designations { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Only configure if not already configured by DI
        if (!optionsBuilder.IsConfigured)
        {
            // optional: fallback from env/config, otherwise do nothing
            var conn = Environment.GetEnvironmentVariable("DefaultConnection");
            if (!string.IsNullOrEmpty(conn))
                optionsBuilder.UseSqlServer(conn);
        }
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       // => optionsBuilder.UseSqlServer("");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__8B69263CD0AC30B6");

            entity.ToTable("Attendance");

            entity.Property(e => e.AttendanceId).HasColumnName("AttendanceID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Status).HasMaxLength(20);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCD2DFB611F");

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
            entity.Property(e => e.Location).HasMaxLength(100);
        });

        modelBuilder.Entity<Designation>(entity =>
        {
            entity.HasKey(e => e.DesignationId).HasName("PK__Designat__BABD603E7E1D7440");

            entity.Property(e => e.DesignationId).HasColumnName("DesignationID");
            entity.Property(e => e.Level).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1FFCF903D");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DesignationId).HasColumnName("DesignationID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(15);
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.LeaveId).HasName("PK__LeaveReq__796DB979E665AE72");

            entity.Property(e => e.LeaveId).HasColumnName("LeaveID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.LeaveType).HasMaxLength(50);
            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(20);
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => e.SalaryId).HasName("PK__Salaries__4BE204B761C3AC9D");

            entity.Property(e => e.SalaryId).HasColumnName("SalaryID");
            entity.Property(e => e.Allowances).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BasicSalary).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Deductions).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Hra)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("HRA");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACE054E91F");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
