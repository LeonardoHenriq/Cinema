using Cinema.Domain;
using Cinema.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Persistence.Contextos
{
    public class CinemaContext : IdentityDbContext<User, Role, int, 
                                                   IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, 
                                                   IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public CinemaContext(DbContextOptions<CinemaContext> options) : base(options) { }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole =>{
                userRole.HasKey(ur => new {ur.UserId, ur.RoleId});
                userRole.HasOne(ur => ur.Role)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();
                
                userRole.HasOne(ur => ur.User)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired();
            });

        }

    }
}