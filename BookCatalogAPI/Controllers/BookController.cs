using BookCatalogAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookCatalogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<string> GetBooks([FromQuery] bool hardcoverOnly = false)
        {
            return await _bookService.GetBooksAsync(hardcoverOnly);
        }
    }
}
