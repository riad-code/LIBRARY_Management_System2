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
        // GET: AssignRole
        [HttpGet]
        public async Task<IActionResult> AssignRole(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();

            var model = new AssignRoleViewModel
            {
                UserId = user.Id,
                Roles = allRoles,
                SelectedRole = currentRoles.FirstOrDefault()
            };

            return View(model);
        }
        // POST: AssignRole (AJAX)
        [HttpPost]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleViewModel model)
        {
            if (string.IsNullOrEmpty(model.UserId) || string.IsNullOrEmpty(model.SelectedRole))
            {
                return Json(new { success = false, message = "Invalid user or role." });
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found." });
            }

            // Remove existing roles
            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                return Json(new { success = false, message = "Failed to remove existing roles." });
            }

            // Assign new role
            var roleExists = await _roleManager.RoleExistsAsync(model.SelectedRole);
            if (!roleExists)
            {
                return Json(new { success = false, message = "Selected role does not exist." });
            }

            var addResult = await _userManager.AddToRoleAsync(user, model.SelectedRole);
            if (!addResult.Succeeded)
            {
                return Json(new { success = false, message = "Failed to assign role." });
            }

            return Json(new { success = true, message = "Role assigned successfully." });
        }

    }

}





