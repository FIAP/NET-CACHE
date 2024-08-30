using Dapper;
using Importador.Models;
using Npgsql;
using System.Data;
using System.Text.Json;

string apiKey = "SUA_CHAVE_API"; // Substitua com sua chave de API
string baseUrl = "https://www.alphavantage.co/query";
string connectionString = "Host=aws-0-sa-east-1.pooler.supabase.com;Port=6543;Username=postgres.yxqzjxihproxnicmbarm;Password=PFi2maNiOsMzhjPa;Database=postgres;";


string symbol = "MSFT";
string interval = "5min"; 
string function = "TIME_SERIES_INTRADAY"; 
string outputSize = "compact"; 

string url = $"{baseUrl}?function={function}&symbol={symbol}&interval={interval}&outputsize={outputSize}&apikey={apiKey}";

using HttpClient client = new HttpClient();
HttpResponseMessage response = await client.GetAsync(url);

if (response.IsSuccessStatusCode)
{
    string jsonResponse = await response.Content.ReadAsStringAsync();
    var options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    var data = JsonSerializer.Deserialize<AlphaVantageResponse>(jsonResponse, options);

    Console.WriteLine("Dados recebidos da API:");

    using IDbConnection db = new NpgsqlConnection(connectionString);
    db.Open();

    foreach (var item in data.TimeSeries)
    {
        var stockData = new
        {
            Symbol = symbol,
            Time = DateTime.Parse(item.Key),
            Open = decimal.Parse(item.Value.Open),
            High = decimal.Parse(item.Value.High),
            Low = decimal.Parse(item.Value.Low),
            Close = decimal.Parse(item.Value.Close),
            Volume = long.Parse(item.Value.Volume)
        };

        string insertQuery = @"
                    INSERT INTO StockData (Symbol, Time, Open, High, Low, Close, Volume)
                    VALUES (@Symbol, @Time, @Open, @High, @Low, @Close, @Volume);";

        db.Execute(insertQuery, stockData);
    }

}
else
{
    Console.WriteLine($"Erro ao acessar a API: {response.StatusCode}");
}