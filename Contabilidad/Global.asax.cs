using Contabilidad.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Contabilidad {

    public class MvcApplication : HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DevExtremeBundleConfig.RegisterBundles(BundleTable.Bundles);

            // Uncomment to use pre-17.2 routing for .Mvc() and .WebApi() data sources
            // DevExtreme.AspNet.Mvc.Compatibility.DataSource.UseLegacyRouting = true;
            // Uncomment to use pre-17.2 behavior for the "required" validation check
            // DevExtreme.AspNet.Mvc.Compatibility.Validation.IgnoreRequiredForBoolean = false;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();

            Response.Clear();

            HttpException httpException = exception as HttpException;
            RouteData routeData = new RouteData();

            routeData.Values.Add("controller", "Error");

            if (httpException != null)
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // page not found
                        routeData.Values.Add("action", "HttpError404");
                        break;
                    case 500:
                        // server error
                        routeData.Values.Add("action", "HttpError500");
                        break;
                    default:
                        routeData.Values.Add("action", "httpErrorGeneral");
                        break;
                }

                // clear error on server
                Server.ClearError();
                Response.TrySkipIisCustomErrors = true;
            }

            IController errorController = new ErrorController();
            errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        }

        public override void Init()
        {
            this.PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        private void MvcApplication_PostAuthenticateRequest(object sender, System.EventArgs e)
        {
            if (HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Contains("~/api"))
            {
                HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
            }
        }

    }
}
