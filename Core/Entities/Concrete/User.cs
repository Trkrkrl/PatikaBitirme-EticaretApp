using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class User:IEntity
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte[] passwordSalt { get; set; }
        public byte[] passwordHash { get; set; }
        public string Status { get; set; } = "active";
        public int FailedRecentLoginAttempts { get; set; }
        public DateTime EndOfSuspension { get; set; } 
    }
}
