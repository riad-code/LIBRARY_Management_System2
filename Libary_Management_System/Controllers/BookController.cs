using Libary_Management_System.Data;
using Libary_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;

namespace Libary_Management_System.Controllers
{
    [Authorize(Roles = "Admin,Librarian")]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BookController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

       public async Task<IActionResult> Index(string? search)
{
    var booksQuery = _context.Books.Include(b => b.Category).AsQueryable();

    if (!string.IsNullOrWhiteSpace(search) && search.Length >= 3)
    {
        booksQuery = booksQuery.Where(b => b.Title.Contains(search));
    }

    var books = await booksQuery.ToListAsync();
    return View(books);
}


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _context.BookCategories.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Book model, IFormFile? coverImage)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Any())
                    .Select(x => $"{x.Key}: {x.Value.Errors.First().ErrorMessage}")
                    .ToList();

                return Json(new { success = false, message = "Invalid data", errors });
            }

            bool duplicateISBN = _context.Books.Any(b => b.ISBN == model.ISBN && b.BookID != model.BookID);
            if (duplicateISBN)
            {
                return Json(new { success = false, message = "ISBN must be unique." });
            }
            if (coverImage != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(coverImage.FileName);
                string path = Path.Combine(_env.WebRootPath, "uploads", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await coverImage.CopyToAsync(stream);
                }

                model.CoverImagePath = "/uploads/" + fileName;
            }

            model.AvailableCopies = model.TotalCopies;

            _context.Books.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Book added successfully." });
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
                return NotFound();

            ViewBag.Categories = _context.BookCategories.ToList();
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] Book model)
        {
            // Category validation
            if (model.CategoryID == 0)
                ModelState.AddModelError("CategoryID", "Category is required.");

            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors)
                                                 .Select(e => e.ErrorMessage)
                                                 .ToList();
                var errorMsg = string.Join(" ", allErrors);
                return Json(new { success = false, message = "Invalid data. " + errorMsg });
            }

            var existingBook = await _context.Books.FindAsync(model.BookID);
            if (existingBook == null)
            {
                return Json(new { success = false, message = "Book not found." });
            }

            // Check for duplicate ISBN excluding current book
            bool duplicateISBN = _context.Books.Any(b => b.ISBN == model.ISBN && b.BookID != model.BookID);
            if (duplicateISBN)
            {
                return Json(new { success = false, message = "ISBN must be unique." });
            }

            // Update fields
            existingBook.Title = model.Title;
            existingBook.Author = model.Author;
            existingBook.ISBN = model.ISBN;
            existingBook.Publisher = model.Publisher;
            existingBook.PublishedDate = model.PublishedDate;
            existingBook.CategoryID = model.CategoryID;
            existingBook.TotalCopies = model.TotalCopies;

            // Adjust AvailableCopies intelligently (optional)
            if (existingBook.AvailableCopies > existingBook.TotalCopies)
            {
                existingBook.AvailableCopies = existingBook.TotalCopies;
            }

            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Book updated successfully." });
        }



        [HttpPost]
        [IgnoreAntiforgeryToken]  // If you don’t send CSRF token via AJAX
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return Json(new { success = false, message = "Book not found." });

            // Example: Prevent delete if book is currently borrowed (if you have BorrowRecords)
            // bool isBorrowed = await _context.BorrowRecords.AnyAsync(br => br.BookID == id && br.ReturnDate == null);
            // if (isBorrowed)
            //     return Json(new { success = false, message = "Cannot delete. Book is currently borrowed." });

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Book deleted successfully." });
        }

    }
}
