namespace backend.Application.Models;

public class ApiDataModel
{
    public string? Symbol { get; set; }
    
    public long MarketCap { get; set; }

    public double RegularMarketPrice { get; set; }

    public string? LongName { get; set; }

    public double PredictedPrice { get; set; }
}