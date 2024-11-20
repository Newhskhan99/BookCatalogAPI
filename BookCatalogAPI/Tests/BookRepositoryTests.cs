using Xunit;
using Moq;
using System.Threading.Tasks;
using BookCatalogAPI.Repositories;
using BookCatalogAPI.Models;
using System.Net.Http;
using Moq.Protected;
using System.Linq;
using System.Net;

namespace BookCatalogAPI.Tests
{
    public class BookRepositoryTests
    {
        [Fact]
        public async Task GetBooksAsync_ReturnsBooksFromApi()
        {
            // Arrange
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("[{\"Name\":\"Book1\",\"Type\":\"Hardcover\",\"Owner\":{\"Name\":\"Owner1\",\"Age\":25}}]")
            };

            // HttpMessageHandler to return the response
            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response);

            // Creating HttpClient with the mocked HttpMessageHandler
            var httpClient = new HttpClient(httpMessageHandlerMock.Object);
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
