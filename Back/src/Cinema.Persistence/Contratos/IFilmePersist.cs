using System;
using System.Threading.Tasks;
using Cinema.Domain;

namespace Cinema.Persistence.Contratos
{
    public interface IFilmePersist
    {
        Task<Filme[]> GetAllFilmesByTituloAsync(string titulo);
        Task<Filme[]> GetAllFilmesAsync();
        Task<Filme> GetFilmesByIdAsync(int filmeId);
    }
}