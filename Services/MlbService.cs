using System.Net.Http.Json;

namespace AzureFunctionsMlbCSharp
{
    public class MlbService : IMlbService
    {
        private readonly IHttpClientFactory _clientFactory;

        public MlbService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}