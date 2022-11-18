using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Application.Contratos;
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

        public SessaoService(ISessaoPersist sessaoPersist, IGeralPersist geralPersist)
        {
            _sessaoPersist = sessaoPersist;
            _geralPersist = geralPersist;
        }
        public async Task<Sessao> AddSessao(Sessao model)
        {
            try
            {
                var duracao = await _sessaoPersist.GetDuracaoFilme(model.FilmeId);

                if(string.IsNullOrEmpty(duracao))
                    throw new Exception($"Não foi possível encontrar a duração do filme");

                model.HorarioFinal = model.HorarioInicial.AddTicks(TimeSpan.Parse(duracao).Ticks);

                if (await _sessaoPersist.SalaAvailable(model.SalaId, model.HorarioInicial, model.HorarioFinal))
                    throw new Exception($"Já existe um cadastro com os mesmos horarios para a sala informada");

                _geralPersist.Add<Sessao>(model);

                if (await _geralPersist.SaveChangesAsync())
                    return await _sessaoPersist.GetSessoesByIdAsync(model.Id);

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
        public async Task<Sessao[]> GetAllSessoesAsync()
        {
            try
            {
                var sessoes = await _sessaoPersist.GetAllSessoesAsync();
                if (sessoes == null && sessoes.Any()) return null;

                return sessoes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Sessao> GetSessoesByIdAsync(int sessaoId)
        {
            try
            {
                var Sessao = await _sessaoPersist.GetSessoesByIdAsync(sessaoId);
                if (Sessao == null) return null;

                return Sessao;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Sala[]> GetSalasDisponiveisAsync(DateTime inicial, DateTime final)
        {
            try
            {
                var salas = await _sessaoPersist.GetSalasDisponiveisAsync(inicial, final);
                if (salas == null && salas.Any()) throw new Exception("N�o existem salas dispon�veis para este hor�rio");

                return salas;
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
                if (TimeSpan.Parse(duracao) == new TimeSpan()) throw new Exception("N�o foi poss�vel recuperar a dura��o do filme");

                return duracao;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}