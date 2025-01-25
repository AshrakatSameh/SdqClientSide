namespace sdq.DTOs
{
    public class TicketMessageDto
    {
        public long Id { get; set; }
        public long TicketId { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Message { get; set; }
        public bool IsSeen { get; set; }
    }
}
