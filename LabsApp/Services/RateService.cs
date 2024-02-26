using System.Net.Http;
using LabsApp.Entities;
using System.Text.Json;
namespace LabsApp.Services;

public class RateService: IRateService
{
    private readonly HttpClient _httpClient;

    private List<Rate> _chosenRates;

    
    public RateService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _chosenRates = new List<Rate>();
    }
    
    public async Task<IEnumerable<Rate>> GetRates(DateTime? date, IEnumerable<string> currencies)
    {
        if (date == null) return _chosenRates;
        
        _chosenRates.Clear();

        if (Connectivity.NetworkAccess != NetworkAccess.Internet) return _chosenRates;
        foreach (var currency in currencies)
        {
            var response = _httpClient
                .GetAsync($"https://api.nbrb.by/exrates/rates/{currency}?parammode=2&ondate={date:yyyy-MM-dd}");
            var jsonText = await response.Result.Content.ReadAsStringAsync();
            _chosenRates.Add(JsonSerializer.Deserialize<Rate>(jsonText) ?? new Rate());
        }

        return _chosenRates;
    }
}