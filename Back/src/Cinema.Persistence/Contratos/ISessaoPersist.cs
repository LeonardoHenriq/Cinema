using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Domain;

namespace Cinema.Persistence.Contratos
{
    public interface ISessaoPersist
    {
        Task<Sessao[]> GetAllSessoesAsync(bool includefilmeandsala = false);
        Task<Sessao> GetSessoesByIdAsync(int sessaoId, bool includefilmeandsala = false);
        Task<bool> GetSessoesByFilmeAsync(int filmeId);
        Task<List<Sala>> SalaIsUsedAsync(DateTime inicial, DateTime final);
        Task<bool> SalaAvailableAsync(int salaId, DateTime inicial, DateTime final);
        Task<TimeSpan> GetDuracaoFilmeAsync(int filmeId);
    }
}