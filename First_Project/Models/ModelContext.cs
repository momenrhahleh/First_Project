using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace First_Project.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aboutu> Aboutus { get; set; }

    public virtual DbSet<Beneficiary> Beneficiaries { get; set; }

    public virtual DbSet<Contactu> Contactus { get; set; }

    public virtual DbSet<Home> Homes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Useradmin> Useradmins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SID=xe)));User Id=C##TASK;Password=Mr12345@1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##TASK")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Aboutu>(entity =>
        {
            entity.HasKey(e => e.Aboutusid).HasName("SYS_C008461");

            entity.ToTable("ABOUTUS");

            entity.Property(e => e.Aboutusid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ABOUTUSID");
            entity.Property(e => e.ImagePath)
                .HasColumnType("BLOB")
                .HasColumnName("IMAGE");
            entity.Property(e => e.Paragraphtext)
                .HasColumnType("CLOB")
                .HasColumnName("PARAGRAPHTEXT");
            entity.Property(e => e.Text)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TEXT");
            
        });

        modelBuilder.Entity<Beneficiary>(entity =>
        {
            entity.HasKey(e => e.Beneficiaryid).HasName("SYS_C008457");

            entity.ToTable("BENEFICIARY");

            entity.Property(e => e.Beneficiaryid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("BENEFICIARYID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Relationship)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("RELATIONSHIP");
            entity.Property(e => e.Subscriptionid)
                .HasColumnType("NUMBER")
                .HasColumnName("SUBSCRIPTIONID");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");
        });

        modelBuilder.Entity<Contactu>(entity =>
        {
            entity.HasKey(e => e.Contactusid).HasName("SYS_C008465");

            entity.ToTable("CONTACTUS");

            entity.Property(e => e.Contactusid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("CONTACTUSID");
            entity.Property(e => e.location)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Email)
                .HasColumnType("CLOB")
                .HasColumnName("Email");
            entity.Property(e => e.PHONE)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PHONE");
           
        });

        modelBuilder.Entity<Home>(entity =>
        {
            entity.HasKey(e => e.Homeid).HasName("SYS_C008459");

            entity.ToTable("HOME");

            entity.Property(e => e.Homeid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("HOMEID");
            entity.Property(e => e.ImagePath)
                .HasColumnType("BLOB")
                .HasColumnName("IMAGE");
            entity.Property(e => e.Text)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TEXT");
            //entity.Property(e => e.Title)
            //    .HasMaxLength(255)
            //    .IsUnicode(false)
            //    .HasColumnName("Title");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("SYS_C008444");

            entity.ToTable("ROLE");

            entity.Property(e => e.Roleid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ROLEID");
            entity.Property(e => e.Rolename)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ROLENAME");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Subscriptionid).HasName("SYS_C008454");

            entity.ToTable("SUBSCRIPTION");

            entity.Property(e => e.Subscriptionid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("SUBSCRIPTIONID");
            entity.Property(e => e.Amount)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("AMOUNT");
            entity.Property(e => e.Paymentstatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PAYMENTSTATUS");
            entity.Property(e => e.Subscriptiondate)
                .HasColumnType("DATE")
                .HasColumnName("SUBSCRIPTIONDATE");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");

            entity.HasOne(d => d.User).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK_SUBSCRIPTION_USER");
        });

        modelBuilder.Entity<Useradmin>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("SYS_C008449");

            entity.ToTable("USERADMIN");

            entity.Property(e => e.Userid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Firstname)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("FIRSTNAME");
            entity.Property(e => e.Lastname)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LASTNAME");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            //entity.Property(e => e.Username)
            //    .HasMaxLength(255)
            //    .IsUnicode(false)
            //    .HasColumnName("USERNAME");
            entity.Property(e => e.ImagePath)
                .HasColumnType("BLOB")
                .HasColumnName("USERPROFILEIMAGE");
            entity.Property(e => e.Userrole)
                .HasColumnType("NUMBER")
                .HasColumnName("USERROLE");

            entity.HasOne(d => d.UserroleNavigation).WithMany(p => p.Useradmins)
                .HasForeignKey(d => d.Userrole)
                .HasConstraintName("FK_USER_ROLE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
