using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace estate_emporium.Models;

public partial class PropertySale
{
    [Key]
    [Column("SaleID")]
    public long SaleId { get; set; }

    [Column("BuyerID")]
    public long BuyerId { get; set; }

    [Column("SellerID")]
    public long SellerId { get; set; }

    [Column("PropertyID")]
    public long PropertyId { get; set; }

    public long SalePrice { get; set; }

    [Column("HomeLoanID")]
    public long HomeLoanId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime PurchaseDate { get; set; }

    [Column("StatusID")]
    public int? StatusId { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("PropertySales")]
    public virtual Status? Status { get; set; }
}
