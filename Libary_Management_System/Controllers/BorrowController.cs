using Libary_Management_System.Data;
using Libary_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Libary_Management_System.Controllers
{
    [Authorize]
    public class BorrowController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BorrowController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // MEMBER: Request a book
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RequestBook(int bookId)
        {
            var userId = _userManager.GetUserId(User);

            var existingRequest = await _context.BookRequests
                .FirstOrDefaultAsync(r => r.UserID == userId && r.BookID == bookId && r.Status == "Pending");

            if (existingRequest != null)
                return Json(new { success = false, message = "You've already requested this book." });

            var request = new BookRequest
            {
                BookID = bookId,
                UserID = userId,
                RequestDate = DateTime.Now,
                Status = "Pending"
            };

            _context.BookRequests.Add(request);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Book request submitted successfully." });
        }

        // MEMBER: View current borrowed books
        [Authorize(Roles = "User")]
        public async Task<IActionResult> MyBorrows()
        {
            var userId = _userManager.GetUserId(User);
            var records = await _context.BorrowRecords
                .Include(b => b.Book)
                .Where(r => r.UserID == userId && r.ReturnDate == null)
                .ToListAsync();

            return View(records);
        }

        // LIBRARIAN/ADMIN: View all pending book requests
        [Authorize(Roles = "Librarian,Admin")]
        public async Task<IActionResult> PendingRequests()
        {
            var pending = await _context.BookRequests
                .Include(r => r.Book)
                .Include(r => r.User)  // <-- Make sure BookRequest has navigation User property
                .Where(r => r.Status == "Pending")
                .ToListAsync();

            return View(pending);
        }

        // LIBRARIAN/ADMIN: Approve request
        [HttpPost]
        [Authorize(Roles = "Librarian,Admin")]
        public async Task<IActionResult> Approve(int requestId)
        {
            var request = await _context.BookRequests.FindAsync(requestId);
            if (request == null || request.Status != "Pending")
                return Json(new { success = false, message = "Invalid request." });

            var book = await _context.Books.FindAsync(request.BookID);
            if (book == null || book.AvailableCopies <= 0)
                return Json(new { success = false, message = "Book not available." });

            var borrow = new BorrowRecord
            {
                BookID = request.BookID,
                UserID = request.UserID,
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(7)
            };

            _context.BorrowRecords.Add(borrow);
            request.Status = "Approved";
            request.ApprovalDate = DateTime.Now;
            book.AvailableCopies--;

            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Request approved and book issued." });
        }

        // LIBRARIAN/ADMIN: Reject request
        [HttpPost]
        [Authorize(Roles = "Librarian,Admin")]
        public async Task<IActionResult> Reject(int requestId)
        {
            var request = await _context.BookRequests.FindAsync(requestId);
            if (request == null || request.Status != "Pending")
                return Json(new { success = false, message = "Invalid request." });

            request.Status = "Rejected";
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Request rejected." });
        }

        // LIBRARIAN/ADMIN: Record return
        [HttpPost]
        [Authorize(Roles = "Librarian,Admin")]
        public async Task<IActionResult> ReturnBook(int borrowId)
        {
            var borrow = await _context.BorrowRecords
                .Include(b => b.Book)
                .FirstOrDefaultAsync(b => b.BorrowID == borrowId);

            if (borrow == null || borrow.ReturnDate != null)
                return Json(new { success = false, message = "Invalid borrow record." });

            borrow.ReturnDate = DateTime.Now;

            var overdueDays = (borrow.ReturnDate.Value - borrow.DueDate).Days;
            borrow.FineAmount = overdueDays > 0 ? overdueDays * 10 : 0; // ৳10 per day fine

            borrow.Book.AvailableCopies++;

            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Book returned successfully." });
        }

        // MEMBER: List of books available for request
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RequestBookList()
        {
            var books = await _context.Books
                .Where(b => b.AvailableCopies > 0)
                .ToListAsync();

            return View(books);
        }

        // MEMBER: Cancel pending request
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CancelRequest(int requestId)
        {
            var userId = _userManager.GetUserId(User);
            var request = await _context.BookRequests
                .FirstOrDefaultAsync(r => r.RequestID == requestId && r.UserID == userId);

            if (request == null)
                return Json(new { success = false, message = "Request not found." });

            if (request.Status != "Pending")
                return Json(new { success = false, message = "Only pending requests can be cancelled." });

            _context.BookRequests.Remove(request);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Request cancelled successfully." });
        }
    }
}
