using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.services;
using my_books.Data.ViewModels;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private BooksService _booksService;

        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody]BookVM book)
        {
            _booksService.AddBook(book);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            return Ok(_booksService.GetAllBooks());
        }

        [HttpGet("get-books-by-id/{id}")]
        public IActionResult GetBookById(int id)
        {
            return Ok(_booksService.GetBookById(id));
        }

        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id,[FromBody]BookVM book)
        {
            var updatedBook=_booksService.UpdateBookById(id,book);
            return Ok(updatedBook);
        }

        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBookById(int id)
        {
            _booksService.DeleteBookByID(id);
            return Ok();
        }
    }
}
