using System.Linq;
using System.Threading.Tasks;
using Cinema.Domain;
using Microsoft.EntityFrameworkCore;
using Cinema.Persistence.Contratos;
using Cinema.Persistence.Contextos;

namespace Cinema.Persistence
{
 public class FilmePersist : IFilmePersist
    {
        private readonly CinemaContext _context;
        public FilmePersist(CinemaContext context)
        {
            _context = context;
        }
        public async Task<Filme[]> GetAllFilmesAsync()
        {
            IQueryable<Filme> query = _context.Filmes;
            query = query.AsNoTracking().OrderBy(f => f.Id);

            return await query.ToArrayAsync();
        }
        public async Task<Filme[]> GetAllFilmesByTituloAsync(string titulo)
        {
            IQueryable<Filme> query = _context.Filmes;

            query = query.AsNoTracking().OrderBy(f => f.Id).Where(f => f.Titulo.ToLower().Contains(titulo.ToLower()));

            return await query.ToArrayAsync();
        }
        public async Task<Filme> GetFilmesByIdAsync(int filmeId)
        {
            IQueryable<Filme> query = _context.Filmes;

            query = query.AsNoTracking().Where(f => f.Id == filmeId);

            return await query.FirstOrDefaultAsync();
        }
    }
}