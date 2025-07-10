using Libary_Management_System.Data;
using Libary_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Libary_Management_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ GET: Load all categories
        public async Task<IActionResult> Index()
        {
            var categories = await _context.BookCategories.ToListAsync(); 

            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // ✅ POST: Create category (AJAX)
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BookCategory model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid data." });

            _context.BookCategories.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Category created successfully." });
        }

        // ✅ GET: Get category by ID (AJAX)
        [HttpGet]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _context.BookCategories.FindAsync(id);
            if (category == null)
                return Json(new { success = false, message = "Category not found." });

            return Json(new
            {
                success = true,
                data = new
                {
                    category.CategoryID,
                    category.CategoryName,
                    category.Description
                }
            });
        }

        // GET: Load Edit Page
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.BookCategories.FindAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        // POST: Handle AJAX edit
        [HttpPost]
        public async Task<IActionResult> Edit(BookCategory model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid data." });

            var category = await _context.BookCategories.FindAsync(model.CategoryID);
            if (category == null)
                return Json(new { success = false, message = "Category not found." });

            category.CategoryName = model.CategoryName;
            category.Description = model.Description;

            _context.BookCategories.Update(category);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Category updated successfully." });
        }





        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var category = await _context.BookCategories
                    .Include(c => c.Books) // 🔥 Important!
                    .FirstOrDefaultAsync(c => c.CategoryID == id);

                if (category == null)
                    return Json(new { success = false, message = "Category not found." });

                if (category.Books != null && category.Books.Any())
                    return Json(new { success = false, message = "Cannot delete. Books exist under this category." });

                _context.BookCategories.Remove(category);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Category deleted successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }



    }
}
