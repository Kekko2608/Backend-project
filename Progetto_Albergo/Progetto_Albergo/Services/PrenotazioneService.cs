using Progetto_Albergo.Models;
using System.Data.SqlClient;

namespace Progetto_Albergo.Services
{
    public class PrenotazioneService : IPrenotazioneService
    {
        private readonly string _connectionString;


        private const string CREATE_PRENOTAZIONE_COMMAND = @"INSERT INTO[dbo].[PRENOTAZIONE]
        (Data, Numero, Anno, Dal, Al, Caparra, Tariffa, Descrizione, FK_Cliente, FK_Camera)
        VALUES (@Data, @Numero, @Anno, @Dal, @Al, @Caparra, @Tariffa, @Descrizione, @FK_Cliente, @FK_Camera)";

        private const string PRENOTAZIONI_BY_CF = @"SELECT
         p.IdPrenotazione,
         p.Data,
         p.Numero,
         p.Anno,
         p.Dal,
         p.Al,
         p.Caparra,
         p.Tariffa,
         p.Descrizione,
         p.FK_Cliente,
         p.FK_Camera
        FROM
            [dbo].[PRENOTAZIONE] p
        INNER JOIN
         [dbo].[CLIENTE] c ON p.FK_Cliente = c.IdCliente
        WHERE
            c.Cod_Fisc = @CodiceFiscale";

       private const string DETTAGLIO_PRENOTAZIONE = @"
        SELECT
            p.IdPrenotazione,
            c.NumeroCamera AS NumeroStanza,
            p.Dal AS DataInizio,
            p.Al AS DataFine,
            p.Tariffa,
            p.Caparra
        FROM
            [dbo].[PRENOTAZIONE] p
        INNER JOIN
            [dbo].[CAMERA] c ON p.FK_Camera = c.IdCamera
        WHERE
            p.IdPrenotazione = @IdPrenotazione";

        private const string SERVIZI_AGGIUNTIVI_BY_PRENOTAZIONE = @"
SELECT
    sa.IdServizio,
    sa.Descrizione,
    ps.Data,
    ps.Quantita,
    ps.Prezzo
FROM
    [dbo].[SERVIZIO_AGGIUNTIVO] sa
INNER JOIN
    [dbo].[PRENOTAZIONI_SERVIZI] ps ON sa.IdServizio = ps.FK_Servizio
WHERE
    ps.FK_Prenotazione = @IdPrenotazione";

        private const string TOTALE_SERVIZI_AGGIUNTIVI = @"
    SELECT SUM(ps.Prezzo * ps.Quantita) 
    FROM [dbo].[PRENOTAZIONI_SERVIZI] ps
    WHERE ps.FK_Prenotazione = @IdPrenotazione";
        private const string ALL_PRENOTAZIONI = @"
    SELECT
        p.IdPrenotazione,
        p.Data,
        p.Numero,
        p.Anno,
        p.Dal,
        p.Al,
        p.Caparra,
        p.Tariffa,
        p.Descrizione,
        p.FK_Cliente,
        p.FK_Camera
    FROM
        [dbo].[PRENOTAZIONE] p
    ORDER BY
        p.Data DESC;  -- Ordina le prenotazioni per data decrescente
";
        private const string TOTALE_PRENOTAZIONI_PENSIONE_COMPLETA = @"
    SELECT COUNT(*)
    FROM [dbo].[PRENOTAZIONE]
    WHERE Descrizione = 'Pensione Completa';
";


        public PrenotazioneService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB");
        }


