using Cinema.API.Dtos;
using Cinema.Application;
using Cinema.Application.Contratos;
using Cinema.Domain;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

                var sessoesRetorno = new List<SessaoDto>();

                foreach (var sessao in sessoes)
                {
                    sessoesRetorno.Add(new SessaoDto()
                    {
                        Id = sessao.Id,
                        DataSessao = sessao.DataSessao.ToString(),
                        HorarioInicial = sessao.HorarioInicial.ToString(),
                        HorarioFinal = sessao.HorarioFinal.ToString(),
                        ValorIngresso = sessao.ValorIngresso,
                        TipoAnimacao = sessao.TipoAnimacao,
                        TipoAudio = sessao.TipoAudio,
                        FilmeId = sessao.FilmeId,
                        SalaId = sessao.SalaId
                    });
                }

                return Ok(sessoesRetorno);
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

        [HttpPost]
        public async Task<IActionResult> Post(Sessao model)
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
                if (await _sessaoService.DeleteSessao(id)) return Ok("Excluido");
                else return BadRequest("sessão não foi excluida");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar excluir sessão: {ex.Message}");
            }
        }
    }
}
