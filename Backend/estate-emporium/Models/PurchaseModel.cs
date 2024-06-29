using System.ComponentModel.DataAnnotations;

namespace estate_emporium.Models
{
    /// <summary>
    /// Model representing the data required to initiate a house purchase.
    /// </summary>
    public class PurchaseModel
    {
        /// <summary>
        /// Persona ID of the buyer.
        /// </summary>
        /// <example>123456789</example>
        [Required]
        public ulong? BuyerId { get; set; }

        /// <summary>
        /// Number of units required 1 to 8
        /// </summary>
        /// <example>3</example>
        [Required]
        [Range(1, 8, ErrorMessage = "The value of the house units must be from 1 to 8.")]
        public int? NumUnits { get; set; }
    }
}
