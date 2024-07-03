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
                property_id = thisSale.PropertyId.ToString(),
                candidate_id = thisSale.BuyerId.ToString(),
                loan_amount_cents = thisSale.SalePrice
            };

            var response = await client.PostAsJsonAsync("api/apply", loanApplication);
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                var resp = await response.Content.ReadFromJsonAsync <LoanResponseModel>();
                await _dbService.populateHomeLoanId(thisSale, resp.Data); 
            }
            else
            {
                await _dbService.failSaleAsync(saleId);
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed to contact homeloan. Status code: {response.StatusCode}\n Error{body}");
                throw new Exception($"Failed to contact homeloan. Status code: {response.StatusCode}");
            }
        }
}
}
