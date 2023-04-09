namespace WeatherParser.DTOs;

public class WeatherResponse
{
    public DateTime DateTime { get; init; }

    public HourData? Now { get; init; }

    public HourData? Next { get; set; }
}

public class HourData
{
    public DateTime Time { get; init; }

    public int Code { get; init; }

    public decimal ChanceOfRain { get; init; }

    public decimal Temp { get; init; }
}