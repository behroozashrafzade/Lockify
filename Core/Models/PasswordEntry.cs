using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class PasswordEntry
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Username { get; set; }
        public required string Password { get; set; }
 
        public  string Email { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public required string PasswordHash { get; set; }

        public required string Website { get; set; }


        public required DateTime CreatedAt { get; set; } = DateTime.Now;

        public required DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
