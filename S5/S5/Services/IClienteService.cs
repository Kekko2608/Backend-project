using S5.Models;

namespace S5.Nuova_cartella1
{
    public interface IClienteService
    {
        IEnumerable<Cliente> GetAllClienti();
        Cliente GetClienteById(int clienteId);
        Cliente CreateCliente(Cliente cliente);
        void UpdateCliente(Cliente cliente);
        void DeleteCliente(int clienteId);
    }
}
