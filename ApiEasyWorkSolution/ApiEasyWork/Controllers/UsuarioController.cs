using ApiEasyWork.App_Start;
using ApiEasyWork.FiltersAttributes;
using ApiEasyWork.Util;
using EasyWorkBusiness.Contrato;
using EasyWorkDataAccess.Models;
using EasyWorkEntities;
using EasyWorkEntities.Authentication.Request;
using EasyWorkEntities.Usuario.Request;
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
using static EasyWorkEntities.Constantes;

namespace ApiEasyWork.Controllers
{
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        private IAuthenticationBO _authenticationBO;
        private TimeSpan tokenExpirationTimeSpan = TimeSpan.FromHours(24);
        private readonly ILog log = LogManager.GetLogger(typeof(UsuarioController));
        private readonly IUsuarioBO _usuarioBO;
        public UsuarioController(IUsuarioBO usuarioBO, IAuthenticationBO authenticationBO)
        {
            _usuarioBO = usuarioBO;
            _authenticationBO = authenticationBO;
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
            if (String.IsNullOrEmpty(request.google_token))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_token_google_empty"),
                    new JProperty("error_description", "Empty Google token.")
                ));
            }
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
                    new JProperty("flgCelularValidado", respAuthGoogle.flgCelularValidado),
                    new JProperty("flgCorreoValidado", respAuthGoogle.flgCorreoValidado),
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
            if (String.IsNullOrEmpty(request.facebook_token))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_token_facebook_empty"),
                    new JProperty("error_description", "Empty Facebook token.")
                ));
            }
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
                    new JProperty("flgCelularValidado", respAuthFacebook.flgCelularValidado),
                    new JProperty("flgCorreoValidado", respAuthFacebook.flgCorreoValidado),
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
        [Route("autenticacion/envio-verificacion-celular")]
        [HttpPost]
        public HttpResponseMessage EnviarSmsOrWhatsapp(EnviarSmsOrWhatsappRequest request)
        {
            string idLogTexto = Guid.NewGuid().ToString();
            if (String.IsNullOrEmpty(request.nroCelular))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_phone_number_empty"),
                    new JProperty("error_description", "Empty Phone Number.")
                ));
            }

            if (String.IsNullOrEmpty(request.tipoEnvio))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_send_type_empty"),
                    new JProperty("error_description", "Empty Send Type.")
                ));
            }

            if (request.tipoEnvio != TipoEnvioCodigoVerificacion.SMS && request.tipoEnvio != TipoEnvioCodigoVerificacion.WHATSAPP)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_send_type_not_reconized"),
                    new JProperty("error_description", "Not reconized Send Type.")
                ));
            }

            var cod_aplicacion = AplicationData.codAplicacion;
            var respEnvioSmsOrWhatsapp = _authenticationBO.EnviarSmsOrWhatsapp(request, cod_aplicacion, idLogTexto);
            if (respEnvioSmsOrWhatsapp.codeRes == HttpStatusCode.OK)
            {
                if (respEnvioSmsOrWhatsapp.codeRes == HttpStatusCode.OK)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new { Message = respEnvioSmsOrWhatsapp.messageRes});
                }
                return Request.CreateResponse(respEnvioSmsOrWhatsapp.codeRes,
                    new MensajeHttpResponse() { Message = respEnvioSmsOrWhatsapp.messageRes });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_send_a_verify_code"),
                    new JProperty("error_description", "Could not send verify code.")
                ));
            }
        }

        [ApplicationAuthenticationFilter]
        [Route("autenticacion/envio-verificacion-correo")]
        [HttpPost]
        public HttpResponseMessage EnviarCodigoVerificacionCorreo(EnviarCodigoVerificacionCorreoRequest request)
        {
            string idLogTexto = Guid.NewGuid().ToString();
            if (String.IsNullOrEmpty(request.correo))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_email_empty"),
                    new JProperty("error_description", "Empty Email.")
                ));
            }

            var cod_aplicacion = AplicationData.codAplicacion;
            var respEnvioCorreo = _authenticationBO.EnviarCodigoVerificacionCorreo(request, cod_aplicacion, idLogTexto);
            if (respEnvioCorreo.codeRes == HttpStatusCode.OK)
            {
                if (respEnvioCorreo.codeRes == HttpStatusCode.OK)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new { Message = respEnvioCorreo.messageRes });
                }
                return Request.CreateResponse(respEnvioCorreo.codeRes,
                    new MensajeHttpResponse() { Message = respEnvioCorreo.messageRes });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_send_a_verify_code"),
                    new JProperty("error_description", "Could not send verify code.")
                ));
            }
        }

        [ApplicationAuthenticationFilter]
        [Route("envio-correo-codigo-recuperacion-clave")]
        [HttpPost]
        public HttpResponseMessage EnviarCorreoCodigoRecuperacionClave(EnviarCorreoCodigoRecuperacionClaveRequest request)
        {
            string idLogTexto = Guid.NewGuid().ToString();
            if (String.IsNullOrEmpty(request.correo))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_email_empty"),
                    new JProperty("error_description", "Empty Email.")
                ));
            }

            var cod_aplicacion = AplicationData.codAplicacion;
            var respEnvioCorreo = _authenticationBO.EnviarCorreoCodigoRecuperacionClave(request, cod_aplicacion, idLogTexto);
            if (respEnvioCorreo.codeRes == HttpStatusCode.OK)
            {
                if (respEnvioCorreo.codeRes == HttpStatusCode.OK)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new { Message = respEnvioCorreo.messageRes });
                }
                return Request.CreateResponse(respEnvioCorreo.codeRes,
                    new MensajeHttpResponse() { Message = respEnvioCorreo.messageRes });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_send_a_recovery_code"),
                    new JProperty("error_description", "Could not send recovery code.")
                ));
            }
        }

        [ApplicationAuthenticationFilter]
        [Route("autenticacion/verificar-codigo-verificacion-correo")]
        [HttpPost]
        public HttpResponseMessage VerificarCodigoVerificacionCorreo(VerificarCodigoVerificacionCorreoRequest request)
        {
            string idLogTexto = Guid.NewGuid().ToString();

            if (String.IsNullOrEmpty(request.codigoVerificacion))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_verify_code_empty"),
                    new JProperty("error_description", "Empty Verify Code.")
                ));
            }

            if (String.IsNullOrEmpty(request.correo))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_email_empty"),
                    new JProperty("error_description", "Empty Email.")
                ));
            }

            var cod_aplicacion = AplicationData.codAplicacion;
            var respEnvioCorreo = _authenticationBO.VerificarCodigoVerificacionCorreo(request, cod_aplicacion, idLogTexto);
            if (respEnvioCorreo.codeRes == HttpStatusCode.OK)
            {
                if (respEnvioCorreo.codeRes == HttpStatusCode.OK)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new { Message = respEnvioCorreo.messageRes });
                }
                return Request.CreateResponse(respEnvioCorreo.codeRes,
                    new MensajeHttpResponse() { Message = respEnvioCorreo.messageRes });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_verify_a_verify_code"),
                    new JProperty("error_description", "Could not verify code.")
                ));
            }
        }

        [ApplicationAuthenticationFilter]
        [Route("autenticacion/verificar-codigo-verificacion-celular")]
        [HttpPost]
        public HttpResponseMessage VerificarCodigoVerificacionCelular(VerificarCodigoVerificacionCelularRequest request)
        {
            string idLogTexto = Guid.NewGuid().ToString();

            if (String.IsNullOrEmpty(request.codigoVerificacion))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_verify_code_empty"),
                    new JProperty("error_description", "Empty Verify Code.")
                ));
            }

            if (String.IsNullOrEmpty(request.nroCelular))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_phone_empty"),
                    new JProperty("error_description", "Empty Phone.")
                ));
            }

            var cod_aplicacion = AplicationData.codAplicacion;
            var respEnvioCorreo = _authenticationBO.VerificarCodigoVerificacionCelular(request, cod_aplicacion, idLogTexto);
            if (respEnvioCorreo.codeRes == HttpStatusCode.OK)
            {
                if (respEnvioCorreo.codeRes == HttpStatusCode.OK)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new { Message = respEnvioCorreo.messageRes });
                }
                return Request.CreateResponse(respEnvioCorreo.codeRes,
                    new MensajeHttpResponse() { Message = respEnvioCorreo.messageRes });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_verify_a_verify_code"),
                    new JProperty("error_description", "Could not verify code.")
                ));
            }
        }

        [ApplicationAuthenticationFilter]
        [Route("actualizar-clave")]
        [HttpPut]
        public HttpResponseMessage ActualizarClave(ActualizarClaveRequest request)
        {
            string idLogTexto = Guid.NewGuid().ToString();
            if (String.IsNullOrEmpty(request.username))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_username_empty"),
                    new JProperty("error_description", "Empty Username.")
                ));
            }

            if (String.IsNullOrEmpty(request.newPassword))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_password_empty"),
                    new JProperty("error_description", "Empty Password.")
                ));
            }

            var cod_aplicacion = AplicationData.codAplicacion;

            var resValRecoveryCode = _authenticationBO.VerificarCodigoVerificacionCorreo(new VerificarCodigoVerificacionCorreoRequest() { codigoVerificacion = request.recoveryCode, correo = request.username}, cod_aplicacion, idLogTexto);
            if (resValRecoveryCode.codeRes != HttpStatusCode.OK)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                   new JProperty("error", "invalid_recovery_code"),
                   new JProperty("error_description", "Invalid Recovery Code.")
               ));
            }

            var usuario = UserManager.FindByName(request.username);
            var passwordToken = UserManager.GeneratePasswordResetToken(usuario.Id);
            IdentityResult resultChangePassword = UserManager.ResetPassword(User.Identity.GetUserId(), passwordToken, request.newPassword);
            if (resultChangePassword.Succeeded)
            {
                usuario.cod_aplicacion_actualizacion = usuario.cod_usuario;
                usuario.fecha_actualizacion = DateTime.Now;
                usuario.cod_aplicacion_actualizacion = cod_aplicacion;
                var registerModified = UserManager.UpdateAsync(usuario);

                return Request.CreateResponse(HttpStatusCode.OK,
                   new { Message = "Clave actualizada" });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                        new MensajeHttpResponse() { Message = "Error interno al actualizar la clave." });
            }
        }

        [ApplicationAuthenticationFilter]
        [Route("autenticacion/registro-usuario-cliente")]
        [HttpPost]
        public async Task<HttpResponseMessage> RegistrarUsuarioCliente(RegistrarUsuarioClienteRequest request)
        {
            string idLogTexto = Guid.NewGuid().ToString();

            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_data_register_client_user"),
                    new JProperty("error_description", errores[0])
                ));
            }
            var cod_aplicacion = AplicationData.codAplicacion;

            var resValExisUsu = _usuarioBO.ValidarExistenciaUsuario(request.datosPersona.correo, request.datosPersona.celular, cod_aplicacion, idLogTexto);
            trs_usuario userSearch;
            if (resValExisUsu.codeRes == HttpStatusCode.OK)
            {
                var resRol = _usuarioBO.ObtenerRolPorCodRol(Roles.CLIENTE, cod_aplicacion, idLogTexto);
                if (resRol.codeRes != HttpStatusCode.OK)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                        new JProperty("error", "invalid_register_user"),
                        new JProperty("error_description", "User could not be registered.(1)")
                    ));
                }

                var resRegPersona = _usuarioBO.RegistrarPersona(request.datosPersona, cod_aplicacion, idLogTexto);
                if (resRegPersona.codeRes != HttpStatusCode.OK)
                {
                    return Request.CreateResponse(resRegPersona.codeRes, new JObject(
                        new JProperty("error", "person_create_no_complete"),
                        new JProperty("error_description", "Person could not be registered.")
                    ));
                }

                trs_usuario usuario = new trs_usuario();
                usuario.id_rol = resRol.idRol;
                usuario.id_persona = resRegPersona.idPersona;
                usuario.username = request.datosPersona.correo.Trim();
                usuario.activo = true;
                usuario.eliminado = false;
                usuario.fecha_registro = DateTime.Now;
                usuario.password = request.contrasenia.Trim();
                usuario.cod_aplicacion_registro = cod_aplicacion;

                var respCreateUser = UserManager.CreateAsync(usuario, usuario.password);
                if (!respCreateUser.Result.Succeeded)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                        new JProperty("error", "invalid_register_user"),
                        new JProperty("error_description", "User could not be registered.")
                    ));
                }
            }

            if (resValExisUsu.codeRes == HttpStatusCode.Continue)
            {
                userSearch = UserManager.FindByName(request.datosPersona.correo);
                if (String.IsNullOrEmpty(userSearch.password))
                {
                    UserManager.AddPassword(userSearch.Id, request.contrasenia);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_duplicated_user"),
                    new JProperty("error_description", "There is a user with the same email.")
                ));
            }

            var resDataSesion = _usuarioBO.ObtenerDataSesion(userSearch.id_usuario, cod_aplicacion, idLogTexto);
            if (resDataSesion.codeRes != HttpStatusCode.OK)
            {
                return Request.CreateResponse(resDataSesion.codeRes, new JObject(
                    new JProperty("error", "error_data_sesion"),
                    new JProperty("error_description", "Data sesion could not be obtained.")
                ));
            }

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
                new JProperty("nombres", resDataSesion.nombres),
                new JProperty("apellidos", resDataSesion.apellidos),
                new JProperty("correo", resDataSesion.correo),
                new JProperty("flgMostrarRegistroUsuario", resDataSesion.flgMostrarRegistroUsuario),
                new JProperty("flgCelularValidado", resDataSesion.flgCelularValidado),
                new JProperty("flgCorreoValidado", resDataSesion.flgCorreoValidado),
                new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
                new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString())
            );

            return Request.CreateResponse(HttpStatusCode.OK, blob);
        }

        [Authorize]
        [Route("registrar-dispositivo")]
        [HttpPost]
        public HttpResponseMessage RegistrarDispositivo(RegistrarDispositivoRequest request)
        {
            string idLogTexto = Guid.NewGuid().ToString();
            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_data_register_device_user"),
                    new JProperty("error_description", errores[0])
                ));
            }

            var cod_aplicacion = AplicationData.codAplicacion;
            var cod_usuario = User.Identity.GetUserId();
            var respEnvioCorreo = _usuarioBO.RegistrarDispositivo(request, cod_usuario, cod_aplicacion, idLogTexto);
            if (respEnvioCorreo.codeRes == HttpStatusCode.OK)
            {
                if (respEnvioCorreo.codeRes == HttpStatusCode.OK)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new { Message = respEnvioCorreo.messageRes });
                }
                return Request.CreateResponse(respEnvioCorreo.codeRes,
                    new MensajeHttpResponse() { Message = respEnvioCorreo.messageRes });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new JObject(
                    new JProperty("error", "invalid_register_device"),
                    new JProperty("error_description", "Could not register device.")
                ));
            }
        }
    }
}