using Progetto_Albergo.Models;

namespace Progetto_Albergo.Services
{
    public interface IAuthService
    {
        Utente Login(string username, string password);
        Utente Register(string username, string password);
    }
}
