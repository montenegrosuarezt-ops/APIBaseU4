using APIBaseU4.Dtos;
using APIBaseU4.Models;
using AutoMapper;

namespace APIBaseU4.PerfilesAutomapper
{
    public class PerfilCursos : Profile
    {
        public PerfilCursos()
        {
            CreateMap<Curso, CursoDto>();
            CreateMap<CursoDto, Curso>();
        }
    }
}
