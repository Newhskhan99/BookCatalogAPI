using BookCatalogAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookCatalogAPI.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooksAsync();
    }

    public class BookRepository : IBookRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _externalUri = "https://digitalcodingtest.bupa.com.au/api/v1/bookowners";

        public BookRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            try
            {var externalUri = "https://digitalcodingtest.bupa.com.au/api/v1/bookowners";// External Api URI
            var response = await _httpClient.GetAsync(externalUri); 
            response.EnsureSuccessStatusCode();
            var books = await response.Content.ReadFromJsonAsync<IEnumerable<Book>>();
                return books;
            }
            catch (HttpRequestException ex) { Console.WriteLine("Error:{ex.Message}"); return Enumerable.Empty<Book>(); }
        }
    }
}
