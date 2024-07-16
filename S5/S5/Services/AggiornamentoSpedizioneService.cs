using S5.Models;
using S5.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace S5.Nuova_cartella1
{
    public class AggiornamentoSpedizioneService : SqlServerServiceBase, IAggiornamentoSpedizioneService
    {
        public AggiornamentoSpedizioneService(IConfiguration config) : base(config)
        {
        }

        public AggiornamentoSpedizione CreateAggiornamento(AggiornamentoSpedizione aggiornamento)
        {
            var query = @"
                INSERT INTO AggiornamentiSpedizioni (SpedizioneID, Stato, Luogo, Descrizione, DataOra)
                OUTPUT INSERTED.AggiornamentoID
                VALUES (@SpedizioneID, @Stato, @Luogo, @Descrizione, @DataOra)";

            using var cmd = GetCommand(query);
            cmd.Parameters.Add(new SqlParameter("@SpedizioneID", aggiornamento.SpedizioneID));
            cmd.Parameters.Add(new SqlParameter("@Stato", aggiornamento.Stato));
            cmd.Parameters.Add(new SqlParameter("@Luogo", aggiornamento.Luogo));
            cmd.Parameters.Add(new SqlParameter("@Descrizione", aggiornamento.Descrizione));
            cmd.Parameters.Add(new SqlParameter("@DataOra", aggiornamento.DataOra));

            using var conn = GetConnection();
            conn.Open();
            aggiornamento.AggiornamentoID = (int)cmd.ExecuteScalar();

            return aggiornamento;
        }

        public void DeleteAggiornamento(int aggiornamentoId)
        {
            var query = "DELETE FROM AggiornamentiSpedizioni WHERE AggiornamentoID = @AggiornamentoID";

            using var cmd = GetCommand(query);
            cmd.Parameters.Add(new SqlParameter("@AggiornamentoID", aggiornamentoId));

            using var conn = GetConnection();
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public AggiornamentoSpedizione GetAggiornamentoById(int aggiornamentoId)
        {
            var query = @"
                SELECT AggiornamentoID, SpedizioneID, Stato, Luogo, Descrizione, DataOra
                FROM AggiornamentiSpedizioni
                WHERE AggiornamentoID = @AggiornamentoID";

            using var cmd = GetCommand(query);
            cmd.Parameters.Add(new SqlParameter("@AggiornamentoID", aggiornamentoId));

            using var conn = GetConnection();
            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new AggiornamentoSpedizione
                {
                    AggiornamentoID = reader.GetInt32(0),
                    SpedizioneID = reader.GetInt32(1),
                    Stato = reader.GetString(2),
                    Luogo = reader.GetString(3),
                    Descrizione = reader.GetString(4),
                    DataOra = reader.GetDateTime(5)
                };
            }
            return null;
        }

        public IEnumerable<AggiornamentoSpedizione> GetAllAggiornamentiBySpedizioneId(int spedizioneId)
        {
            var query = @"
                SELECT AggiornamentoID, SpedizioneID, Stato, Luogo, Descrizione, DataOra
                FROM AggiornamentiSpedizioni
                WHERE SpedizioneID = @SpedizioneID
                ORDER BY DataOra DESC";

            using var cmd = GetCommand(query);
            cmd.Parameters.Add(new SqlParameter("@SpedizioneID", spedizioneId));

            using var conn = GetConnection();
            conn.Open();
            using var reader = cmd.ExecuteReader();
            var ListaAggiornamenti = new List<AggiornamentoSpedizione>();
            while (reader.Read())
            {
                ListaAggiornamenti.Add(new AggiornamentoSpedizione
                {
                    AggiornamentoID = reader.GetInt32(0),
                    SpedizioneID = reader.GetInt32(1),
                    Stato = reader.GetString(2),
                    Luogo = reader.GetString(3),
                    Descrizione = reader.GetString(4),
                    DataOra = reader.GetDateTime(5)
                });
            }
            return ListaAggiornamenti;
        }

        public void UpdateAggiornamento(AggiornamentoSpedizione aggiornamento)
        {
            var query = @"
                UPDATE AggiornamentiSpedizioni
                SET SpedizioneID = @SpedizioneID,
                    Stato = @Stato,
                    Luogo = @Luogo,
                    Descrizione = @Descrizione,
                    DataOra = @DataOra
                WHERE AggiornamentoID = @AggiornamentoID";

            using var cmd = GetCommand(query);
            cmd.Parameters.Add(new SqlParameter("@AggiornamentoID", aggiornamento.AggiornamentoID));
            cmd.Parameters.Add(new SqlParameter("@SpedizioneID", aggiornamento.SpedizioneID));
            cmd.Parameters.Add(new SqlParameter("@Stato", aggiornamento.Stato));
            cmd.Parameters.Add(new SqlParameter("@Luogo", aggiornamento.Luogo));
            cmd.Parameters.Add(new SqlParameter("@Descrizione", aggiornamento.Descrizione));
            cmd.Parameters.Add(new SqlParameter("@DataOra", aggiornamento.DataOra));

            using var conn = GetConnection();
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
