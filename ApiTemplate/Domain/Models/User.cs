using System;

namespace ApiTemplate.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public User()
        {
        }

        public User(string name, string email, string role)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Role = role;
        }

        public void Update(string name, string email, string role)
        {
            Name = name;
            Email = email;
            Role = role;
        }
    }
}