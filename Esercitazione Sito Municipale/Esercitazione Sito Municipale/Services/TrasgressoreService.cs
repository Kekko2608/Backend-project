using Esercitazione_Sito_Municipale.Models;
using System.Data.Common;
using System.Data.SqlClient;

namespace Esercitazione_Sito_Municipale.Services
{
    public class TrasgressoreService : SqlServerServiceBase, ITrasgressoreService
    {
        public TrasgressoreService(IConfiguration config) : base(config)
        {
        }

        public Anagrafica Create(DbDataReader reader)
        {
            return new Anagrafica
            {
                Nome = reader.GetString(0),
                Cognome = reader.GetString(1),
                Indirizzo = reader.GetString(2),
                Citta = reader.GetString(3),
                CAP = reader.GetString(4),
                Cod_Fisc = reader.GetString(5),
            };
        }

        public void CreaTrasgressore(Anagrafica trasgressore)
        {
            var query = "INSERT INTO ANAGRAFICA (Nome, Cognome, Indirizzo, Citta, CAP, Cod_Fisc)" +
                "OUTPUT INSERTED.IDAnagrafica " +
                "VALUES (@Nome, @Cognome, @Indirizzo, @Citta, @CAP, @Cod_Fisc)";
            var cmd = GetCommand(query);
            cmd.Parameters.Add(new SqlParameter("@Nome", trasgressore.Nome));
            cmd.Parameters.Add(new SqlParameter("@Cognome", trasgressore.Cognome));
            cmd.Parameters.Add(new SqlParameter("@Indirizzo", trasgressore.Indirizzo));
            cmd.Parameters.Add(new SqlParameter("@Citta", trasgressore.Citta));
            cmd.Parameters.Add(new SqlParameter("@CAP", trasgressore.CAP));
            cmd.Parameters.Add(new SqlParameter("@Cod_Fisc", trasgressore.Cod_Fisc));

            using var conn = GetConnection();
            conn.Open();
            var result = cmd.ExecuteScalar();

            if (result == null)
                throw new Exception("Creazione non completata");

            trasgressore.IdAnagrafica = Convert.ToInt32(result);

        }

        public IEnumerable<Anagrafica> GetAllTrasgressori()
        {
            var query = "SELECT * FROM Anagrafica";

            var cmd = GetCommand(query);
            using var conn = GetConnection();
            conn.Open();
            var reader = cmd.ExecuteReader();
            var ListaTrasgressori = new List<Anagrafica>();
            while (reader.Read())
                ListaTrasgressori.Add(Create(reader));
            return ListaTrasgressori;
        }
    }
}
