using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GameMonitor.Data;
using System.Threading;
using GameMonitor.Filters;
using GameMonitor.Services;


namespace GameMonitor.Controllers
{
    [ApiAuthenticationFilter]
    public class AuthenticateController : ApiController
    {
        public AuthenticateController(){}

        [Route("login")]
        [Route("authenticate")]
        [Route("get/token")]
        [HttpPost]
        public HttpResponseMessage Authenticate()
        {
            if (Thread.CurrentPrincipal != null &&
                Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                var basicAuthenticationIdentity =
                    Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                if(basicAuthenticationIdentity != null)
                {
                    var userId = basicAuthenticationIdentity.UserId;
                    return GetAuthToken(userId);
                }
            }
            return null;
        }

        //need logout route

        private HttpResponseMessage GetAuthToken(int userId)
        {
            using (GameMonitorDbContext db = new GameMonitorDbContext())
            {
                var _tokenServices = new TokenServices();
                var token = _tokenServices.GenerateToken(userId);
                var response = Request.CreateResponse(HttpStatusCode.OK, "Authorized");
                response.Headers.Add("Token", token.AuthToken);
                response.Headers.Add("TokenExpiry", Convert.ToString(token.ExpiresOn));
                response.Headers.Add("Access-Control-Expose-Headers", "Token,TokenExpiry");
                return response;
            } 
        }
    }
}
