using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cinema.Application.Contratos;
using Cinema.Application.Dtos;
using Cinema.Domain;
using Cinema.Persistence.Contratos;

namespace Cinema.Application
{
    public class SalaService : ISalaService
    {
        private readonly ISalaPersist _salaPersist;
        private readonly IMapper _mapper;

        public SalaService(ISalaPersist salaPersist,IMapper mapper)
        {
            _salaPersist = salaPersist;
            _mapper = mapper;
        }
        public async Task<SalaDto[]> GetAllSalasAsync()
        {
            try
            {
                var salas = await _salaPersist.GetAllSalasAsync();
                if (salas == null) return null;

                var resultado = _mapper.Map<SalaDto[]>(salas);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SalaDto> GetSalasByIdAsync(int salaId)
        {
            try
            {
                var sala = await _salaPersist.GetSalasByIdAsync(salaId);
                if (sala == null) return null;

                var resultado = _mapper.Map<SalaDto>(sala);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}