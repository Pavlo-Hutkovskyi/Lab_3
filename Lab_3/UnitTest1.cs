using Example.Models;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using System.Net;

namespace Lab_3
{
    [TestFixture]
    public class Tests
    {
        private RestClient client;

        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://restful-booker.herokuapp.com/");
        }

        [Test]
        public void CheckSeccessfullResponseGetCreateBookingIds()
        {
            // arrange
            RestRequest request = new RestRequest("booking", Method.GET);
            // act
            IRestResponse response = client.Execute(request);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void CheckSeccessfullResponseWhenCreateBooking()
        {
            // arrange
            RestRequest request = new RestRequest("booking", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new Book()
            {
                firstname = "Pavlo",
                lastname = "Hytkovskyi",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new BookDates()
                {
                    checkin = "2018-01-01",
                    checkout = "2019-01-01"
                },
                additionalneeds = "Breakfast"
            });
            // act
            IRestResponse<GetBook> response = client.Execute<GetBook>(request);
            // assert
            Assert.That(response.Data.booking.firstname, Is.EqualTo("Pavlo"));
        }

        [Test]
        public void CheckSeccessfullResponseWhenUpdateBooking()
        {
            // arrange
            RestRequest request = new RestRequest("booking", Method.GET);
            IRestResponse response = client.Execute(request);
            var books = new JsonDeserializer().Deserialize<List<BookId>>(response);
            request = new RestRequest($"booking/{books[1].bookingid}", Method.PUT);
            client.Authenticator = new HttpBasicAuthenticator("admin", "password123");
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new Book()
            {
                firstname = "Pavlo",
                lastname = "Hytkovskyi",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new BookDates()
                {
                    checkin = "2018-01-01",
                    checkout = "2019-01-01"
                },
                additionalneeds = "Breakfast"
            });
            // act
            response = client.Execute(request);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var result = new JsonDeserializer().Deserialize<Dictionary<string, string>>(response);
            Assert.That(result["firstname"], Is.EqualTo("Pavlo"));
            Assert.That(result["lastname"], Is.EqualTo("Hytkovskyi"));
        }

        [Test]
        public void CheckSeccessfullResponseWhenDeleteBooking()
        {
            // arrange
            RestRequest request = new RestRequest("booking", Method.GET);
            IRestResponse response = client.Execute(request);
            var books = new JsonDeserializer().Deserialize<List<BookId>>(response);
            request = new RestRequest($"booking/{books[1].bookingid}", Method.DELETE);
            client.Authenticator = new HttpBasicAuthenticator("admin", "password123");
            request.AddHeader("Content-Type", "application/json");
            // act
            response = client.Execute(request);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
    }
}