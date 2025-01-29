using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

//namespace TempEfScaffold.Models;

namespace sdq.Entities
{
    [Table("Category")]
    public partial class Category
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; } = null!;

    
        [InverseProperty("Category")]
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}