using EasyWorkBusiness.Contrato;
using EasyWorkDataAccess.Contrato;
using EasyWorkEntities.Authentication.Request;
using EasyWorkEntities.Authentication.Response;
using log4net;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using static EasyWorkEntities.Constantes;

namespace EasyWorkBusiness.Implementacion
{
    public class AuthenticationBO : IAuthenticationBO
    {
        private string _twilioAccountSid = ConfigurationManager.AppSettings["TWILIO_ACCOUNT_SID"];
        private string _twilioAuthToken = ConfigurationManager.AppSettings["TWILIO_AUTH_TOKEN"];
        private string _twilioPhoneNumber = ConfigurationManager.AppSettings["TWILIO_PHONE_NUMBER"];
        private string _twilioMessagingServiceSid = ConfigurationManager.AppSettings["TWILIO_MESSAGING_SERVICE_SID"];
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
                var resDataGoogle = _authenticationDO.ObtenerDataGoogle(request.google_token, cod_aplicacion, idLogTexto);
                if (resDataGoogle.codeRes != HttpStatusCode.OK)
                {
                    response.codeRes = resDataGoogle.codeRes;
                    response.messageRes = resDataGoogle.messageRes;
                    return response;
                }

                var resRegDtGoogle = _authenticationDO.RegistrarDatosGoogle(resDataGoogle.dataGoogle, request.latitud, request.longitud, request.google_token, cod_aplicacion, idLogTexto);

                response.codeRes = resRegDtGoogle.codeRes;
                response.messageRes = resRegDtGoogle.messageRes;

                if (resRegDtGoogle.codeRes != HttpStatusCode.OK)
                {
                    return response;
                }
                
                var resDataPrincipalUsu = _authenticationDO.ObtenerDataPrincipalUsuario(resRegDtGoogle.codUsuarioCreado, resRegDtGoogle.idUsuarioCreado, MedioAcceso.COD_AUTH_GMAIL, cod_aplicacion, idLogTexto);

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
                var resDataFacebook = _authenticationDO.ObtenerDataFacebook(request.facebook_token, cod_aplicacion, idLogTexto);
                if (resDataFacebook.codeRes != HttpStatusCode.OK)
                {
                    response.codeRes = resDataFacebook.codeRes;
                    response.messageRes = resDataFacebook.messageRes;
                    return response;
                }

                var resRegDtFacebook = _authenticationDO.RegistrarDatosFacebook(resDataFacebook.dataFacebook, request.latitud, request.longitud, request.facebook_token, cod_aplicacion, idLogTexto);

                response.codeRes = resRegDtFacebook.codeRes;
                response.messageRes = resRegDtFacebook.messageRes;

                if (resRegDtFacebook.codeRes != HttpStatusCode.OK)
                {
                    return response;
                }

                var resDataPrincipalUsu = _authenticationDO.ObtenerDataPrincipalUsuario(resRegDtFacebook.codUsuarioCreado, resRegDtFacebook.idUsuarioCreado, MedioAcceso.COD_AUTH_FACEBOOK, cod_aplicacion, idLogTexto);

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

