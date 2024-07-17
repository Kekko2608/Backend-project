using S5.Models;
using System.Data.SqlClient;

namespace S5.Services
{
    public class AuthService : IAuthService
    {
        private string connectionString;

        private const string LOGIN_COMMAND = "SELECT Id, FriendlyName FROM Users WHERE Username = @user AND Password = @pass";
        private const string ROLES_COMMAND = "SELECT RoleName FROM Roles ro JOIN UserRoles ur ON ro.Id = ur.RoleId WHERE UserId = @id";

        public AuthService(IConfiguration config)
        {
            connectionString = config.GetConnectionString("AuthDb")!;
        }

        public ApplicationUser Login(string username, string password)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(LOGIN_COMMAND, conn);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pass", password);
                using var r = cmd.ExecuteReader();
                if (r.Read())
                {
                    var u = new ApplicationUser { Id = r.GetInt32(0), Password = password, Username = username, FriendlyName = r.GetString(1) };
                    r.Close();
                    using var roleCmd = new SqlCommand(ROLES_COMMAND, conn);
                    roleCmd.Parameters.AddWithValue("@id", u.Id);
                    using var re = roleCmd.ExecuteReader();
                    while (re.Read())
                    {
                        u.Roles.Add(re.GetString(0));
                    }
                    return u;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}
