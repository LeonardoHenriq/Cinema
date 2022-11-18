using System.Threading.Tasks;
using Cinema.Application.Dtos;

namespace Cinema.Application.Contratos
{
    public interface ISalaService
    {
        Task<SalaDto[]> GetAllSalasAsync();
        Task<SalaDto> GetSalasByIdAsync(int salaId);
    }
}