        public EnviarSmsOrWhatsappResponse EnviarSmsOrWhatsapp(EnviarSmsOrWhatsappRequest request, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var response = new EnviarSmsOrWhatsappResponse();
                var verifyCode = GenerateCode(6);

                var resRegCodigoVerificacion = _authenticationDO.RegistrarCodigoVerificacion(verifyCode, String.Empty, request.nroCelular,
                    true, false, (request.tipoEnvio == TipoEnvioCodigoVerificacion.WHATSAPP ? false : true),cod_aplicacion, idLogTexto);
                if (resRegCodigoVerificacion.codeRes != HttpStatusCode.Created)
                {
                    response.codeRes = resRegCodigoVerificacion.codeRes;
                    response.messageRes = resRegCodigoVerificacion.messageRes;
                    return response;
                }

                var resEnvioCodigo = request.tipoEnvio == TipoEnvioCodigoVerificacion.WHATSAPP ? EnviarCodigoWhatsapp(request.nroCelular, verifyCode, cod_aplicacion, idLogTexto) : EnviarCodigoSms(request.nroCelular, verifyCode, cod_aplicacion, idLogTexto);

                response.codeRes = resEnvioCodigo.codeRes;
                response.messageRes = resEnvioCodigo.messageRes;

                return response;
            }
            catch (Exception e)
            {
                log.Error($"AuthenticationBO ({idLogTexto}) ->  EnviarSmsOrWhatsapp. Request: {JsonConvert.SerializeObject(request)}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno al enviar código de verificación. " +
                    "Detalle error: " + JsonConvert.SerializeObject(e));
                return new EnviarSmsOrWhatsappResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al enviar código de verificación."
                };
            }
        }

        public EnviarSmsOrWhatsappResponse EnviarCodigoWhatsapp(string nroCelular, string verifyCode, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var response = new EnviarSmsOrWhatsappResponse();
                TwilioClient.Init(_twilioAccountSid, _twilioAuthToken);
                var messageOptions = new CreateMessageOptions(new PhoneNumber(TipoEnvioCodigoVerificacion.TWILIO_WHATSAPP + $":+51{nroCelular}"));
                messageOptions.From = new PhoneNumber(TipoEnvioCodigoVerificacion.TWILIO_WHATSAPP + ":" + _twilioPhoneNumber);
                messageOptions.Body = ContentMessageVerifyCode.BODY_WHATSAPP.Replace("@verifyCode", verifyCode);

                var message = MessageResource.Create(messageOptions);
                if (message.Status.ToString() == MessageResource.StatusEnum.Queued.ToString())
                {
                    response.codeRes = HttpStatusCode.OK;
                    response.messageRes = "Código de verificación enviado a Whatsapp";
                }
                else
                {
                    response.codeRes = HttpStatusCode.BadRequest;
                    response.messageRes = "No se pudo enviar el código de verificación a Whatsapp";
                }

                log.Info($"AuthenticationBO ({idLogTexto}) ->  EnviarCodigoWhatsapp. Aplicacion: {cod_aplicacion}." +
                  "Numero de celular: " + nroCelular + ". " +
                  "Código de verificación: " + verifyCode + ". " +
                  "Mensaje al cliente: Código de verificación enviado por Whatsapp. " +
                  "Respuesta Twilio: " + JsonConvert.SerializeObject(message.Body));

                return response;
            }
            catch (Exception e)
            {
                log.Error($"AuthenticationBO ({idLogTexto}) ->  EnviarCodigoWhatsapp. Aplicacion: {cod_aplicacion}." +
                   "Numero de celular: " + nroCelular + ". " +
                   "Código de verificación: " + verifyCode + ". " +
                   "Mensaje al cliente: Error interno al enviar código de verificación por WhatsApp. " +
                   "Detalle error: " + JsonConvert.SerializeObject(e));
                return new EnviarSmsOrWhatsappResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al enviar código de verificación por WhatsApp."
                };
            }
        }

        public EnviarSmsOrWhatsappResponse EnviarCodigoSms(string nroCelular, string verifyCode, string cod_aplicacion, string idLogTexto)
        {
            try
            {
                var response = new EnviarSmsOrWhatsappResponse();
                TwilioClient.Init(_twilioAccountSid, _twilioAuthToken);
                var messageOptions = new CreateMessageOptions(new PhoneNumber($"+51{nroCelular}"));
                messageOptions.MessagingServiceSid = _twilioMessagingServiceSid;
                messageOptions.Body = ContentMessageVerifyCode.BODY_SMS.Replace("@verifyCode", verifyCode);

                var message = MessageResource.Create(messageOptions);
                if (message.Status.ToString() == MessageResource.StatusEnum.Accepted.ToString())
                {
                    response.codeRes = HttpStatusCode.OK;
                    response.messageRes = "Código de verificación enviado por SMS";
                }
                else
                {
                    response.codeRes = HttpStatusCode.BadRequest;
                    response.messageRes = "No se pudo enviar el código de verificación por SMS";
                }

                log.Info($"AuthenticationBO ({idLogTexto}) ->  EnviarCodigoSms. Aplicacion: {cod_aplicacion}." +
                  "Numero de celular: " + nroCelular + ". " +
                  "Código de verificación: " + verifyCode + ". " +
                  "Mensaje al cliente: Código de verificación enviado por . " +
                  "Respuesta Twilio: " + JsonConvert.SerializeObject(message.Body));

                return response;
            }
            catch (Exception e)
            {
                log.Error($"AuthenticationBO ({idLogTexto}) ->  EnviarCodigoSms. Aplicacion: {cod_aplicacion}." +
                   "Numero de celular: " + nroCelular + ". " +
                   "Código de verificación: " + verifyCode + ". " +
                   "Mensaje al cliente: Error interno al enviar código de verificación por SMS. " +
                   "Detalle error: " + JsonConvert.SerializeObject(e));
                return new EnviarSmsOrWhatsappResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al enviar código de verificación por SMS."
                };
            }
        }

        public EnviarCodigoVerificacionCorreoResponse EnviarCodigoVerificacionCorreo(EnviarCodigoVerificacionCorreoRequest request, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var response = new EnviarCodigoVerificacionCorreoResponse();
                var verifyCode = GenerateCode(6);

                var resRegCodigoVerificacion = _authenticationDO.RegistrarCodigoVerificacion(verifyCode, request.correo, String.Empty,
                    false, true, null, cod_aplicacion, idLogTexto);
                if (resRegCodigoVerificacion.codeRes != HttpStatusCode.Created)
                {
                    response.codeRes = resRegCodigoVerificacion.codeRes;
                    response.messageRes = resRegCodigoVerificacion.messageRes;
                    return response;
                }

                //var resEnvioCodigo = 

                //response.codeRes = resEnvioCodigo.codeRes;
                //response.messageRes = resEnvioCodigo.messageRes;

                return response;
            }
            catch (Exception e)
            {
                log.Error($"AuthenticationBO ({idLogTexto}) ->  EnviarCodigoVerificacionCorreo. Request: {JsonConvert.SerializeObject(request)}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno al enviar código de verificación. " +
                    "Detalle error: " + JsonConvert.SerializeObject(e));
                return new EnviarCodigoVerificacionCorreoResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al enviar código de verificación."
                };
            }
        }
        public string GenerateCode(int p_CodeLength)
        {
            string result = "";
            string pattern = "01234567890123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
            Random myRndGenerator = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < p_CodeLength; i++)
            {
                int mIndex = myRndGenerator.Next(pattern.Length);
                result += pattern[mIndex];
            }

            return result;
        }
    }
}
