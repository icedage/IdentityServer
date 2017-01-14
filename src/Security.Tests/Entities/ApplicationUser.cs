using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Tests.Entities
{
    public class ApplicationUser 
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public string Role { get; set; }

    }
}
