using S5.Models;
using S5.Services;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace S5.Nuova_cartella1
{
    public class ClienteService : SqlServerServiceBase, IClienteService
    {
        public ClienteService(IConfiguration config) : base(config)
        {
        }

        public Cliente CreateCliente(DbDataReader reader)
        {
            return new Cliente
            {
                ClienteID = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Cognome = reader.GetString(2),
                CodiceFiscale = reader.IsDBNull(3) ? null : reader.GetString(3),
                PartitaIVA = reader.IsDBNull(4) ? null : reader.GetString(4),
                Indirizzo = reader.GetString(5),
                Telefono = reader.GetString(6),
                Email = reader.GetString(7)
            };
        }

        public Cliente CreateCliente(Cliente cliente)
        {
            var query = @"
                INSERT INTO Clienti (Nome, Cognome, CodiceFiscale, PartitaIVA, Indirizzo, Telefono, Email)
                OUTPUT INSERTED.ClienteID
                VALUES (@Nome, @Cognome, @CodiceFiscale, @PartitaIVA, @Indirizzo, @Telefono, @Email)";

            using var cmd = GetCommand(query);
            cmd.Parameters.Add(new SqlParameter("@Nome", cliente.Nome));
            cmd.Parameters.Add(new SqlParameter("@Cognome", cliente.Cognome));
            cmd.Parameters.Add(new SqlParameter("@CodiceFiscale", cliente.CodiceFiscale ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@PartitaIVA", cliente.PartitaIVA ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@Indirizzo", cliente.Indirizzo));
            cmd.Parameters.Add(new SqlParameter("@Telefono", cliente.Telefono));
            cmd.Parameters.Add(new SqlParameter("@Email", cliente.Email));

            using var conn = GetConnection();
            conn.Open();
            cliente.ClienteID = (int)cmd.ExecuteScalar();

            return cliente;
        }

        public void DeleteCliente(int clienteId)
        {
            var query = "DELETE FROM Clienti WHERE ClienteID = @ClienteID";

            using var cmd = GetCommand(query);
            cmd.Parameters.Add(new SqlParameter("@ClienteID", clienteId));

            using var conn = GetConnection();
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Cliente> GetAllClienti()
        {
            var query = "SELECT ClienteID, Nome, Cognome, CodiceFiscale, PartitaIVA, Indirizzo, Telefono, Email FROM Clienti";

            using var cmd = GetCommand(query);
            using var conn = GetConnection();
            conn.Open();
            using var reader = cmd.ExecuteReader();
            var ListaClienti = new List<Cliente>();
            while (reader.Read())
            {
                ListaClienti.Add(CreateCliente(reader));
            }
            return ListaClienti;
        }

        public Cliente GetClienteById(int clienteId)
        {
            var query = "SELECT ClienteID, Nome, Cognome, CodiceFiscale, PartitaIVA, Indirizzo, Telefono, Email FROM Clienti WHERE ClienteID = @ClienteID";

            using var cmd = GetCommand(query);
            cmd.Parameters.Add(new SqlParameter("@ClienteID", clienteId));

            using var conn = GetConnection();
            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return CreateCliente(reader);
            }
            return null;
        }

        public void UpdateCliente(Cliente cliente)
        {
            var query = @"
                UPDATE Clienti
                SET Nome = @Nome,
                    Cognome = @Cognome,
                    CodiceFiscale = @CodiceFiscale,
                    PartitaIVA = @PartitaIVA,
                    Indirizzo = @Indirizzo,
                    Telefono = @Telefono,
                    Email = @Email
                WHERE ClienteID = @ClienteID";

            using var cmd = GetCommand(query);
            cmd.Parameters.Add(new SqlParameter("@ClienteID", cliente.ClienteID));
            cmd.Parameters.Add(new SqlParameter("@Nome", cliente.Nome));
            cmd.Parameters.Add(new SqlParameter("@Cognome", cliente.Cognome));
            cmd.Parameters.Add(new SqlParameter("@CodiceFiscale", cliente.CodiceFiscale ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@PartitaIVA", cliente.PartitaIVA ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@Indirizzo", cliente.Indirizzo));
            cmd.Parameters.Add(new SqlParameter("@Telefono", cliente.Telefono));
            cmd.Parameters.Add(new SqlParameter("@Email", cliente.Email));

            using var conn = GetConnection();
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
