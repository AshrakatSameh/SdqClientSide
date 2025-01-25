using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace sdq.Entities
{
    [Table("Status")]

    public class Status
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; } = null!;

        [InverseProperty("Status")]
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
