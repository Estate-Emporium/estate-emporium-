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
                long sellerbankID=await getPersonaAccount(thisSale.SellerId);
                await payCompany(thisSale, amountToPersona, sellerbankID.ToString(), "Sale");
            }

            await payCompany(thisSale, commissionAmount, Consts.ourAccount, "Commission");
            await payCompany(thisSale, amountToTax, Consts.taxAccount, "Tax");

        }
        public async Task payCompany(PropertySale thisSale, long amount, string recepient, string reason)
        {

            var client = _httpClientFactory.CreateClient(nameof(HttpClientEnum.retail_bank));
            var payload = new PaymentModel()
            {
                AmountInMibiBBDough = amount,
                SenderId = thisSale.BuyerId,
                Reference = $"{reason} for Home: {thisSale.PropertyId} with Loan: {thisSale.HomeLoanId}",
                Recepient = new Recipient()
                {
                    AccountId = recepient,
                    BankId = 1001
                }
            };
            var response = await client.PostAsJsonAsync("api/transactions/payments", payload);

            if (response.IsSuccessStatusCode)
            {
               
            }
            else
            {
                await _dbService.failSaleAsync(thisSale.SaleId);
                throw new Exception("Unable to complete payments");
            }

        }
        public async Task<long> getPersonaAccount(long personaId)
        {
            var client = _httpClientFactory.CreateClient(nameof(HttpClientEnum.retail_bank));
            var response = await client.GetAsync($"api/customers/{personaId}/accounts");
            var accountResponse = await response.Content.ReadFromJsonAsync<AccountModel>();
            return accountResponse.AccountId;
        }

    }
}
