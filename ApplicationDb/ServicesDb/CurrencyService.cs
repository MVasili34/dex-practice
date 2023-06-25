using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesDb;

public class CurrencyService
{
	private readonly string apikey = null!;
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

public class ConvertCurrency
{
	public string From { get; set; } = null!;
	public string To { get; set; } = null!;
	public decimal Amount { get; set; }
}

public class AmdorenResponse
{
	public int Error { get; set; }
	public string Error_Message { get; set; } = null!;
	public decimal Amount { get; set; }
}