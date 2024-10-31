using Domain.Contracts;
namespace Domain.Entities
{
    public class User : BaseEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Access { get; set; }
        public List<Transfer>? Transfers { get; set; } = new List<Transfer>();


        public User UpdateProfile(string? firstName, string? lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            return this;
        }

        public User UpdateAccessRole(string role, string access)
        {
            Role = role;
            Access = access;
            return this;
        }

        public User UpdateCredentials(string username, string password)
        {
            Username = username;
            Password = password;
            return this;
        }
    }
}
