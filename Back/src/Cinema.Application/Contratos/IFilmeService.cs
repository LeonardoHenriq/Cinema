using System.Threading.Tasks;
using Cinema.Domain;

namespace Cinema.Application.Contratos
{
 public interface IFilmeService
    {
        Task<Filme> AddFilme(Filme model);
        Task<Filme> UpdateFilme(int filmeId,Filme model);
        Task<bool> DeleteFilme(int filmeId);
        Task<Filme[]> GetAllFilmesAsync();
        Task<Filme[]> GetAllFilmesByTituloAsync(string titulo);
        Task<Filme> GetFilmesByIdAsync(int filmeId);
    }
}