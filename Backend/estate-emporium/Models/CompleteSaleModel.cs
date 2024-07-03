using System.ComponentModel.DataAnnotations;

namespace estate_emporium.Models
{
    public class completeSaleModel
    {
        /// <summary>
        /// Persona ID of the buyer.
        /// </summary>
        /// <example>123456789</example>
        [Required]
        public ulong personaId { get; set; }

        [Required]
        public bool isSuccess { get; set; }

    }
}
