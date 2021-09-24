using CarbonPlatformExercise.Api.Models;
using CarbonPlatformExercise.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CarbonPlatformExercise.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly IBookService _bookService;

        public HomeController(ILogger<HomeController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetBooks();
            foreach (var book in books)
            {
                book.Url = $"{HttpContext.Request.Scheme}://{Request.Host.Value}/{book.Url}";
            }
            return View(books);
        }

        [HttpGet("/{url}")]
        public async Task<IActionResult> Details(string url)
        {
            if (url != null)
            {
                var book = await _bookService.GetBookByUrl(url);
                if(book == null)
                {
                    return RedirectToAction("Error");
                }
                return View("Details", book);
            }
            return RedirectToAction("Error");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
