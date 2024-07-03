using System.ComponentModel.DataAnnotations;

namespace estate_emporium.Models.PropertyManager
{
    public class SellPropertyModel
    {
        /// <summary>
        /// Persona ID of the seller.
        /// </summary>
        /// <example>123456789</example>
        [Required]
        public ulong sellerId { get; set; }

    }
}
