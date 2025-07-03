using Libary_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Libary_Management_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: User list page
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        // GET: Edit user page (normal full page)
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: Update user email via AJAX, returns JSON
        [HttpPost]
        public async Task<IActionResult> EditEmail(string id, string email)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return Json(new { success = false, message = "User not found" });

            user.Email = email;
            user.UserName = email;
            var result = await _userManager.UpdateAsync(user);

            return Json(new
            {
                success = result.Succeeded,
                message = result.Succeeded ? "Email updated successfully." : "Failed to update email."
            });
        }

        // POST: Delete user via AJAX, returns JSON
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return Json(new { success = false, message = "User not found" });

            var result = await _userManager.DeleteAsync(user);

            return Json(new
            {
                success = result.Succeeded,
                message = result.Succeeded ? "User deleted successfully." : "Failed to delete user."
            });
        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            var model = new AssignRoleViewModel
            {
                UserId = user.Id,
                Roles = _roleManager.Roles.Select(r => r.Name).ToList(),
                SelectedRole = roles.FirstOrDefault()
            };

            return View(model);
        }

        // POST: Assign new role via AJAX, returns JSON
        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string selectedRole)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(selectedRole))
                return Json(new { success = false, message = "Invalid data." });

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Json(new { success = false, message = "User not found." });

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            var result = await _userManager.AddToRoleAsync(user, selectedRole);

            return Json(new
            {
                success = result.Succeeded,
                message = result.Succeeded ? "Role assigned successfully." : "Failed to assign role."
            });
        }


    }
}

