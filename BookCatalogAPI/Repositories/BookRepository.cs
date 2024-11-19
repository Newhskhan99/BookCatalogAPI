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

        public BookRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            var response = await _httpClient.GetAsync("(https://localhost:7129/swagger"); // This URI for local machine
            response.EnsureSuccessStatusCode();
            var books = await response.Content.ReadFromJsonAsync<IEnumerable<Book>>();
            return books;
        }
    }
}
