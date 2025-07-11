using Libary_Management_System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Libary_Management_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetIssuedBooks(string filter)
        {
            var query = _context.BorrowRecords
                .Include(b => b.Book)
                .Include(b => b.User)
                .Where(b => b.BorrowDate != null);

            var now = DateTime.Now;

            if (filter == "week")
                query = query.Where(b => b.BorrowDate >= now.AddDays(-7));
            else if (filter == "month")
                query = query.Where(b => b.BorrowDate >= now.AddMonths(-1));

            var result = await query.Select(b => new
            {
                bookTitle = b.Book.Title,
                borrower = b.User.UserName,
                borrowDate = b.BorrowDate.ToString("yyyy-MM-dd"),
                dueDate = b.DueDate.ToString("yyyy-MM-dd")
            }).ToListAsync();

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetReturnedBooks()
        {
            var result = await _context.BorrowRecords
                .Include(b => b.Book)
                .Include(b => b.User)
                .Where(b => b.ReturnDate != null)
                .Select(b => new
                {
                    bookTitle = b.Book.Title,
                    borrower = b.User.UserName,
                    returnDate = b.ReturnDate.Value.ToString("yyyy-MM-dd")
                }).ToListAsync();

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetOverdueBooks()
        {
            var today = DateTime.Today;
            var result = await _context.BorrowRecords
                .Include(b => b.Book)
                .Include(b => b.User)
                .Where(b => b.DueDate < today && b.ReturnDate == null)
                .Select(b => new
                {
                    bookTitle = b.Book.Title,
                    borrower = b.User.UserName,
                    dueDate = b.DueDate.ToString("yyyy-MM-dd")
                }).ToListAsync();

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetMemberSummary()
        {
            var result = await _context.Users
                .Select(user => new
                {
                    member = user.UserName,
                    totalBorrowed = _context.BorrowRecords.Count(b => b.User.Id == user.Id),
                    currentlyBorrowed = _context.BorrowRecords.Count(b => b.User.Id == user.Id && b.ReturnDate == null)
                }).ToListAsync();

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetFineSummary()
        {
            var data = await _context.BorrowRecords
                .Where(b => b.FineAmount > 0)
                .Include(b => b.User)
                .Select(b => new
                {
                    member = b.User.UserName,
                    fine = b.FineAmount
                }).ToListAsync();

            var grouped = data.GroupBy(x => x.member)
                .Select(g => new
                {
                    member = g.Key,
                    totalFine = g.Sum(x => x.fine)
                }).ToList();

            return Json(grouped);
        }
    }
}
