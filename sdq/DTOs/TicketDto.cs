using sdq.Entities;

namespace sdq.DTOs
{
    public class TicketDto
    {
        public long Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? AssignedToEmployee { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
        public ICollection<TicketAttachment> Attachments { get; set; }
        public ICollection<TicketEmployeeAssignmentHistory> AssignmentHistories { get; set; }
        public ICollection<TicketMessage> Messages { get; set; }
    }
}
