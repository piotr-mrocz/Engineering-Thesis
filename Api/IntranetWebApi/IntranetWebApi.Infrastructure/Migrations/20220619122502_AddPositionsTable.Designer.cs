﻿// <auto-generated />
using System;
using IntranetWebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IntranetWebApi.Infrastructure.Migrations
{
    [DbContext(typeof(IntranetDbContext))]
    [Migration("20220619122502_AddPositionsTable")]
    partial class AddPositionsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("IntranetWebApi.Domain.Models.Entities.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdSupervisor")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("IntranetWebApi.Domain.Models.Entities.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdUser")
                        .IsUnique();

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("IntranetWebApi.Domain.Models.Entities.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("IntranetWebApi.Domain.Models.Entities.Presence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AbsenceReason")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<decimal>("ExtraWorkHours")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<bool>("IsPresent")
                        .HasColumnType("bit");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<decimal>("WorkHours")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdUser");

                    b.ToTable("Presences");
                });

            modelBuilder.Entity("IntranetWebApi.Domain.Models.Entities.RequestForLeave", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AbsenceType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfAcceptance")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfCancel")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdApplicant")
                        .HasColumnType("int");

                    b.Property<int>("IdSupervisor")
                        .HasColumnType("int");

                    b.Property<bool>("IsAcceptedBySupervisor")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCancel")
                        .HasColumnType("bit");

                    b.Property<string>("RejectionReason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdApplicant");

                    b.ToTable("RequestForLeaves");
                });

            modelBuilder.Entity("IntranetWebApi.Domain.Models.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("IntranetWebApi.Domain.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfEmployment")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfRelease")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdDepartment")
                        .HasColumnType("int");

                    b.Property<int>("IdRole")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdDepartment");

                    b.HasIndex("IdRole")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IntranetWebApi.Domain.Models.Entities.Views.VUsersPresence", b =>
                {
                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUser"), 1L, 1);

                    b.Property<int?>("AbsenceReason")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<decimal>("ExtraWorkHours")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IdDepartment")
                        .HasColumnType("int");

                    b.Property<bool>("IsPresent")
                        .HasColumnType("bit");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("WorkHours")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdUser");

                    b.ToView("VUsersPresences");
                });

            modelBuilder.Entity("IntranetWebApi.Domain.Models.Entities.Views.VUsersRequestForLeave", b =>
                {
                    b.Property<int>("IdRequest")
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRequest"), 1L, 1);

                    b.Property<int>("AbsenceType")
                        .HasColumnType("int");

                    b.Property<string>("DisplayUserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdApplicant")
                        .HasColumnType("int");

                    b.Property<int>("IdSupervisor")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("IdRequest");

                    b.ToView("VUsersRequestsForLeave");
                });

            modelBuilder.Entity("IntranetWebApi.Models.Entities.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TestowaTabela");
                });

            modelBuilder.Entity("IntranetWebApi.Domain.Models.Entities.Photo", b =>
                {
                    b.HasOne("IntranetWebApi.Domain.Models.Entities.User", "User")
                        .WithOne("Photo")
                        .HasForeignKey("IntranetWebApi.Domain.Models.Entities.Photo", "IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("IntranetWebApi.Domain.Models.Entities.Presence", b =>
                {
                    b.HasOne("IntranetWebApi.Domain.Models.Entities.User", "User")
                        .WithMany("Presences")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("IntranetWebApi.Domain.Models.Entities.RequestForLeave", b =>
                {
                    b.HasOne("IntranetWebApi.Domain.Models.Entities.User", "Applicant")
                        .WithMany("RequestForLeaves")
                        .HasForeignKey("IdApplicant")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applicant");
                });

            modelBuilder.Entity("IntranetWebApi.Domain.Models.Entities.User", b =>
                {
                    b.HasOne("IntranetWebApi.Domain.Models.Entities.Department", "Department")
                        .WithMany("Users")
                        .HasForeignKey("IdDepartment")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IntranetWebApi.Domain.Models.Entities.Role", "Role")
                        .WithOne("User")
                        .HasForeignKey("IntranetWebApi.Domain.Models.Entities.User", "IdRole")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("IntranetWebApi.Domain.Models.Entities.Department", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("IntranetWebApi.Domain.Models.Entities.Role", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("IntranetWebApi.Domain.Models.Entities.User", b =>
                {
                    b.Navigation("Photo")
                        .IsRequired();

                    b.Navigation("Presences");

                    b.Navigation("RequestForLeaves");
                });
#pragma warning restore 612, 618
        }
    }
}