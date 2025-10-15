using miniLibraryManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using miniLibraryManagementSystem.Helpers;
using miniLibraryManagementSystem.Services;
using miniLibraryManagementSystem.Web.ViewModels;

namespace miniLibraryManagementSystem.Controllers
{
    public class BorrowTransactionsController : Controller
    {
        private readonly IBorrowingService _borrowingService;
        private readonly IBookService _bookService;

        public BorrowTransactionsController(IBorrowingService borrowingService, IBookService bookService)
        {
            _borrowingService = borrowingService;
            _bookService = bookService;
        }

        // GET: Borrowing
        public async Task<IActionResult> Index(string filter = "all")
        {
            var transactions = await _borrowingService.GetTransactionsAsync();

            transactions = filter switch
            {
                "active" => transactions.Where(t => t.ReturnedDate == null),
                "returned" => transactions.Where(t => t.ReturnedDate != null),
                _ => transactions
            };

            ViewBag.Filter = filter;
            return View(transactions);
        }

        // GET: Borrow
        public async Task<IActionResult> Borrow()
        {
            var books = await _bookService.GetAllAsync();
            var availableBooks = books.Where(b => !b.IsBorrowed);
            var model = viewModelFactory.CreateBorrowBookViewModel(books);

            return View(model);
        }

// POST: Borrow
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Borrow(BorrowBookViewModel model)
{
    if (!ModelState.IsValid)
    {
        var books = await _bookService.GetAllAsync();
        model = viewModelFactory.CreateBorrowBookViewModel(books);
        return View(model);
    }

    try
    {
        var result = await _borrowingService.BorrowAsync(model.BookId);

        if (result == null)
        {
            ModelState.AddModelError("", "This book could not be borrowed.");
            return await ReloadBorrowView(model);
        }

        return RedirectToAction(nameof(Index));
    }
    catch (InvalidOperationException ex)
    {
        // Handle concurrency or validation exceptions from BorrowAsync or ReturnAsync
        ModelState.AddModelError("", ex.Message);
        return await ReloadBorrowView(model);
    }
    catch (Exception)
    {
        // Catch-all for unexpected errors
        ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
        return await ReloadBorrowView(model);
    }
}

private async Task<IActionResult> ReloadBorrowView(BorrowBookViewModel model)
{
    var books = await _bookService.GetAllAsync();
    model.Books = books.Where(b => !b.IsBorrowed)
                       .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Title });
    return View("Borrow", model);
}


        public async Task<IActionResult> Return(int id)
        {
            await _borrowingService.ReturnAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> CheckAvailability(int bookId)
        {
            var book = await _bookService.GetByIdAsync(bookId);
            if (book == null)
                return Json(new { available = false, message = "Book not found." });

            if (book.IsBorrowed)
                return Json(new { available = false, message = "Checked Out" });

            return Json(new { available = true, message = "Available" });
        }
    }
}
