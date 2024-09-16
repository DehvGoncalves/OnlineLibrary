namespace ProjetoEmprestimoLivros.Services.AutenticacaoService
{
    public interface IAutenticacaoInterface
    {
        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
        //Vamos retornar uma senha, um bayte de string senhaHas e um byte de string senhaSalt
        bool VerificaLogin(string senha, byte[] senhaHash, byte[] senhaSalt);
    }
}
