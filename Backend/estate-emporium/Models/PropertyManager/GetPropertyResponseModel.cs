using System.ComponentModel.DataAnnotations;

namespace estate_emporium.Models.PropertyManager
{
    public class GetPropertyResponseModel
    {
        /// <summary>
        /// price
        /// </summary>
        /// <example>3</example>
        [Required]
        public long price { get; set; }
        /// <summary>
        /// property ID
        /// </summary>
        /// <example> 2</example>
        [Required]
        public long propertyId { get; set; }
    }
}
