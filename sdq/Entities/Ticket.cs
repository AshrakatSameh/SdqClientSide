using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//namespace TempEfScaffold.Models;

namespace sdq.Entities
{
    [Table("Ticket")]
    public partial class Ticket
    {
        [Key]
        public long Id { get; set; }

        public Guid CustomerId { get; set; }

        public Guid? AssignedToEmployee { get; set; }

        [StringLength(50)]
        public string Title { get; set; } = null!;

        [StringLength(500)]
        public string Description { get; set; } = null!;
        public int StatusId { get; set; }

        public int CategoryId { get; set; }

        public int Priority { get; set; }

        [ForeignKey("AssignedToEmployee")]
        [InverseProperty("TicketAssignedToEmployeeNavigations")]
        public virtual ApplicationUser? AssignedToEmployeeNavigation { get; set; }

        [ForeignKey("CategoryId")]
        [InverseProperty("Tickets")]
        public virtual Category Category { get; set; } = null!;
        [ForeignKey("StatusId")]
        [InverseProperty("Tickets")]
        public virtual Status Status { get; set; } = null!;

        [ForeignKey("CustomerId")]
        [InverseProperty("TicketCustomers")]
        public virtual ApplicationUser Customer { get; set; } = null!;

        [ForeignKey("Priority")]
        [InverseProperty("Tickets")]
        public virtual Priority PriorityNavigation { get; set; } = null!;

        [InverseProperty("Ticket")]
        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; } = new List<TicketAttachment>();

        [InverseProperty("Ticket")]
        public virtual ICollection<TicketEmployeeAssignmentHistory> TicketEmployeeAssignmentHistories { get; set; } = new List<TicketEmployeeAssignmentHistory>();

        [InverseProperty("Ticket")]
        public virtual ICollection<TicketMessage> TicketMessages { get; set; } = new List<TicketMessage>();
    }
}
