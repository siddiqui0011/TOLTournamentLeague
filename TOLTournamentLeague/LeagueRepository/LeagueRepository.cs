using System.Data.SqlClient;
using System.Data;
using TOLTournamentLeague.DOM;

namespace TOLTournamentLeague.LeagueRepository
{
    public class LeagueRepository : ILeagueRepository
    {
        private readonly string _connectionString;
        
        public LeagueRepository(IConfiguration configuration)
        {
           
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        // THIS CODE IS CREATED WITH SQL QUERY
        //public async Task<TournamentLeague> GetActiveLeagueAsync()
        //{
        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync();

        //        string sqlQuery = "SELECT id,name AS Title, is_active FROM Leagues WHERE is_active =1;";
        //        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
        //        {
        //            using (SqlDataReader reader = await command.ExecuteReaderAsync())
        //            {
        //                if (await reader.ReadAsync())
        //                {
        //                    return new TournamentLeague
        //                    {
        //                        Id = reader.GetInt32(reader.GetOrdinal("id")),
        //                        Title = reader.GetString(reader.GetOrdinal("Title")),
        //                        Is_Active = reader.GetBoolean(reader.GetOrdinal("is_active"))
        //                    };
        //                }
        //            }
        //        }
        //    }
        //    return null;
        //}

        //public async Task<TournamentLeague> GetLeagueByIdAsync(int id)
        //{
        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync();

        //        string sqlQuery = "SELECT id,name AS Title, is_active FROM Leagues WHERE id = @id;";
        //        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
        //        {
        //            command.Parameters.AddWithValue("@id", id);
        //            using (SqlDataReader reader = await command.ExecuteReaderAsync())
        //            {
        //                if (await reader.ReadAsync())
        //                {
        //                    return new TournamentLeague
        //                    {
        //                        Id = reader.GetInt32(reader.GetOrdinal("id")),
        //                        Title = reader.GetString(reader.GetOrdinal("Title")),
        //                        Is_Active = reader.GetBoolean(reader.GetOrdinal("is_active"))
        //                    };
        //                }
        //            }
        //        }
        //    }
        //    return null;
        //}

        //public async Task ActivateLeagueAsync(int id)
        //{
        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync();

        //        // Deactivate all leagues
        //        string deactivateQuery = "UPDATE Leagues SET is_active = 0;";
        //        using (SqlCommand deactivateCommand = new SqlCommand(deactivateQuery, connection))
        //        {
        //            await deactivateCommand.ExecuteNonQueryAsync();
        //        }

        //        // Activate the selected league
        //        string activateQuery = "UPDATE Leagues SET is_active = 1 WHERE id = @id;";
        //        using (SqlCommand activateCommand = new SqlCommand(activateQuery, connection))
        //        {
        //            activateCommand.Parameters.AddWithValue("@id", id);
        //            await activateCommand.ExecuteNonQueryAsync();
        //        }
        //    }
        //}

        // THIS CODE IS CREATED WITH SQL QUERY

        public async Task<IEnumerable<TournamentLeague>> GetAllLeaguesAsync()//this code is created with storeprocedure
        {
            List<TournamentLeague> leagues = new List<TournamentLeague>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("GetAllLeagues", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            leagues.Add(new TournamentLeague
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Name")),
                                Is_Active = reader.GetBoolean(reader.GetOrdinal("Is_Active"))
                            });
                        }
                    }
                }
            }

            return leagues;
        }

        public async Task AddLeagueAsync(string name)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("AddLeague", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@name", name);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<TournamentLeague> GetActiveLeagueAsync()//this code is created with storeprocedure
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("GetActiveLeague", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new TournamentLeague
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Is_Active = reader.GetBoolean(reader.GetOrdinal("is_active"))
                            };
                        }
                    }
                }
            }
            return null;
        }

        public async Task<TournamentLeague> GetLeagueByIdAsync(int id)// this code is created with storeprocedure
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("GetLeagueById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new TournamentLeague
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Is_Active = reader.GetBoolean(reader.GetOrdinal("is_active"))
                            };
                        }
                    }
                }
            }
            return null;
        }

        public async Task ActivateLeagueAsync(int id)// this code is created with store procedure
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("ActivateLeague", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
