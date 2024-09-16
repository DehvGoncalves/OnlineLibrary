using AutoMapper;
using ProjetoEmprestimoLivros.Dto.Endereco;
using ProjetoEmprestimoLivros.Dto.Livro;
using ProjetoEmprestimoLivros.Dto.Relatorio;
using ProjetoEmprestimoLivros.Models;


namespace ProjetoEmprestimoLivros.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {   //Objeto tipo LivroCriacaoDto pode ser LivrosModel
            CreateMap<LivroCriacaoDto, LivroModel>(); 
            CreateMap<LivroModel, LivroEdicaoDto>();
            CreateMap<LivroEdicaoDto, LivroModel>();
            CreateMap<EnderecoModel, EnderecoEditarDto>();
            CreateMap<EnderecoEditarDto, EnderecoModel>();
            CreateMap<LivroModel, LivroRelatorioDto>();
            CreateMap<UsuarioModel, UsuarioRelatorioDto>();
            CreateMap<EmprestimoModel, EmprestimoRelatorioDto>();
        }
    }
}
