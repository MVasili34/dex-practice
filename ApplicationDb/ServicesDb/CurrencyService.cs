using Newtonsoft.Json;

namespace ServicesDb;

public class CurrencyService
{
	private readonly string _apiKey = null!;
	public CurrencyService(string apiKey)
	{
		_apiKey = apiKey;
	}

    /// <summary>
    /// Метод, отвечающий за конвертацию валюту
    /// </summary>
    /// <param name="convertCurrency">Объект класса ConvertCurrency</param>
    /// <returns>Объект класса AmdorenResponse</returns>
    public async Task<AmdorenResponse> Convert(ConvertCurrency convertCurrency)
	{
		using (HttpClient httpClient = new())
		{
			string baseUrl = new($"https://www.amdoren.com/api/currency.php?api_key={_apiKey}&" +
                    $"from={convertCurrency.From}&to={convertCurrency.To}");

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

public class ConvertCurrency
{
	public string From { get; set; } = null!;
	public string To { get; set; } = null!;
	public decimal Amount { get; set; }
}

public class AmdorenResponse
{
	public int Error { get; set; }
	public string ErrorMessage { get; set; } = null!;
	public decimal Amount { get; set; }
}