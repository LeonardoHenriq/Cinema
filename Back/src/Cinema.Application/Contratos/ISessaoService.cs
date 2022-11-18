using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Domain;

namespace Cinema.Application.Contratos
{
    public interface ISessaoService
    {
        Task<Sessao> AddSessao(Sessao model);
        Task<bool> DeleteSessao(int sessaoId);
        Task<Sessao[]> GetAllSessoesAsync();
        Task<Sessao> GetSessoesByIdAsync(int sessaoId);
        Task<Sala[]> GetSalasDisponiveisAsync(DateTime inicial, DateTime final);
        Task<string> GetDuracaoFilme(int filmeId);
    }
}