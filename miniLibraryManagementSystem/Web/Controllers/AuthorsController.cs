using miniLibraryManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using miniLibraryManagementSystem.Services;

namespace miniLibraryManagementSystem.Web.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: /Author
        public async Task<IActionResult> Index()
        {
            var authors = await _authorService.GetAllAsync();
            return View(authors);
        }

        // GET: /Author/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null) return NotFound();
            return View(author);
        }

        // GET: /Author/Create
        public IActionResult Create() => View();

        // POST: /Author/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Author author)
        {
            if (await _authorService.AnyAsync(author.Email, author.Id))
            {
                ModelState.AddModelError("Email", "This email is already taken.");
                return View(author);
            }

            // Check if model is valid
            if (ModelState.IsValid)
            {
                try
                {
                    await _authorService.CreateAsync(author);
                }
                catch (DbUpdateException ex)
                {
                    // Optional: handle another user inserted same email
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("UNIQUE"))
                    {
                        ModelState.AddModelError("Email", "This email is already taken.");
                        return View(author);
                    }
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: /Author/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null) return NotFound();
            return View(author);
        }

        // POST: /Author/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Author author)
        {
            if (!ModelState.IsValid) return View(author);
            if (await _authorService.AnyAsync(author.Email, author.Id))
            {
                ModelState.AddModelError("Email", "This email is already taken.");
                return View(author);
            }
            try
            {
                await _authorService.UpdateAsync(author); // DB unique constraint still protects
            }
            catch (DbUpdateException ex)
            {
                // Optional: handle rare race conditions where another user inserted same email
                if (ex.InnerException != null && ex.InnerException.Message.Contains("unique"))
                {
                    ModelState.AddModelError("Email", "This email is already taken.");
                    return View(author);
                }
                throw; // other DB errors rethrow
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: /Author/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null) return NotFound();
            return View(author);
        }

        // POST: /Author/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _authorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

