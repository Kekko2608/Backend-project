using Progetto_Municipale.Models;
using System.Data.SqlClient;

namespace Progetto_Municipale.Services
{
    public class AnagraficaService : IAnagraficaService
    {

        private readonly string _connectionString;


        private const string CREATE_ANAGRAFICA_COMMAND = "INSERT INTO [dbo].[Anagrafica] " +
            "(Nome, Cognome, Indirizzo, Citta, CAP, Cod_Fisc) " +
            "OUTPUT INSERTED.IDAnagrafica " +
            "VALUES (@Nome, @Cognome, @Indirizzo, @Citta, @CAP, @Cod_Fisc)";


        public AnagraficaService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB");
        }

        public Anagrafica Create(Anagrafica anagrafica)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(CREATE_ANAGRAFICA_COMMAND, connection))
                    {
                        command.Parameters.AddWithValue("@Nome", anagrafica.Nome);
                        command.Parameters.AddWithValue("@Cognome", anagrafica.Cognome);
                        command.Parameters.AddWithValue("@Indirizzo", anagrafica.Indirizzo);
                        command.Parameters.AddWithValue("@Citta", anagrafica.Citta);
                        command.Parameters.AddWithValue("@CAP", anagrafica.CAP);
                        command.Parameters.AddWithValue("@Cod_Fisc", anagrafica.Cod_Fisc);

                        anagrafica.IdAnagrafica = (int)command.ExecuteScalar();
                    }
                    return anagrafica;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la creazione dell'anagrafica. Dettagli: " + ex.Message);
            }
        }


        private const string ALL_VERBALI_BY_PUNTI_DECURTATI_COMMAND = "SELECT a.IDAnagrafica, a.Nome, a.Cognome, SUM(v.DecurtamentoPunti) AS TotalePuntiDecurtati " +
           "FROM [dbo].[Verbale] v " +
           "JOIN [dbo].[Anagrafica] a ON v.FK_Anagrafica = a.IdAnagrafica " +
           "GROUP BY a.IdAnagrafica, a.Nome, a.Cognome " +
           "ORDER BY TotalePuntiDecurtati DESC;";
        public List<TrasgrByPuntiDecurtati> GetAllTrasgrByPuntiDec()
        {
            var result = new List<TrasgrByPuntiDecurtati>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(ALL_VERBALI_BY_PUNTI_DECURTATI_COMMAND, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var verbaleByPuntiDecurtati = new TrasgrByPuntiDecurtati
                                {
                                    IDAnagrafica = reader.GetInt32(0),
                                    Nome = reader.GetString(1),
                                    Cognome = reader.GetString(2),
                                    TotalePuntiDecurtati = reader.GetInt32(3)
                                };
                                result.Add(verbaleByPuntiDecurtati);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recuperare i verbali per punti decurtati. Verifica la connessione al database e i parametri di ricerca. Dettagli tecnici:  " + ex.Message);
            }

            return result;
        }
    }
}

