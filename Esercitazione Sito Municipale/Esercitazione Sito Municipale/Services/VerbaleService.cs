using Esercitazione_Sito_Municipale.Models;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Esercitazione_Sito_Municipale.Services
{
    public class VerbaleService : SqlServerServiceBase, IVerbaleService
    {
        public VerbaleService(IConfiguration config) : base(config)
        {
        }

        public Verbale Create (DbDataReader reader)
        {
            return new Verbale
            {

                DataViolazione = reader.GetDateTime(0),
                IndirizzoViolazione=reader.GetString(1),
                NominativoAgente=reader.GetString(2),
                DataTrascrizioneVerbale=reader.GetDateTime(3),
                Importo=reader.GetDecimal(4),
                DecurtamentoPunti=reader.GetInt32(5),
                FK_Violazione= (int)reader["IdViolazione"],
                FK_Anagrafica= (int)reader["IdAnagrafica"]

            };
        }

        public void CreaVerbale(Verbale verbale)
        {

            var query = "INSERT INTO Verbale (DataViolazione, IndirizzoViolazione,NominativoAgente,DataTrascrizioneVerbale, Importo, DecurtamentoPunti, FK_Anagrafica, FK_Violazione)" +
                " OUTPUT INSERTED.IDAnagrafica " +
                "VALUES (@DataViolazione, @IndirizzoViolazione, @NominativoAgente, @DataTrascrizioneVerbale, @Importo, @DecurtamentoPunti, @FK_Anagrafica, @FK_Violazione)";
            var cmd = GetCommand(query);
            cmd.Parameters.Add(new SqlParameter("@DataViolazione", verbale.DataViolazione));
            cmd.Parameters.Add(new SqlParameter("@IndirizzoViolazione", verbale.IndirizzoViolazione));
            cmd.Parameters.Add(new SqlParameter("@NominativoAgente", verbale.NominativoAgente));
            cmd.Parameters.Add(new SqlParameter("@DataTrascrizioneVerbale", verbale.DataTrascrizioneVerbale));
            cmd.Parameters.Add(new SqlParameter("@Importo", verbale.Importo));
            cmd.Parameters.Add(new SqlParameter("@DecurtamentoPunti", verbale.DecurtamentoPunti));
            cmd.Parameters.Add(new SqlParameter("@FK_Anagrafica", verbale.FK_Anagrafica));
            cmd.Parameters.Add(new SqlParameter("@FK_Violazione", verbale.FK_Violazione));

            using var conn = GetConnection();
            conn.Open();
            var result = cmd.ExecuteScalar();

            if (result == null)
                throw new Exception("Creazione non completata");

            verbale.IdVerbale = Convert.ToInt32(result);
        }










        public IEnumerable<Verbale> GetAllVerbali()
        {
            var query = "SELECT * FROM Verbale";

            var cmd = GetCommand(query);
            using var conn = GetConnection();
            conn.Open();
            var reader = cmd.ExecuteReader();
            var ListaVerbali = new List<Verbale>();
            while (reader.Read())
                ListaVerbali.Add(Create(reader));
            return ListaVerbali;
        }
    }
}
