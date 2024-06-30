using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace estate_emporium.Models.db;

public partial class PropertySale
{
    [Key]
    [Column("SaleID")]
    public long SaleId { get; set; }

    [Column("BuyerID")]
    public long BuyerId { get; set; }

    [Column("SellerID")]
    public long SellerId { get; set; } = -1;

    [Column("PropertyID")]
    public long PropertyId { get; set; } = -1;

    public long SalePrice { get; set; } = -1;

    [Column("HomeLoanID")]
    public long HomeLoanId { get; set; } = -1;

    [Column(TypeName = "datetime")]
    public DateTime PurchaseDate { get; set; } = DateTime.Now;

    [Column("StatusID")]
    public int? StatusId { get; set; } = 0;

    [ForeignKey("StatusId")]
    [InverseProperty("PropertySales")]
    public virtual Status? Status { get; set; }
}
