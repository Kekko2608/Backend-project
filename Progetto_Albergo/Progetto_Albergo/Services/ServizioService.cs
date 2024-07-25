using Progetto_Albergo.Models;
using System.Data.SqlClient;

namespace Progetto_Albergo.Services
{
    public class ServizioService : IServizioService
    {
        private readonly string _connectionString;

        public ServizioService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB");
        }

        public List<Servizio_Aggiuntivo> GetAllServizi()
        {
            var result = new List<Servizio_Aggiuntivo>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"SELECT IdServizio, Descrizione FROM [dbo].[SERVIZIO_AGGIUNTIVO]";
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new Servizio_Aggiuntivo
                                {
                                    IdServizio = reader.GetInt32(0),
                                    Descrizione = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recuperare i servizi. Dettagli tecnici: " + ex.Message);
            }

            return result;
        }

        public Servizio_Aggiuntivo GetServizioById(int id)
        {
            Servizio_Aggiuntivo servizio = null;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"SELECT IdServizio, Descrizione FROM [dbo].[SERVIZIO_AGGIUNTIVO] WHERE IdServizio = @IdServizio";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdServizio", id);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                servizio = new Servizio_Aggiuntivo
                                {
                                    IdServizio = reader.GetInt32(0),
                                    Descrizione = reader.GetString(1),
                                   
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recuperare il servizio. Dettagli tecnici: " + ex.Message);
            }

            return servizio;
        }
    }
}
