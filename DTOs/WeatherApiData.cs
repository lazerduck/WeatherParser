namespace WeatherParser.DTOs;

public class WeatherApiData
{
    public LocationData Location { get; set; } = new LocationData();

    public Forecast Forecast { get; set; } = new Forecast();
}

public class LocationData
{
    public DateTime LocalTime { get; set; }
}

public class Forecast
{
    public IEnumerable<ForecastDay> ForecastDay { get; set; } = new List<ForecastDay>();
}

public class ForecastDay
{
    public IEnumerable<Hour> Hour { get; set; } = new List<Hour>();
}

public class Hour
{
    public DateTime Time { get; set; }

    public decimal Chance_Of_Rain { get; set; }

    public decimal Temp_C { get; set; }

    public Condition Condition { get; set; } = new Condition();
}

public class Condition
{
    public int Code { get; set; }
}

