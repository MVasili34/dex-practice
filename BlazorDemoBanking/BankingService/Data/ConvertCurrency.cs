namespace BankingService.Data;

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