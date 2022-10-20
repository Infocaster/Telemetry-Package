using System.Web;
using Umbraco.Core;
using Umbraco.Core.IO;
using Umbraco.Core.Security;
using Umbraco.Web;

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
            if (UmbracoContext.Current != null && !UmbracoContext.Current.IsFrontEndUmbracoRequest && Request.AppRelativeCurrentExecutionFilePath.StartsWith(SystemDirectories.Umbraco))
            {
                var auth = new HttpContextWrapper(HttpContext.Current).GetUmbracoAuthTicket();
                if (auth != null)
                {
                    var currentUser = ApplicationContext.Current.Services.UserService.GetByUsername(auth.Name);

                    if (currentUser != null)
                        return;
                }

                if (_requestIsLocal)
                {
                    UmbracoContext.Current.Security.PerformLogin(0);
                    Response.Redirect(Request.RawUrl);
                }
            }
#endif
        }
    }
}