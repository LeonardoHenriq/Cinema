using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cinema.Application.Contratos;
using Cinema.Application.Dtos;
using Cinema.Domain;
using Cinema.Persistence;
using Cinema.Persistence.Contratos;
using Cinema.Persistence.Migrations;

namespace Cinema.Application
{
    public class SessaoService : ISessaoService
    {
        private readonly ISessaoPersist _sessaoPersist;
        private readonly IGeralPersist _geralPersist;
        private readonly IMapper _mapper;

        public SessaoService(ISessaoPersist sessaoPersist, IGeralPersist geralPersist,IMapper mapper)
        {
            _sessaoPersist = sessaoPersist;
            _geralPersist = geralPersist;
            _mapper = mapper;
        }
        public async Task<SessaoDto> AddSessao(SessaoDto model)
        {
            try
            {
                var sessao = _mapper.Map<Sessao>(model);
                var duracao = await _sessaoPersist.GetDuracaoFilme(sessao.FilmeId);

                if(duracao == new TimeSpan())
                    throw new Exception($"Não foi possível encontrar a duração do filme");


                sessao.HorarioFinal = sessao.HorarioInicial.AddTicks(duracao.Ticks);

                if (await _sessaoPersist.SalaAvailable(sessao.SalaId, sessao.HorarioInicial, sessao.HorarioFinal))
                    throw new Exception($"Já existe um cadastro com os mesmos horarios para a sala informada");

                _geralPersist.Add<Sessao>(sessao);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var sessaoRetorno = await _sessaoPersist.GetSessoesByIdAsync(sessao.Id);

                    return _mapper.Map<SessaoDto>(sessaoRetorno);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteSessao(int sessaoId)
        {
            try
            {
                var sessao = await _sessaoPersist.GetSessoesByIdAsync(sessaoId);
                if (sessao == null) throw new Exception("Sess�o para exclus�o n�o encontrada.");

                var test = sessao.DataSessao - DateTime.Today;

                if (test.TotalDays < 10) throw new Exception("Erro, sess�o s� pode ser excluida se faltar 10 ou mais para ocorrer.");

                _geralPersist.Delete<Sessao>(sessao);
                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<SessaoDto[]> GetAllSessoesAsync(bool includefilmeandsala = false)
        {
            try
            {
                var sessoes = await _sessaoPersist.GetAllSessoesAsync(includefilmeandsala);
                if (sessoes == null && sessoes.Any()) return null;

                var resultado = _mapper.Map<SessaoDto[]>(sessoes);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<SessaoDto> GetSessoesByIdAsync(int sessaoId, bool includefilmeandsala = false)
        {
            try
            {
                var sessao = await _sessaoPersist.GetSessoesByIdAsync(sessaoId,includefilmeandsala);
                if (sessao == null) return null;

                var resultado = _mapper.Map<SessaoDto>(sessao);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<SalaDto[]> GetSalasDisponiveisAsync(DateTime inicial, DateTime final)
        {
            try
            {
                var salas = await _sessaoPersist.GetSalasDisponiveisAsync(inicial, final);
                if (salas == null && salas.Any()) throw new Exception("N�o existem salas dispon�veis para este hor�rio");

                var resultado = _mapper.Map<SalaDto[]>(salas);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> GetDuracaoFilme(int filmeId)
        {
            try
            {
                var duracao = await _sessaoPersist.GetDuracaoFilme(filmeId);
                if (duracao == new TimeSpan()) throw new Exception("N�o foi poss�vel recuperar a dura��o do filme");

                return duracao.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}