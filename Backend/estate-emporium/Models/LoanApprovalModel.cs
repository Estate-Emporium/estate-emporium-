using System.ComponentModel.DataAnnotations;

namespace estate_emporium.Models
{
    /// <summary>
    /// Model representing the data required to update a loan's approval status.
    /// </summary>
    public class LoanApprovalModel
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
        /// Indicates if the loan was approved or denied.
        /// </summary>
        /// <example>true</example>
        [Required]
        public bool IsApproved { get; set; }
    }
}
