using System;
using Cinema.Domain;

namespace Cinema.API.Dtos
{
    public class SessaoDto
    {
        public int Id { get; set; }
        public string DataSessao { get; set; }
        public string HorarioInicial { get; set; }
        public string HorarioFinal { get; set; }
        public decimal ValorIngresso { get; set; }
        public string TipoAnimacao { get; set; }
        public string TipoAudio { get; set; }
        public int FilmeId { get; set; }
        //public Filme filme { get; set; }
        public int SalaId { get; set; }
        //public Sala sala { get; set; }
    }
}