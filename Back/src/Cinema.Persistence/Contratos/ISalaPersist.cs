using System;
using System.Threading.Tasks;
using Cinema.Domain;

namespace Cinema.Persistence.Contratos
{
    public interface ISalaPersist
    {
        Task<Sala[]> GetAllSalasAsync();
        Task<Sala> GetSalasByIdAsync(int salaId);
    }
}