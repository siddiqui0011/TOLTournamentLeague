﻿using TOLTournamentLeague.DOM;

namespace TOLTournamentLeague.UserRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
    }
}
