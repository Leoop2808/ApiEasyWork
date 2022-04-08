using EasyWorkBusiness.Contrato;
using EasyWorkDataAccess.Contrato;
using EasyWorkEntities;
using EasyWorkEntities.Authentication.Request;
using EasyWorkEntities.Authentication.Response;
using log4net;
using Newtonsoft.Json;
using System;
using System.Net;

namespace EasyWorkBusiness.Implementacion
{
    public class AuthenticationBO : IAuthenticationBO
    {
        private readonly ILog log = LogManager.GetLogger(typeof(AuthenticationBO));
        readonly IAuthenticationDO _authenticationDO;
        public AuthenticationBO(IAuthenticationDO authenticationDO)
        {
            _authenticationDO = authenticationDO;
        }

        public AutenticarGoogleResponse AutenticarGoogle(AutenticarGoogleRequest request, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var response = new AutenticarGoogleResponse();
                var resRegDtGoogle = _authenticationDO.RegistrarDatosGoogle(request, cod_aplicacion, idLogTexto);

                response.codeRes = resRegDtGoogle.codeRes;
                response.messageRes = resRegDtGoogle.messageRes;

                if (resRegDtGoogle.codeRes != HttpStatusCode.OK)
                {
                    return response;
                }
                
                var resDataPrincipalUsu = _authenticationDO.ObtenerDataPrincipalUsuario(resRegDtGoogle.codUsuarioCreado, Constantes.COD_AUTH_GMAIL, cod_aplicacion, idLogTexto);

                if (resDataPrincipalUsu.codeRes != HttpStatusCode.OK)
                {
                    response.codeRes = resDataPrincipalUsu.codeRes;
                    response.messageRes = resDataPrincipalUsu.messageRes;
                    return response;
                }

                response.datos = resDataPrincipalUsu.datos;

                return response;
            }
            catch (Exception e)
            {
                log.Error($"AuthenticationBO ({idLogTexto}) ->  AutenticarGoogle. Request: {JsonConvert.SerializeObject(request)}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno al autentiicar mediante google. " +
                    "Detalle error: " + JsonConvert.SerializeObject(e));
                return new AutenticarGoogleResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al autenticar mediante google."
                };
            }
        }
        public AutenticarFacebookResponse AutenticarFacebook(AutenticarFacebookRequest request, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var response = new AutenticarFacebookResponse();
                var resRegDtGoogle = _authenticationDO.RegistrarDatosFacebook(request, cod_aplicacion, idLogTexto);

                response.codeRes = resRegDtGoogle.codeRes;
                response.messageRes = resRegDtGoogle.messageRes;

                if (resRegDtGoogle.codeRes != HttpStatusCode.OK)
                {
                    return response;
                }

                var resDataPrincipalUsu = _authenticationDO.ObtenerDataPrincipalUsuario(resRegDtGoogle.codUsuarioCreado, Constantes.COD_AUTH_FACEBOOK, cod_aplicacion, idLogTexto);

                if (resDataPrincipalUsu.codeRes != HttpStatusCode.OK)
                {
                    response.codeRes = resDataPrincipalUsu.codeRes;
                    response.messageRes = resDataPrincipalUsu.messageRes;
                    return response;
                }

                response.datos = resDataPrincipalUsu.datos;

                return response;
            }
            catch (Exception e)
            {
                log.Error($"AuthenticationBO ({idLogTexto}) ->  AutenticarFacebook. Request: {JsonConvert.SerializeObject(request)}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno al autenticar mediante facebook. " +
                    "Detalle error: " + JsonConvert.SerializeObject(e));
                return new AutenticarFacebookResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al autenticar mediante facebook."
                };
            }
        }
    }
}
