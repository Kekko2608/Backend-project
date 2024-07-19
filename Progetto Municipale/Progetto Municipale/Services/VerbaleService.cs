using Progetto_Municipale.Models;
using System.Data.SqlClient;

namespace Progetto_Municipale.Services
{
    public class VerbaleService :IVerbaleService
    {
        private readonly string _connectionString;

        public VerbaleService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB");
        }


        private const string CREATE_VERBALE_COMMAND = "INSERT INTO [dbo].[Verbale] " +
            "(DataViolazione, IndirizzoViolazione, NominativoAgente, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, FK_Anagrafica, FK_Violazione) " +
            "OUTPUT INSERTED.IdVerbale " +
            "VALUES (@DataViolazione, @IndirizzoViolazione, @NominativoAgente, @DataTrascrizioneVerbale, @Importo, @DecurtamentoPunti, @FK_Anagrafica, @FK_Violazione)";
        public Verbale Create(Verbale verbale)
        {
            try
            {
                verbale.DataTrascrizioneVerbale = DateTime.Now;

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(CREATE_VERBALE_COMMAND, connection))
                    {
                        command.Parameters.AddWithValue("@DataViolazione", verbale.DataViolazione);
                        command.Parameters.AddWithValue("@IndirizzoViolazione", verbale.IndirizzoViolazione);
                        command.Parameters.AddWithValue("@NominativoAgente", verbale.NominativoAgente);
                        command.Parameters.AddWithValue("@DataTrascrizioneVerbale", verbale.DataTrascrizioneVerbale);
                        command.Parameters.AddWithValue("@Importo", verbale.Importo);
                        command.Parameters.AddWithValue("@DecurtamentoPunti", verbale.DecurtamentoPunti);
                        command.Parameters.AddWithValue("@FK_Anagrafica", verbale.FK_Anagrafica);
                        command.Parameters.AddWithValue("@FK_Violazione", verbale.FK_Violazione);

                        verbale.IdVerbale = (int)command.ExecuteScalar();
                    }
                    return verbale;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nella creazione del Verbale. Dettagli: " + ex.Message);
            }
        }


        private const string ALLVERBALI_BY_TRASGRESSORE_COMMAND = "SELECT a.IdAnagrafica, a.Nome, a.Cognome, COUNT(v.IdVerbale) AS TotaleVerbali " +
            "FROM [dbo].[Verbale] v " +
            "JOIN [dbo].[Anagrafica] a ON v.FK_Anagrafica = a.IdAnagrafica " +
            "GROUP BY a.IdAnagrafica, a.Nome, a.Cognome " +
            "ORDER BY TotaleVerbali DESC;";

        public List<VerbaleByIdTrasgr> GetAllVerbaliByTrasgr()
        {
            var result = new List<VerbaleByIdTrasgr>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(ALLVERBALI_BY_TRASGRESSORE_COMMAND, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var verbaleByTrasgressore = new VerbaleByIdTrasgr
                                {
                                    IdAnagrafica = reader.GetInt32(0),
                                    Nome = reader.GetString(1),
                                    Cognome = reader.GetString(2),
                                    TotaleVerbali = reader.GetInt32(3)
                                };
                                result.Add(verbaleByTrasgressore);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recuperare i verbali per il trasgressore. Controlla che i dati del trasgressore siano corretti e che la connessione al database sia stabile. Dettagli tecnici:" + ex.Message);
            }

            return result;
        }



    }
}

