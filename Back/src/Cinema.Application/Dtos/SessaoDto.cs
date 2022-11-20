using System.ComponentModel.DataAnnotations;
using System;
using Cinema.Domain;
using Cinema.Domain.Enum;

namespace Cinema.Application.Dtos
{
    public class SessaoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public DateTime DataSessao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public DateTime HorarioInicial { get; set; }

        public DateTime HorarioFinal { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public decimal ValorIngresso { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public TipoAnimacao TipoAnimacao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public TipoAudio TipoAudio { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public int FilmeId { get; set; }

        public FilmeDto filme { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public int SalaId { get; set; }
        public SalaDto sala { get; set; }
    }
}