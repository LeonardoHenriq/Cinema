using Cinema.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<Filme> Filmes { get; set; }
    }
}