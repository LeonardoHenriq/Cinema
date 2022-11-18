using System.Threading.Tasks;
using Cinema.Application.Dtos;

namespace Cinema.Application.Contratos
{
    public interface IFilmeService
    {
        Task<FilmeDto> AddFilme(FilmeDto model);
        Task<FilmeDto> UpdateFilme(int filmeId,FilmeDto model);
        Task<bool> DeleteFilme(int filmeId);
        Task<FilmeDto[]> GetAllFilmesAsync();
        Task<FilmeDto[]> GetAllFilmesByTituloAsync(string titulo);
        Task<FilmeDto> GetFilmesByIdAsync(int filmeId);
    }
}