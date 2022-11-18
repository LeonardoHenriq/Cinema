using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cinema.Application.Contratos;
using Cinema.Application.Dtos;
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
        private readonly IMapper _mapper;

        public FilmeService(IGeralPersist geralPersist, IFilmePersist filmePersist, ISessaoPersist sessaoPersist, IMapper mapper)
        {
            _geralPersist = geralPersist;
            _filmePersist = filmePersist;
            _sessaoPersist = sessaoPersist;
            _mapper = mapper;
        }
        public async Task<FilmeDto> AddFilme(FilmeDto model)
        {
            try
            {
                var filme = _mapper.Map<Filme>(model);

                var verifica = await _filmePersist.GetAllFilmesByTituloAsync(filme.Titulo);
                if (verifica != null && verifica.Any())
                    throw new Exception("Já existe um filme com o mesmo titulo.");

                _geralPersist.Add<Filme>(filme);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var filmeRetorno = await _filmePersist.GetFilmesByIdAsync(filme.Id);

                    return _mapper.Map<FilmeDto>(filmeRetorno);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<FilmeDto> UpdateFilme(int filmeId, FilmeDto model)
        {
            try
            {
                var verifica = await _filmePersist.GetAllFilmesByTituloAsync(model.Titulo);
                if (verifica != null && verifica.Any())
                    throw new Exception("Já existe um filme com o mesmo titulo.");

                var filme = await _filmePersist.GetFilmesByIdAsync(filmeId);
                if (filme == null) return null;

                model.Id = filme.Id;

                _mapper.Map(model, filme);

                _geralPersist.Update<Filme>(filme);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var filmeRetorno = await _filmePersist.GetFilmesByIdAsync(filme.Id);

                    return _mapper.Map<FilmeDto>(filmeRetorno);
                }

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

        public async Task<FilmeDto[]> GetAllFilmesAsync()
        {
            try
            {
                var filmes = await _filmePersist.GetAllFilmesAsync();
                if (filmes == null && filmes.Any()) return null;

                var resultado = _mapper.Map<FilmeDto[]>(filmes);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FilmeDto[]> GetAllFilmesByTituloAsync(string titulo)
        {
            try
            {
                var filmes = await _filmePersist.GetAllFilmesByTituloAsync(titulo);
                if (filmes == null && filmes.Any()) return null;

                var resultado = _mapper.Map<FilmeDto[]>(filmes);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FilmeDto> GetFilmesByIdAsync(int filmeId)
        {
            try
            {
                var filme = await _filmePersist.GetFilmesByIdAsync(filmeId);
                if (filme == null) return null;

                var resultado = _mapper.Map<FilmeDto>(filme);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}