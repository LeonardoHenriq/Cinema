using System.Threading.Tasks;
using Cinema.Persistence.Contratos;
using Cinema.Persistence.Contextos;

namespace Cinema.Persistence
{
 public class GeralPersist : IGeralPersist
    {
        private readonly CinemaContext _context;
        public GeralPersist(CinemaContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}