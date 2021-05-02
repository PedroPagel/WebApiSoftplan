using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApiGetter
    {
        private readonly HttpClient _client;
        public ApiGetter(HttpClient client)
        {
            _client = client;
        }

        public async Task<T> Get<T>(string uri)
        {
            var response = await _client.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Não foi possível realizar a busca de api");
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
