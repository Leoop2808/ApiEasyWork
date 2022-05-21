using EasyWorkBusiness.Contrato;
using EasyWorkDataAccess.Contrato;
using EasyWorkEntities.Authentication.Request;
using EasyWorkEntities.Authentication.Response;
using EasyWorkEntities.Helpers.Request;
using EasyWorkHelpers;
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
                response.flgCorreoValidado = true;
                response.flgCelularValidado = resRegDtGoogle.flgCelularValidado;
                response.flgMostrarRegistroUsuario = resRegDtGoogle.flgMostrarRegistroUsuario;

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
                response.flgCorreoValidado = true;
                response.flgCelularValidado = resRegDtFacebook.flgCelularValidado;
                response.flgMostrarRegistroUsuario = resRegDtFacebook.flgMostrarRegistroUsuario;

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
                var verifyCode = Helpers.GenerateCode(6);
                var codTipoCodigoVerificacion = (request.tipoEnvio == TipoEnvioCodigoVerificacion.SMS) ? TipoEnvioCodigoVerificacion.COD_SMS : TipoEnvioCodigoVerificacion.COD_WHATSAPP;
                var resRegCodigoVerificacion = _authenticationDO.RegistrarCodigoVerificacion(verifyCode, String.Empty, request.nroCelular,
                    codTipoCodigoVerificacion,cod_aplicacion, idLogTexto);
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
                var verifyCode = Helpers.GenerateCode(6);
                
                var resRegCodigoVerificacion = _authenticationDO.RegistrarCodigoVerificacion(verifyCode, request.correo, String.Empty,
                    TipoEnvioCodigoVerificacion.COD_CORREO, cod_aplicacion, idLogTexto);
                if (resRegCodigoVerificacion.codeRes != HttpStatusCode.Created)
                {
                    response.codeRes = resRegCodigoVerificacion.codeRes;
                    response.messageRes = resRegCodigoVerificacion.messageRes;
                    return response;
                }

                var resEnvioCodigo = Helpers.EnviarCorreo(
                    new EnviarCorreoRequest() {
                        correo = request.correo,
                        asunto = AsuntoEmails.VALIDATION_EMAIL_VERIFY_CODE,
                        body = BodyEmails.VALIDATION_EMAIL_VERIFY_CODE.Replace("@verifyCode", verifyCode),
                        idLogTexto = idLogTexto,
                        cod_aplicacion = cod_aplicacion
                    }
                );

                response.codeRes = resEnvioCodigo.codeRes;
                response.messageRes = resEnvioCodigo.messageRes;

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
        public EnviarCorreoCodigoRecuperacionClaveResponse EnviarCorreoCodigoRecuperacionClave(EnviarCorreoCodigoRecuperacionClaveRequest request, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var response = new EnviarCorreoCodigoRecuperacionClaveResponse();
                var verifyCode = Helpers.GenerateCode(6);

                var resValExisUsuCorreo = _authenticationDO.ValidarExistenciaUsuarioCorreo(request.correo, cod_aplicacion, idLogTexto);
                if (resValExisUsuCorreo.codeRes != HttpStatusCode.OK)
                {
                    response.codeRes = resValExisUsuCorreo.codeRes;
                    response.messageRes = resValExisUsuCorreo.messageRes;
                    return response;
                }

                var resRegCodigoVerificacion = _authenticationDO.RegistrarCodigoVerificacion(verifyCode, request.correo, String.Empty,
                    TipoEnvioCodigoVerificacion.COD_CORREO, cod_aplicacion, idLogTexto);
                if (resRegCodigoVerificacion.codeRes != HttpStatusCode.Created)
                {
                    response.codeRes = resRegCodigoVerificacion.codeRes;
                    response.messageRes = resRegCodigoVerificacion.messageRes;
                    return response;
                }

                var resEnvioCodigo = Helpers.EnviarCorreo(
                    new EnviarCorreoRequest()
                    {
                        correo = request.correo,
                        asunto = AsuntoEmails.FORGOT_PASSWORD_VERIFY_CODE,
                        body = BodyEmails.FORGOT_PASSWORD_VERIFY_CODE.Replace("@verifyCode", verifyCode),
                        idLogTexto = idLogTexto,
                        cod_aplicacion = cod_aplicacion
                    }
                );

                response.codeRes = resEnvioCodigo.codeRes;
                response.messageRes = resEnvioCodigo.messageRes;

                return response;
            }
            catch (Exception e)
            {
                log.Error($"AuthenticationBO ({idLogTexto}) ->  EnviarCorreoCodigoRecuperacionClave. Request: {JsonConvert.SerializeObject(request)}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno al enviar código de recuperación. " +
                    "Detalle error: " + JsonConvert.SerializeObject(e));
                return new EnviarCorreoCodigoRecuperacionClaveResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al enviar código de recuperación."
                };
            }
        }
        public VerificarCodigoVerificacionCorreoResponse VerificarCodigoVerificacionCorreo(VerificarCodigoVerificacionCorreoRequest request, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var response = new VerificarCodigoVerificacionCorreoResponse();

                var resRegCodigoVerificacion = _authenticationDO.VerificarCodigoVerificacion(request.codigoVerificacion, request.correo, String.Empty, false, cod_aplicacion, idLogTexto);

                response.codeRes = resRegCodigoVerificacion.codeRes;
                response.messageRes = resRegCodigoVerificacion.messageRes;
                return response;
            }
            catch (Exception e)
            {
                log.Error($"AuthenticationBO ({idLogTexto}) ->  VerificarCodigoVerificacionCorreo. Request: {JsonConvert.SerializeObject(request)}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno al verificar código de verificación. " +
                    "Detalle error: " + JsonConvert.SerializeObject(e));
                return new VerificarCodigoVerificacionCorreoResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al verificar código de verificación."
                };
            }
        }
        public VerificarCodigoVerificacionCelularResponse VerificarCodigoVerificacionCelular(VerificarCodigoVerificacionCelularRequest request, string cod_aplicacion, string idLogTexto)
        {
            try
            {
                var response = new VerificarCodigoVerificacionCelularResponse();

                var resRegCodigoVerificacion = _authenticationDO.VerificarCodigoVerificacion(request.codigoVerificacion, String.Empty, request.nroCelular, true, cod_aplicacion, idLogTexto);

                response.codeRes = resRegCodigoVerificacion.codeRes;
                response.messageRes = resRegCodigoVerificacion.messageRes;
                return response;
            }
            catch (Exception e)
            {
                log.Error($"AuthenticationBO ({idLogTexto}) ->  VerificarCodigoVerificacionCelular. Request: {JsonConvert.SerializeObject(request)}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno al verificar código de verificación. " +
                    "Detalle error: " + JsonConvert.SerializeObject(e));
                return new VerificarCodigoVerificacionCelularResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al verificar código de verificación."
                };
            }
        }
        public EnviarSmsOrWhatsappAutenticacionResponse EnviarSmsOrWhatsappAutenticacion(EnviarSmsOrWhatsappAutenticacionRequest request, string cod_aplicacion, string idLogTexto)
        {
            try
            {
                var response = new EnviarSmsOrWhatsappAutenticacionResponse();

                var resValExis = _authenticationDO.ValidarExistenciaUsuarioCelular(verifyCode, String.Empty, request.nroCelular, cod_aplicacion, idLogTexto);
                if (resValExis.codeRes != HttpStatusCode.OK)
                {
                    response.codeRes = resValExis.codeRes;
                    response.messageRes = resValExis.messageRes;
                    return response;
                }

                var verifyCode = Helpers.GenerateCode(6);
                var codTipoCodigoVerificacion = (request.tipoEnvio == TipoEnvioCodigoVerificacion.SMS) ? TipoEnvioCodigoVerificacion.COD_AUTH_SMS : TipoEnvioCodigoVerificacion.COD_AUTH_WHATSAPP;
                var resRegCodigoVerificacion = _authenticationDO.RegistrarCodigoVerificacion(verifyCode, String.Empty, request.nroCelular,
                    codTipoCodigoVerificacion, cod_aplicacion, idLogTexto);
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
                log.Error($"AuthenticationBO ({idLogTexto}) ->  EnviarSmsOrWhatsappAutenticacion. Request: {JsonConvert.SerializeObject(request)}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno al enviar código de verificación. " +
                    "Detalle error: " + JsonConvert.SerializeObject(e));
                return new EnviarSmsOrWhatsappAutenticacionResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al enviar código de verificación."
                };
            }
        }
        public AutenticarCelularResponse AutenticarCelular(AutenticarCelularRequest request, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var response = new AutenticarCelularResponse();

                var resVerCodAut = _authenticationDO.VerificarCodigoAutenticacion(request.codVerificacion, request.nroCelular, request.latitud, request.longitud, cod_aplicacion, idLogTexto);

                response.codeRes = resVerCodAut.codeRes;
                response.messageRes = resVerCodAut.messageRes;
                response.flgCorreoValidado = true;
                response.flgCelularValidado = resVerCodAut.flgCelularValidado;
                response.flgMostrarRegistroUsuario = resVerCodAut.flgMostrarRegistroUsuario;

                if (resVerCodAut.codeRes != HttpStatusCode.OK)
                {
                    return response;
                }

                var resDataPrincipalUsu = _authenticationDO.ObtenerDataPrincipalUsuario(resVerCodAut.codUsuario, resVerCodAut.idUsuario, MedioAcceso.COD_AUTH_CELULAR, cod_aplicacion, idLogTexto);

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
                log.Error($"AuthenticationBO ({idLogTexto}) ->  AutenticarCelular. Request: {JsonConvert.SerializeObject(request)}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno al autenticar mediante celular. " +
                    "Detalle error: " + JsonConvert.SerializeObject(e));
                return new AutenticarCelularResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al autenticar mediante celular."
                };
            }
        }
    }
}
