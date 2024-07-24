using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Progetto_Albergo.Services;
using System.Security.Claims;
using Progetto_Albergo.Models;

namespace Progetto_Albergo.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAuthService authService, ILogger<AccountController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Utente utenti)
        {
            try
            {
                var utente = _authService.Login(utenti.Username, utenti.Password);
                if (utente == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return RedirectToAction("Index", "Home");
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, utente.Username),
                    new Claim("utenteId", utente.IdUtente.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                ModelState.AddModelError(string.Empty, "An error occurred. Please try again.");
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Utente utenti)
        {
            try
            {
                var utente = _authService.Register(utenti.Username, utenti.Password);
                if (utente == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid register attempt.");
                    return View(utenti);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, utente.Username),
                    new Claim("utenteId", utente.IdUtente.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Registration failed.");
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View(utenti);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
