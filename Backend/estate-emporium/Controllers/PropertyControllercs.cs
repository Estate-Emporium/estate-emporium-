using estate_emporium.Models;
using estate_emporium.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace estate_emporium.Controllers
{
    [Route("api")]

    public class PropertyController(PropertyManagerService propertyManagerService) : Controller
    {
        PropertyManagerService _propertyManagerService=propertyManagerService;
        /// <summary>
        /// Initiates the purchase of a house.
        /// </summary>
        /// <returns>A response indicating the result.</returns>
        [HttpPost]
        [Route("buy")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Purchase([FromBody] PurchaseModel purchaseModel)
        {
            // Your code logic here

            // 1. call property manager to get home price
             await _propertyManagerService.GetProperty(purchaseModel);
            //2. call home loan
            return Ok("Purchase Successful");
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
    }
}
