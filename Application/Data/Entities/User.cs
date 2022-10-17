using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string? PasswordHash { get; set; }
        public string? Email { get; set; }
        public DateTime? Created { get; set; }
    }
}
