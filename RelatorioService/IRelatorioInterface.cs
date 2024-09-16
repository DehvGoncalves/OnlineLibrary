using System.Data;

namespace ProjetoEmprestimoLivros.Services.RelatorioService
{
    public interface IRelatorioInterface
    {
        //T é o tipo genérico porque ele pode receber qualquer tipo de dado
        DataTable ColetarDados<T>(List<T> dados, int idRelatorio);
    }
}
