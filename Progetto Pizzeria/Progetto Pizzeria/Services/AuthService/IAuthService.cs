using Humanizer.Localisation;
using Progetto_Pizzeria.Models;

namespace Progetto_Pizzeria.Services.AuthService
{
    public interface IAuthService
    {
        User Login(string Name, string Password);
        User Register(string Name, string Password);
    }
}
