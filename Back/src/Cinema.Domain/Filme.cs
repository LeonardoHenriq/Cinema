using System;

namespace Cinema.Domain
{
    public class Filme
    {
        public int Id { get; set; }
        public string ImagemURL { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public TimeSpan Duracao { get; set; }
    }
}