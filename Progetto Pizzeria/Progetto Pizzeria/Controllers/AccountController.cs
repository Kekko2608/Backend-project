using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Progetto_Pizzeria.Context;
using Progetto_Pizzeria.Models;

namespace Progetto_Pizzeria.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly DataContext _ctx;

        public AccountController(DataContext dataContext, ILogger<AccountController> logger)
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
                    TempData["User"] = "Anonimo";
                }
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
