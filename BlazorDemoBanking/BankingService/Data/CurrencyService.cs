using Newtonsoft.Json;

namespace BankingService.Data;

public class CurrencyService
{
    public readonly string apikey = string.Empty;
    public CurrencyService(string apikey)
    {
        this.apikey = apikey;
    }

    public async Task<AmdorenResponse> Convert(ConvertCurrency convertCurrency)
    {
        using (HttpClient httpClient = new())
        {
            string baseUrl = new($"https://www.amdoren.com/api/currency.php?api_key={apikey}&" +
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
