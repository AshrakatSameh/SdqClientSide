using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

//namespace TempEfScaffold.Models;

namespace sdq.Entities { 
[Table("Priority")]
public partial class Priority
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Title { get; set; } = null!;

    [InverseProperty("PriorityNavigation")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}}
