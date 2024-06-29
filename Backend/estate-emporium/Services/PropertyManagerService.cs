using estate_emporium.Models;

namespace estate_emporium.Services
{
    public class PropertyManagerService(IHttpClientFactory httpClientFactory)
{
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        public async Task GetProperty(PurchaseModel purchaseModel)
        {
            var client = _httpClientFactory.CreateClient(nameof(HttpClientEnum.property_manager));
            var getProperty = new GetPropertyModel { size = purchaseModel.NumUnits };

            var response = await client.PutAsJsonAsync("PropertyManager/Propertys", getProperty);
        }
}
}
