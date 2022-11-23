using AutoMapper;
using Cinema.Application;
using Cinema.Application.Dtos;
using Cinema.Persistence.Contratos;
using Cinema.Testes.Mock;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cinema.Testes.Services
{
    public class SessaoServiceTests
    {
        private SessaoService sessaoService;
        public SessaoServiceTests()
        {
            var mapper = new AutoMocker();
            sessaoService = new SessaoService(new SessaoMock(),
                                              new SalaMock(),
                                              new GeralMock(),
                                              mapper.LoadMapper());
        }

        [Fact]
        public async void AddSessao_NaoAchouDuracaoFilme()
        {
            var result = Assert.ThrowsAsync<Exception>(() => sessaoService.AddSessao(new SessaoDto {FilmeId = 0}));

            Assert.Equal("Não foi possível encontrar a duração do filme", result.Result.Message);
        }
        [Fact]
        public async void AddSessao_PossuiSalacomMesmoHorario()
        {
            var result = Assert.ThrowsAsync<Exception>(() => sessaoService.AddSessao(new SessaoDto {HorarioInicial = DateTime.Now, FilmeId = 1}));

            Assert.Equal("Já existe um cadastro com os mesmos horarios para a sala informada", result.Result.Message);
        }
        
        [Fact]
        public async void DeletesSessao_10diasparaOcorrer()
        {
            var retorno = await sessaoService.DeleteSessao(1);
            Assert.Equal("Erro, sessao so pode ser excluida se faltar 10 ou mais para ocorrer.", retorno);
        }

        [Fact]
        public async void DeleteSessao_FilmeNaoEncontrado()
        {
            var retorno = await sessaoService.DeleteSessao(0);

            Assert.Equal("Sessao para exclusao nao encontrada.", retorno);
        }
        

    }
}
