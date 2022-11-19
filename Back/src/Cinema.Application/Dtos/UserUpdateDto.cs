using Cinema.Domain.Enum;
using Cinema.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Dtos
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NomeCompleto { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public Funcao Funcao { get; set; }  
    }
}
