using GymPortal.Application.Interfaces;
using GymPortal.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymPortal.Web.Controllers
{
    [Authorize]
    public class MembershipController : Controller
    {
        private readonly IMembershipService _membershipService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly GymDbContext _context;

        public MembershipController(
            IMembershipService membershipService,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            GymDbContext context)
        {
            _membershipService = membershipService;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User)!;
            var membership = await _membershipService.GetUserMembershipAsync(userId);
            return View(membership);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string type)
        {
            var userId = _userManager.GetUserId(User)!;
            var success = await _membershipService.CreateMembershipAsync(userId, type);

            if (success)
            {
                TempData["SuccessMessage"] = "Ditt medlemskap är nu skapat!";
            }
            else
            {
                TempData["ErrorMessage"] = "Det gick inte att skapa medlemskapet. Du kanske redan har ett?";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var userId = user.Id;

            var memberships = _context.Memberships.Where(m => m.UserId == userId);
            _context.Memberships.RemoveRange(memberships);

            var bookings = _context.Bookings.Where(b => b.UserId == userId);
            _context.Bookings.RemoveRange(bookings);

            await _context.SaveChangesAsync();

            await _userManager.DeleteAsync(user);
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}