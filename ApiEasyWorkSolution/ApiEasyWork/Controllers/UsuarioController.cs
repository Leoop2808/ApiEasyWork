using ApiEasyWork.App_Start;
using ApiEasyWork.FiltersAttributes;
using ApiEasyWork.Util;
using EasyWorkBusiness.Contrato;
using EasyWorkDataAccess.Models;
using EasyWorkEntities.Authentication.Request;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace ApiEasyWork.Controllers
{
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        private IAuthenticationBO _authenticationBO;
        private TimeSpan tokenExpirationTimeSpan = TimeSpan.FromHours(24);
        private readonly ILog log = LogManager.GetLogger(typeof(UsuarioController));
        private readonly IUsuarioBO _usuarioBO;
        public UsuarioController(IUsuarioBO usuarioBO)
        {
            _usuarioBO = usuarioBO;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        [ApplicationAuthenticationFilter]
        [Route("autenticacion/google")]
        [HttpPost]
        public async Task<HttpResponseMessage> AutenticarGoogle(AutenticarGoogleRequest request)
        {
            string idLogTexto = Guid.NewGuid().ToString();
            var cod_aplicacion = AplicationData.codAplicacion;
            var respAuthGoogle = _authenticationBO.AutenticarGoogle(request, cod_aplicacion, idLogTexto);
            if (respAuthGoogle.codeRes == HttpStatusCode.OK)
            {
                var userSearch = UserManager.FindByName(respAuthGoogle.datos.correo);
                var identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
                identity.AddClaim(new Claim("cod_aplicacion", cod_aplicacion.ToString()));
                identity.AddClaim(new Claim("user_id", userSearch.id_usuario.ToString()));
                identity.AddClaim(new Claim("user_code", userSearch.cod_usuario.ToString()));

                EasyWorkDBEntities ctxBD = new EasyWorkDBEntities();
                mst_rol rol = ctxBD.mst_rol.Where(x => x.id_rol == userSearch.id_rol).FirstOrDefault();

                identity.AddClaim(new Claim(ClaimTypes.Role, rol.cod_rol.ToString()));
                var cookiesIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationType);

                AuthenticationTicket ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
                var currentUtc = new Microsoft.Owin.Infrastructure.SystemClock().UtcNow;
                ticket.Properties.IssuedUtc = currentUtc;
                ticket.Properties.ExpiresUtc = currentUtc.Add(tokenExpirationTimeSpan);
                var accesstoken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);
                Authentication.SignIn(cookiesIdentity);

                // Create the response
                JObject blob = new JObject(
                    new JProperty("access_token", accesstoken),
                    new JProperty("token_type", "bearer"),
                    new JProperty("expires_in", tokenExpirationTimeSpan.TotalSeconds.ToString()),
                    new JProperty("nombres", respAuthGoogle.datos.nombres),
                    new JProperty("apellidos", respAuthGoogle.datos.apellidos),
                    new JProperty("correo", respAuthGoogle.datos.correo),
                    new JProperty("flgMostrarRegistroUsuario", respAuthGoogle.flgMostrarRegistroUsuario),
                    new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
                    new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString())
                );

                return Request.CreateResponse(HttpStatusCode.OK, blob);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_token_facebook"),
                    new JProperty("error_description", "Could not authenticate with Facebook.")
                ));
            }
        }

        [ApplicationAuthenticationFilter]
        [Route("autenticacion/facebook")]
        [HttpPost]
        public async Task<HttpResponseMessage> AutenticarFacebook(AutenticarFacebookRequest request)
        {
            string idLogTexto = Guid.NewGuid().ToString();
            var cod_aplicacion = AplicationData.codAplicacion;
            var respAuthFacebook = _authenticationBO.AutenticarFacebook(request, cod_aplicacion, idLogTexto);
            if (respAuthFacebook.codeRes == HttpStatusCode.OK)
            {
                var userSearch = UserManager.FindByName(respAuthFacebook.datos.correo);
                var identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
                identity.AddClaim(new Claim("cod_aplicacion", cod_aplicacion.ToString()));
                identity.AddClaim(new Claim("user_id", userSearch.id_usuario.ToString()));
                identity.AddClaim(new Claim("user_code", userSearch.cod_usuario.ToString()));

                EasyWorkDBEntities ctxBD = new EasyWorkDBEntities();
                mst_rol rol = ctxBD.mst_rol.Where(x => x.id_rol == userSearch.id_rol).FirstOrDefault();

                identity.AddClaim(new Claim(ClaimTypes.Role, rol.cod_rol.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userSearch.id_usuario.ToString()));

                var cookiesIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationType);

                AuthenticationTicket ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
                var currentUtc = new Microsoft.Owin.Infrastructure.SystemClock().UtcNow;
                ticket.Properties.IssuedUtc = currentUtc;
                ticket.Properties.ExpiresUtc = currentUtc.Add(tokenExpirationTimeSpan);
                var accesstoken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);
                Authentication.SignIn(cookiesIdentity);

                // Create the response
                JObject blob = new JObject(
                    new JProperty("access_token", accesstoken),
                    new JProperty("token_type", "bearer"),
                    new JProperty("expires_in", tokenExpirationTimeSpan.TotalSeconds.ToString()),
                    new JProperty("nombres", respAuthFacebook.datos.nombres),
                    new JProperty("apellidos", respAuthFacebook.datos.apellidos),
                    new JProperty("correo", respAuthFacebook.datos.correo),
                    new JProperty("flgMostrarRegistroUsuario", respAuthFacebook.flgMostrarRegistroUsuario),
                    new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
                    new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString())
                );

                return Request.CreateResponse(HttpStatusCode.OK, blob);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_token_facebook"),
                    new JProperty("error_description", "Could not authenticate with Facebook.")
                ));
            }
        }
    }
}