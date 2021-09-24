using CarbonPlatformExercise.Api.Dto;
using CarbonPlatformExercise.Api.Models;
using CarbonPlatformExercise.Data;
using CarbonPlatformExercise.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarbonPlatformExercise.Api.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IShortUrlService _shortUrlService;

        public BooksController(IBookService bookService, IShortUrlService shortUrlService)
        {
            _bookService = bookService;
            _shortUrlService = shortUrlService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var response = new ResponseModel<IEnumerable<Book>>();
           var books = await _bookService.GetBooks();
            foreach (var book in books)
            {
                book.Url = $"{HttpContext.Request.Scheme}://{Request.Host.Value}/{book.Url}";
            }
            response.Data = await _bookService.GetBooks();
            return Ok(response);
        }

        [HttpGet("get-book/{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var response = new ResponseModel<Book>();
            response.Data = await _bookService.GetBook(id);
            return Ok(response);
        }


        [HttpGet("get-book-by-url/{url}")]
        public async Task<IActionResult> GetBookByUrl([FromRoute] string url)
        {
            var response = new ResponseModel<Book>();
            response.Data = await _bookService.GetBookByUrl(url);
            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] BookDto model)
        {
            var response = new ResponseModel<string>();
            if (!ModelState.IsValid)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Some required fields are missing";
                return BadRequest(response);
            }
            var book = new Book
            {
                Text = model.Text,
                Url = await _shortUrlService.GenerateUrlCode(),
                Expiry = model.Expiry
            };
           var result = await _bookService.InsertBook(book);
            var callbackUrl = $"{HttpContext?.Request?.Scheme}://{Request?.Host.Value}/{result?.Url}";
            response.Data = callbackUrl;
            return Ok(response);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BookDto model)
        {
            var response = new ResponseModel<Book>();
            if (!ModelState.IsValid)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Some required fields are missing";
                return BadRequest(response);
            }
            var book = await _bookService.GetBook(id);
            if(book == null)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = "Book not found.";
                return NotFound(response);
            }
            book.Text = model.Text;
            book.Expiry = model.Expiry;
            await _bookService.UpdateBook(book);
            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            var response = new ResponseModel<Book>();
            var book = await _bookService.GetBook(id);
            if (book == null)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = "Book not found.";
                return NotFound(response);
            }
            await _bookService.DeleteBook(id);
            response.Message = "Book deleted successfully.";
            return Ok(response);
        }
    }
}
