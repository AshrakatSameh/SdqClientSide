using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sdq.DTOs
{
    public class TicketAttachmentDto
    {
        public long Id { get; set; }

        public long TicketId { get; set; }
        public string FileTitle { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public DateTime UploadedAt { get; set; }
        public Guid UploadedBy { get; set; }
    }
}
