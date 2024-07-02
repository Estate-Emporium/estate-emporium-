using estate_emporium.Models;
using estate_emporium.Models.HomeLoans;
using estate_emporium.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace estate_emporium.Controllers
{
    [Route("api")]
    public class PropertyController(BackgroundTaskService taskService, DbService dbService): Controller
    {
        private readonly BackgroundTaskService _taskService= taskService;
        private readonly DbService _dbService = dbService;

        /// <summary>
        /// Initiates the purchase of a house.
        /// </summary>
        /// <returns>A response indicating the result.</returns>
        [HttpPost("buy")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Purchase([FromBody] PurchaseModel purchaseModel)
        {
            _taskService.QueueBackgroundWorkItem(async serviceProvider =>
            {
                var propertyManagerService = serviceProvider.GetRequiredService<PropertyManagerService>();
                var loanService = serviceProvider.GetRequiredService<LoanService>();
                try
                {
                    var saleId = await propertyManagerService.GetProperty(purchaseModel);

                    if (saleId == null)
                    {
                        throw new Exception("Dependency failed: No property available");
                    }

                    var thisSaleId = (long)saleId;
                    try
                    {
                        await loanService.submitLoanApplicationAsync(thisSaleId);
                    }
                    catch (Exception ex)
                    {
                        await propertyManagerService.CompleteSale(thisSaleId, false);
                        // TODO: Call persona to inform them it failed
                    }
                }
                catch (Exception ex)
                {
                    // TODO: Call persona to inform them it failed
                }
            });

            return Accepted("Request Received");
        }

        /// <summary>
        /// Lists a house for sale.
        /// </summary>
        /// <param name="personaId">The Persona ID of the seller.</param>
        /// <returns>A response indicating the result of the listing operation.</returns>
        [HttpPut("sell")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ListHouseForSale([FromBody] ulong personaId)
        {
            //TODO

            return Ok("Endpoint not yet implemented");
        }

        /// <summary>
        /// Approves or denies a loan for a house purchase.
        /// </summary>
        /// <param name="loanApprovalModel">The model containing the loanId, and approval status.</param>
        /// <returns>A response indicating the result of the loan approval operation.</returns>
        [HttpPut("loan/update")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateLoanStatusAsync([FromBody] LoanApprovalModel loanApprovalModel)
        {
            var thisSale = await _dbService.getSalebyLoanId((long)loanApprovalModel.LoanId);
            if (thisSale == null)
            {
                return NotFound("Can't find a sale with the given loan ID");
            }

            _taskService.QueueBackgroundWorkItem(async serviceProvider =>
            {
                var propertyManagerService = serviceProvider.GetRequiredService<PropertyManagerService>();
                var bankService = serviceProvider.GetRequiredService<BankService>();

                try
                {
                    if ((bool)!loanApprovalModel.IsApproved)
                    {
                        await propertyManagerService.CompleteSale(thisSale.SaleId, false);
                        // TODO: Call persona to inform them it failed
                    }
                    else
                    {
                        thisSale.StatusId += 1;
                        await _dbService.saveChangesAsync();

                        // Call back to transfer money to us, tax, seller
                        await bankService.transferAllMoney(thisSale);
                        await propertyManagerService.CompleteSale(thisSale.SaleId, true);
                        // TODO: Call persona to inform them it succeeded
                    }
                }
                catch (Exception ex)
                {
                    await propertyManagerService.CompleteSale(thisSale.SaleId, false);
                    // TODO: Call persona to inform them it failed
                }
            });

            return Accepted("Loan details updated");
        }
    }
}