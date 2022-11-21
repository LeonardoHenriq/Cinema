using System.Linq;
using System;
using System.Threading.Tasks;
using Cinema.Domain;
using Microsoft.EntityFrameworkCore;
using Cinema.Persistence.Contratos;
using Cinema.Persistence.Contextos;
using Cinema.Persistence.Migrations;
using System.Collections.Generic;

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
        public async Task<Sessao> GetSessoesByIdAsync(int sessaoId, bool includefilmeandsala = false)
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
        public async Task<List<Sala>> SalasAvailableAsync()
        {
            IQueryable<Sala> salas = _context.Salas;

            return await salas.ToListAsync();
        }
        public async Task<List<Sala>> SalaIsUsedAsync(DateTime inicial, DateTime final)
        {
            IQueryable<Sessao> Sessoes = _context.Sessoes;
            var salas = Sessoes.AsNoTracking().Where(s => inicial >= s.HorarioInicial && inicial <= s.HorarioFinal ||
                                                      final <= s.HorarioInicial && final >= s.HorarioFinal).Select(s => s.sala).ToListAsync();
            return await salas;
        }
        public async Task<bool> SalaAvailableAsync(int salaId, DateTime inicial, DateTime final)
        {
            IQueryable<Sessao> query = _context.Sessoes;
            var resultado = query.AsNoTracking().Where(s => s.SalaId == salaId && (inicial >= s.HorarioInicial && inicial <= s.HorarioFinal) ||
                                    (final <= s.HorarioInicial && final >= s.HorarioFinal)).Select(s => s.sala);

            return await resultado.AnyAsync();
        }
        public async Task<TimeSpan> GetDuracaoFilmeAsync(int filmeId)
        {
            IQueryable<Filme> query = _context.Filmes;
            query = query.AsNoTracking().Where(f => f.Id == filmeId);

            return await query.Select(f => f.Duracao).FirstOrDefaultAsync();
        }
    }
}