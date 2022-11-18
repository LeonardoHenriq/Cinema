using System.Linq;
using System.Threading.Tasks;
using Cinema.Domain;
using Microsoft.EntityFrameworkCore;
using Cinema.Persistence.Contratos;
using Cinema.Persistence.Contextos;

namespace Cinema.Persistence
{
 public class SalaPersist : ISalaPersist
    {
        private readonly CinemaContext _context;
        public SalaPersist(CinemaContext context)
        {
            _context = context;

        }
        public async Task<Sala[]> GetAllSalasAsync()
        {
            IQueryable<Sala> query = _context.Salas;
            query = query.AsNoTracking().OrderBy(s => s.Id);

            return await query.ToArrayAsync();
        }
        public async Task<Sala> GetSalasByIdAsync(int salaId)
        {
            IQueryable<Sala> query = _context.Salas;

            query = query.AsNoTracking().Where(s => s.Id == salaId);

            return await query.FirstOrDefaultAsync();
        }
    }
}