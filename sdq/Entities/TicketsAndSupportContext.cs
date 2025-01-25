using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using sdq.Entities;

//namespace TempEfScaffold.Models;

namespace sdq.Entities
{
    public partial class TicketsAndSupportContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public TicketsAndSupportContext()
        {
        }

        public TicketsAndSupportContext(DbContextOptions<TicketsAndSupportContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public virtual DbSet<Category> Categories { get; set; }
        
        public virtual DbSet<Status> Statuses { get; set; }

        public virtual DbSet<Priority> Priorities { get; set; }

        public virtual DbSet<Ticket> Tickets { get; set; }

        public virtual DbSet<TicketAttachment> TicketAttachments { get; set; }

        public virtual DbSet<TicketEmployeeAssignmentHistory> TicketEmployeeAssignmentHistories { get; set; }

        public virtual DbSet<TicketMessage> TicketMessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=ASHRAKAT;Database=TicketsAndSupport;Trusted_Connection=True;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ApplicationUser>(entity =>
            //{
            //    entity.Property(e => e.Id).ValueGeneratedNever();
            //});

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Priority>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasOne(d => d.AssignedToEmployeeNavigation).WithMany(p => p.TicketAssignedToEmployeeNavigations).HasConstraintName("FK_Ticket_ApplicationUser1");

                entity.HasOne(d => d.Category).WithMany(p => p.Tickets)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_Category");
                entity.HasOne(d => d.Status).WithMany(p => p.Tickets)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_Status");

                entity.HasOne(d => d.Customer).WithMany(p => p.TicketCustomers)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_ApplicationUser");

                entity.HasOne(d => d.PriorityNavigation).WithMany(p => p.Tickets)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_Priority");
            });

            modelBuilder.Entity<TicketAttachment>(entity =>
            {
                entity.HasOne(d => d.Ticket).WithMany(p => p.TicketAttachments)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketAttachments_Ticket");

                entity.HasOne(d => d.UploadedByNavigation).WithMany(p => p.TicketAttachments)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketAttachments_ApplicationUser");
            });

            modelBuilder.Entity<TicketEmployeeAssignmentHistory>(entity =>
            {
                entity.HasOne(d => d.AssignedByNavigation).WithMany(p => p.TicketEmployeeAssignmentHistoryAssignedByNavigations)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketEmployeeAssignmentHistory_ApplicationUser1");

                entity.HasOne(d => d.Employee).WithMany(p => p.TicketEmployeeAssignmentHistoryEmployees)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketEmployeeAssignmentHistory_ApplicationUser");

                entity.HasOne(d => d.Ticket).WithMany(p => p.TicketEmployeeAssignmentHistories)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketEmployeeAssignmentHistory_Ticket");
            });

            modelBuilder.Entity<TicketMessage>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TicketMessages)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketMessages_ApplicationUser");

                entity.HasOne(d => d.Ticket).WithMany(p => p.TicketMessages)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketMessages_Ticket");
            });


            // Configure Identity tables with Guid keys
            modelBuilder.Entity<IdentityUserLogin<Guid>>().HasKey(login => new { login.LoginProvider, login.ProviderKey });
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(role => new { role.UserId, role.RoleId });
            modelBuilder.Entity<IdentityUserToken<Guid>>().HasKey(token => new { token.UserId, token.LoginProvider, token.Name });

            // Additional configurations for ApplicationUser if needed
            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser");
            modelBuilder.Entity<IdentityRole<Guid>>().ToTable("Roles");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}