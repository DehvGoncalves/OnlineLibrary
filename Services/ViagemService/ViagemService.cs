using ProjetoEmprestimoLivros.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using ProjetoEmprestimoLivros.Models.Viagens;
using ProjetoEmprestimoLivros.Data;
using ProjetoEmprestimoLivros.Dto.ViagemDto;

namespace ProjetoEmprestimoLivros.Services.ViagemService
{
    public class ViagemService : IViagemInterface
    {
        private readonly AppDbContext _context;
        public ViagemService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ViagensFeitasModel> BuscarLivroPorId(int? id)
        {
            try
            {
                var viagemSelecionada = await _context.ViagensFeitas.FirstOrDefaultAsync(v => v.Id == id);
                return viagemSelecionada;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ViagensFeitasModel> CadastrarViagem(ViagemCadastroDto cadastrarViagemDto, IFormFile foto_lugar)
        {
            try
            {
                var dadosDaViagem = new ViagensFeitasModel
                {
                    Foto = cadastrarViagemDto.Foto,
                    Lugar = cadastrarViagemDto.Lugar,
                    CompanhiaDeViagem = cadastrarViagemDto.CompanhiaDeViagem,
                    Data = cadastrarViagemDto.Data,
                    Hospedagem = cadastrarViagemDto.Hospedagem,
                    ValorHospedagem = cadastrarViagemDto.ValorHospedagem,
                    QuantidadePessoas = cadastrarViagemDto.QuantidadePessoas,
                    NomePessoas = cadastrarViagemDto.NomePessoas,
                    Nota = cadastrarViagemDto.Nota,

                };
                _context.ViagensFeitas.Add(dadosDaViagem);
                await _context.SaveChangesAsync();
                return dadosDaViagem;
            }
            catch
            {
                throw new Exception("Erro ao cadastrar viagem");

            }
        }

        public async Task<List<ViagensFeitasModel>> ListarViagem()
        {
            try
            {
                var viagens = await _context.ViagensFeitas.ToListAsync();
                return viagens;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
