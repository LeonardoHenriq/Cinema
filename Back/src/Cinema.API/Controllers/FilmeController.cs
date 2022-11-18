using Cinema.Domain;
using Microsoft.AspNetCore.Mvc;
using Cinema.Application.Contratos;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Cinema.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmeController : ControllerBase
    {
        private readonly IFilmeService _filmeService;

        public FilmeController(IFilmeService filmeService)
        {
            this._filmeService = filmeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var filmes = await _filmeService.GetAllFilmesAsync();
                if (filmes == null) return NotFound("Nenhum filme encontrado.");

                return Ok(filmes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar filmes: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var filme = await _filmeService.GetFilmesByIdAsync(id);
                if (filme == null) return NotFound("Nenhum filme encontrado.");

                return Ok(filme);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar filme: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Filme model)
        {
            try
            {
                var filme = await _filmeService.AddFilme(model);
                if (filme == null) return BadRequest("Erro ao tentar adicionar o filme.");

                return Ok(filme);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar filme: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Filme model)
        {
            try
            {
                var filme = await _filmeService.UpdateFilme(id, model);
                if (filme == null) return BadRequest("Erro ao tentar atualizar o filme.");

                return Ok(filme);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar filme: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _filmeService.DeleteFilme(id)) return Ok("Excluido");
                else return BadRequest("Filme não foi excluido");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar excluir filme: {ex.Message}");
            }
        }
    }
}
