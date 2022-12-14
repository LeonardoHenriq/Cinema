using Cinema.Application.Contratos;
using Cinema.Application.Dtos;
using Cinema.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Cinema.API.Controllers
{
    [Authorize(Roles ="Gerente")]
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
                var sessoes = await _sessaoService.GetAllSessoesAsync(true);
                if (sessoes == null) return NoContent();

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
                var sessao = await _sessaoService.GetSessoesByIdAsync(id, true);
                if (sessao == null) return NoContent();

                return Ok(sessao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar sessões: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(SessaoDto model)
        {
            try
            {
                var sessao = await _sessaoService.AddSessao(model);
                if (sessao == null) return BadRequest("Erro ao tentar adicionar uma sessão.");

                return Ok(sessao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar uma sessão: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _sessaoService.DeleteSessao(id);

                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar excluir sessão: {ex.Message}");
            }
        }
        [HttpGet("salas-available")]
        public async Task<IActionResult> SalasAvailable(DateTime dataSessao, string horarioInicial,int idfilme)
        {
            try
            {
                var duracao = await _sessaoService.GetDuracaoFilme(idfilme);
                if (string.IsNullOrEmpty(duracao))
                   throw new Exception("Erro ao tentar recuperar duração do filme");

                TimeSpan auxini;
                TimeSpan.TryParse(horarioInicial, out auxini);

                if (auxini == new TimeSpan())
                    throw new Exception("Horario Inicial Invalido");

                TimeSpan auxduracao; 
                TimeSpan.TryParse(duracao, out auxduracao);

                if (auxduracao == new TimeSpan())
                    throw new Exception("Duração do filme invalido");

                var inicial = dataSessao.AddTicks(auxini.Ticks);
                var final = inicial.AddTicks(auxduracao.Ticks);

                if (inicial == new DateTime() || final == new DateTime())
                    throw new Exception("Data inicial ou final invalidas");


                var result = await _sessaoService.GetSalasDisponiveisAsync(inicial, final);

                if (result == null) return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro! : {ex.Message}");
            }
        }

    }
}
