using AutoMapper;
using Cinema.Application.Dtos;
using Cinema.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Application.Helpers
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            CreateMap<Filme, FilmeDto>().ReverseMap();
            CreateMap<Sala, SalaDto>().ReverseMap();
            CreateMap<Sessao, SessaoDto>().ReverseMap();
        }
    }
}