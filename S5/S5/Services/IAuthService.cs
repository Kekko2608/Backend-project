using S5.Models;

namespace S5.Services
{
    public interface IAuthService
    {
        ApplicationUser Login(string username, string password);
    }
}
