using System.ComponentModel.DataAnnotations;

namespace estate_emporium.Models.PropertyManager
{
    public class PutPropertyCompleteModel
    {
        /// <summary>
        /// property ID
        /// </summary>
        /// <example> 2</example>
        [Required]
        public long propertyId { get; set; }

        /// <summary>
        /// Persona ID of the seller.
        /// </summary>
        /// <example>123456789</example>
        [Required]
        public long sellerId { get; set; }

        /// <summary>
        /// Persona ID of the buyer.
        /// </summary>
        /// <example>123456789</example>
        [Required]
        public long buyerId { get; set; }



        /// <summary>
        /// price
        /// </summary>
        /// <example>3</example>
        [Required]
        public long price { get; set; }

        /// <summary>
        /// Indicates transfer worked or not
        /// </summary>
        /// <example>true</example>
        [Required]
        public bool approval { get; set; }

    }
}
