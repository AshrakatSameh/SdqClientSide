using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

//namespace TempEfScaffold.Models;

namespace sdq.Entities
{
    public partial class TicketAttachment
    {
        [Key]
        public long Id { get; set; }

        public long TicketId { get; set; }

        [StringLength(50)]
        public string FileTitle { get; set; } = null!;

        [StringLength(150)]
        public string FilePath { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime UploadedAt { get; set; }

        public Guid UploadedBy { get; set; }

        [ForeignKey("TicketId")]
        [InverseProperty("TicketAttachments")]
        public virtual Ticket Ticket { get; set; } = null!;

        [ForeignKey("UploadedBy")]
        [InverseProperty("TicketAttachments")]
        public virtual ApplicationUser UploadedByNavigation { get; set; } = null!;
    }
}