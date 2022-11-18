using Cinema.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Persistence.Contextos
{
    public class CinemaContext : DbContext
    {
        public CinemaContext(DbContextOptions<CinemaContext> options) : base(options) { }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }

    }
}