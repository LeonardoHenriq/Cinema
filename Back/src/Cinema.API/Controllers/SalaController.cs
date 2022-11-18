using Cinema.Application.Contratos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Cinema.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaController : ControllerBase
    {
        private readonly ISalaService _salaService;

        public SalaController(ISalaService salaService)
        {
            _salaService = salaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var salas = await _salaService.GetAllSalasAsync();
                if (salas == null) return NotFound("Nenhuma sala encontrada.");

                return Ok(salas);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar salas: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var sala = await _salaService.GetSalasByIdAsync(id);
                if (sala == null) return NotFound("Nenhuma saka encontrada.");

                return Ok(sala);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar sala: {ex.Message}");
            }
        }
    }
}
