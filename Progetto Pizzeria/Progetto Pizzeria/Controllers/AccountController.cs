using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Progetto_Pizzeria.Context;
using Progetto_Pizzeria.Models;

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
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _ctx.Users
                    .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .SingleOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if (user == null)
                    TempData["User"] = "Anonimo";
                else
                {
                    TempData["User"] = user.Email;
                    var roles = user.UserRoles.Select(ur => ur.Role.Name).ToArray();
                    TempData["Roles"] = roles;
                }
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
                    Password = model.Password // Considera di hashare la password per motivi di sicurezza
                };

                _ctx.Users.Add(newUser);
                await _ctx.SaveChangesAsync();

                // Assegna il ruolo all'utente
                var userRole = new UserRole
                {
                    UserId = newUser.Id,
                    RoleId = roleId
                };

                _ctx.UserRoles.Add(userRole);
                await _ctx.SaveChangesAsync();

                TempData["SuccessMessage"] = "Registrazione completata con successo!";
                return RedirectToAction("Login");
            }

            return View(model);
        }


        public IActionResult Logout()
        {
            // Pulisce i dati dell'utente dalla sessione
            TempData.Remove("User");
            TempData.Remove("Roles");

            // Reindirizza alla pagina di login o alla home page
            return RedirectToAction("Login");
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
