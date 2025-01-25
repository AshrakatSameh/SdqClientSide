using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

//namespace TempEfScaffold.Models;

namespace sdq.Entities
{
    [Table("ApplicationUser")]
    public partial class ApplicationUser : IdentityUser<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        [InverseProperty("AssignedToEmployeeNavigation")]
        public virtual ICollection<Ticket> TicketAssignedToEmployeeNavigations { get; set; } = new List<Ticket>();

        [InverseProperty("UploadedByNavigation")]
        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; } = new List<TicketAttachment>();

        [InverseProperty("Customer")]
        public virtual ICollection<Ticket> TicketCustomers { get; set; } = new List<Ticket>();

        [InverseProperty("AssignedByNavigation")]
        public virtual ICollection<TicketEmployeeAssignmentHistory> TicketEmployeeAssignmentHistoryAssignedByNavigations { get; set; } = new List<TicketEmployeeAssignmentHistory>();

        [InverseProperty("Employee")]
        public virtual ICollection<TicketEmployeeAssignmentHistory> TicketEmployeeAssignmentHistoryEmployees { get; set; } = new List<TicketEmployeeAssignmentHistory>();

        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<TicketMessage> TicketMessages { get; set; } = new List<TicketMessage>();
    }
}