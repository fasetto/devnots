using System;
using DevNots.Domain;

namespace DevNots.Application.Contracts
{
    public class UserDto : IUserDetails
    {
        public string Id { get; internal set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
