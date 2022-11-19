using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Domain.Enum;
using Microsoft.AspNetCore.Identity;

namespace Cinema.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public string NomeCompleto { get; set; }
        public string ImagemURL { get; set; }
        public Funcao funcao { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}