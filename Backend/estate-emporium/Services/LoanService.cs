using estate_emporium.Models;

namespace estate_emporium.Services
{
    public class LoanService(IHttpClientFactory httpClientFactory, DbService dbService)
{
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly DbService _dbService = dbService;
        public async Task submitLoanApplicationAsync(long saleID)
        {
            var client = _httpClientFactory.CreateClient(nameof(HttpClientEnum.home_loans));
            var thisSale= _dbService.getSaleById(saleID);
            var loanApplication = new LoanApplicationModel()
            {
                HouseId = thisSale.PropertyId,
                PersonId = (ulong)thisSale.BuyerId,
                Price = thisSale.SalePrice
            };

            var response = await client.PostAsJsonAsync("api/apply", loanApplication);
        }
}
}
