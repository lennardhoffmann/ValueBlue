using ExternalFilmService.Models;
using ExternalFilmService.Services;
using FluentAssertions;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using VB.Tests.ObjectBuilders;

namespace VB.Tests
{
    public class ExternalFilmQueryServiceTests
    {
        private readonly ExternalFilmQueryService _sut;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        public ExternalFilmQueryServiceTests()
        {
            var properties = new FilmServiceProperties
            {
                BaseUrl = "http://lets.go.test",
                ApiKey = "123456",
                ServiceName = "SomeService"
            };

            _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            _sut = new ExternalFilmQueryService(_httpClient, properties);
        }

        [Fact]
        public async void GetFilmByName_ValidOperations_ReturnsFilmServiceResponse()
        {
            var filmName = "Inception";
            var filmServiceFilmResponse = new FilmServiceResponseBuilder().Build();
            string jsonContent = JsonConvert.SerializeObject(filmServiceFilmResponse);
            var filmServiceResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };

            _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(filmServiceResponse);

            var result = await _sut.GetFilmByName(filmName);
            result.Title.Should().Be(filmName);
            result.Released.Should().NotBe(null);
        }

        [Fact]
        public async void GetFilmByName_EmptyFilmName_ThrowsException()
        {
            await _sut.Invoking(sut => sut.GetFilmByName(""))
                      .Should()
                      .ThrowAsync<ArgumentNullException>();
        }
    }
}
