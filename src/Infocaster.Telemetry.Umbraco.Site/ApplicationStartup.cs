using System.Web;
using Umbraco.Core.IO;
using Umbraco.Web;
using Umbraco.Web.Security;

namespace Infocaster.Telemetry.Umbraco.Site
{
    public class ApplicationStartup : UmbracoApplication
    {
        private bool _requestIsLocal =>
            Request.UserHostAddress.StartsWith("192.168") ||
            Request.UserHostAddress.StartsWith("10.") ||
            Request.UserHostAddress == "127.0.0.1" ||
            Request.UserHostAddress == "::1";

        protected void Application_AuthenticateRequest()
        {
#if DEBUG
            // Auto login user if debug on local machine
            var context = global::Umbraco.Web.Composing.Current.UmbracoContext;

            if (context != null && !context.IsFrontEndUmbracoRequest && Request.AppRelativeCurrentExecutionFilePath.StartsWith(SystemDirectories.Umbraco))
            {
                var auth = new HttpContextWrapper(HttpContext.Current).GetUmbracoAuthTicket();
                if (auth != null)
                {
                    var userService = global::Umbraco.Web.Composing.Current.Services.UserService;
                    var currentUser = userService.GetByUsername(auth.Identity.Name);

                    if (currentUser != null)
                        return;
                }

                if (_requestIsLocal)
                {
                    context.Security.PerformLogin(-1);
#pragma warning disable SCS0027 // Potential Open Redirect vulnerability was found where '{0}' in '{1}' may be tainted by user-controlled data from '{2}' in method '{3}'.
                    Response.Redirect(Request.RawUrl);
#pragma warning restore SCS0027 // Potential Open Redirect vulnerability was found where '{0}' in '{1}' may be tainted by user-controlled data from '{2}' in method '{3}'.
                }
            }
#endif
        }
    }
}