using Progetto_Municipale.Models;

namespace Progetto_Municipale.Services
{
    public interface IVerbaleService
    {
        Verbale Create(Verbale verbale);
        List<VerbaleByIdTrasgr> GetAllVerbaliByTrasgr();

    }
}
