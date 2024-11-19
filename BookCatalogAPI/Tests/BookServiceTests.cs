using Xunit;
using Moq;
using System.Threading.Tasks;
using BookCatalogAPI.Services;
using BookCatalogAPI.Repositories;
using BookCatalogAPI.Models;

namespace BookCatalogAPI.Tests
{
    public class BookServiceTests
    {
        [Fact]
        public async Task GetBooksAsync_ReturnsBooksGroupedByAgeCategory()
        {
            // Arrange
            var repositoryMock = new Mock<IBookRepository>();
            var books = new[]
            {
                new Book { Name = "Book1", Type = "Hardcover", Owner = new Owner { Name = "Owner1", Age = 25 } },
                new Book { Name = "Book2", Type = "Paperback", Owner = new Owner { Name = "Owner2", Age = 17 } },
            };
            repositoryMock.Setup(r => r.GetBooksAsync()).ReturnsAsync(books);

            var service = new BookService(repositoryMock.Object, null);

            // Act
            var result = await service.GetBooksAsync(false);

            // Assert
            Assert.Contains("Adults", result);
            Assert.Contains("Book1 (Hardcover)", result);
            Assert.Contains("Children", result);
            Assert.Contains("Book2 (Paperback)", result);
        }

        [Fact]
        public async Task GetBooksAsync_ReturnsOnlyHardcoverBooks()
        {
            // Arrange
            var repositoryMock = new Mock<IBookRepository>();
            var books = new[]
            {
                new Book { Name = "Book1", Type = "Hardcover", Owner = new Owner { Name = "Owner1", Age = 25 } },
                new Book { Name = "Book2", Type = "Paperback", Owner = new Owner { Name = "Owner2", Age = 17 } },
            };
            repositoryMock.Setup(r => r.GetBooksAsync()).ReturnsAsync(books);

            var service = new BookService(repositoryMock.Object, null);

            // Act
            var result = await service.GetBooksAsync(true);

            // Assert
            Assert.Contains("Adults", result);
            Assert.Contains("Book1 (Hardcover)", result);
            Assert.DoesNotContain("Book2 (Paperback)", result);
        }
    }
}
