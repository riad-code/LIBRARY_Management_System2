using Libary_Management_System.Data;
using Libary_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq; // for ToList

namespace Libary_Management_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context; // Add your DbContext here

        public AdminController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Route("AdminDashboard")]
        public IActionResult Dashboard()
        {
            // Total Users
            int totalUsers = _context.Users.Count();

            // Total Books
            int totalBooks = _context.Books.Count();

            // Pending Requests
            int pendingRequests = _context.BookRequests.Count(r => r.Status == "Pending");

            // ✅ Unpaid Fines (Assuming you don't track IsFinePaid)
            decimal unpaidFines = _context.BorrowRecords
                .Where(r => r.FineAmount > 0) // If you have IsFinePaid, add && !r.IsFinePaid
                .Sum(r => (decimal?)r.FineAmount) ?? 0;

            // Pass data to view
            ViewData["TotalUsers"] = totalUsers;
            ViewData["TotalBooks"] = totalBooks;
            ViewData["PendingRequests"] = pendingRequests;
            ViewData["UnpaidFines"] = unpaidFines;

            return View();
        }
    }
}

