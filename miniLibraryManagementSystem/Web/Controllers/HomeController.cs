using Microsoft.AspNetCore.Mvc;
using miniLibraryManagementSystem.Web.ViewModels;
using System.Diagnostics;
using miniLibraryManagementSystem.Services;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;

        public HomeController(ILogger<HomeController> logger, IBookService bookService, IAuthorService authorService)
        {
            _logger = logger;
            _bookService = bookService;
            _authorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel();

            var allBooks = (await _bookService.GetAllAsync()).Count();
            var availableBooks = (await _bookService.GetAllAsync()).Where(b => !b.IsBorrowed).Count();
            model.TotalBooks = allBooks; // Example data
            model.TotalAuthors = (await _authorService.GetAllAsync()).Count(); // Example data
            model.BorrowedBooks = allBooks - availableBooks; // Example data
            model.AvailableBooks = availableBooks; // Example data
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
