using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace ToDoApi.Controllers
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
            /// ID of the house.
            /// </summary>
            /// <example>987654321</example>
            [Required]
            public long HouseId { get; set; }

            /// <summary>
            /// Price of the house in BBDough
            /// </summary>
            /// <example>250000.00</example>
            [Required]
            public decimal Price { get; set; }

            /// <summary>
            /// Persona ID of the seller. (optional).
            /// </summary>
            /// <example>112233445</example>
            public ulong? SellerId { get; set; }
        }
    }
}
