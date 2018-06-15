using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Contabilidad.Controllers
{
    public class SessionExpireFilterAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Validar la información que se encuentra en la sesión.
            if (HttpContext.Current.Session["User"] == null)
            {
                // Si la información es nula, redireccionar a 
                // página de error u otra página deseada.
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                    { "Controller", "Login" },
                    { "Action", "Index" }
                });
            }

            base.OnActionExecuting(filterContext);
        }
    }
}