using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

//namespace TempEfScaffold.Models;

namespace sdq.Entities
{


    [Table("TicketEmployeeAssignmentHistory")]
    public partial class TicketEmployeeAssignmentHistory
    {
        [Key]
        public long Id { get; set; }

        public long TicketId { get; set; }

        public Guid EmployeeId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime AssignmentDate { get; set; }

        public Guid AssignedBy { get; set; }

        [ForeignKey("AssignedBy")]
        [InverseProperty("TicketEmployeeAssignmentHistoryAssignedByNavigations")]
        public virtual ApplicationUser AssignedByNavigation { get; set; } = null!;

        [ForeignKey("EmployeeId")]
        [InverseProperty("TicketEmployeeAssignmentHistoryEmployees")]
        public virtual ApplicationUser Employee { get; set; } = null!;

        [ForeignKey("TicketId")]
        [InverseProperty("TicketEmployeeAssignmentHistories")]
        public virtual Ticket Ticket { get; set; } = null!;
    }
}