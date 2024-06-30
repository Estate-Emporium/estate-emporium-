using System.ComponentModel.DataAnnotations;

namespace estate_emporium.Models.HomeLoans
{
    public class LoanApplicationModel
    {
        /// <summary>
        /// Persona ID of the person.
        /// </summary>
        /// <example>123456789</example>
        [Required]
        public ulong PersonId { get; set; }

        /// <summary>
        /// House ID for which the loan is being updated.
        /// </summary>
        /// <example>987654321</example>
        [Required]
        public long HouseId { get; set; }

        /// <summary>
        /// Price in cents
        /// </summary>
        /// <example>23232</example>
        [Required]
        public long Price { get; set; }

    }
}
