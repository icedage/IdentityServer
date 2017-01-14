using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Configuration;

namespace Security.Tests.Client
{
    public class RestSharpComponent
    {
        public IRestRequest Request { get; set; }

        public IRestClient Client { get; set; }

        public RestSharpComponent()
        {
            var url = ConfigurationManager.AppSettings["AuthorizationServer"];
            Client = new RestClient(url);
            Request = new RestRequest();
        }
    }
}
