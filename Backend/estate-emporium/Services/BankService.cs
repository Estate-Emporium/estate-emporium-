using estate_emporium.Models;
using estate_emporium.Models.Bank;
using estate_emporium.Models.db;
using estate_emporium.Models.PropertyManager;
using estate_emporium.Utils;
using Microsoft.AspNetCore.Http.Connections;

namespace estate_emporium.Services
{
    public class BankService(IHttpClientFactory httpClientFactory, DbService dbService)
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly DbService _dbService = dbService;


        public async Task transferAllMoney(PropertySale thisSale) {


            var basePrice = thisSale.SalePrice / (1 + Consts.Commission / 100);
            var commissionAmount = thisSale.SalePrice-basePrice;
            long amountToTax = 0;
            if(thisSale.SellerId == -1) {
                amountToTax = basePrice;
            }
            else
            {
                amountToTax = basePrice*Consts.taxPercentage/100;
                var amountToPersona = basePrice - amountToTax;
                long sellerbankID=thisSale.SellerId;
                await pay(thisSale, amountToPersona, sellerbankID.ToString(), "Sale", false);
            }

            await pay(thisSale, commissionAmount, Consts.ourAccount, "Commission");
            await pay(thisSale, amountToTax, Consts.taxAccount, "Tax");

        }
        public async Task pay(PropertySale thisSale, long amount, string recepient, string reason, bool isCompany=true)
        {

            var client = _httpClientFactory.CreateClient(nameof(HttpClientEnum.retail_bank));
            var payload = new PaymentModel()
            {
                AmountInMibiBBDough = amount,
                SenderId = thisSale.BuyerId,
                Reference = $"real_estate_sales : {reason} for Home: {thisSale.PropertyId} with Loan: {thisSale.HomeLoanId}",
                Recepient = new Recipient()
                {
                    AccountId = recepient,
                    BankId = isCompany ? 1001 : 1000
        }
            };
            var response = await client.PostAsJsonAsync("api/transactions/payments", payload);

            if (response.IsSuccessStatusCode)
            {
               
            }
            else
            {
                await _dbService.failSaleAsync(thisSale.SaleId);
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed to contact bank. Status code: {response.StatusCode}\n Error{body}");
                throw new Exception("Unable to complete payments");
            }

        }
    }
}
