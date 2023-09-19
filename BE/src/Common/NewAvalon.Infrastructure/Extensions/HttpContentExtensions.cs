using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewAvalon.Infrastructure.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<TData> ReadAsJsonAsync<TData>(this HttpContent httpContent)
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            string content = await httpContent.ReadAsStringAsync();

            TData data = JsonConvert.DeserializeObject<TData>(content, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });

            return data;
        }
    }
}
