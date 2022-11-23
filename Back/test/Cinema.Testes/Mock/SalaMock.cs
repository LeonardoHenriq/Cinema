using Cinema.Domain;
using Cinema.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Testes.Mock
{
    public class SalaMock : ISalaPersist
    {
        Sala sala;
        public async Task<Sala[]> GetAllSalasAsync()
        {
            return GetSalas();
        }

        public async Task<Sala> GetSalasByIdAsync(int salaId)
        {
            var salas = GetSalas();
            sala = new Sala();
            foreach(var s in salas)
            {
                if (s.Id == salaId)
                    sala = s;
            }
            return sala;
        }

        private Sala[] GetSalas()
        {
            var salas = new  Sala[2];
            salas[0] = new Sala { Id = 1, Nome = "Sala 1", QuantidadeAssentos = 100 };
            salas[1] = new Sala { Id = 2, Nome = "Sala 2", QuantidadeAssentos = 100 };
            return salas;
        }
    }
}
