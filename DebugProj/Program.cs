using QuantumCraft;
using System;
using System.Net.Http;
using System.Threading.Tasks;

class QuantumMaster : IDisposable
{
    private HttpClient client;
    private string master = "";

    public QuantumMaster(string reposUrl = "https://raw.githubusercontent.com/Laamy/QuantumCraft/master/")
    {
        client = new HttpClient(); // init quantum master webclient
        master = reposUrl.EndsWith("/") ? reposUrl : reposUrl + "/";
    }

    public async Task<string> GetDirectAsync(string path, string ext = "")
    {
        try
        {
            var response = await client.GetStringAsync(new Uri(master + path.Replace("\\", "/") + (ext != "" ? ("." + ext) : "")));
            return response;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching data: {ex.Message}");
            return string.Empty;
        }
    }

    public void Dispose()
    {
        client?.Dispose();
    }
}

class Program
{
    static void Main(string[] args)
    {
        QuantumMaster master = new QuantumMaster();

        Console.WriteLine(new DataService(master.GetDirectAsync("Version", "txt").Result)["Latest", "Quantum"]);

        Console.ReadKey();
    }
}