using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.API.Models
{
    public class Filme
    {
        public int FilmeId { get; set; }
        public string ImagemURL { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Duracao { get; set; }
    }
}