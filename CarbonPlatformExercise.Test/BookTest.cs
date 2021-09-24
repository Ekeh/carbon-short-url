using CarbonPlatformExercise.Api.Controllers;
using CarbonPlatformExercise.Data;
using CarbonPlatformExercise.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CarbonPlatformExercise.Test
{
    public class BookTest
    {
        #region Property  
        public Mock<IBookService> mockBook = new Mock<IBookService>();
        public Mock<IShortUrlService> mockShortCode = new Mock<IShortUrlService>();
        #endregion
        [Fact]
        public async Task GetBooksTestAsync()
        {
            mockBook.Setup(p => p.GetBooks()).ReturnsAsync(new List<Book>());
            mockShortCode.Setup(p => p.GenerateUrlCode(8)).ReturnsAsync("ABCEDF");
            BooksController booksController = new BooksController(mockBook.Object, mockShortCode.Object);
            var result = await booksController.GetAll();
            Assert.Equal("Microsoft.AspNetCore.Mvc.OkObjectResult", result.ToString());
        }

        [Fact]
        public async Task CreateBookTestAsync()
        {
            mockBook.Setup(p => p.InsertBook(new Book { Text = "This is a book", Expiry = DateTime.Now })).ReturnsAsync(new Book { Text = "This is a book", Expiry = DateTime.Now });
            mockShortCode.Setup(p => p.GenerateUrlCode(8)).ReturnsAsync("ABCEDF");
            BooksController booksController = new BooksController(mockBook.Object, mockShortCode.Object);
            var result = await booksController.Create(new Api.Dto.BookDto { Text = "This is a book", Expiry = DateTime.Now});
            Assert.Equal("Microsoft.AspNetCore.Mvc.OkObjectResult", result.ToString());
        }
    }
}
