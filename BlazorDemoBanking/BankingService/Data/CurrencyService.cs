using Newtonsoft.Json;
using System.Text;

namespace BankingService.Data;

public class CurrencyService
{
    public readonly string apikey = String.Empty;
    public CurrencyService(string apikey)
    {
        this.apikey = apikey;
    }

    public async Task<AmdorenResponse> Convert(ConvertCurrency convertCurrency)
    {
        using (HttpClient httpClient = new())
        {
            Uri request;
            if (convertCurrency.Amount == 0)
            {
                request = new($"https://www.amdoren.com/api/currency.php?api_key={apikey}&" +
                    $"from={convertCurrency.From}&to={convertCurrency.To}");
            }
            else
            {
                request = new($"https://www.amdoren.com/api/currency.php?api_key={apikey}" +
                    $"&from={convertCurrency.From}&to={convertCurrency.To}&amount={convertCurrency.Amount}");
            }
            HttpResponseMessage responseMessage = await httpClient.GetAsync(request);
            responseMessage.EnsureSuccessStatusCode();
            string message = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AmdorenResponse>(message)!;
        }
    }
}
