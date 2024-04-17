using ProjetoEmprestimoLivros.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using ProjetoEmprestimoLivros.Models.Viagens;
using ProjetoEmprestimoLivros.Data;

namespace ProjetoEmprestimoLivros.Services.ViagemService
{
    public class ViagemService : IViagemInterface
    {
        private readonly AppDbContext _context;
        public ViagemService(AppDbContext context)
        {
            _context = context;
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
