using Progetto_Municipale.Models;
using System.Data.SqlClient;

namespace Progetto_Municipale.Services
{
    public class ViolazioneService : IViolazioneService
    {
       
            private readonly string _connectionString;



            public ViolazioneService(IConfiguration configuration)
            {
                _connectionString = configuration.GetConnectionString("DB");
            }

            private const string CREATE_VIOLAZIONE_COMMAND = "INSERT INTO [dbo].[Violazione] (Descrizione) OUTPUT INSERTED.IdViolazione VALUES (@Descrizione)";

            public Violazione Create(Violazione violazione)
            {
                try
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        using (var command = new SqlCommand(CREATE_VIOLAZIONE_COMMAND, connection))
                        {
                            command.Parameters.AddWithValue("@Descrizione", violazione.Descrizione);

                            violazione.IdViolazione = (int)command.ExecuteScalar();
                        }
                    }
                    return violazione;
                }
                catch (Exception ex)
                {
                    throw new Exception("Errore nella creazione Violazione: " + ex.Message);
                }
            }


            private const string GET_ALL_VIOLAZIONI_COMMAND = "SELECT IdViolazione, Descrizione FROM [dbo].[Violazione]";
            public List<Violazione> GetAllViolazioni()
            {
                var violazioni = new List<Violazione>();

                try
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        using (var command = new SqlCommand(GET_ALL_VIOLAZIONI_COMMAND, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var violazione = new Violazione
                                    {
                                        IdViolazione = reader.GetInt32(0),
                                        Descrizione = reader.GetString(1)
                                    };
                                    violazioni.Add(violazione);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving violazioni: " + ex.Message);
                }

                return violazioni;
            }


            private const string GET_VIOLAZIONI_OVER_10_PUNTI_COMMAND = @"
            SELECT v.Importo, a.Nome, a.Cognome, v.DataViolazione, v.DecurtamentoPunti
            FROM [dbo].[Verbale] v
            INNER JOIN [dbo].[Anagrafica] a ON v.FK_Anagrafica = a.IdAnagrafica
            WHERE v.DecurtamentoPunti > 10
            ORDER BY v.DecurtamentoPunti DESC;";
            public List<ViolOver10Punti> GetViolOver10Punti()
            {
                var result = new List<ViolOver10Punti>();

                try
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        using (var command = new SqlCommand(GET_VIOLAZIONI_OVER_10_PUNTI_COMMAND, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var violazione = new ViolOver10Punti
                                    {
                                        Importo = reader.GetDecimal(0),
                                        Nome = reader.GetString(1),
                                        Cognome = reader.GetString(2),
                                        DataViolazione = reader.GetDateTime(3),
                                        DecurtamentoPunti = reader.GetInt32(4)
                                    };
                                    result.Add(violazione);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving violations with over 10 points: " + ex.Message);
                }

                return result;
            }

            private const string GET_VIOLAZIONI_OVER_400_IMPORTO_COMMAND = @"
            SELECT a.Nome, a.Cognome, v.DataViolazione, v.Importo
            FROM [dbo].[Verbale] v
            INNER JOIN [dbo].[Anagrafica] a ON v.FK_Anagrafica = a.IdAnagrafica
            WHERE v.Importo > 400
            ORDER BY v.Importo DESC;";

            public List<ViolOver400Importo> GetViolOver400Importo()
            {
                var result = new List<ViolOver400Importo>();

                try
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        using (var command = new SqlCommand(GET_VIOLAZIONI_OVER_400_IMPORTO_COMMAND, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var violazione = new ViolOver400Importo
                                    {
                                        Nome = reader.GetString(0),
                                        Cognome = reader.GetString(1),
                                        DataViolazione = reader.GetDateTime(2),
                                        Importo = reader.GetDecimal(3)
                                    };
                                    result.Add(violazione);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving violations with import over 400 euros: " + ex.Message);
                }

                return result;
            }

        }
    }

