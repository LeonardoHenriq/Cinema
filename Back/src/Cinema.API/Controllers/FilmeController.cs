using System.Collections.Generic;
using System.Linq;
using Cinema.API.Data;
using Cinema.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmeController : ControllerBase
    {
        private readonly DataContext _context;
        public FilmeController(DataContext context)
        {
           _context = context;
        }

        [HttpGet]
        public IEnumerable<Filme> Get()
        {
            return _context.Filmes;
        }
        [HttpGet("{id}")]
        public Filme Get(int id)
        {
            return _context.Filmes.FirstOrDefault(f => f.FilmeId == id);
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
