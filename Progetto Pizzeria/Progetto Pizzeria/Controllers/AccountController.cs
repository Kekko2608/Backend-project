using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Progetto_Pizzeria.Context;
using Progetto_Pizzeria.Models;
using System.Security.Claims;

namespace Progetto_Pizzeria.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _ctx;

        public AccountController(DataContext dataContext, ILogger<HomeController> logger)
        {
            _ctx = dataContext;
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _ctx.Users
                    .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .SingleOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Email o password non validi.");
                    return View(model);
                }

                // Crea le credenziali e il ticket di autenticazione
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.UserRoles.FirstOrDefault()?.Role.Name ?? "Cliente")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true // Mantiene il login tra le sessioni
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }


        public IActionResult Register()
        {
            var roles = _ctx.Roles.ToList();
            ViewBag.Roles = roles;
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model, int roleId)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _ctx.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "L'email è già in uso.");
                    return View(model);
                }

                var newUser = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password
                };

                _ctx.Users.Add(newUser);
                await _ctx.SaveChangesAsync();

                var userRole = new UserRole
                {
                    UserId = newUser.Id,
                    RoleId = roleId
                };

                _ctx.UserRoles.Add(userRole);
                await _ctx.SaveChangesAsync();

                return RedirectToAction("Login");
            }

            return View(model);
        }



        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
