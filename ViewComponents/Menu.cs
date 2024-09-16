using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetoEmprestimoLivros.Models;


namespace ProjetoEmprestimoLivros.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUsuario = HttpContext.Session.GetString("SessaoUsuario");

            if (string.IsNullOrEmpty(sessaoUsuario))
            {   // Retorna a view sem modelo se a sessão estiver vazia
                return View();
            }

            UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
            return View(usuario);
        }
    }
}
