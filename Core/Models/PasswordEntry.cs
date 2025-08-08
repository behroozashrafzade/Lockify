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
 
        public required string Email { get; set; }

        public required string PasswordHash { get; set; }

        public required string Website { get; set; }


        public required DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
