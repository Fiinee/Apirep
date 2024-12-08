using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models;

public partial class PractikaContext : DbContext
{
    public PractikaContext()
    {
    }

    public PractikaContext(DbContextOptions<PractikaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Agency> Agencies { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<CommentAgency> CommentAgencies { get; set; }

    public virtual DbSet<CommentTour> CommentTours { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Guide> Guides { get; set; }

    public virtual DbSet<Tour2> Tour2s { get; set; }

    public virtual DbSet<TourPlan> TourPlans { get; set; }

    public virtual DbSet<UsersCart> UsersCarts { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        modelBuilder.Entity<Agency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Agency__737584F7FE97EFFE");

            entity.ToTable("Agency");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NumEmployeer).HasColumnName("Num_employeer");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.CountryNavigation).WithMany(p => p.Cities)
                .HasForeignKey(d => d.Country)
                .HasConstraintName("FK__Cities__Country__398D8EEE");
        });

        modelBuilder.Entity<CommentAgency>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__CommentA__99D3E6C3F942F615");

            entity.ToTable("CommentAgency");

            entity.Property(e => e.CommentId).HasColumnName("Comment_id");
            entity.Property(e => e.CommentText)
                .HasMaxLength(200)
                .HasColumnName("Comment_Text");

            entity.HasOne(d => d.AgencyNavigation).WithMany(p => p.CommentAgencies)
                .HasForeignKey(d => d.Agency)
                .HasConstraintName("FK_CommentAgency_Agency");
        });

        modelBuilder.Entity<CommentTour>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__CommentT__99D3E6C37397F80E");

            entity.ToTable("CommentTour");

            entity.Property(e => e.CommentId).HasColumnName("Comment_id");
            entity.Property(e => e.CommentText)
                .HasMaxLength(200)
                .HasColumnName("Comment_Text");
            entity.Property(e => e.TourPlan).HasColumnName("Tour_Plan");

            entity.HasOne(d => d.TourPlanNavigation).WithMany(p => p.CommentTours)
                .HasForeignKey(d => d.TourPlan)
                .HasConstraintName("FK_CommentTour_Tour_Plan");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("PK__Countrie__737584F791E89EFC");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Guide>(entity =>
        {
            entity.HasKey(e => e.EmployeeCode).HasName("PK__Guide__AB80DE1982717AB3");

            entity.ToTable("Guide");

            entity.Property(e => e.EmployeeCode).HasColumnName("Employee_code");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.AgencyNavigation).WithMany(p => p.Guides)
                .HasForeignKey(d => d.Agency)
                .HasConstraintName("FK_Guide_Agency");
        });

        modelBuilder.Entity<Tour2>(entity =>
        {
            entity.HasKey(e => e.TourCode).HasName("PK__Tour2__EC54E201A0361FC1");

            entity.ToTable("Tour2");

            entity.Property(e => e.TourCode).HasColumnName("Tour_code");
            entity.Property(e => e.DateTime)
                .HasColumnType("datetime")
                .HasColumnName("Date_time");
            entity.Property(e => e.GuideCode).HasColumnName("Guide_code");
            entity.Property(e => e.TourPlan).HasColumnName("Tour_Plan");

            entity.HasOne(d => d.GuideCodeNavigation).WithMany(p => p.Tour2s)
                .HasForeignKey(d => d.GuideCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tour2__Guide_cod__5629CD9C");

            entity.HasOne(d => d.TourPlanNavigation).WithMany(p => p.Tour2s)
                .HasForeignKey(d => d.TourPlan)
                .HasConstraintName("FK_Tour2_Tour_Plan");
        });

        modelBuilder.Entity<TourPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tour_Pla__737584F71D297A01");

            entity.ToTable("Tour_Plan");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CityId).HasColumnName("City_id");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.City).WithMany(p => p.TourPlans)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_Tour_Plan_Cities");
        });

        modelBuilder.Entity<UsersCart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users_ca__C737CA77DB1A17A1");

            entity.ToTable("Users_cart");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TourCode).HasColumnName("Tour_code");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.TourCodeNavigation).WithMany(p => p.UsersCarts)
                .HasForeignKey(d => d.TourCode)
                .HasConstraintName("FK__Users_car__Tour___59FA5E80");

            entity.HasOne(d => d.User).WithMany(p => p.UsersCarts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Users_cart_Account");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
