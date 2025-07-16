using Libary_Management_System.Data;
using Libary_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq; // for ToList
using System.Net.Mail;
using System.Net;

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

        [HttpGet]
        public IActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailEntity model, IFormFile attachment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please fill all required fields correctly.");
            }

            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("ahriad.cse@gmail.com", "ldwh euxp jwig qpwt"), // Replace here
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(model.FromEmail ?? "ahriad.cse@gmail.com"), // Must match credentials ideally
                    Subject = model.Subject,
                    Body = model.EmailBody,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(model.ToEmailAddress);

                if (attachment != null && attachment.Length > 0)
                {
                    var ms = new MemoryStream();
                    await attachment.CopyToAsync(ms);
                    ms.Position = 0;
                    mailMessage.Attachments.Add(new Attachment(ms, attachment.FileName, attachment.ContentType));
                }

                await smtpClient.SendMailAsync(mailMessage);

                return Json(new { success = true, message = "Email sent successfully!" });
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                if (ex.InnerException != null)
                    error += " Inner Exception: " + ex.InnerException.Message;

                return BadRequest("Error sending email: " + error);
            }
        }
    }
}
