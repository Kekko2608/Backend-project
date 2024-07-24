using Progetto_Albergo.Models;
using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Progetto_Albergo.Services
{
    public class ClienteService : IClienteService
    {
        private readonly string _connectionString;

        private const string CREATE_CLIENTE_COMMAND = @"
            INSERT INTO Cliente (Nome, Cognome, Cod_Fisc, Citta, Provincia, Email, Telefono, Cellulare)
            VALUES (@Nome, @Cognome, @Cod_Fisc, @Citta, @Provincia, @Email, @Telefono, @Cellulare);
            SELECT SCOPE_IDENTITY();";

        public ClienteService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB");
        }

        public Cliente AddCliente(Cliente cliente)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(CREATE_CLIENTE_COMMAND, connection)) // Associa il comando alla connessione
                    {
                        command.Connection = connection; // Assicuriamoci che la connessione sia impostata
                        command.Parameters.AddWithValue("@Nome", cliente.Nome);
                        command.Parameters.AddWithValue("@Cognome", cliente.Cognome);
                        command.Parameters.AddWithValue("@Cod_Fisc", cliente.Cod_Fisc);
                        command.Parameters.AddWithValue("@Citta", cliente.Citta);
                        command.Parameters.AddWithValue("@Provincia", cliente.Provincia);
                        command.Parameters.AddWithValue("@Email", cliente.Email);
                        command.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                        command.Parameters.AddWithValue("@Cellulare", cliente.Cellulare);

                        cliente.IdCliente = Convert.ToInt32(command.ExecuteScalar());
                    }
                    return cliente;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la creazione del cliente. Dettagli: " + ex.Message);
            }
        }

        public Cliente GetClienteByCodiceFiscale(string codiceFiscale)
        {
            throw new NotImplementedException();
        }
    }
}
