using Cinema.Domain;
using Cinema.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Testes.Mock
{
    public class SessaoMock : ISessaoPersist
    {
        Sessao sessao;
        public async Task<Sessao[]> GetAllSessoesAsync(bool includefilmeandsala = false)
        {
            return GetSessao();
        }

        public async Task<TimeSpan> GetDuracaoFilmeAsync(int filmeId)
        {

            return filmeId != 1 ? new TimeSpan() : TimeSpan.Parse("02:00");
        }

        public async Task<bool> GetSessoesByFilmeAsync(int filmeId)
        {
            return true;
        }

        public async Task<Sessao> GetSessoesByIdAsync(int sessaoId, bool includefilmeandsala = false)
        {
            var sessoes = GetSessao();
            foreach (var s in sessoes)
            {
                if (s.Id == sessaoId)
                    sessao = s;
            }

            return sessao;
        }

        public async Task<bool> SalaAvailableAsync(int salaId, DateTime inicial, DateTime final)
        {
            return true;
        }

        public async Task<List<Sala>> SalaIsUsedAsync(DateTime inicial, DateTime final)
        {
            return new List<Sala>() { new Sala() {Id=1,Nome="sala 1", QuantidadeAssentos = 100 } };
        }

        public Sessao[] GetSessao()
        {
            var sessao = new Sessao[2];

            sessao[0] = new Sessao()
            {
                Id = 1,
                DataSessao = DateTime.Today,
                HorarioInicial = DateTime.Now,
                HorarioFinal = DateTime.Now.AddHours(2),
                TipoAnimacao = Domain.Enum.TipoAnimacao.TresD,
                TipoAudio = Domain.Enum.TipoAudio.Dublado,
                ValorIngresso = 25,
                FilmeId = 1,
                SalaId = 2,
                sala = new Sala(),
                filme = new Filme()
            };
            sessao[1] = new Sessao()
            {
                Id = 2,
                DataSessao = DateTime.Today,
                HorarioInicial = DateTime.Now,
                HorarioFinal = DateTime.Now.AddHours(2),
                TipoAnimacao = Domain.Enum.TipoAnimacao.DoisD,
                TipoAudio = Domain.Enum.TipoAudio.Original,
                ValorIngresso = 30,
                FilmeId = 2,
                SalaId = 1,
                sala = new Sala(),
                filme = new Filme()
            };
            return sessao;
        }
    }
}
