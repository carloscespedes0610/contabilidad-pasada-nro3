using System.Web.Mvc;

namespace Contabilidad.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult httpError404()
        {
            return View();
        }

        public ActionResult httpError500()
        {
            return View();
        }

        public ActionResult httpErrorGeneral()
        {
            return View();
        }

        public ActionResult httpErrorMsg(string MessageErr)
        {
            ViewBag.MessageErr = MessageErr;
            return View();
        }
    }
}