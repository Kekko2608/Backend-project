using Progetto_Albergo.Models;
using System.Data.SqlClient;

namespace Progetto_Albergo.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _connectionString;
        private const string LOGIN_COMMAND = "SELECT IdUtente, Username FROM Utente WHERE Username = @Username AND Password = @Password";
        private const string REGISTER_COMMAND = "INSERT INTO Utente (Username, Password) VALUES (@Username, @Password); SELECT SCOPE_IDENTITY();";

        public AuthService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB");
        }

        public Utente Login(string username, string password)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(LOGIN_COMMAND, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var utente = new Utente
                                {
                                    IdUtente = reader.GetInt32(0),
                                    Username = reader.GetString(1),
                                    Password = password
                                };
                                reader.Close();
                                return utente;
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public Utente Register(string username, string password)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(REGISTER_COMMAND, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password); 
                        var utenteId = Convert.ToInt32(command.ExecuteScalar());
                        return new Utente
                        {
                            IdUtente = utenteId,
                            Username = username,
                            Password = password
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Registration failed: " + ex.Message);
            }
        }


    }
}

