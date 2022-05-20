using AutoMapper;
using EasyWorkDataAccess.Contrato;
using EasyWorkDataAccess.Models;
using EasyWorkEntities.Authentication.Request;
using EasyWorkEntities.Authentication.Response;
using log4net;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace EasyWorkDataAccess.Implementacion
{
    public class AuthenticationDO : IAuthenticationDO
    {
        private int timeOut = 5;
        private string _apiGraphFacebook = ConfigurationManager.AppSettings["API_GRAPH_FACEBOOK"];
        private string _apiGoogle = ConfigurationManager.AppSettings["API_GOOGLE"];

        private readonly ILog log = LogManager.GetLogger(typeof(AuthenticationDO));
        public DataGoogleResponse ObtenerDataGoogle(string google_token, string cod_aplicacion, string idLogTexto)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.Timeout = new TimeSpan(0, timeOut, 0);
                var responseGoogle = httpClient.GetAsync(_apiGoogle + "?id_token=" + google_token).Result;

                if (responseGoogle.StatusCode == HttpStatusCode.OK)
                {
                    Stream stream = responseGoogle.Content.ReadAsStreamAsync().Result;
                    StreamReader re = new StreamReader(stream);
                    String json = re.ReadToEnd();
                    var dataGoogle = (DataGoogle)JsonConvert.DeserializeObject(json, typeof(DataGoogle));
                    if (dataGoogle == null)
                    {
                        return new DataGoogleResponse()
                        {
                            codeRes = HttpStatusCode.NoContent,
                            messageRes = "No se pudo obtener los datos de usuario desde google"
                        };
                    }

                    if (re != null)
                    {
                        re = null;
                    }
                    if (stream != null)
                    {
                        stream = null;
                    }

                    return new DataGoogleResponse()
                    {
                        codeRes = HttpStatusCode.OK,
                        messageRes = "Datos de google obtenidos con exito",
                        dataGoogle = dataGoogle
                    };
                }
                else
                {
                    return new DataGoogleResponse()
                    {
                        codeRes = responseGoogle.StatusCode,
                        messageRes = "No se pudo establecer conexión con google"
                    };
                }
            }
            catch (Exception e)
            {
                log.Error($"AuthenticationDO ({idLogTexto}) ->  ObtenerDataGoogle. Aplicacion: {cod_aplicacion}. " +
                   $"Token: {google_token}. " +
                   "Mensaje al cliente: No se obtuvo la data de google. " +
                   "Detalle error: " + JsonConvert.SerializeObject(e));
                return new DataGoogleResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "No se obtuvo la data de google."
                };
            }
        }
        public DataFacebookResponse ObtenerDataFacebook(string facebook_token, string cod_aplicacion, string idLogTexto)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.Timeout = new TimeSpan(0, timeOut, 0);
                var requestUri = $"&access_token={facebook_token}";
                var responseFacebook = httpClient.GetAsync(_apiGraphFacebook + requestUri).Result;

                if (responseFacebook.StatusCode == HttpStatusCode.OK)
                {
                    Stream stream = responseFacebook.Content.ReadAsStreamAsync().Result;
                    StreamReader re = new StreamReader(stream);
                    String json = re.ReadToEnd();
                    var dataFacebook = (DataFacebook)JsonConvert.DeserializeObject(json, typeof(DataFacebook));
                    if (dataFacebook == null)
                    {
                        return new DataFacebookResponse()
                        {
                            codeRes = HttpStatusCode.NoContent,
                            messageRes = "No se pudo obtener los datos de usuario desde facebook"
                        };
                    }
                    if (re != null)
                    {
                        re = null;
                    }
                    if (stream != null)
                    {
                        stream = null;
                    }

                    return new DataFacebookResponse()
                    {
                        codeRes = HttpStatusCode.OK,
                        messageRes = "Datos de facebook obtenidos con exito",
                        dataFacebook = dataFacebook
                    };
                }
                else
                {
                    return new DataFacebookResponse()
                    {
                        codeRes = responseFacebook.StatusCode,
                        messageRes = "No se pudo establecer conexión con facebook"
                    };
                }
            }
            catch (Exception e)
            {
                log.Error($"AuthenticationDO ({idLogTexto}) ->  ObtenerDataFacebook. Aplicacion: {cod_aplicacion}. " +
                   $"Token: {facebook_token}. " +
                   "Mensaje al cliente: No se obtuvo la data de facebook. " +
                   "Detalle error: " + JsonConvert.SerializeObject(e));
                return new DataFacebookResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "No se obtuvo la data de facebook."
                };
            }
        }
        public RegistrarDatosGoogleResponse RegistrarDatosGoogle(DataGoogle request, double latitud, double longitud, string google_token, string cod_aplicacion, string idLogTexto)
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var resRegDtGoogle = ctx.SP_REGISTRAR_DATOS_GOOGLE(request.sub, request.email, request.email_verified,
                request.name, request.given_name, request.family_name, request.picture, request.locale, google_token, Convert.ToDecimal(latitud),
                Convert.ToDecimal(longitud), cod_aplicacion).FirstOrDefault();

                if (resRegDtGoogle != null)
                {
                    if (resRegDtGoogle.codeRes.GetValueOrDefault() == 201)
                    {
                        return new RegistrarDatosGoogleResponse()
                        {
                            codeRes = HttpStatusCode.Created,
                            messageRes = resRegDtGoogle.messageRes,
                            codUsuarioCreado = resRegDtGoogle.codUsuarioCreado,
                            idUsuarioCreado = resRegDtGoogle.idUsuarioCreado.GetValueOrDefault(),
                            flgMostrarRegistroUsuario = resRegDtGoogle.flgMostrarRegistroUsuario.GetValueOrDefault(),
                            flgCelularValidado = resRegDtGoogle.flgCelularValidado.GetValueOrDefault()
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
        public RegistrarDatosFacebookResponse RegistrarDatosFacebook(DataFacebook request, double latitud, double longitud, string facebook_token, string cod_aplicacion, string idLogTexto)
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var resRegDtGoogle = ctx.SP_REGISTRAR_DATOS_FACEBOOK(request.id, request.first_name, request.last_name,
                request.username, request.email, request.picture.data.is_silhouette, request.picture.data.url,
                facebook_token, Convert.ToDecimal(latitud),
                Convert.ToDecimal(longitud), cod_aplicacion).FirstOrDefault();

                if (resRegDtGoogle != null)
                {
                    if (resRegDtGoogle.codeRes.GetValueOrDefault() == 201)
                    {
                        return new RegistrarDatosFacebookResponse()
                        {
                            codeRes = HttpStatusCode.Created,
                            messageRes = resRegDtGoogle.messageRes,
                            codUsuarioCreado = resRegDtGoogle.codUsuarioCreado,
                            idUsuarioCreado = resRegDtGoogle.idUsuarioCreado.GetValueOrDefault(),
                            flgMostrarRegistroUsuario = resRegDtGoogle.flgMostrarRegistroUsuario.GetValueOrDefault(),
                            flgCelularValidado = resRegDtGoogle.flgCelularValidado.GetValueOrDefault()
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
        public ObtenerDataPrincipalUsuarioResponse ObtenerDataPrincipalUsuario(string codUsuarioCreado, int idUsuario, string codMedioAcceso, string cod_aplicacion, string idLogTexto)
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var resDtPrincipalUsuario = ctx.SP_OBTENER_DATA_PRINCIPAL_USUARIO(idUsuario, codMedioAcceso).FirstOrDefault();
                if (resDtPrincipalUsuario != null)
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<SP_OBTENER_DATA_PRINCIPAL_USUARIO_Result, AuthenticatedUserData>();
                    });

                    IMapper mapper = config.CreateMapper();
                    var datosMapeados = mapper.Map<SP_OBTENER_DATA_PRINCIPAL_USUARIO_Result, AuthenticatedUserData>(resDtPrincipalUsuario);

                    if (resDtPrincipalUsuario.codeRes == 200)
                    {
                        return new ObtenerDataPrincipalUsuarioResponse()
                        {
                            codeRes = HttpStatusCode.OK,
                            messageRes = resDtPrincipalUsuario.messageRes,
                            datos = datosMapeados
                        };
                    }
                    else
                    {
                        return new ObtenerDataPrincipalUsuarioResponse()
                        {
                            codeRes = HttpStatusCode.NotFound,
                            messageRes = resDtPrincipalUsuario.messageRes
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
        public ValidarExistenciaUsuarioCorreoResponse ValidarExistenciaUsuarioCorreo(string correo, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var resValExisUsuCorreo= ctx.SP_VALIDAR_EXISTENCIA_USUARIO_CORREO(correo, cod_aplicacion).FirstOrDefault();
                if (resValExisUsuCorreo.codeRes.GetValueOrDefault() == 200)
                {
                    return new ValidarExistenciaUsuarioCorreoResponse()
                    {
                        codeRes = HttpStatusCode.OK,
                        messageRes = resValExisUsuCorreo.messageRes,
                    };
                }
                else
                {
                    log.Error($"AuthenticationDO ({idLogTexto}) ->  ValidarExistenciaUsuarioCorreo. Aplicacion: {cod_aplicacion}. " +
                    $"Correo: {correo}. " +
                    "Mensaje al cliente: No se obtuvo respuesta al validar existencia del usuario correo. " +
                    "Detalle error: " + "No se obtuvo respuesta al validar existencia del usuario correo en la base de datos.");
                    return new ValidarExistenciaUsuarioCorreoResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al validar existencia del usuario correo."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error($"AuthenticationDO ({idLogTexto}) ->  ValidarExistenciaUsuarioCorreo. Aplicacion: {cod_aplicacion}. " +
                    "Mensaje al cliente: Error interno al validar existencia del usuario correo. " +
                    "Detalle error: " + JsonConvert.SerializeObject(e));
                return new ValidarExistenciaUsuarioCorreoResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al validar existencia del usuario correo."
                };
            }
        }
        public RegistrarCodigoVerificacionResponse RegistrarCodigoVerificacion(string verifyCode, string correo, string nroCelular, string codTipoCodigoVerificacion, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var resRegDtGoogle = ctx.SP_REGISTRAR_CODIGO_VERIFICACION(verifyCode, correo, nroCelular, codTipoCodigoVerificacion).FirstOrDefault();

                if (resRegDtGoogle != null)
                {
                    if (resRegDtGoogle.codeRes.GetValueOrDefault() == 201)
                    {
                        return new RegistrarCodigoVerificacionResponse()
                        {
                            codeRes = HttpStatusCode.Created,
                            messageRes = resRegDtGoogle.messageRes
                        };
                    }
                    else
                    {
                        log.Error($"AuthenticationDO ({idLogTexto}) ->  RegistrarCodigoVerificacion. Aplicacion: {cod_aplicacion}. " +
                        $"Código de verificación: {verifyCode}. " +
                        $"Correo: {correo}. " +
                        $"Número de celular: {nroCelular}. " +
                        $"codTipoCodigoVerificacion: {codTipoCodigoVerificacion.ToString()}. " +
                        "Mensaje al cliente: No se obtuvo respuesta al almacenar el código de verificación. " +
                        "Detalle error: " + "No se obtuvo respuesta al almacenar el código de verificación en la base de datos.");
                        return new RegistrarCodigoVerificacionResponse()
                        {
                            codeRes = HttpStatusCode.NoContent,
                            messageRes = "No se obtuvo respuesta al almacenar el código de verificación."
                        };
                    }
                }
                else
                {
                    log.Error($"AuthenticationDO ({idLogTexto}) ->  RegistrarCodigoVerificacion. Aplicacion: {cod_aplicacion}. " +
                    $"Código de verificación: {verifyCode}. " +
                    $"Correo: {correo}. " +
                    $"Número de celular: {nroCelular}. " +
                    $"codTipoCodigoVerificacion: {codTipoCodigoVerificacion.ToString()}. " +
                    "Mensaje al cliente: No se obtuvo respuesta al almacenar el código de verificación. " +
                    "Detalle error: " + "No se obtuvo respuesta al almacenar el código de verificación en la base de datos.");
                    return new RegistrarCodigoVerificacionResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al almacenar el código de verificación."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error($"AuthenticationDO ({idLogTexto}) ->  RegistrarCodigoVerificacion. Aplicacion: {cod_aplicacion}. " +
                  $"Código de verificación: {verifyCode}. " +
                  $"Correo: {correo}. " +
                  $"Número de celular: {nroCelular}. " +
                  $"codTipoCodigoVerificacion: {codTipoCodigoVerificacion.ToString()}. " +
                  "Mensaje al cliente: No se obtuvo respuesta al guardar registro del código de verificación. " +
                  "Detalle error: " + JsonConvert.SerializeObject(e));
                return new RegistrarCodigoVerificacionResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "No se obtuvo respuesta al guardar registro del código de verificación."
                }; 
            }
        }
        public VerificarCodigoVerificacionResponse VerificarCodigoVerificacion(string codigoVerificacion, string correo, string nroCelular, bool flgCelularcorreo, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var resVerifyCode = ctx.SP_VERIFICAR_CODIGO_VERIFICACION(codigoVerificacion, correo, nroCelular, flgCelularcorreo).FirstOrDefault();

                if (resVerifyCode != null)
                {
                    if (resVerifyCode.codeRes.GetValueOrDefault() == 200)
                    {
                        return new VerificarCodigoVerificacionResponse()
                        {
                            codeRes = HttpStatusCode.OK,
                            messageRes = resVerifyCode.messageRes
                        };
                    }
                    else
                    {
                        log.Error($"AuthenticationDO ({idLogTexto}) ->  VerificarCodigoVerificacion. Aplicacion: {cod_aplicacion}. " +
                        $"Código de verificación: {codigoVerificacion}. " +
                        $"Correo: {correo}. " +
                        $"Número de celular: {nroCelular}. " +
                        $"flgCelularcorreo: {flgCelularcorreo.ToString()}. " +
                        "Mensaje al cliente: No se obtuvo respuesta al verificar el código de verificación. " +
                        "Detalle error: " + "No se obtuvo respuesta al verificar el código de verificación en la base de datos.");
                        return new VerificarCodigoVerificacionResponse()
                        {
                            codeRes = HttpStatusCode.NoContent,
                            messageRes = "No se obtuvo respuesta al verificar el código de verificación."
                        };
                    }
                }
                else
                {
                    log.Error($"AuthenticationDO ({idLogTexto}) ->  VerificarCodigoVerificacion. Aplicacion: {cod_aplicacion}. " +
                    $"Código de verificación: {codigoVerificacion}. " +
                    $"Correo: {correo}. " +
                    $"Número de celular: {nroCelular}. " +
                    $"flgCelularcorreo: {flgCelularcorreo.ToString()}. " +
                    "Mensaje al cliente: No se obtuvo respuesta al verificar el código de verificación. " +
                    "Detalle error: " + "No se obtuvo respuesta al verificar el código de verificación en la base de datos.");
                    return new VerificarCodigoVerificacionResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al verificar el código de verificación."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error($"AuthenticationDO ({idLogTexto}) ->  VerificarCodigoVerificacion. Aplicacion: {cod_aplicacion}. " +
                  $"Código de verificación: {codigoVerificacion}. " +
                  $"Correo: {correo}. " +
                  $"Número de celular: {nroCelular}. " +
                  $"flgCelularcorreo: {flgCelularcorreo.ToString()}. " +
                  "Mensaje al cliente: No se obtuvo respuesta al verificar el código de verificación. " +
                  "Detalle error: " + JsonConvert.SerializeObject(e));
                return new VerificarCodigoVerificacionResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "No se obtuvo respuesta al verificar el código de verificación."
                };
            }
        }
        public VerificarCodigoAutenticacionResponse VerificarCodigoAutenticacion(string codVerificacion, string nroCelular, double latitud, double longitud, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var resVerifyCode = ctx.SP_VERIFICAR_CODIGO_AUTENTICACION(codVerificacion, nroCelular, Convert.ToDecimal(latitud), Convert.ToDecimal(longitud)).FirstOrDefault();

                if (resVerifyCode != null)
                {
                    if (resVerifyCode.codeRes.GetValueOrDefault() == 200)
                    {
                        return new VerificarCodigoAutenticacionResponse()
                        {
                            codeRes = HttpStatusCode.OK,
                            messageRes = resVerifyCode.messageRes
                        };
                    }
                    else
                    {
                        log.Error($"AuthenticationDO ({idLogTexto}) ->  VerificarCodigoAutenticacion. Aplicacion: {cod_aplicacion}. " +
                        $"Código de verificación: {codVerificacion}. " +
                        $"Número de celular: {nroCelular}. " +
                        $"latitud: {latitud.ToString()}. " +
                        $"longitud: {longitud.ToString()}. " +
                        "Mensaje al cliente: No se obtuvo respuesta al verificar el código de autenticación. " +
                        "Detalle error: " + "No se obtuvo respuesta al verificar el código de autenticación en la base de datos.");
                        return new VerificarCodigoAutenticacionResponse()
                        {
                            codeRes = HttpStatusCode.NoContent,
                            messageRes = "No se obtuvo respuesta al verificar el código de autenticación."
                        };
                    }
                }
                else
                {
                    log.Error($"AuthenticationDO ({idLogTexto}) ->  VerificarCodigoAutenticacion. Aplicacion: {cod_aplicacion}. " +
                    $"Código de verificación: {codVerificacion}. " +
                    $"Número de celular: {nroCelular}. " +
                    $"latitud: {latitud.ToString()}. " +
                    $"longitud: {longitud.ToString()}. " +
                    "Mensaje al cliente: No se obtuvo respuesta al verificar el código de autenticación. " +
                    "Detalle error: " + "No se obtuvo respuesta al verificar el código de autenticación en la base de datos.");
                    return new VerificarCodigoAutenticacionResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al verificar el código de autenticación."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error($"AuthenticationDO ({idLogTexto}) ->  VerificarCodigoAutenticacion. Aplicacion: {cod_aplicacion}. " +
                  $"Código de verificación: {codVerificacion}. " +
                  $"Número de celular: {nroCelular}. " +
                  $"latitud: {latitud.ToString()}. " +
                  $"longitud: {longitud.ToString()}. " +
                  "Mensaje al cliente: No se obtuvo respuesta al verificar el código de autenticación. " +
                  "Detalle error: " + JsonConvert.SerializeObject(e));
                return new VerificarCodigoAutenticacionResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "No se obtuvo respuesta al verificar el código de autenticación."
                };
            }
        }
    }
}
