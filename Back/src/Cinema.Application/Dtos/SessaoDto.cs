using System.ComponentModel.DataAnnotations;
using System;
using Cinema.Domain;

namespace Cinema.Application.Dtos
{
    public class SessaoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string DataSessao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string HorarioInicial { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string HorarioFinal { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public decimal ValorIngresso { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string TipoAnimacao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string TipoAudio { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public int FilmeId { get; set; }

        public FilmeDto filme { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public int SalaId { get; set; }
        public SalaDto sala { get; set; }
    }
    public enum TipoAnimacao
    {
        doisD,
        tresD
    }
    public enum TipoAudio
    {
        original,
        dublado
    }
}