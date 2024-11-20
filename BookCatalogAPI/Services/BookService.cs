using BookCatalogAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BookCatalogAPI.Services
{
    public interface IBookService { Task<string> GetBooksAsync(bool hardcoverOnly); }
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository; private readonly IHttpClientFactory _httpClientFactory;
        public BookService(IBookRepository repository, IHttpClientFactory httpClientFactory) { _repository = repository; _httpClientFactory = httpClientFactory; }
        public async Task<string> GetBooksAsync(bool hardcoverOnly)
        {
            try
            { 
            
                    var books = await _repository.GetBooksAsync(); var filteredBooks = hardcoverOnly ? books.Where(b => b.Type == "Hardcover") : books; var groupedBooks = filteredBooks.GroupBy(b => b.Owner.Age >= 18 ? "Adults" : "Children"); var result = new StringBuilder();
            foreach (var group in groupedBooks) { result.AppendLine(group.Key); foreach (var book in group.OrderBy(b => b.Name)) { result.AppendLine($"  {book.Name} ({book.Type})"); } }
                return result.ToString();
            }
                catch (Exception ex) { Console.WriteLine($"Error:{ex.Message}"); return string.Empty; }
        }
    }
}
