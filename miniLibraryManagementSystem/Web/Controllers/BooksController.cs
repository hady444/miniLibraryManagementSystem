using miniLibraryManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using miniLibraryManagementSystem.Services;

namespace miniLibraryManagementSystem.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;

        public BooksController(IBookService bookService, IAuthorService authorService)
        {
            _bookService = bookService;
            _authorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllAsync();
            return View(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = await _authorService.GetAllAsync();
            ViewData["AuthorId"] = new SelectList(ViewBag.Authors, "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {if (!ModelState.IsValid)
            {
                ViewBag.Authors = await _authorService.GetAllAsync();
                ViewData["AuthorId"] = new SelectList(ViewBag.Authors, "Id", "Email", book.AuthorId);
                return View(book);
            }
            await _bookService.CreateAsync(book);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null) return NotFound();

            ViewBag.Authors = await _authorService.GetAllAsync();
            ViewBag.AuthorId = new SelectList(ViewBag.Authors, "Id", "Email");
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = await _authorService.GetAllAsync();
                ViewBag.AuthorId = new SelectList(ViewBag.Authors, "Id", "Email");
                return View(book);
            }
            await _bookService.UpdateAsync(book);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
