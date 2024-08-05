namespace TOLTournamentLeague.DOM
{
    public class DTO
    {
        public class UserSignupDto
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }

        public class UserLoginDto
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

    }
}
