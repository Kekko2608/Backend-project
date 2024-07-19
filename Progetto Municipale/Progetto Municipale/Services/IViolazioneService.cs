using Progetto_Municipale.Models;

namespace Progetto_Municipale.Services
{
    public interface IViolazioneService
    {
        Violazione Create(Violazione violazione);   
        List<Violazione> GetAllViolazioni();  
        List<ViolOver10Punti> GetViolOver10Punti();  
        List<ViolOver400Importo> GetViolOver400Importo();   
    }
}
