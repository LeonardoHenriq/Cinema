using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Application.Dtos;
using Cinema.Domain;

namespace Cinema.Application.Contratos
{
    public interface ISessaoService
    {
        Task<SessaoDto> AddSessao(SessaoDto model);
        Task<string> DeleteSessao(int sessaoId);
        Task<SessaoDto[]> GetAllSessoesAsync(bool includefilmeandsala = false);
        Task<SessaoDto> GetSessoesByIdAsync(int sessaoId, bool includefilmeandsala = false);
        Task<SalaDto[]> GetSalasDisponiveisAsync(DateTime inicial, DateTime final);
        Task<string> GetDuracaoFilme(int filmeId);
    }
}