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
            var users = _userManager.Users.ToList(); // fetch all users

            int totalUsers = users.Count;
            int totalBooks = _context.Books.Count();
            int pendingRequests = _context.BookRequests.Count(r => r.Status == "Pending");

            decimal unpaidFines = _context.BorrowRecords
                .Where(r => r.FineAmount > 0 && r.ReturnDate == null)
                .Sum(r => (decimal?)r.FineAmount) ?? 0;

            ViewBag.TotalUsers = totalUsers;
            ViewBag.TotalBooks = totalBooks;
            ViewBag.PendingRequests = pendingRequests;
            ViewBag.UnpaidFines = unpaidFines;

            return View(users);
        }

    }
}
