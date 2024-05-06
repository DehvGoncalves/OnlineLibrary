using AutoMapper;
using ProjetoEmprestimoLivros.Dto.Livro;
using ProjetoEmprestimoLivros.Models;

namespace ProjetoEmprestimoLivros.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<LivroCriacaoDto, LivroModel>(); //Objeto tipo LivroCriacaoDto pode ser LivrosModel
            CreateMap<LivroModel, LivroEdicaoDto>();
            CreateMap<LivroEdicaoDto, LivroModel>();
        }
    }
}
