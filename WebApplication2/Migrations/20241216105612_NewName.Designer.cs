﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication2.DataAccess.Models;



#nullable disable

namespace WebApplication2.Migrations
{
    [DbContext(typeof(PractikaContext))]
    [Migration("20241216105612_NewName")]
    partial class NewName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApplication2.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("PasswordReset")
                        .HasColumnType("datetime2");

                    b.Property<string>("ResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Verified")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("WebApplication2.Models.Agency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("NumEmployeer")
                        .HasColumnType("int")
                        .HasColumnName("Num_employeer");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.HasKey("Id")
                        .HasName("PK__Agency__737584F7FE97EFFE");

                    b.ToTable("Agency", (string)null);
                });

            modelBuilder.Entity("WebApplication2.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Country");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("WebApplication2.Models.CommentAgency", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Comment_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"));

                    b.Property<int>("Agency")
                        .HasColumnType("int");

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("Comment_Text");

                    b.HasKey("CommentId")
                        .HasName("PK__CommentA__99D3E6C3F942F615");

                    b.HasIndex("Agency");

                    b.ToTable("CommentAgency", (string)null);
                });

            modelBuilder.Entity("WebApplication2.Models.CommentTour", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Comment_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"));

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("Comment_Text");

                    b.Property<int>("TourPlan")
                        .HasColumnType("int")
                        .HasColumnName("Tour_Plan");

                    b.HasKey("CommentId")
                        .HasName("PK__CommentT__99D3E6C37397F80E");

                    b.HasIndex("TourPlan");

                    b.ToTable("CommentTour", (string)null);
                });

            modelBuilder.Entity("WebApplication2.Models.Country", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Population")
                        .HasColumnType("int");

                    b.HasKey("Name")
                        .HasName("PK__Countrie__737584F791E89EFC");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("WebApplication2.Models.Guide", b =>
                {
                    b.Property<int>("EmployeeCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Employee_code");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeCode"));

                    b.Property<int>("Agency")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.HasKey("EmployeeCode")
                        .HasName("PK__Guide__AB80DE1982717AB3");

                    b.HasIndex("Agency");

                    b.ToTable("Guide", (string)null);
                });

            modelBuilder.Entity("WebApplication2.Models.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByIp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReasonRevoked")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReplacedByToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime2");

                    b.Property<string>("RevolkedByIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("WebApplication2.Models.Tour2", b =>
                {
                    b.Property<int>("TourCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Tour_code");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TourCode"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime")
                        .HasColumnName("Date_time");

                    b.Property<int>("GuideCode")
                        .HasColumnType("int")
                        .HasColumnName("Guide_code");

                    b.Property<int>("TourPlan")
                        .HasColumnType("int")
                        .HasColumnName("Tour_Plan");

                    b.HasKey("TourCode")
                        .HasName("PK__Tour2__EC54E201A0361FC1");

                    b.HasIndex("GuideCode");

                    b.HasIndex("TourPlan");

                    b.ToTable("Tour2", (string)null);
                });

            modelBuilder.Entity("WebApplication2.Models.TourPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CityId")
                        .HasColumnType("int")
                        .HasColumnName("City_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.HasKey("Id")
                        .HasName("PK__Tour_Pla__737584F71D297A01");

                    b.HasIndex("CityId");

                    b.ToTable("Tour_Plan", (string)null);
                });

            modelBuilder.Entity("WebApplication2.Models.UsersCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("TourCode")
                        .HasColumnType("int")
                        .HasColumnName("Tour_code");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("User_id");

                    b.HasKey("Id")
                        .HasName("PK__Users_ca__C737CA77DB1A17A1");

                    b.HasIndex("TourCode");

                    b.HasIndex("UserId");

                    b.ToTable("Users_cart", (string)null);
                });

            modelBuilder.Entity("WebApplication2.Models.City", b =>
                {
                    b.HasOne("WebApplication2.Models.Country", "CountryNavigation")
                        .WithMany("Cities")
                        .HasForeignKey("Country")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Cities__Country__398D8EEE");

                    b.Navigation("CountryNavigation");
                });

            modelBuilder.Entity("WebApplication2.Models.CommentAgency", b =>
                {
                    b.HasOne("WebApplication2.Models.Agency", "AgencyNavigation")
                        .WithMany("CommentAgencies")
                        .HasForeignKey("Agency")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_CommentAgency_Agency");

                    b.Navigation("AgencyNavigation");
                });

            modelBuilder.Entity("WebApplication2.Models.CommentTour", b =>
                {
                    b.HasOne("WebApplication2.Models.TourPlan", "TourPlanNavigation")
                        .WithMany("CommentTours")
                        .HasForeignKey("TourPlan")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_CommentTour_Tour_Plan");

                    b.Navigation("TourPlanNavigation");
                });

            modelBuilder.Entity("WebApplication2.Models.Guide", b =>
                {
                    b.HasOne("WebApplication2.Models.Agency", "AgencyNavigation")
                        .WithMany("Guides")
                        .HasForeignKey("Agency")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Guide_Agency");

                    b.Navigation("AgencyNavigation");
                });

            modelBuilder.Entity("WebApplication2.Models.RefreshToken", b =>
                {
                    b.HasOne("WebApplication2.Models.Account", null)
                        .WithMany("RefreshTokens")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("WebApplication2.Models.Tour2", b =>
                {
                    b.HasOne("WebApplication2.Models.Guide", "GuideCodeNavigation")
                        .WithMany("Tour2s")
                        .HasForeignKey("GuideCode")
                        .IsRequired()
                        .HasConstraintName("FK__Tour2__Guide_cod__5629CD9C");

                    b.HasOne("WebApplication2.Models.TourPlan", "TourPlanNavigation")
                        .WithMany("Tour2s")
                        .HasForeignKey("TourPlan")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Tour2_Tour_Plan");

                    b.Navigation("GuideCodeNavigation");

                    b.Navigation("TourPlanNavigation");
                });

            modelBuilder.Entity("WebApplication2.Models.TourPlan", b =>
                {
                    b.HasOne("WebApplication2.Models.City", "City")
                        .WithMany("TourPlans")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Tour_Plan_Cities");

                    b.Navigation("City");
                });

            modelBuilder.Entity("WebApplication2.Models.UsersCart", b =>
                {
                    b.HasOne("WebApplication2.Models.Tour2", "TourCodeNavigation")
                        .WithMany("UsersCarts")
                        .HasForeignKey("TourCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Users_car__Tour___59FA5E80");

                    b.HasOne("WebApplication2.Models.Account", "User")
                        .WithMany("UsersCarts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Users_cart_Account");

                    b.Navigation("TourCodeNavigation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApplication2.Models.Account", b =>
                {
                    b.Navigation("RefreshTokens");

                    b.Navigation("UsersCarts");
                });

            modelBuilder.Entity("WebApplication2.Models.Agency", b =>
                {
                    b.Navigation("CommentAgencies");

                    b.Navigation("Guides");
                });

            modelBuilder.Entity("WebApplication2.Models.City", b =>
                {
                    b.Navigation("TourPlans");
                });

            modelBuilder.Entity("WebApplication2.Models.Country", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("WebApplication2.Models.Guide", b =>
                {
                    b.Navigation("Tour2s");
                });

            modelBuilder.Entity("WebApplication2.Models.Tour2", b =>
                {
                    b.Navigation("UsersCarts");
                });

            modelBuilder.Entity("WebApplication2.Models.TourPlan", b =>
                {
                    b.Navigation("CommentTours");

                    b.Navigation("Tour2s");
                });
#pragma warning restore 612, 618
        }
    }
}