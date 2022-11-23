using AutoMapper;
using Cinema.Application.Contratos;
using Cinema.Application.Dtos;
using Cinema.Domain;
using Cinema.Persistence.Contratos;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Testes.Mock
{
    public class FilmeMock : IFilmePersist
    {
        Filme filme;
        public async Task<Filme[]> GetAllFilmesAsync()
        {
            return GetFilmes();
        }

        public async Task<Filme[]> GetAllFilmesByTituloAsync(string titulo)
        {
            return GetFilmes();
        }

        public async Task<Filme> GetFilmesByIdAsync(int filmeId)
        {
            var filmes = GetFilmes();
            foreach (var f in filmes)
            {
                if (f.Id == filmeId)
                    filme = f;
            }

            return filme;
        }

        public Filme[] GetFilmes()
        {
            var filme = new Filme[2];
            filme[0] = new Filme() { Id = 1, Titulo = "Senhor dos Aneis", Descricao = "Alguma Descricao", Duracao = TimeSpan.Parse("02:00"), ImagemURL = "foto.png" };
            filme[1] = new Filme() { Id = 2, Titulo = "Senhor dos Aneis", Descricao = "Alguma Descricao", Duracao = TimeSpan.Parse("02:00"), ImagemURL = "foto.png" };

            return filme;
        }
    }
}
