using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Hava Durumu Bilgileri\n");

        await GetAndDisplayWeather("İstanbul");
        await GetAndDisplayWeather("İzmir");
        await GetAndDisplayWeather("Ankara");

        Console.WriteLine("İşlem tamamlandı. Çıkmak için bir tuşa basın.");
        Console.ReadLine();
    }

    static async Task GetAndDisplayWeather(string city)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                string apiUrl = $"https://goweather.herokuapp.com/weather/{city}";
                string jsonData = await client.GetStringAsync(apiUrl);

                WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(jsonData);

                Console.WriteLine($"--- {city.ToUpper()} ---");
                Console.WriteLine($"Bugünkü Hava Durumu Sıcaklığı {weatherData.Temperature} olup , Gökyüzünde {weatherData.Description} iken Rüzgarın hızı {weatherData.Wind}.");
                Console.WriteLine("Önümüzdeki 3 günün Tahmini Hava Durumu:");
                foreach (var forecast in weatherData.Forecast)
                {
                    Console.WriteLine($"{forecast.Day}: \nSıcaklık: {forecast.Temperature}, {forecast.Description}");
                    Console.WriteLine($"Rüzgar Hızı: {forecast.Wind}");
                }

                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hava durumu verileri alınırken bir hata oluştu: {ex.Message}");
            }
        }
    }
}

class WeatherData
{
    public string Temperature { get; set; }
    public string Description { get; set; }
    public string Wind { get; set; }
    public ForecastData[] Forecast { get; set; }
}

class ForecastData
{
    public string Day { get; set; }
    public string Temperature { get; set; }
    public string Description { get; set; }
    public string Wind { get; set; }
}
