﻿// <auto-generated />
using System;
using ComplaintTicketAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ComplaintTicketAPI.Migrations
{
    [DbContext(typeof(ComplaintTicketContext))]
    [Migration("20241201080309_small-update")]
    partial class smallupdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.35")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ComplaintTicketAPI.Models.Complaint", b =>
                {
                    b.Property<int>("ComplaintId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ComplaintId"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizationId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ComplaintId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("UserId");

                    b.ToTable("Complaints");
                });

            modelBuilder.Entity("ComplaintTicketAPI.Models.ComplaintCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ComplaintCategories");
                });

            modelBuilder.Entity("ComplaintTicketAPI.Models.ComplaintFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ComplaintId")
                        .HasColumnType("int");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ComplaintId");

                    b.ToTable("ComplaintFiles");
                });

            modelBuilder.Entity("ComplaintTicketAPI.Models.ComplaintStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CommentByUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ComplaintStatuses");
                });

            modelBuilder.Entity("ComplaintTicketAPI.Models.ComplaintStatusDate", b =>
                {
                    b.Property<int>("ComplaintId")
                        .HasColumnType("int");

                    b.Property<int>("ComplaintStatusId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("StatusDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ComplaintId", "ComplaintStatusId");

                    b.HasIndex("ComplaintStatusId");

                    b.ToTable("ComplaintStatusDates");
                });

            modelBuilder.Entity("ComplaintTicketAPI.Models.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Types")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("ComplaintTicketAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("HashKey")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("PStatus")
                        .HasColumnType("int");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Roles")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TokenExpiry")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ComplaintTicketAPI.Models.UserOtp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Otp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OtpExpiry")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("UserOtp");
                });

            modelBuilder.Entity("ComplaintTicketAPI.Models.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Preferences")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("ComplaintTicketAPI.Models.Complaint", b =>
                {
                    b.HasOne("ComplaintTicketAPI.Models.ComplaintCategory", "Category")
                        .WithMany("Complaints")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ComplaintTicketAPI.Models.Organization", null)
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ComplaintTicketAPI.Models.User", "User")
                        .WithMany("Complaints")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ComplaintTicketAPI.Models.ComplaintFile", b =>
                {
                    b.HasOne("ComplaintTicketAPI.Models.Complaint", "Complaint")
                        .WithMany("ComplaintFiles")
                        .HasForeignKey("ComplaintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Complaint");
                });

            modelBuilder.Entity("ComplaintTicketAPI.Models.ComplaintStatusDate", b =>
                {
                    b.HasOne("ComplaintTicketAPI.Models.Complaint", "Complaint")
                        .WithMany("ComplaintStatusDates")
                        .HasForeignKey("ComplaintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComplaintTicketAPI.Models.ComplaintStatus", "ComplaintStatus")
                        .WithMany("ComplaintStatusDates")
                        .HasForeignKey("ComplaintStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Complaint");

                    b.Navigation("ComplaintStatus");
                });

            modelBuilder.Entity("ComplaintTicketAPI.Models.Organization", b =>
                {
                    b.HasOne("ComplaintTicketAPI.Models.User", null)
                        .WithOne("Organization")
                        .HasForeignKey("ComplaintTicketAPI.Models.Organization", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ComplaintTicketAPI.Models.UserProfile", b =>
                {
                    b.HasOne("ComplaintTicketAPI.Models.User", null)
                        .WithOne("Profile")
                        .HasForeignKey("ComplaintTicketAPI.Models.UserProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ComplaintTicketAPI.Models.Complaint", b =>
                {
                    b.Navigation("ComplaintFiles");

                    b.Navigation("ComplaintStatusDates");
                });

            modelBuilder.Entity("ComplaintTicketAPI.Models.ComplaintCategory", b =>
                {
                    b.Navigation("Complaints");
                });

            modelBuilder.Entity("ComplaintTicketAPI.Models.ComplaintStatus", b =>
                {
                    b.Navigation("ComplaintStatusDates");
                });

            modelBuilder.Entity("ComplaintTicketAPI.Models.User", b =>
                {
                    b.Navigation("Complaints");

                    b.Navigation("Organization");

                    b.Navigation("Profile");
                });
#pragma warning restore 612, 618
        }
    }
}
