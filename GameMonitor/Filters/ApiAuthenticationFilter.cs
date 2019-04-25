using GameMonitor.Services;
using System.Threading;
using System.Web.Http.Controllers;

namespace GameMonitor.Filters
{
    public class ApiAuthenticationFilter : GenericAuthenticationFilter
    {
        public ApiAuthenticationFilter(){}

        public ApiAuthenticationFilter(bool isActive)
            : base(isActive)
        {
        }

        protected override bool OnAuthorizeUser(string username, string password, HttpActionContext actionContext)
        {
            try
            {
                UserServices userServices = new UserServices();
                var userId = userServices.Authenticate(username, password);
                if (userId > 0)
                {
                    var basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                    if (basicAuthenticationIdentity != null)
                        basicAuthenticationIdentity.UserId = userId;
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
}