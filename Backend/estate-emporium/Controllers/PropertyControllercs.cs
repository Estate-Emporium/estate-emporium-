using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace estate_emporium.Controllers
{
    [Route("api")]

    public class PropertyController : Controller
    {
        /// <summary>
        /// Initiates the purchase of a house.
        /// </summary>
        /// <returns>A response indicating the result.</returns>
        [HttpPost]
        [Route("buy")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Purchase([FromBody] PurchaseModel purchaseModel)
        {
            // Your code logic here

            return new JsonResult("Purchase Successful");
        }

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
            public ulong BuyerId { get; set; }

            /// <summary>
            /// Number of units required 1 to 8
            /// </summary>
            /// <example>3</example>
            [Required]
            public int NumUnits { get; set; }
        }
        /// <summary>
        /// Lists a house for sale.
        /// </summary>
        /// <param name="personaId">The Persona ID of the seller.</param>
        /// <returns>A response indicating the result of the listing operation.</returns>
        [HttpPut]
        [Route("sell")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ListHouseForSale([FromBody] ulong personaId)
        {
            // Your code logic to list the house for sale

            return new JsonResult("House listed for sale successfully");
        }
        /// <summary>
        /// Approves or denies a loan for a house purchase.
        /// </summary>
        /// <param name="loanApprovalModel">The model containing the personID, houseID, and approval status.</param>
        /// <returns>A response indicating the result of the loan approval operation.</returns>
        [HttpPut]
        [Route("loan/update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateLoanStatus([FromBody] LoanApprovalModel loanApprovalModel)
        {
            // Your code logic to update the loan status

            return new JsonResult($"Loan status updated: {(loanApprovalModel.IsApproved ? "Approved" : "Denied")}");
        }

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
}
