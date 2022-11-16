using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cinema.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmeController : ControllerBase
    {
        public IEnumerable<Filme> _filme = new Filme[]{
                new Filme(){
                FilmeId = 1, 
                ImagemURL = "Imagem1.png",
                Titulo = "Star Wars",
                Descricao = "Guerra nas Estrelas",
                Duracao ="02:30:00"
                },
                new Filme(){
                FilmeId = 2, 
                ImagemURL = "Imagem2.png",
                Titulo = "Star Trek",
                Descricao = "Start Trek antigo",
                Duracao ="02:50:00"
                }
            };
        public FilmeController()
        {
            
        }

        [HttpGet]
        public IEnumerable<Filme> Get()
        {
            return _filme;
        }
        [HttpGet("{id}")]
        public IEnumerable<Filme> Get(int id)
        {
            return _filme.Where(f => f.FilmeId == id).ToArray();
        }

        [HttpPost]
        public string Post()
        {
            return "Exemplo de Post";
        }
        [HttpPut("{id}")]
        public string Put(int id)
        {
            return $"Exemplo de Put com id = {id}";
        }
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"Exemplo de Delete com id = {id}";
        }
    }
}
