using System;

namespace DevNots.Domain
{
    public interface IUserDetails
    {
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
        string Username { get; }
        string Password { get; }
        DateTime CreatedAt { get; }
    }
}
