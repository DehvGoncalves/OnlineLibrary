using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using ProjetoEmprestimoLivros.Models;

namespace ProjetoEmprestimoLivros.Filtros
{
    public class UsuarioLogadoCliente : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {

            string sessaoUsuario = context.HttpContext.Session.GetString("SessaoUsuario"); 

            if (string.IsNullOrEmpty(sessaoUsuario))
            {

                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller", "Home"},
                    { "action", "Login"}
                });
            }
            else
            {
                UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);

                if (usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                        {"controller", "Home"},
                        { "action", "Login"}
                    });
                }else if(usuario.Perfil == Enum.PerfilEnum.Cliente)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                        {"controller", "Home"},
                        { "action", "Index"}
                    });
                }
            }



            base.OnActionExecuted(context); 
          
        }
    }
}
