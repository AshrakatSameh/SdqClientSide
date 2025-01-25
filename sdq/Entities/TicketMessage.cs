using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

//namespace TempEfScaffold.Models;

namespace sdq.Entities
{
    public partial class TicketMessage
    {
        [Key]
        public long Id { get; set; }

        public long TicketId { get; set; }

        public Guid CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }

        [StringLength(500)]
        public string Message { get; set; } = null!;

        public bool IsSeen { get; set; }

        [ForeignKey("CreatedBy")]
        [InverseProperty("TicketMessages")]
        public virtual ApplicationUser CreatedByNavigation { get; set; } = null!;

        [ForeignKey("TicketId")]
        [InverseProperty("TicketMessages")]
        public virtual Ticket Ticket { get; set; } = null!;
    }
}