using System.Security.Cryptography;

namespace ProjetoEmprestimoLivros.Services.AutenticacaoService
{
    public class AutenticacaoService : IAutenticacaoInterface
    {
        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }

        //Verifica se a senha fornecida corresponde ao hash armazenado
        public bool VerificaLogin(string senha, byte[] senhaHash, byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512(senhaSalt))
            {
               var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                return computedHash.SequenceEqual(senhaHash); //SequenceEqual compara dois arrays de bytes, compara se a senha que foi passada é igual a senha que foi armazenada
            }
        }
    }
}
