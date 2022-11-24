using _2.Models;
using RestSharp;
using System.Net;

namespace _2
{
    public class Tests
    {
        private RestClient client;

        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://wikimedia.org/api/rest_v1/");
        }

        [Test]
        public void CheckSuccessfullyResponseWhenGetRequestFromWikimedia()
        {
            // arrange
            RestRequest request = new RestRequest("feed/availability", Method.GET);
            // act
            IRestResponse response = client.Execute(request);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void CheckSuccessfullyResponseWhenPostRequestWithFormula()
        {
            // arrange
            RestRequest request = new RestRequest("media/math/check/tex", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(new Formula()
            {
                q = "2+2"
            });
            // act
            IRestResponse response = client.Execute(request);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}