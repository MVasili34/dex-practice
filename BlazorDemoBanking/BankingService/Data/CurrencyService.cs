using Newtonsoft.Json;

namespace BankingService.Data;

public class CurrencyService
{
    private readonly string _apiKey = null!;
    public CurrencyService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<AmdorenResponse> Convert(ConvertCurrency convertCurrency)
    {
        using (HttpClient httpClient = new())
        {
            string baseUrl = new($"https://www.amdoren.com/api/currency.php?api_key={_apiKey}&" +
                    $"from={convertCurrency.From}&to={convertCurrency.To}"); ;
            if (convertCurrency.Amount != 0)
            {
                baseUrl += $"&amount={convertCurrency.Amount}";
            }

            Uri request = new(baseUrl);
            HttpResponseMessage responseMessage = await httpClient.GetAsync(request);
            responseMessage.EnsureSuccessStatusCode();
            string message = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AmdorenResponse>(message)!;
        }
    }
}
