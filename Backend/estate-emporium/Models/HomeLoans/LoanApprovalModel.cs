using System.ComponentModel.DataAnnotations;

namespace estate_emporium.Models.HomeLoans
{
    /// <summary>
    /// Model representing the data required to update a loan's approval status.
    /// </summary>
    public class LoanApprovalModel
    {
        /// <summary>
        /// Home loan ID that is approved or denied
        /// </summary>
        /// <example>43</example>
        [Required]
        public ulong? LoanId { get; set; }

        /// <summary>
        /// Indicates if the loan was approved or denied.
        /// </summary>
        /// <example>true</example>
        [Required]
        public bool? IsApproved { get; set; }
    }
}
