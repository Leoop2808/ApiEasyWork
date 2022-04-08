using AutoMapper;
using EasyWorkDataAccess.Contrato;
using EasyWorkDataAccess.Models;
using EasyWorkEntities.Authentication.Request;
using EasyWorkEntities.Authentication.Response;
using log4net;
using Newtonsoft.Json;
using System;
using System.Net;

namespace EasyWorkDataAccess.Implementacion
{
    public class AuthenticationDO : IAuthenticationDO
    {
        private readonly ILog log = LogManager.GetLogger(typeof(AuthenticationDO));
        public RegistrarDatosGoogleResponse RegistrarDatosGoogle(AutenticarGoogleRequest request, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var resRegDtGoogle = ctx.SP_REGISTRAR_DATOS_GOOGLE(request.account, request.idToken, request.id, 
                request.email, request.displayName, request.photoUrl, request.serverAuthCode, request.idExpired,
                request.accountObj.name, request.accountObj.type, cod_aplicacion).FirstOrDefault();

                if (resRegDtGoogle != null)
                {
                    if (resRegDtGoogle.codRes.GetValueOrDefault() == 201)
                    {
                        return new RegistrarDatosGoogleResponse()
                        {
                            codeRes = HttpStatusCode.Created,
                            messageRes = resRegDtGoogle.mensajeRes,
                            codUsuarioCreado = resRegDtGoogle.codUsuarioCreado,
                            flgMostrarRegistroUsuario = resRegDtGoogle.flgMostrarRegistroUsuario.GetValueOrDefault()
                        };
                    }
                    else
                    {
                        log.Error($"AuthenticationDO ({idLogTexto}) ->  RegistrarDatosGoogle. Aplicacion: {cod_aplicacion}. " +
                        $"Data: {JsonConvert.SerializeObject(request)}. " +
                        "Mensaje al cliente: No se obtuvo respuesta al almacenar los datos de google. " +
                        "Detalle error: " + "No se obtuvo respuesta al almacenar los datos de google en la base de datos.");
                        return new RegistrarDatosGoogleResponse()
                        {
                            codeRes = HttpStatusCode.NoContent,
                            messageRes = "No se obtuvo respuesta al almacenar los datos de google."
                        };
                    }
                }
                else
                {
                    log.Error($"AuthenticationDO ({idLogTexto}) ->  RegistrarDatosGoogle. Aplicacion: {cod_aplicacion}. " +
                    $"Data: {JsonConvert.SerializeObject(request)}. " +
                    "Mensaje al cliente: No se obtuvo respuesta al almacenar los datos de google. " +
                    "Detalle error: " + "No se obtuvo respuesta al almacenar los datos de google en la base de datos.");
                    return new RegistrarDatosGoogleResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al almacenar los datos de google."
                    };
                }                
            }
            catch (Exception e)
            {
                log.Error($"AuthenticationDO ({idLogTexto}) ->  RegistrarDatosGoogle. Aplicacion: {cod_aplicacion}. " +
                   $"Data: {JsonConvert.SerializeObject(request)}. " +
                   "Mensaje al cliente: Error interno al almacenar datos de google. " +
                   "Detalle error: " + JsonConvert.SerializeObject(e));
                return new RegistrarDatosGoogleResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "No se obtuvo respuesta al almacenar los datos de google."
                };
            }
        }
        public RegistrarDatosFacebookResponse RegistrarDatosFacebook(AutenticarFacebookRequest request, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var resRegDtGoogle = ctx.SP_REGISTRAR_DATOS_FACEBOOK(cod_aplicacion).FirstOrDefault();

                if (resRegDtGoogle != null)
                {
                    if (resRegDtGoogle.codRes.GetValueOrDefault() == 201)
                    {
                        return new RegistrarDatosFacebookResponse()
                        {
                            codeRes = HttpStatusCode.Created,
                            messageRes = resRegDtGoogle.mensajeRes,
                            codUsuarioCreado = resRegDtGoogle.codUsuarioCreado,
                            flgMostrarRegistroUsuario = resRegDtGoogle.flgMostrarRegistroUsuario.GetValueOrDefault()
                        };
                    }
                    else
                    {
                        log.Error($"AuthenticationDO ({idLogTexto}) ->  RegistrarDatosFacebook. Aplicacion: {cod_aplicacion}. " +
                        $"Data: {JsonConvert.SerializeObject(request)}. " +
                        "Mensaje al cliente: No se obtuvo respuesta al almacenar los datos de facebook. " +
                        "Detalle error: " + "No se obtuvo respuesta al almacenar los datos de facebook en la base de datos.");
                        return new RegistrarDatosFacebookResponse()
                        {
                            codeRes = HttpStatusCode.NoContent,
                            messageRes = "No se obtuvo respuesta al almacenar los datos de facebook."
                        };
                    }
                }
                else
                {
                    log.Error($"AuthenticationDO ({idLogTexto}) ->  RegistrarDatosFacebook. Aplicacion: {cod_aplicacion}. " +
                    $"Data: {JsonConvert.SerializeObject(request)}. " +
                    "Mensaje al cliente: No se obtuvo respuesta al almacenar los datos de facebook. " +
                    "Detalle error: " + "No se obtuvo respuesta al almacenar los datos de facebook en la base de datos.");
                    return new RegistrarDatosFacebookResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al almacenar los datos de facebook."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error($"AuthenticationDO ({idLogTexto}) ->  RegistrarDatosFacebook. Aplicacion: {cod_aplicacion}. " +
                   $"Data: {JsonConvert.SerializeObject(request)}. " +
                   "Mensaje al cliente: Error interno al almacenar datos de facebook. " +
                   "Detalle error: " + JsonConvert.SerializeObject(e));
                return new RegistrarDatosFacebookResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "No se obtuvo respuesta al almacenar los datos de facebook."
                };
            }
        }
        public ObtenerDataPrincipalUsuarioResponse ObtenerDataPrincipalUsuario(string codUsuarioCreado, string codMedioAcceso, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var resDtPrincipalUsuario = ctx.SP_OBTENER_DATA_PRINCIPAL_USUARIO(codUsuarioCreado, codMedioAcceso, cod_aplicacion).FirstOrDefault();
                if (resDtPrincipalUsuario != null)
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<SP_OBTENER_DATA_PRINCIPAL_USUARIO_Result, AuthenticatedUserData>();
                    });

                    IMapper mapper = config.CreateMapper();
                    var datosMapeados = mapper.Map<SP_OBTENER_DATA_PRINCIPAL_USUARIO_Result, AuthenticatedUserData>(resDtPrincipalUsuario);

                    if (resDtPrincipalUsuario.codigoRes == 200)
                    {
                        return new ObtenerDataPrincipalUsuarioResponse()
                        {
                            codeRes = HttpStatusCode.OK,
                            messageRes = resDtPrincipalUsuario.mensajeRes,
                            datos = datosMapeados
                        };
                    }
                    else
                    {
                        return new ObtenerDataPrincipalUsuarioResponse()
                        {
                            codeRes = HttpStatusCode.NotFound,
                            messageRes = resDtPrincipalUsuario.mensajeRes
                        };
                    }
                }
                else
                {
                    return new ObtenerDataPrincipalUsuarioResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se pudo obtener los datos del usuario."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error($"AuthenticationDO ({idLogTexto}) ->  ObtenerDataPrincipalUsuario. Usuario: {codUsuarioCreado}, Aplicacion: {cod_aplicacion}. " +
                    "Mensaje al cliente: Error interno al obtener los datos del usuario. " +
                    "Detalle error: " + JsonConvert.SerializeObject(e));
                return new ObtenerDataPrincipalUsuarioResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al obtener los datos del usuario."
                };
            }
        }
    }
}
