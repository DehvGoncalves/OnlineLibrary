using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using ProjetoEmprestimoLivros.Models;

namespace ProjetoEmprestimoLivros.Filtros
{
    public class UsuarioLogado : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {

            //Aqui vamos colocar as regras de negócio para verificar se o usuário está logado ou não.
            string sessaoUsuario = context.HttpContext.Session.GetString("SessaoUsuario"); //verificar se existe a sessão do usuário
            
            //Estou tentando entrar em uma página sem estar logado, então ele vai direcionar para a página de login, qualquer página.
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
                UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);//se existir a sessão, ele vai pegar o usuário logado e deserializar p/ eu conseguir tratar

                //eu tenho uma sessão, mas o usuário está nulo, então ele vai direcionar para a página de login. pq ele não está logado.
                if(usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                        {"controller", "Home"},
                        { "action", "Login"}
                    });
                }
            }



            base.OnActionExecuted(context); //Estaremos basicamente sobrescrevendo o método OnActionExecuted, que é chamado após a execução de uma ação.
            //Se algum problema aconteceu, eu não posso acessar a rota.
        }
    }
}
//Resumindo, só acessa se iver logado, e mesmo que ele encontre uma sessão, se o usuário estiver nulo, ele vai direcionar para a página de login. Pois pode ser que a sessão tenha expirado.