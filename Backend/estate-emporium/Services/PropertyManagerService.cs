using estate_emporium.Models;
using estate_emporium.Models.PropertyManager;

namespace estate_emporium.Services
{
    public class PropertyManagerService(IHttpClientFactory httpClientFactory, DbService dbService)
{
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly DbService _dbService = dbService;
        public async Task<long?> GetProperty(PurchaseModel purchaseModel)
        {

            var saleId=await _dbService.initPropertySaleAsync(purchaseModel);
            var client = _httpClientFactory.CreateClient(nameof(HttpClientEnum.property_manager));
            var getProperty = new GetPropertyModel { size = (int)purchaseModel.NumUnits };

            var response = await client.PutAsJsonAsync("PropertyManager/Property", getProperty);
            if (response.IsSuccessStatusCode)
            {
                var propertyResponse = await response.Content.ReadFromJsonAsync<GetPropertyResponseModel>();
                await _dbService.updateWithPropertyResponse(saleId, propertyResponse);
                return saleId;
            }
            else
            {
                    var body = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to get property. Status code: {response.StatusCode}\n Error{body}");
                    await _dbService.failSaleAsync(saleId);
                    throw new Exception("Dependency failed: No properties availible");
            }
        }
        public async Task CompleteSale(long saleId, bool isSuccessful)
        {
            var thisSale= await _dbService.getSaleByIdAsync(saleId);

            var client = _httpClientFactory.CreateClient(nameof(HttpClientEnum.property_manager));
            var complete = new PutPropertyCompleteModel()
            {
                approval = isSuccessful,
                buyerId = thisSale.BuyerId,
                price = thisSale.SalePrice,
                propertyId = thisSale.PropertyId,
                sellerId = thisSale.SellerId
            };

            if (!isSuccessful ) { thisSale.StatusId = -1; }
            else { thisSale.StatusId++; }
            await _dbService.saveChangesAsync();

            var response = await client.PutAsJsonAsync("PropertyManager/Approval", complete);
            if(response.IsSuccessStatusCode) { Console.WriteLine("Purchase completed"); }
            else
            {
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed to get property. Status code: {response.StatusCode}\n Error{body}");
            }
        }
}
}
