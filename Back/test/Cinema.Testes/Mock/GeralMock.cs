using Cinema.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Testes.Mock
{
    public class GeralMock : IGeralPersist
    {
        public void Add<T>(T entity) where T : class
        {
            
        }

        public void Delete<T>(T entity) where T : class
        {
            
        }

        public void DeleteRange<T>(T[] entity) where T : class
        {
            
        }

        public async Task<bool> SaveChangesAsync()
        {
            return true;
        }

        public void Update<T>(T entity) where T : class
        {
            
        }
    }
}
