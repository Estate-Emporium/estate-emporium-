using System.ComponentModel.DataAnnotations;

namespace estate_emporium.Models.Stock
{
  public class PostStockModel
  {
    [Required]
    public string name { get; set; }

    public string bankAccount { get; set; }
  }
}
