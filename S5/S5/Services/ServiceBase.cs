using System.Data.Common;

namespace S5.Services
{
    public abstract class ServiceBase
    {
        protected abstract DbConnection GetConnection();
        protected abstract DbCommand GetCommand(string commandText);
    }
}
