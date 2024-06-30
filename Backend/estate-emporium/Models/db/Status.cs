using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace estate_emporium.Models.db;

[Table("Status")]
public partial class Status
{
    [Key]
    [Column("StatusID")]
    public int StatusId { get; set; }

    [Column("Status")]
    [StringLength(255)]
    public string? StatusName { get; set; }

    [InverseProperty("Status")]
    public virtual ICollection<PropertySale> PropertySales { get; set; } = new List<PropertySale>();
}
