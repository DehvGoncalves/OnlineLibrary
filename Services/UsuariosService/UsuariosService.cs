using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjetoEmprestimoLivros.Data;
using ProjetoEmprestimoLivros.Dto.Usuario;
using ProjetoEmprestimoLivros.Models;
using ProjetoEmprestimoLivros.Services.AutenticacaoService;
using System.Runtime.CompilerServices;

namespace ProjetoEmprestimoLivros.Services.UsuariosService
{
    public class UsuariosService : IUsuariosInterface
    {
        private readonly AppDbContext _context;
        private readonly IAutenticacaoInterface _autenticacaoInterface;
        private readonly IMapper _mapper;

        public UsuariosService(AppDbContext context, IAutenticacaoInterface autenticacaoInterface, IMapper mapper)
        {
            _context = context;
            _autenticacaoInterface = autenticacaoInterface;
            _mapper = mapper;
        }

        public async Task<UsuarioModel> BuscarUsuarioPorId(int? id)
        {
            try
            {
                var usuario = await _context.Usuarios.Include(e => e.Endereco).FirstOrDefaultAsync(idUsuario => idUsuario.Id == id);
                return usuario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<UsuarioModel>> BuscarUsuarios(int? id)
        {
            try
            {
                var registros = new List<UsuarioModel>();

                if (id == 0)
                {
                    registros = await _context.Usuarios.Where(cliente => cliente.Perfil == 0).Include(endereco => endereco.Endereco).ToListAsync();
                }
                else
                {
                    registros = await _context.Usuarios.Where(funcionario => funcionario.Perfil != 0).Include(endereco => endereco.Endereco).ToListAsync();
                }
                return registros;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<List<UsuarioModel>> BuscarUsuarios()
        {
            try
            {
                var usuarios = _context.Usuarios.Include(e => e.Endereco).ToListAsync();
                return usuarios;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UsuarioCriacaoDto> Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            try
            {
                _autenticacaoInterface.CriarSenhaHash(usuarioCriacaoDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);
                var usuario = new UsuarioModel
                {
                    NomeCompleto = usuarioCriacaoDto.NomeCompleto,
                    Usuario = usuarioCriacaoDto.Usuario,
                    Email = usuarioCriacaoDto.Email,
                    Perfil = usuarioCriacaoDto.Perfil,
                    Turno = usuarioCriacaoDto.Turno,
                    SenhaHash = senhaHash, // senha criptografada, de acordo com oque está vindo no parametro
                    SenhaSalt = senhaSalt,
                };

                var enderco = new EnderecoModel
                {
                    Logradouro = usuarioCriacaoDto.Logradouro,
                    Bairro = usuarioCriacaoDto.Bairro,
                    Numero = usuarioCriacaoDto.Numero,
                    CEP = usuarioCriacaoDto.CEP,
                    Estado = usuarioCriacaoDto.Estado,
                    Complemento = usuarioCriacaoDto.Complemento,
                    Usuario = usuario //relacionamento com a tabela de usuario, esse endereço pertence ao usuario que está sendo cadastrado
                };
                usuario.Endereco = enderco;

                _context.Add(usuario);
                await _context.SaveChangesAsync();

                return usuarioCriacaoDto;
            }
            catch
            {
                throw new Exception("Erro ao cadastrar usuário");
            }
        }

        public async Task<UsuarioModel> Editar(UsuarioEdicaoDto usuarioEdicaoDto)
        {
            try
            {
                //vamos procurar até acahar um usuario que o id seja igual ao id que estamos recebendo no parâmetro
                var usuarioBanco = await _context.Usuarios.Include(e => e.Endereco)
                    .FirstOrDefaultAsync(usuarioBanco => usuarioBanco.Id == usuarioEdicaoDto.Id);

                if (usuarioBanco != null)
                {//se o usuário existir vamos mapear as propriedades
                    usuarioBanco.Turno = usuarioEdicaoDto.Turno;
                    usuarioBanco.NomeCompleto = usuarioEdicaoDto.NomeCompleto;
                    usuarioBanco.Email = usuarioEdicaoDto.Email;
                    usuarioBanco.Perfil = usuarioEdicaoDto.Perfil;
                    usuarioBanco.Usuario = usuarioEdicaoDto.Usuario;
                    usuarioBanco.DataAlteracao = DateTime.Now;
                    usuarioBanco.Endereco = _mapper.Map<EnderecoModel>(usuarioEdicaoDto.Endereco); //Transformar em Model oque recebemos como Dto, e colocar no usuarioBanco

                    _context.Update(usuarioBanco);
                    await _context.SaveChangesAsync();
                    return usuarioBanco;
                }
                return usuarioBanco;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UsuarioModel> MudarStatusUsuario(int? id)
        {
            try
            {
                var usuarioMudarStatus = await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Id == id);
                if (id != null)
                {
                    if (usuarioMudarStatus.Status == true)
                    {
                        usuarioMudarStatus.Status = false;
                        usuarioMudarStatus.DataAlteracao = DateTime.Now;
                    }
                    else
                    {
                        usuarioMudarStatus.Status = true;
                        usuarioMudarStatus.DataAlteracao = DateTime.Now;
                    }
                    _context.Update(usuarioMudarStatus);
                    await _context.SaveChangesAsync();
                    return usuarioMudarStatus;
                }
                return usuarioMudarStatus;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> VerificarSeJaExisteUsuario(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            try
            {
                var usuarioJaCadastrado = await _context.Usuarios
                    .FirstOrDefaultAsync(usuario => usuario.Email == usuarioCriacaoDto.Email || usuario.Usuario == usuarioCriacaoDto.Usuario);

                if (usuarioJaCadastrado != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                //var teste = usuarioJaCadastrado ?? null;
                //return usuarioJaCadastrado != null ? true : false; //If ternário

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
