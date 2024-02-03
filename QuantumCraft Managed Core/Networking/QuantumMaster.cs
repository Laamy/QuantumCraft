using System.Threading.Tasks;
using System;
using System.Net.Http;

namespace QuantumCraft.Networking
{
    public class QuantumMaster : IDisposable
    {
        private HttpClient client;
        private string master = "";

        public QuantumMaster(string reposUrl = "https://raw.githubusercontent.com/Laamy/QuantumCraft/master/")
        {
            client = new HttpClient(); // init quantum master webclient
            master = reposUrl.EndsWith("/") ? reposUrl : reposUrl + "/";
        }

        /// <summary>
        /// Request raw http data of a webpage directly from the master + path + ext(ext includes an automatic dot)
        /// </summary>
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
}