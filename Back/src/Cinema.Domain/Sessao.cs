using System;

namespace Cinema.Domain
{
    public class Sessao
    {
        public int Id { get; set; }
        public DateTime DataSessao { get; set; }
        public DateTime HorarioInicial { get; set; }
        public DateTime HorarioFinal { get; set; }
        public decimal ValorIngresso { get; set; }
        public string TipoAnimacao { get; set; }
        public string TipoAudio { get; set; }
        public int FilmeId { get; set; }
        public Filme filme { get; set; }
        public int SalaId { get; set; }
        public Sala sala { get; set; }
    }
}