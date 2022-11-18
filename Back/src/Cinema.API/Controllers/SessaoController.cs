using Cinema.Application;
using Cinema.Application.Contratos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cinema.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessaoController : ControllerBase
    {
        private readonly ISessaoService _sessaoService;

        public SessaoController(ISessaoService sessaoService)
        {
            _sessaoService = sessaoService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var sessoes = await _sessaoService.GetAllSessoesAsync();
                if (sessoes == null) return NotFound("Nenhuma sessão encontrada");

                return Ok(sessoes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar sessões: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var sessao = await _sessaoService.GetSessoesByIdAsync(id);
                if (sessao == null) return NotFound("Nenhuma sessão encontrado.");

                return Ok(sessao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar sessões: {ex.Message}");
            }
        }

        [HttpGet("duracao-filme/{id}")]
        public async Task<IActionResult> GetByDuracaofilme(int id)
        {
            try
            {
                var duracao = await _sessaoService.GetDuracaoFilme(id);
                if (TimeSpan.Parse(duracao) == new TimeSpan()) return NotFound("A duração do filme não foi encontrada.");

                return Ok(duracao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar duração do filme: {ex.Message}");
            }
        }

    }
}
