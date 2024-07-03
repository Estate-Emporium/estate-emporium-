using estate_emporium.Models;

namespace estate_emporium.Services;

    public class PersonaService(IHttpClientFactory httpClientFactory, DbService dbService)
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly DbService _dbService = dbService;

        public async Task CompleteSale(ulong personaId, bool isSuccessful)
        {

            var client = _httpClientFactory.CreateClient(nameof(HttpClientEnum.persona));
            var payload = new completeSaleModel()
            {
                personaId = (ulong)personaId,
                isSuccess = isSuccessful
            };

            var response = await client.PostAsJsonAsync("api/buyHouseSuccess", payload);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Failed to contact persona. Status code: {response.StatusCode}\n Error{body}");
            }

        }
    }
