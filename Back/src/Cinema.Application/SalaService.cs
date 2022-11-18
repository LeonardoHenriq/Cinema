using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Application.Contratos;
using Cinema.Domain;
using Cinema.Persistence.Contratos;

namespace Cinema.Application
{
    public class SalaService : ISalaService
    {
        private readonly ISalaPersist _salaPersist;

        public SalaService(ISalaPersist salaPersist)
        {
            _salaPersist = salaPersist;
        }
        public async Task<Sala[]> GetAllSalasAsync()
        {
            try
            {
                var salas = await _salaPersist.GetAllSalasAsync();
                if (salas == null) return null;

                return salas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Sala> GetSalasByIdAsync(int salaId)
        {
            try
            {
                var sala = await _salaPersist.GetSalasByIdAsync(salaId);
                if (sala == null) return null;

                return sala;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}