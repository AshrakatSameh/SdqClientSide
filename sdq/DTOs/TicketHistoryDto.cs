using System.ComponentModel.DataAnnotations.Schema;

namespace sdq.DTOs
{
    public class TicketHistoryDto
    {
        public long Id { get; set; }

        public long TicketId { get; set; }

        public Guid EmployeeId { get; set; }

        public DateTime AssignmentDate { get; set; }

        public Guid AssignedBy { get; set; }
    }
}
