using GymPortal.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymPortal.Web.Controllers
{
    public class SessionsController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IBookingService _bookingService;
        private readonly UserManager<IdentityUser> _userManager;

        public SessionsController(ISessionService sessionService, IBookingService bookingService, UserManager<IdentityUser> userManager)
        {
            _sessionService = sessionService;
            _bookingService = bookingService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var sessions = await _sessionService.GetAllSessionsAsync();
            return View(sessions);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Book(int id)
        {
            var userId = _userManager.GetUserId(User);
            var success = await _bookingService.BookSessionAsync(userId, id);

            if (success)
            {
                TempData["SuccessMessage"] = "Snyggt! Ditt träningspass är nu bokat.";
            }
            else
            {
                TempData["ErrorMessage"] = "Det gick inte att boka. Passet är fullt eller så är du redan inbokad.";
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> MyBookings()
        {
            var userId = _userManager.GetUserId(User);
            var bookings = await _bookingService.GetUserBookingsAsync(userId);
            return View(bookings);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = _userManager.GetUserId(User);
            var success = await _bookingService.CancelBookingAsync(userId, id);

            if (success)
            {
                TempData["SuccessMessage"] = "Passet är nu avbokat!";
            }
            else
            {
                TempData["ErrorMessage"] = "Det gick inte att avboka passet.";
            }

            return RedirectToAction("MyBookings");
        }
    }
}