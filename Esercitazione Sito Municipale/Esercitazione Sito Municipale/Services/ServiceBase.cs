using System.Data.Common;

namespace Esercitazione_Sito_Municipale.Services
{
    public abstract class ServiceBase
    {
        protected abstract DbConnection GetConnection();
        protected abstract DbCommand GetCommand(string commandText);

    }
}
