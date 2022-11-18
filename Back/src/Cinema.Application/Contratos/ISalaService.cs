using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Domain;

namespace Cinema.Application.Contratos
{
    public interface ISalaService
    {
        Task<Sala[]> GetAllSalasAsync();
        Task<Sala> GetSalasByIdAsync(int salaId);
    }
}