using System;

namespace DevNots.Domain
{
    public class User: AggregateRoot, IUserDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }

        public User(string email, string username, string password)
        {
            Email = email;
            Username = username;
            Password = password;
        }

    }
}
