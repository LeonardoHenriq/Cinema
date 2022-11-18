using System;
using System.Threading.Tasks;
using Cinema.Domain;

namespace Cinema.Persistence.Contratos
{
    public interface ISessaoPersist
    {
        Task<Sessao[]> GetAllSessoesAsync();
        Task<Sessao> GetSessoesByIdAsync(int sessaoId);
        Task<bool> GetSessoesByFilmeAsync(int filmeId);
        Task<Sala[]> GetSalasDisponiveisAsync(DateTime inicial, DateTime final);
        Task<string> GetDuracaoFilme(int filmeId);
    }
}