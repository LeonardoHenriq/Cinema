using AutoMapper;
using Cinema.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Testes.Mock
{
    public class AutoMocker
    {
        public IMapper LoadMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CinemaProfile>();
            });
            return mapper.CreateMapper();
        }
    }
}
