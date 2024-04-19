using AutoMapper;
using ProjetoEmprestimoLivros.Dto;
using ProjetoEmprestimoLivros.Models;

namespace ProjetoEmprestimoLivros.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<LivroCriacaoDto, LivrosModel>();
        }
    }
}
