﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using sdq.Entities;

#nullable disable

namespace sdq.Migrations
{
    [DbContext(typeof(TicketsAndSupportContext))]
    [Migration("20250123025547_second")]
    partial class second
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("TempEfScaffold.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUser", (string)null);
                });

            modelBuilder.Entity("TempEfScaffold.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("TempEfScaffold.Models.Priority", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Priority");
                });

            modelBuilder.Entity("TempEfScaffold.Models.Ticket", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<Guid?>("AssignedToEmployee")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AssignedToEmployee");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("Priority");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("TempEfScaffold.Models.TicketAttachment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("FileTitle")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long>("TicketId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UploadedAt")
                        .HasColumnType("datetime");

                    b.Property<Guid>("UploadedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TicketId");

                    b.HasIndex("UploadedBy");

                    b.ToTable("TicketAttachments");
                });

            modelBuilder.Entity("TempEfScaffold.Models.TicketEmployeeAssignmentHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<Guid>("AssignedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AssignmentDate")
                        .HasColumnType("datetime");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("TicketId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AssignedBy");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("TicketId");

                    b.ToTable("TicketEmployeeAssignmentHistory");
                });

            modelBuilder.Entity("TempEfScaffold.Models.TicketMessage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsSeen")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<long>("TicketId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("TicketId");

                    b.ToTable("TicketMessages");
                });

            modelBuilder.Entity("TempEfScaffold.Models.Ticket", b =>
                {
                    b.HasOne("TempEfScaffold.Models.ApplicationUser", "AssignedToEmployeeNavigation")
                        .WithMany("TicketAssignedToEmployeeNavigations")
                        .HasForeignKey("AssignedToEmployee")
                        .HasConstraintName("FK_Ticket_ApplicationUser1");

                    b.HasOne("TempEfScaffold.Models.Category", "Category")
                        .WithMany("Tickets")
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("FK_Ticket_Category");

                    b.HasOne("TempEfScaffold.Models.ApplicationUser", "Customer")
                        .WithMany("TicketCustomers")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK_Ticket_ApplicationUser");

                    b.HasOne("TempEfScaffold.Models.Priority", "PriorityNavigation")
                        .WithMany("Tickets")
                        .HasForeignKey("Priority")
                        .IsRequired()
                        .HasConstraintName("FK_Ticket_Priority");

                    b.Navigation("AssignedToEmployeeNavigation");

                    b.Navigation("Category");

                    b.Navigation("Customer");

                    b.Navigation("PriorityNavigation");
                });

            modelBuilder.Entity("TempEfScaffold.Models.TicketAttachment", b =>
                {
                    b.HasOne("TempEfScaffold.Models.Ticket", "Ticket")
                        .WithMany("TicketAttachments")
                        .HasForeignKey("TicketId")
                        .IsRequired()
                        .HasConstraintName("FK_TicketAttachments_Ticket");

                    b.HasOne("TempEfScaffold.Models.ApplicationUser", "UploadedByNavigation")
                        .WithMany("TicketAttachments")
                        .HasForeignKey("UploadedBy")
                        .IsRequired()
                        .HasConstraintName("FK_TicketAttachments_ApplicationUser");

                    b.Navigation("Ticket");

                    b.Navigation("UploadedByNavigation");
                });

            modelBuilder.Entity("TempEfScaffold.Models.TicketEmployeeAssignmentHistory", b =>
                {
                    b.HasOne("TempEfScaffold.Models.ApplicationUser", "AssignedByNavigation")
                        .WithMany("TicketEmployeeAssignmentHistoryAssignedByNavigations")
                        .HasForeignKey("AssignedBy")
                        .IsRequired()
                        .HasConstraintName("FK_TicketEmployeeAssignmentHistory_ApplicationUser1");

                    b.HasOne("TempEfScaffold.Models.ApplicationUser", "Employee")
                        .WithMany("TicketEmployeeAssignmentHistoryEmployees")
                        .HasForeignKey("EmployeeId")
                        .IsRequired()
                        .HasConstraintName("FK_TicketEmployeeAssignmentHistory_ApplicationUser");

                    b.HasOne("TempEfScaffold.Models.Ticket", "Ticket")
                        .WithMany("TicketEmployeeAssignmentHistories")
                        .HasForeignKey("TicketId")
                        .IsRequired()
                        .HasConstraintName("FK_TicketEmployeeAssignmentHistory_Ticket");

                    b.Navigation("AssignedByNavigation");

                    b.Navigation("Employee");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("TempEfScaffold.Models.TicketMessage", b =>
                {
                    b.HasOne("TempEfScaffold.Models.ApplicationUser", "CreatedByNavigation")
                        .WithMany("TicketMessages")
                        .HasForeignKey("CreatedBy")
                        .IsRequired()
                        .HasConstraintName("FK_TicketMessages_ApplicationUser");

                    b.HasOne("TempEfScaffold.Models.Ticket", "Ticket")
                        .WithMany("TicketMessages")
                        .HasForeignKey("TicketId")
                        .IsRequired()
                        .HasConstraintName("FK_TicketMessages_Ticket");

                    b.Navigation("CreatedByNavigation");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("TempEfScaffold.Models.ApplicationUser", b =>
                {
                    b.Navigation("TicketAssignedToEmployeeNavigations");

                    b.Navigation("TicketAttachments");

                    b.Navigation("TicketCustomers");

                    b.Navigation("TicketEmployeeAssignmentHistoryAssignedByNavigations");

                    b.Navigation("TicketEmployeeAssignmentHistoryEmployees");

                    b.Navigation("TicketMessages");
                });

            modelBuilder.Entity("TempEfScaffold.Models.Category", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("TempEfScaffold.Models.Priority", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("TempEfScaffold.Models.Ticket", b =>
                {
                    b.Navigation("TicketAttachments");

                    b.Navigation("TicketEmployeeAssignmentHistories");

                    b.Navigation("TicketMessages");
                });
#pragma warning restore 612, 618
        }
    }
}
