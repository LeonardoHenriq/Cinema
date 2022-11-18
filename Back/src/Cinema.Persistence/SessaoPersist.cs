using System.Linq;
using System;
using System.Threading.Tasks;
using Cinema.Domain;
using Microsoft.EntityFrameworkCore;
using Cinema.Persistence.Contratos;
using Cinema.Persistence.Contextos;

namespace Cinema.Persistence
{
    public class SessaoPersist : ISessaoPersist
    {
        private readonly CinemaContext _context;
        public SessaoPersist(CinemaContext context)
        {
            _context = context;

        }
        public async Task<Sessao[]> GetAllSessoesAsync(bool includefilmeandsala = false)
        {
            IQueryable<Sessao> query = _context.Sessoes;
            if (includefilmeandsala)
            {
                query = query
                    .Include(s => s.filme);

                query = query
                    .Include(s => s.sala);
            }

            query = query.AsNoTracking().OrderBy(s => s.Id);

            return await query.ToArrayAsync();
        }
        public async Task<Sessao> GetSessoesByIdAsync(int sessaoId,bool includefilmeandsala = false)
        {
            IQueryable<Sessao> query = _context.Sessoes;

            if (includefilmeandsala)
            {
                query = query
                    .Include(s => s.filme);

                query = query
                    .Include(s => s.sala);
            }

            query = query.AsNoTracking().Where(s => s.Id == sessaoId);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<bool> GetSessoesByFilmeAsync(int filmeId)
        {
            IQueryable<Sessao> query = _context.Sessoes;
            query = query.AsNoTracking().Where(s => s.FilmeId == filmeId);

            return await query.AnyAsync();
        }
        public async Task<Sala[]> GetSalasDisponiveisAsync(DateTime inicial, DateTime final)
        {
            IQueryable<Sessao> query = _context.Sessoes;
            var resultado = query.AsNoTracking().Where(s => !(inicial >= s.HorarioInicial && inicial <= s.HorarioFinal) ||
                                    !(final <= s.HorarioInicial && final >= s.HorarioFinal)).Select(s => s.sala);

            return await resultado.ToArrayAsync();
        }
        public async Task<bool> SalaAvailable(int salaId,DateTime inicial, DateTime final)
        {
            IQueryable<Sessao> query = _context.Sessoes;
            var resultado = query.AsNoTracking().Where(s =>s.SalaId == salaId && (inicial >= s.HorarioInicial && inicial <= s.HorarioFinal) ||
                                    (final <= s.HorarioInicial && final >= s.HorarioFinal)).Select(s => s.sala);

            return await resultado.AnyAsync();
        }
        public async Task<TimeSpan> GetDuracaoFilme(int filmeId)
        {
            IQueryable<Filme> query = _context.Filmes;
            query = query.AsNoTracking().Where(f => f.Id == filmeId);

            return await query.Select(f => f.Duracao).FirstOrDefaultAsync();
        }
    }
}