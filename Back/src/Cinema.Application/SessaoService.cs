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
        private readonly ISalaPersist _salaPersist;
        private readonly IGeralPersist _geralPersist;
        private readonly IMapper _mapper;

        public SessaoService(ISessaoPersist sessaoPersist,ISalaPersist salaPersist, IGeralPersist geralPersist, IMapper mapper)
        {
            _sessaoPersist = sessaoPersist;
            _salaPersist = salaPersist;
            _geralPersist = geralPersist;
            _mapper = mapper;
        }
        public async Task<SessaoDto> AddSessao(SessaoDto model)
        {
            try
            {
                var sessao = _mapper.Map<Sessao>(model);
                var duracao = await _sessaoPersist.GetDuracaoFilmeAsync(sessao.FilmeId);

                if (duracao == new TimeSpan())
                    throw new Exception($"Não foi possível encontrar a duração do filme");


                sessao.HorarioFinal = sessao.HorarioInicial.AddTicks(duracao.Ticks);

                if (await _sessaoPersist.SalaAvailableAsync(sessao.SalaId, sessao.HorarioInicial, sessao.HorarioFinal))
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
        public async Task<string> DeleteSessao(int sessaoId)
        {
            try
            {
                var sessao = await _sessaoPersist.GetSessoesByIdAsync(sessaoId);
                if (sessao == null) return "Sessao para exclusao nao encontrada.";

                var test = sessao.DataSessao - DateTime.Today;

                if (test.TotalDays < 10) return "Erro, sessao so pode ser excluida se faltar 10 ou mais para ocorrer.";

                _geralPersist.Delete<Sessao>(sessao);
                return await _geralPersist.SaveChangesAsync() ? "Excluido" : throw new Exception("Erro ao tentar Excluir"); 
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
                var sessao = await _sessaoPersist.GetSessoesByIdAsync(sessaoId, includefilmeandsala);
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
                var usadas = await _sessaoPersist.SalaIsUsedAsync(inicial, final);
                var todas = await _salaPersist.GetAllSalasAsync();

                var salasUsadas = _mapper.Map<List<SalaDto>>(usadas);
                var salas = _mapper.Map<List<SalaDto>>(todas);

                if (salas == null && salas.Any()) throw new Exception("Nao existem salas disponiveis para este horario");

                salasUsadas.ForEach(s => salas.RemoveAll(sa => sa.Id == s.Id));

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
                var duracao = await _sessaoPersist.GetDuracaoFilmeAsync(filmeId);
                if (duracao == new TimeSpan()) throw new Exception("Nao foi possivel recuperar a duracao do filme");

                return duracao.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}