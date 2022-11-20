using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Dtos
{
    public class FilmeUpdateDto
    {
        public int Id { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", 
            ErrorMessage = "Não é uma imagem válida. (gif,jpg,jpeg,bmp ou png)")]
        public string ImagemURL { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Duracao { get; set; }
    }
}