        public Prenotazione AddPrenotazione(Prenotazione prenotazione)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(CREATE_PRENOTAZIONE_COMMAND, connection)) // Associa il comando alla connessione
                    {
                        command.Connection = connection; // Assicuriamoci che la connessione sia impostata
                        command.Parameters.AddWithValue("@Data", prenotazione.Data);
                        command.Parameters.AddWithValue("@Numero", prenotazione.Numero);
                        command.Parameters.AddWithValue("@Anno", prenotazione.Anno);
                        command.Parameters.AddWithValue("@Dal", prenotazione.Dal);
                        command.Parameters.AddWithValue("@Al", prenotazione.Al);
                        command.Parameters.AddWithValue("@Caparra", prenotazione.Caparra);
                        command.Parameters.AddWithValue("@Tariffa", prenotazione.Tariffa);
                        command.Parameters.AddWithValue("@Descrizione", prenotazione.Descrizione);
                        command.Parameters.AddWithValue("@FK_Cliente", prenotazione.FK_Cliente);
                        command.Parameters.AddWithValue("@FK_Camera", prenotazione.FK_Camera);


                        prenotazione.IdPrenotazione = Convert.ToInt32(command.ExecuteScalar());
                    }
                    return prenotazione;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la creazione del cliente. Dettagli: " + ex.Message);
            }
        }

        public PrenotazioneInfo GetPrenotazioneById(int idPrenotazione)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(DETTAGLIO_PRENOTAZIONE, connection))
                    {
                        command.Parameters.AddWithValue("@IdPrenotazione", idPrenotazione);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new PrenotazioneInfo
                                {
                                    IdPrenotazione = reader.GetInt32(0),
                                    NumeroStanza = reader.GetString(1),
                                    DataInizio = reader.GetDateTime(2),
                                    DataFine = reader.GetDateTime(3),
                                    Tariffa = reader.GetDecimal(4),
                                    Caparra=reader.GetDecimal(5)
                                };
                            }
                            else
                            {
                                throw new Exception("Prenotazione non trovata.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recuperare il dettaglio della prenotazione. Dettagli tecnici: " + ex.Message);
            }
        }
    

        public List<Prenotazione> GetPrenotazioniByCodiceFiscale(string CodiceFiscale)
        {
            var result = new List<Prenotazione>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(PRENOTAZIONI_BY_CF, connection))
                    {
                        // Aggiungere il parametro per la query
                        command.Parameters.AddWithValue("@CodiceFiscale", CodiceFiscale);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var prenotByCF = new Prenotazione
                                {
                                    IdPrenotazione = reader.GetInt32(0),
                                    Data = reader.GetDateTime(1),
                                    Numero = reader.GetInt32(2),
                                    Anno = reader.GetInt32(3),
                                    Dal = reader.GetDateTime(4),
                                    Al = reader.GetDateTime(5),
                                    Caparra = reader.GetDecimal(6),
                                    Tariffa = reader.GetDecimal(7),
                                    Descrizione = reader.GetString(8),
                                    FK_Cliente = reader.GetInt32(9),
                                    FK_Camera = reader.GetInt32(10)
                                };
                                result.Add(prenotByCF);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recuperare le prenotazioni. Dettagli tecnici: " + ex.Message);
            }

            return result;
        }


        public List<Prenotazioni_Servizi> GetServiziAggiuntiviByPrenotazione(int idPrenotazione)
        {
            var result = new List<Prenotazioni_Servizi>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(SERVIZI_AGGIUNTIVI_BY_PRENOTAZIONE, connection))
                    {
                        command.Parameters.AddWithValue("@IdPrenotazione", idPrenotazione);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var servizio = new Prenotazioni_Servizi
                                {
                                    IdPrenotServizi = reader.GetInt32(0),
                                    Descrizione = reader.GetString(1),  
                                    Data = reader.GetDateTime(2),
                                    Quantita = reader.GetInt32(3),
                                    Prezzo = reader.GetDecimal(4),
                                    FK_Prenotazione = idPrenotazione,
                                    FK_Servizio = reader.GetInt32(5)  
                                };
                                result.Add(servizio);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recuperare i servizi aggiuntivi. Dettagli tecnici: " + ex.Message);
            }

            return result;
        }

        public decimal GetTotaleServiziAggiuntivi(int idPrenotazione)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(TOTALE_SERVIZI_AGGIUNTIVI, connection))
                    {
                        command.Parameters.AddWithValue("@IdPrenotazione", idPrenotazione);

                        var result = command.ExecuteScalar();
                        return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel calcolare il totale dei servizi aggiuntivi. Dettagli tecnici: " + ex.Message);
            }
        }

        public List<Prenotazione> GetAllPrenotazioni()
        {
            var result = new List<Prenotazione>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(ALL_PRENOTAZIONI, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var prenotazione = new Prenotazione
                                {
                                    IdPrenotazione = reader.GetInt32(0),
                                    Data = reader.GetDateTime(1),
                                    Numero = reader.GetInt32(2),
                                    Anno = reader.GetInt32(3),
                                    Dal = reader.GetDateTime(4),
                                    Al = reader.GetDateTime(5),
                                    Caparra = reader.GetDecimal(6),
                                    Tariffa = reader.GetDecimal(7),
                                    Descrizione = reader.GetString(8),
                                    FK_Cliente = reader.GetInt32(9),
                                    FK_Camera = reader.GetInt32(10),
                                    
                                };

                                result.Add(prenotazione);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recuperare le prenotazioni. Dettagli tecnici: " + ex.Message);
            }

            return result;
        }

        public int GetTotalePrenotazioniPensioneCompleta()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(TOTALE_PRENOTAZIONI_PENSIONE_COMPLETA, connection))
                    {
                        return (int)command.ExecuteScalar(); // Restituisce il conteggio delle prenotazioni
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recuperare il totale delle prenotazioni per pensione completa. Dettagli tecnici: " + ex.Message);
            }
        }

    }
}

