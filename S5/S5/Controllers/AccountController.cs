using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using S5.Models;
using S5.Services;
using System.Security.Claims;

namespace S5.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService authenticationService;

        public AccountController(IAuthService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(ApplicationUser user)
        {
            try
            {
                var u = authenticationService.Login(user.Username, user.Password);
                if (u == null)
                {
                    ViewBag.ErrorMessage = "Login fallito. Username o password non validi.";
                    return View();
                }

                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, u.Username),
                    new Claim("FriendlyName", u.FriendlyName)
                };
                u.Roles.ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r)));
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                     new ClaimsPrincipal(identity)
                );

                // Reindirizza alla view "CreaCliente" dopo il login
                return RedirectToAction("CreaCliente", "Home");
            }
            catch (Exception ex)
            {
                // Aggiungi log o gestisci l'eccezione come necessario
                ViewBag.ErrorMessage = "Si è verificato un errore durante il login.";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
