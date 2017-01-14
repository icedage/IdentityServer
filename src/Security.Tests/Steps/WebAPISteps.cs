using System;
using System.Net;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using Security.Tests.Client;
using Security.Tests.Entities;
using TechTalk.SpecFlow;

namespace Security.Tests.Steps
{
    [Binding]
    public class WebAPISteps
    {
        private IRestResponse _response;
        private ApplicationUser _user;
        private Role _role;


        [Given(@"an authorized request")]
        public void GivenAnAuthorizedRequest()
        {
            _user = new ApplicationUser()
            {
                Username = "Oscar34PG",
                Password = "P@ssword123",
                FirstName = "Oscar",
                LastName = "Maddison",
                Email = "test89@test.com",
                BirthDate = DateTime.Now
            };

            var client = new HttpRequestWrapper("http://localhost:55112/Users/CreateAsync", Method.POST);
             client.Execute(_user);
        }
        
        [When(@"I apply for a loan")]
        public void WhenIApplyForALoan()
        {
            var user = new User()
            {
                UserName = _user.Username,
                Password = _user.Password
            };

            _response = new HttpRequestWrapper("http://localhost:18115/api/Loan/Apply", Method.POST)
                                .TokenizeRequest(user, "api")
                                .Execute(null);
        }

        [Given(@"an authorized request for an underwtiter")]
        public void GivenAnAuthorizedRequestForAnUnderwtiter()
        {
            _role = new Role()
            {
                Name = "Underwriter"
            };

            var client = new HttpRequestWrapper("http://localhost:55112/Roles/Create", Method.POST);
            client.Execute(_role);

            _user = new ApplicationUser()
            {
                Username = "Oscar34PG",
                Password = "P@ssword123",
                FirstName = "Oscar",
                LastName = "Maddison",
                Email = "test89@test.com",
                BirthDate = DateTime.Now,
                Role = _role.Name
            };

            client = new HttpRequestWrapper("http://localhost:55112/Users/CreateUserRole", Method.POST);
            client.Execute(_user);
        }

        [When(@"I approve a loan request")]
        public void WhenIApproveALoanRequest()
        {
            var user = new User()
            {
                UserName = _user.Username,
                Password = _user.Password
            };

            _response = new HttpRequestWrapper("http://localhost:18115/api/Underwriter/Approve", Method.POST)
                                .TokenizeRequest(user, "api")
                                .Execute(null);
        }

        [Then(@"the result should respond with (.*)")]
        public void ThenTheResultShouldRespondWith(int p0)
        {
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
        }

        [AfterScenario]
        public void CleanUp()
        {
            //Delete user 
            var client = new HttpRequestWrapper("http://localhost:55112/Users/Get", Method.GET).AddParameter("email",_user.Email);
            _response = client.Execute(null);
            var user = JsonConvert.DeserializeObject<ApplicationUser>(_response.Content);
            client = new HttpRequestWrapper("http://localhost:55112/Users/Delete", Method.DELETE).AddParameter("id", user.Id);
            client.Execute(null);

            //Delete Role
            client = new HttpRequestWrapper("http://localhost:55112/Roles/Get", Method.GET).AddParameter("id", _role.Id);
            _response = client.Execute(null);
            var role = JsonConvert.DeserializeObject<Role>(_response.Content);
            client = new HttpRequestWrapper("http://localhost:55112/Roles/Delete", Method.DELETE).AddParameter("id", role.Id);
            client.Execute(null);
        }
    }
}
