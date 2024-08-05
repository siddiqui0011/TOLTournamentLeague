using System.Data;
using System.Data.SqlClient;
using TOLTournamentLeague.DOM;

namespace TOLTournamentLeague.TOLRegistrationRepository
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly string _connectionString;

        public RegistrationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        //this code is created by sqlquery

        //public async Task<IEnumerable<TOLRegistration>> GetRegistrationsAsync()//this code is create by sqlquery
        //{
        //    List<TOLRegistration> registrations = new List<TOLRegistration>();

        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync();

        //        string sqlQuery = "SELECT Id, FullName, EmailId, Mobile, LinkedInUrl FROM Registrations";

        //        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
        //        {
        //            using (SqlDataReader reader = await command.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    registrations.Add(new TOLRegistration
        //                    {
        //                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //                        FullName = reader.GetString(reader.GetOrdinal("FullName")),
        //                        EmailId = reader.GetString(reader.GetOrdinal("EmailId")),
        //                        Mobile = reader.GetString(reader.GetOrdinal("Mobile")),
        //                        LinkedInUrl = reader.GetString(reader.GetOrdinal("LinkedInUrl"))
        //                    });
        //                }
        //            }
        //        }
        //    }

        //    return registrations;
        //}

        //public async Task<TOLRegistration> GetRegistrationByIdAsync(int id)//this code is create by sqlquery
        //{
        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync();

        //        string sqlQuery = "SELECT Id, FullName, EmailId, Mobile, LinkedInUrl FROM Registrations WHERE Id = @id";

        //        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
        //        {
        //            command.Parameters.AddWithValue("@id", id);

        //            using (SqlDataReader reader = await command.ExecuteReaderAsync())
        //            {
        //                if (await reader.ReadAsync())
        //                {
        //                    return new TOLRegistration
        //                    {
        //                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //                        FullName = reader.GetString(reader.GetOrdinal("FullName")),
        //                        EmailId = reader.GetString(reader.GetOrdinal("EmailId")),
        //                        Mobile = reader.GetString(reader.GetOrdinal("Mobile")),
        //                        LinkedInUrl = reader.GetString(reader.GetOrdinal("LinkedInUrl"))
        //                    };
        //                }
        //            }
        //        }
        //    }

        //    return null;
        //}

        ////public async Task AddRegistrationAsync(TOLRegistration registration)
        ////{
        ////    using (SqlConnection connection = new SqlConnection(_connectionString))
        ////    {
        ////        await connection.OpenAsync();

        ////        string sqlQuery = "INSERT INTO Registrations (FullName, EmailId, Mobile, LinkedInUrl) VALUES (@FullName, @EmailId, @Mobile, @LinkedInUrl)";

        ////        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
        ////        {
        ////            command.Parameters.AddWithValue("@FullName", registration.FullName);
        ////            command.Parameters.AddWithValue("@EmailId", registration.EmailId);
        ////            command.Parameters.AddWithValue("@Mobile", registration.Mobile);
        ////            command.Parameters.AddWithValue("@LinkedInUrl", registration.LinkedInUrl);

        ////            await command.ExecuteNonQueryAsync();
        ////        }
        ////    }
        ////}

        //public async Task AddRegistrationAsync(TOLRegistration registration)//this code is create by sqlquery
        //{
        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync();

        //        // Check if LinkedInUrl already exists
        //        string checkExistingQuery = "SELECT COUNT(*) FROM Registrations WHERE LinkedInUrl = @LinkedInUrl";
        //        using (SqlCommand checkExistingCommand = new SqlCommand(checkExistingQuery, connection))
        //        {
        //            checkExistingCommand.Parameters.AddWithValue("@LinkedInUrl", registration.LinkedInUrl);
        //            int count = (int)await checkExistingCommand.ExecuteScalarAsync();

        //            if (count > 0)
        //            {
        //                throw new InvalidOperationException("LinkedIn URL already exists.");
        //            }
        //        }

        //        // If LinkedInUrl does not exist, insert new registration
        //        string insertQuery = "INSERT INTO Registrations (FullName, EmailId, Mobile, LinkedInUrl) " +
        //                             "VALUES (@FullName, @EmailId, @Mobile, @LinkedInUrl)";
        //        using (SqlCommand command = new SqlCommand(insertQuery, connection))
        //        {
        //            command.Parameters.AddWithValue("@FullName", registration.FullName);
        //            command.Parameters.AddWithValue("@EmailId", registration.EmailId);
        //            command.Parameters.AddWithValue("@Mobile", registration.Mobile);
        //            command.Parameters.AddWithValue("@LinkedInUrl", registration.LinkedInUrl);

        //            await command.ExecuteNonQueryAsync();
        //        }
        //        await connection.CloseAsync();
        //    }
        //}


        //public async Task UpdateRegistrationAsync(TOLRegistration registration)//this code is create by sqlquery
        //{
        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync();

        //        string sqlQuery = "UPDATE Registrations SET FullName = @FullName, EmailId = @EmailId, Mobile = @Mobile, LinkedInUrl = @LinkedInUrl WHERE Id = @Id";

        //        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
        //        {
        //            command.Parameters.AddWithValue("@Id", registration.Id);
        //            command.Parameters.AddWithValue("@FullName", registration.FullName);
        //            command.Parameters.AddWithValue("@EmailId", registration.EmailId);
        //            command.Parameters.AddWithValue("@Mobile", registration.Mobile);
        //            command.Parameters.AddWithValue("@LinkedInUrl", registration.LinkedInUrl);

        //            await command.ExecuteNonQueryAsync();
        //        }
        //        await connection.CloseAsync();
        //    }
        //}

        //public async Task DeleteRegistrationAsync(int id)//this code is create by sqlquery
        //{
        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync();

        //        string sqlQuery = "DELETE FROM Registrations WHERE Id = @id";

        //        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
        //        {
        //            command.Parameters.AddWithValue("@id", id);
        //            await command.ExecuteNonQueryAsync();
        //        }
        //    }
        //}

        public async Task<IEnumerable<TOLRegistration>> GetRegistrationsAsync()
        {
            List<TOLRegistration> registrations = new List<TOLRegistration>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("GetRegistrations", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            registrations.Add(new TOLRegistration
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                                EmailId = reader.GetString(reader.GetOrdinal("EmailId")),
                                Mobile = reader.GetInt64(reader.GetOrdinal("Mobile")),
                                LinkedInUrl = reader.GetString(reader.GetOrdinal("LinkedInUrl"))
                            });
                        }
                    }
                }
            }

            return registrations;
        }

        public async Task<TOLRegistration> GetRegistrationByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("GetRegistrationById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new TOLRegistration
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                                EmailId = reader.GetString(reader.GetOrdinal("EmailId")),
                                Mobile = reader.GetInt64(reader.GetOrdinal("Mobile")),
                                LinkedInUrl = reader.GetString(reader.GetOrdinal("LinkedInUrl"))
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task AddRegistrationAsync(TOLRegistration registration)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("AddRegistration", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FullName", registration.FullName);
                    command.Parameters.AddWithValue("@EmailId", registration.EmailId);
                    command.Parameters.AddWithValue("@Mobile", registration.Mobile);
                    command.Parameters.AddWithValue("@LinkedInUrl", registration.LinkedInUrl);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateRegistrationAsync(TOLRegistration registration)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("UpdateRegistration", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", registration.Id);
                    command.Parameters.AddWithValue("@FullName", registration.FullName);
                    command.Parameters.AddWithValue("@EmailId", registration.EmailId);
                    command.Parameters.AddWithValue("@Mobile", registration.Mobile);
                    command.Parameters.AddWithValue("@LinkedInUrl", registration.LinkedInUrl);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteRegistrationAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("DeleteRegistration", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
