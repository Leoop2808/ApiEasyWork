using ApiEasyWork.App_Start;
using EasyWorkDataAccess.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiEasyWork.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            EasyWorkDBEntities ctxBD = new EasyWorkDBEntities();

            string clientId = string.Empty;
            string clientSecret = string.Empty;
            
            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {

                //new LoggerRepository().registroLogError(null, null, "invalid_client", "Client credentials could not be retrieved through the Authorization header.");

                context.SetError("invalid_client", "Client credentials could not be retrieved through the Authorization header.");
                return Task.FromResult<object>(null);
            }

            //Check the existence of by calling the ValidateClient method
            mst_aplicacion client = ctxBD.mst_aplicacion.FirstOrDefault(user =>
                                    user.nombre_aplicacion == clientId &&
                                    user.access_id == clientSecret);


            if (client == null)
            {
                //new LoggerRepository().registroLogError(null, null, "invalid_client", "Client credentials are invalid.");

                // Client could not be validated.
                context.SetError("invalide_client", "Client credentials are invalid.");
                return Task.FromResult<object>(null);
            }
            else
            {
                if (client.activo == null || client.activo == false)
                {
                    //new LoggerRepository().registroLogError(null, null, "invalid_client", "Client is inactive.");

                    context.SetError("invalid_client", "Client is inactive.");
                    return Task.FromResult<object>(null);
                }
                // Client has been verified.
                context.OwinContext.Set<mst_aplicacion>("ta:client", client);

                context.OwinContext.Set<string>("ta:clientAllowedOrigin", client.allowedorigin);
                context.OwinContext.Set<string>("ta:clientRefreshTokenLifeTime", client.refreshTokenLife.ToString());
                context.Validated();
                return Task.FromResult<object>(null);
            }
            context.Validated();
            return Task.FromResult<object>(null);
        }



        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                EasyWorkDBEntities ctxBD = new EasyWorkDBEntities();


                var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();


                trs_usuario user = await userManager.FindAsync(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "Usuario o clave incorrectos.");
                    return;
                }
                else
                {
                    if (user.activo == false || user.activo == null)
                    {
                        context.SetError("invalid_grant", "El usuario o clave es incorrecto.");
                        return;
                    }
                    if (user.eliminado == true)
                    {
                        context.SetError("invalid_grant", "Usuario inexistente.");
                        return;
                    }
                }

                ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, OAuthDefaults.AuthenticationType);

                var clienteAplicativo = context.OwinContext.Get<mst_aplicacion>("ta:client");

                oAuthIdentity.AddClaim(new Claim("user_id", user.id_usuario.ToString()));
                oAuthIdentity.AddClaim(new Claim("user_code", user.cod_usuario.ToString()));
                oAuthIdentity.AddClaim(new Claim("cod_aplicacion", clienteAplicativo.cod_aplicacion.ToString()));


                ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager, CookieAuthenticationDefaults.AuthenticationType);
                AuthenticationProperties properties = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "Username", user.username
                    }
                });

                AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
                context.Validated(ticket);
                context.Request.Context.Authentication.SignIn(cookiesIdentity);

            }
            catch (Exception)
            {
                context.SetError("error_server", "Error server");
                return;
            }

        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(trs_usuario userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "username", userName.UserName.ToString() },
            };
            return new AuthenticationProperties(data);
        }
    }
}