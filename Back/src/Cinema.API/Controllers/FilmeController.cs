using Microsoft.AspNetCore.Mvc;
using Cinema.Application.Contratos;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Cinema.Application.Dtos;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace Cinema.API.Controllers
{
    //[Authorize(Roles ="Gerente")]
    [ApiController]
    [Route("api/[controller]")]
    public class FilmeController : ControllerBase
    {
        private readonly IFilmeService _filmeService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public FilmeController(IFilmeService filmeService, IWebHostEnvironment hostEnvironment)
        {
            _filmeService = filmeService;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var filmes = await _filmeService.GetAllFilmesAsync();
                if (filmes == null) return NoContent();

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
                if (filme == null) return NoContent();

                return Ok(filme);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar filme: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(FilmeDto model)
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

        [HttpPost("upload-image/{filmeId}")]
        public async Task<IActionResult> UploadImage(int filmeId)
        {
            try
            {
                var filme = await _filmeService.GetFilmesIdAsync(filmeId);
                
                if (filme == null) return NoContent();

                var file = Request.Form.Files[0];

                if (file.Length > 0)
                {
                    DeleteImage(filme.ImagemURL);
                    filme.ImagemURL = await SaveImage(file);
                }

                var filmeRetorno = await _filmeService.UpdateFilme(filmeId, filme);

                return Ok(filmeRetorno);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar filme: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, FilmeUpdateDto model)
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

                var filme = await _filmeService.GetFilmesByIdAsync(id);
                if(filme == null) return NoContent();

                var result = await _filmeService.DeleteFilme(filme.Id);
                if(result == "Excluido"){

                    DeleteImage(filme.ImagemURL);
                    return Ok(new {message = result});
                }else
                throw new Exception("ocorreu uma fala interna");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar excluir filme: {ex.Message}");
            }
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');

            imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.FileName)}";

            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/Images", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }
        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources\Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}
