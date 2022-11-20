using System.Threading.Tasks;
using Cinema.Application.Dtos;

namespace Cinema.Application.Contratos
{
    public interface IFilmeService
    {
        Task<FilmeDto> AddFilme(FilmeDto model);
        Task<FilmeUpdateDto> UpdateFilme(int filmeId, FilmeUpdateDto model);
        Task<bool> DeleteFilme(int filmeId);
        Task<FilmeDto[]> GetAllFilmesAsync();
        Task<FilmeDto[]> GetAllFilmesByTituloAsync(string titulo);
        Task<FilmeDto> GetFilmesByIdAsync(int filmeId);
        Task<FilmeUpdateDto> GetFilmesIdAsync(int filmeId);
    }
}