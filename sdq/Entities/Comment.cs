namespace sdq.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string CreatedBy { get; set; }
        public int TicketId { get; set; }

    }
}
