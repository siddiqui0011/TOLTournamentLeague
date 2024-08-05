using System.Data.SqlClient;
using System.Data;
using TOLTournamentLeague.DOM;

namespace TOLTournamentLeague.UserRepository
{
    public class UserRepository:IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration configuration)
        {

            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            User user = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("GetUserByUsername", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", username);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            user = new User
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                                PasswordHash = (byte[])reader["PasswordHash"],
                                PasswordSalt = (byte[])reader["PasswordSalt"],
                                //RoleId = reader.GetInt32(reader.GetOrdinal("RoleId"))
                            };
                        }
                    }
                }
            }

            return user;
        }

        public async Task AddUserAsync(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("AddUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    command.Parameters.AddWithValue("@PasswordSalt", user.PasswordSalt);
                    //command.Parameters.AddWithValue("@RoleId", user.RoleId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
