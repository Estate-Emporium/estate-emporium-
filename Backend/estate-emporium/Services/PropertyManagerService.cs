using estate_emporium.Models;

namespace estate_emporium.Services
{
    public class PropertyManagerService(IHttpClientFactory httpClientFactory)
{
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        public async Task<GetPropertyResponseModel> GetProperty(PurchaseModel purchaseModel)
        {
            var client = _httpClientFactory.CreateClient(nameof(HttpClientEnum.property_manager));
            var getProperty = new GetPropertyModel { size = (int)purchaseModel.NumUnits };

            var response = await client.PutAsJsonAsync("PropertyManager/Property", getProperty);
            if (response.IsSuccessStatusCode)
            {
                var propertyResponse = await response.Content.ReadFromJsonAsync<GetPropertyResponseModel>();
                return propertyResponse;
            }
            else
            {
                    // Handle failure
                    Console.WriteLine($"Failed to get property. Status code: {response.StatusCode}");
                    return null;
                
            }

        }
}
}
