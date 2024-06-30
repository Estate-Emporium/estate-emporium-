using estate_emporium.Models;
using estate_emporium.Models.HomeLoans;

namespace estate_emporium.Services
{
    public class LoanService(IHttpClientFactory httpClientFactory, DbService dbService)
{
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly DbService _dbService = dbService;
        public async Task submitLoanApplicationAsync(long saleId)
        {

            var client = _httpClientFactory.CreateClient(nameof(HttpClientEnum.home_loans));
            var thisSale=await _dbService.getSaleByIdAsync(saleId);
            var loanApplication = new LoanApplicationModel()
            {
                HouseId = thisSale.PropertyId,
                PersonId = (ulong)thisSale.BuyerId,
                Price = thisSale.SalePrice
            };

            var response = await client.PostAsJsonAsync("api/apply", loanApplication);
            if (response.IsSuccessStatusCode)
            {
                long homeLoanId = await response.Content.ReadFromJsonAsync <long>();
                thisSale.HomeLoanId = homeLoanId;
                await _dbService.saveChangesAsync(); 
            }
            else
            {
                await _dbService.failSaleAsync(saleId);
                Console.WriteLine($"Failed to request Loan. Status code: {response.StatusCode}");
                throw new Exception("Loan service unavailible");
            }
        }
}
}
