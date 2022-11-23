using AutoMapper;
using Cinema.Application;
using Cinema.Application.Dtos;
using System;
using Xunit;
using Cinema.Testes.Mock;

namespace Cinema.Testes.Services
{
    public class FilmeServiceTests : Profile
    {
        private FilmeService filmeService;
        
        public FilmeServiceTests()
        {
            var mapper = new AutoMocker();
            filmeService = new FilmeService(new GeralMock(), new FilmeMock(), new SessaoMock(), mapper.LoadMapper());
        }

        [Fact]
        public void AddFilme_TituloRepetido()
        {
            var result = Assert.ThrowsAsync<Exception>(() => filmeService.AddFilme(new FilmeDto() { Titulo = "Senhor dos Aneis" }));

            Assert.Equal("Já existe um filme com o mesmo titulo.", result.Result.Message);
        }

        [Fact]
        public async void DeleteFilme_FilmeComSesao()
        {
            var retorno = await filmeService.DeleteFilme(0);

            Assert.Equal("Erro ao excluir, filme possuí uma Sessão.", retorno);
        }
    }
}
