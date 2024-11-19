using Xunit;
using Moq;
using System.Threading.Tasks;
using BookCatalogAPI.Repositories;
using BookCatalogAPI.Models;
using System.Net.Http;
using Moq.Protected;

namespace BookCatalogAPI.Tests
{
    public class BookRepositoryTests
    {
        [Fact]
        public async Task GetBooksAsync_ReturnsBooksFromApi()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent("[{\"Name\":\"Book1\",\"Type\":\"Hardcover\",\"Owner\":{\"Name\":\"Owner1\",\"Age\":25}}]")
            };
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response);

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://localhost:7129/swagger") // This URI for local machine
            };
            var repository = new BookRepository(httpClient);

            // Act
            var books = await repository.GetBooksAsync();

            // Assert
            Assert.Single(books);
            Assert.Equal("Book1", books.First().Name);
            Assert.Equal("Hardcover", books.First().Type);
            Assert.Equal("Owner1", books.First().Owner.Name);
            Assert.Equal(25, books.First().Owner.Age);
        }
    }
}
