using System;
using System.Threading.Tasks;
using Cinema.Domain;

namespace Cinema.Persistence.Contratos
{
    public interface ISessaoPersist
    {
        Task<Sessao[]> GetAllSessoesAsync(bool includefilmeandsala = false);
        Task<Sessao> GetSessoesByIdAsync(int sessaoId, bool includefilmeandsala = false);
        Task<bool> GetSessoesByFilmeAsync(int filmeId);
        Task<Sala[]> GetSalasDisponiveisAsync(DateTime inicial, DateTime final);
        Task<bool> SalaAvailable(int salaId, DateTime inicial, DateTime final);
        Task<TimeSpan> GetDuracaoFilme(int filmeId);
    }
}