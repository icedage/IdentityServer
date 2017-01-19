using RestSharp;

namespace Security.IdentityManagementTool.GatewayAPI
{
    public class HttpRequestWrapper
    {
        private readonly IRestRequest _request;

        private readonly IRestClient _client;

        public HttpRequestWrapper(string resource, Method method)
        {
            _client = new RestClient(resource);
            _request = new RestRequest { Method = method };
        }

        public HttpRequestWrapper AddParameter(string name, object value)
        {
            _request.AddParameter(name, value);
            return this;
        }

        public IRestResponse Execute(string token)
        {
            _request.AddHeader("Accept", "application/json");
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", $"Bearer {token}");


            var response = _client.Execute(_request);
            return response;
        }
    }
}