using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using Security.Tests.Entities;

namespace Security.Tests.Client
{
    public class HttpRequestWrapper
    {
        private readonly IRestRequest _request;

        private readonly IRestClient _client;


        private const string SecretApi = "0caf5019-6705-415c-bb59-2bbff2f07384";

        public HttpRequestWrapper(string resource, Method method)
        {
            _client = new RestClient(resource);
            _request = new RestRequest {Method = method};
        }

        public HttpRequestWrapper(string resource)
        {
            _client = new RestClient(resource);
            _request = new RestRequest { Method = Method.GET };
        }


        public HttpRequestWrapper AddParameter(string name, object value)
        {
            _request.AddParameter(name, value);
            return this;
        }

        public IRestResponse Execute(object body)
        {
             _request.AddHeader("Accept", "application/json");
            _request.AddHeader("Content-Type", "application/json");
           // _request.AddHeader("Content-Type", "application/json");

            if (body != null)
                _request.AddJsonBody(body);
            
            var response = _client.Execute(_request);
            return response;
        }

        public IRestResponse Execute(string token)
        {
            _request.AddHeader("Accept", "application/json");
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authentication", $"Bearer {token}");
            // _request.AddHeader("Content-Type", "application/json");
            

            var response = _client.Execute(_request);
            return response;
        }

        public HttpRequestWrapper TokenizeRequest(User user, string clientid)
        {
            var token = GetToken(user, clientid).Result;
            _request.AddHeader("Authorization", $"Bearer {token.AccessToken}");
            return this;
        }

        private async Task<TokenResponse> GetToken(User user, string clientid)
        {
            var client = new TokenClient(Constants.TokenEndpoint, clientid, SecretApi);

            
            return  client.RequestResourceOwnerPasswordAsync(user.UserName, user.Password, "sampleApi").Result;
        }
    }
}
