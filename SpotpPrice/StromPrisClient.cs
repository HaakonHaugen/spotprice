using SpotpPrice;
using System.Text.Json;

public class StromPrisClient : IStromPrisClient
{
    private readonly HttpClient _client;
    public StromPrisClient(HttpClient client)
    {
        _client = client;
    }
    // test.
    public async Task<List <NordPoolData>> GetNordPoolPrisAsync()
    {
        var response = await _client.GetAsync($"{_client.BaseAddress}/api/v1/prices/2023/02-15_NO1.json");
        var contentAsObject = await JsonSerializer.DeserializeAsync<List<NordPoolData>>(await response.Content.ReadAsStreamAsync());
        return contentAsObject;
    }
}