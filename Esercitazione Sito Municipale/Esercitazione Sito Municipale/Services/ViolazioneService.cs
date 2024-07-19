using Esercitazione_Sito_Municipale.Models;
using System.Data.Common;
using System.Data.SqlClient;

namespace Esercitazione_Sito_Municipale.Services
{
    public class ViolazioneService : SqlServerServiceBase, IViolazioneService
    {
        public ViolazioneService(IConfiguration config) : base(config)
        {
        }

        public Violazione Create(DbDataReader reader)
        {
            return new Violazione
            {
               Descrizione = reader.GetString(0),
            };
        }

        public void CreaViolazione(Violazione violazione)
        {
            var query = "INSERT INTO Violazione (Descrizione) OUTPUT INSERTED.IDAnagrafica " +
                "VALUES (@Descrizione)";
            var cmd = GetCommand(query);
            cmd.Parameters.Add(new SqlParameter("@Descrizione", violazione.Descrizione));

            using var conn = GetConnection();
            conn.Open();
            var result = cmd.ExecuteScalar();

            if (result == null)
                throw new Exception("Creazione non completata");

            violazione.IdViolazione = Convert.ToInt32(result);
        }

        public IEnumerable<Violazione> GetAllViolazioni()
        {
            var query = "SELECT IdViolazione, Descrizione FROM Violazione";

            var cmd = GetCommand(query);
            using var conn = GetConnection();
            conn.Open();
            var reader = cmd.ExecuteReader();
            var ListaViolazioni = new List<Violazione>();
            while (reader.Read())
                ListaViolazioni.Add(Create(reader));
            return ListaViolazioni;
        }
    }
}
