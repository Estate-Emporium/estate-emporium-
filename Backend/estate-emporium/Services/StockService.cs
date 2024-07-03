using estate_emporium.Models;
using estate_emporium.Models.Stock;

namespace estate_emporium.Services
{
  public class StockService(IHttpClientFactory httpClientFactory, DbService dbService)
  {
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    public async Task registerStockAsync()
    {
      var client = _httpClientFactory.CreateClient(nameof(HttpClientEnum.stock));
      var payload = new PostStockModel()
      {
        name = "real_estate_sales",
        bankAccount = "real_estate_sales"
      };

      var response = client.PostAsJsonAsync("businesses", payload);
    }
  }
}
