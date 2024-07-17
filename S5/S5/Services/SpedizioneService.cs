using S5.Models;
using S5.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace S5.Nuova_cartella1
{
    public class SpedizioneService : SqlServerServiceBase, ISpedizioneService
    {
        public SpedizioneService(IConfiguration config) : base(config)
        {
        }

        public Spedizione CreateSpedizione(Spedizione spedizione)
        {
            var query = @"
                INSERT INTO Spedizioni (ClienteID, NumeroIdentificativo, DataSpedizione, Peso, CittaDestinataria, IndirizzoDestinatario, NominativoDestinatario, Costo, DataConsegnaPrevista)
                OUTPUT INSERTED.SpedizioneID
                VALUES (@ClienteID, @NumeroIdentificativo, @DataSpedizione, @Peso, @CittaDestinataria, @IndirizzoDestinatario, @NominativoDestinatario, @Costo, @DataConsegnaPrevista)";

            using var conn = GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.Add(new SqlParameter("@ClienteID", spedizione.ClienteID));
            cmd.Parameters.Add(new SqlParameter("@NumeroIdentificativo", spedizione.NumeroIdentificativo));
            cmd.Parameters.Add(new SqlParameter("@DataSpedizione", spedizione.DataSpedizione));
            cmd.Parameters.Add(new SqlParameter("@Peso", spedizione.Peso));
            cmd.Parameters.Add(new SqlParameter("@CittaDestinataria", spedizione.CittaDestinataria));
            cmd.Parameters.Add(new SqlParameter("@IndirizzoDestinatario", spedizione.IndirizzoDestinatario));
            cmd.Parameters.Add(new SqlParameter("@NominativoDestinatario", spedizione.NominativoDestinatario));
            cmd.Parameters.Add(new SqlParameter("@Costo", spedizione.Costo));
            cmd.Parameters.Add(new SqlParameter("@DataConsegnaPrevista", spedizione.DataConsegnaPrevista));

            conn.Open();
            spedizione.SpedizioneID = (int)cmd.ExecuteScalar();
            conn.Close();

            return spedizione;
        }

        public void DeleteSpedizione(int spedizioneId)
        {
            var query = "DELETE FROM Spedizioni WHERE SpedizioneID = @SpedizioneID";

            using var conn = GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.Add(new SqlParameter("@SpedizioneID", spedizioneId));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public IEnumerable<Spedizione> GetAllSpedizioni()
        {
            var query = @"
                SELECT SpedizioneID, ClienteID, NumeroIdentificativo, DataSpedizione, Peso, CittaDestinataria, IndirizzoDestinatario, NominativoDestinatario, Costo, DataConsegnaPrevista
                FROM Spedizioni";

            using var conn = GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = query;

            conn.Open();
            using var reader = cmd.ExecuteReader();
            var ListaSpedizioni = new List<Spedizione>();
            while (reader.Read())
            {
                ListaSpedizioni.Add(new Spedizione
                {
                    SpedizioneID = reader.GetInt32(0),
                    ClienteID = reader.GetInt32(1),
                    NumeroIdentificativo = reader.GetString(2),
                    DataSpedizione = reader.GetDateTime(3),
                    Peso = reader.GetDecimal(4),
                    CittaDestinataria = reader.GetString(5),
                    IndirizzoDestinatario = reader.GetString(6),
                    NominativoDestinatario = reader.GetString(7),
                    Costo = reader.GetDecimal(8),
                    DataConsegnaPrevista = reader.GetDateTime(9)
                });
            }
            conn.Close();
            return ListaSpedizioni;
        }

        public Spedizione GetSpedizioneById(int spedizioneId)
        {
            var query = @"
                SELECT SpedizioneID, ClienteID, NumeroIdentificativo, DataSpedizione, Peso, CittaDestinataria, IndirizzoDestinatario, NominativoDestinatario, Costo, DataConsegnaPrevista
                FROM Spedizioni
                WHERE SpedizioneID = @SpedizioneID";

            using var conn = GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.Add(new SqlParameter("@SpedizioneID", spedizioneId));

            conn.Open();
            using var reader = cmd.ExecuteReader();
            Spedizione? spedizione = null;
            if (reader.Read())
            {
                spedizione = new Spedizione
                {
                    SpedizioneID = reader.GetInt32(0),
                    ClienteID = reader.GetInt32(1),
                    NumeroIdentificativo = reader.GetString(2),
                    DataSpedizione = reader.GetDateTime(3),
                    Peso = reader.GetDecimal(4),
                    CittaDestinataria = reader.GetString(5),
                    IndirizzoDestinatario = reader.GetString(6),
                    NominativoDestinatario = reader.GetString(7),
                    Costo = reader.GetDecimal(8),
                    DataConsegnaPrevista = reader.GetDateTime(9)
                };
            }
            conn.Close();
            return spedizione;
        }

        public void UpdateSpedizione(Spedizione spedizione)
        {
            var query = @"
                UPDATE Spedizioni
                SET ClienteID = @ClienteID,
                    NumeroIdentificativo = @NumeroIdentificativo,
                    DataSpedizione = @DataSpedizione,
                    Peso = @Peso,
                    CittaDestinataria = @CittaDestinataria,
                    IndirizzoDestinatario = @IndirizzoDestinatario,
                    NominativoDestinatario = @NominativoDestinatario,
                    Costo = @Costo,
                    DataConsegnaPrevista = @DataConsegnaPrevista
                WHERE SpedizioneID = @SpedizioneID";

            using var conn = GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.Add(new SqlParameter("@SpedizioneID", spedizione.SpedizioneID));
            cmd.Parameters.Add(new SqlParameter("@ClienteID", spedizione.ClienteID));
            cmd.Parameters.Add(new SqlParameter("@NumeroIdentificativo", spedizione.NumeroIdentificativo));
            cmd.Parameters.Add(new SqlParameter("@DataSpedizione", spedizione.DataSpedizione));
            cmd.Parameters.Add(new SqlParameter("@Peso", spedizione.Peso));
            cmd.Parameters.Add(new SqlParameter("@CittaDestinataria", spedizione.CittaDestinataria));
            cmd.Parameters.Add(new SqlParameter("@IndirizzoDestinatario", spedizione.IndirizzoDestinatario));
            cmd.Parameters.Add(new SqlParameter("@NominativoDestinatario", spedizione.NominativoDestinatario));
            cmd.Parameters.Add(new SqlParameter("@Costo", spedizione.Costo));
            cmd.Parameters.Add(new SqlParameter("@DataConsegnaPrevista", spedizione.DataConsegnaPrevista));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
