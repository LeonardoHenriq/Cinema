using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Application.Contratos;
using Cinema.Domain;
using Cinema.Persistence.Contratos;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Cinema.Application
{
    public class FilmeService : IFilmeService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IFilmePersist _filmePersist;
        private readonly ISessaoPersist _sessaoPersist;

        public FilmeService(IGeralPersist geralPersist, IFilmePersist filmePersist, ISessaoPersist sessaoPersist)
        {
            _geralPersist = geralPersist;
            _filmePersist = filmePersist;
            _sessaoPersist = sessaoPersist;
        }
        public async Task<Filme> AddFilme(Filme model)
        {
            try
            {
                var verifica = await _filmePersist.GetAllFilmesByTituloAsync(model.Titulo);
                if (verifica != null && verifica.Any())
                    throw new Exception("Já existe um filme com o mesmo titulo.");

                _geralPersist.Add<Filme>(model);

                if (await _geralPersist.SaveChangesAsync())
                    return await _filmePersist.GetFilmesByIdAsync(model.Id);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Filme> UpdateFilme(int filmeId, Filme model)
        {
            try
            {
                var verifica = await _filmePersist.GetAllFilmesByTituloAsync(model.Titulo);
                if (verifica != null && verifica.Any())
                    throw new Exception("Já existe um filme com o mesmo titulo.");

                var filme = await _filmePersist.GetFilmesByIdAsync(filmeId);
                if (filme == null) return null;

                model.Id = filme.Id;

                _geralPersist.Update(model);

                if (await _geralPersist.SaveChangesAsync())
                    return await _filmePersist.GetFilmesByIdAsync(model.Id);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteFilme(int filmeId)
        {
            try
            {
                if (await _sessaoPersist.GetSessoesByFilmeAsync(filmeId))
                    throw new Exception("Erro ao excluir, filme possuí uma Sessão.");

                var filme = await _filmePersist.GetFilmesByIdAsync(filmeId);
                if (filme == null) throw new Exception("Filme para delete não encontrado.");


                _geralPersist.Delete<Filme>(filme);
                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Filme[]> GetAllFilmesAsync()
        {
            try
            {
                var filmes = await _filmePersist.GetAllFilmesAsync();
                if (filmes == null && filmes.Any()) return null;

                return filmes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Filme[]> GetAllFilmesByTituloAsync(string titulo)
        {
            try
            {
                var filmes = await _filmePersist.GetAllFilmesByTituloAsync(titulo);
                if (filmes == null && filmes.Any()) return null;

                return filmes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Filme> GetFilmesByIdAsync(int filmeId)
        {
            try
            {
                var filmes = await _filmePersist.GetFilmesByIdAsync(filmeId);
                if (filmes == null) return null;

                return filmes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}