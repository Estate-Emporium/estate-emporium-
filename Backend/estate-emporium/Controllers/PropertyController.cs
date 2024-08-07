﻿using estate_emporium.Models;
using estate_emporium.Models.HomeLoans;
using estate_emporium.Models.PropertyManager;
using estate_emporium.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace estate_emporium.Controllers
{
    [Route("api")]
    public class PropertyController(BackgroundTaskService taskService, DbService dbService, PropertyManagerService propertyManager): Controller
    {
        private readonly BackgroundTaskService _taskService= taskService;
        private readonly DbService _dbService = dbService;

        private readonly PropertyManagerService _propertyManagerService = propertyManager;

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
                var personaService=serviceProvider.GetRequiredService<PersonaService>();
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
                        await personaService.CompleteSale((ulong)purchaseModel.BuyerId, false);
                       
                    }
                }
                catch (Exception ex)
                {
                    await personaService.CompleteSale((ulong)purchaseModel.BuyerId, false);
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
        public async Task<IActionResult> ListHouseForSaleAsync([FromBody] SellPropertyModel sellModel)
        {
                try
                {
                    await _propertyManagerService.SellProperty((long)sellModel.sellerId);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

            return Ok("LISTED FOR SALE");
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
            var thisSale = await _dbService.getSalebyLoanId(loanApprovalModel.LoanId);
            if (thisSale == null)
            {
                return NotFound("Can't find a sale with the given loan ID");
            }

            _taskService.QueueBackgroundWorkItem(async serviceProvider =>
            {
                var propertyManagerService = serviceProvider.GetRequiredService<PropertyManagerService>();
                var bankService = serviceProvider.GetRequiredService<BankService>();
                var personaService = serviceProvider.GetRequiredService<PersonaService>();
                var dbSerivce=serviceProvider.GetRequiredService<DbService>();

                try
                {
                    if ((bool)!loanApprovalModel.IsApproved)
                    {
                        await propertyManagerService.CompleteSale(thisSale.SaleId, false);
                        await personaService.CompleteSale((ulong)thisSale.BuyerId, false);
                    }
                    else
                    {
                        await dbSerivce.setStatusbyId(thisSale.SaleId,2);
                        await bankService.transferAllMoney(thisSale);
                        await dbSerivce.setStatusbyId(thisSale.SaleId, 3);
                        await propertyManagerService.CompleteSale(thisSale.SaleId, true);
                        await dbSerivce.setStatusbyId(thisSale.SaleId, 4);
                        await personaService.CompleteSale((ulong)thisSale.BuyerId, true);
                        await dbSerivce.setStatusbyId(thisSale.SaleId, 5);
                    }
                }
                catch (Exception ex)
                {
                    await propertyManagerService.CompleteSale(thisSale.SaleId, false);
                    await personaService.CompleteSale((ulong)thisSale.BuyerId, false);
                }
            });

            return Accepted("Loan details updated");
        }
    }
}