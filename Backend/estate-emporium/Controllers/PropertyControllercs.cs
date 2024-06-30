using estate_emporium.Models;
using estate_emporium.Models.HomeLoans;
using estate_emporium.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace estate_emporium.Controllers
{
    [Route("api")]

    public class PropertyController(PropertyManagerService propertyManagerService, LoanService loanService) : Controller
    {
        PropertyManagerService _propertyManagerService=propertyManagerService;
        LoanService _loanService=loanService;   
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
            try
            {
            
                var saleId = await _propertyManagerService.GetProperty(purchaseModel);
                if (saleId == null) { 
                    throw new Exception("Dependency failed: No property availible"); 
                }
                else
                {
                    //let this call happen in the background to not keep persona waiting
                    //if this fails, we will call persona and property maanger on their failure endpoints to let them know
                   
                        var thisSaleId = (long)saleId;
                        try
                        {
                            await _loanService.submitLoanApplicationAsync(thisSaleId);
                        }
                        catch (Exception ex)
                        {
                            await _propertyManagerService.CompleteSale(thisSaleId, false);
                        throw new Exception("Dependency failed: Unable to get a homeloan");
                    }
                    
                }
                return Ok("Request Recieved");
            }
            catch (Exception ex) {
                return StatusCode(424, $"Failed to purchase a home: {ex.Message}");
            }
 
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

            return Ok("Endpoint not yet implemented");
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

            return Ok("Endpoint not yet implemented but body sent correctly");
        }
    }
}
