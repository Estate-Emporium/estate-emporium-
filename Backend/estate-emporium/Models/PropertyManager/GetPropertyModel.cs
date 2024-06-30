using System.ComponentModel.DataAnnotations;

namespace estate_emporium.Models.PropertyManager
{
    public class GetPropertyModel
    {

        /// <summary>
        /// Number of units required 1 to 8
        /// </summary>
        /// <example>3</example>
        [Required]
        public int size { get; set; }

        /// <summary>
        /// Not for us, should always be false
        /// </summary>
        /// <example>false </example>

        public bool toRent { get; set; } = false;
    }
}
