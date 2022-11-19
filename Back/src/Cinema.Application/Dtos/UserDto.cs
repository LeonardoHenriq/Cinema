using Cinema.Domain.Enum;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Dtos
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Funcao funcao { get; set; }
        public string NomeCompleto { get; set; }
    }
}
