using Progetto_Albergo.Models;

namespace Progetto_Albergo.Services
{
    public interface IClienteService
    {
        Cliente GetClienteByCodiceFiscale(string codiceFiscale);
        Cliente AddCliente(Cliente cliente);
    }
}
