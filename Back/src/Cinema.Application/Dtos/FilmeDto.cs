using System.ComponentModel.DataAnnotations;

namespace Cinema.Application.Dtos
{
    public class FilmeDto
    {
        public int Id { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", 
            ErrorMessage = "Não é uma imagem válida. (gif,jpg,jpeg,bmp ou png)")]
        public string ImagemURL { get; set; }

        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Duracao { get; set; }
    }
}