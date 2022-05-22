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
                log.Info($"responseGoogle --> " + JsonConvert.SerializeObject(responseGoogle));
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
                log.Error("Error :" + JsonConvert.SerializeObject(e));
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
                log.Info($"responseFacebook --> " + JsonConvert.SerializeObject(responseFacebook));
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
                log.Error("Error :" + JsonConvert.SerializeObject(e));
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
                log.Info($"resRegDtGoogle --> " + JsonConvert.SerializeObject(resRegDtGoogle));
                if (resRegDtGoogle != null)
                {
                    if (resRegDtGoogle.codeRes.GetValueOrDefault() == 201 || resRegDtGoogle.codeRes.GetValueOrDefault() == 200)
                    {
                        return new RegistrarDatosGoogleResponse()
                        {
                            codeRes = HttpStatusCode.OK,
                            messageRes = resRegDtGoogle.messageRes,
                            codUsuarioCreado = resRegDtGoogle.codUsuarioCreado,
                            idUsuarioCreado = resRegDtGoogle.idUsuarioCreado.GetValueOrDefault(),
                            flgMostrarRegistroUsuario = resRegDtGoogle.flgMostrarRegistroUsuario.GetValueOrDefault(),
                            flgCelularValidado = resRegDtGoogle.flgCelularValidado.GetValueOrDefault()
                        };
                    }
                    else
                    {
                        return new RegistrarDatosGoogleResponse()
                        {
                            codeRes = HttpStatusCode.NoContent,
                            messageRes = "No se obtuvo respuesta al almacenar los datos de google."
                        };
                    }
                }
                else
                {
                    return new RegistrarDatosGoogleResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al almacenar los datos de google."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
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
                var resRegDtFacebook = ctx.SP_REGISTRAR_DATOS_FACEBOOK(request.id, request.first_name, request.last_name,
                request.username, request.email, request.picture.data.is_silhouette, request.picture.data.url,
                facebook_token, Convert.ToDecimal(latitud),
                Convert.ToDecimal(longitud), cod_aplicacion).FirstOrDefault();
                log.Info($"resRegDtFacebook --> " + JsonConvert.SerializeObject(resRegDtFacebook));
                if (resRegDtFacebook != null)
                {
                    if (resRegDtFacebook.codeRes.GetValueOrDefault() == 200)
                    {
                        return new RegistrarDatosFacebookResponse()
                        {
                            codeRes = HttpStatusCode.OK,
                            messageRes = resRegDtFacebook.messageRes,
                            codUsuarioCreado = resRegDtFacebook.codUsuarioCreado,
                            idUsuarioCreado = resRegDtFacebook.idUsuarioCreado.GetValueOrDefault(),
                            flgMostrarRegistroUsuario = resRegDtFacebook.flgMostrarRegistroUsuario.GetValueOrDefault(),
                            flgCelularValidado = resRegDtFacebook.flgCelularValidado.GetValueOrDefault()
                        };
                    }
                    else
                    {
                        return new RegistrarDatosFacebookResponse()
                        {
                            codeRes = HttpStatusCode.NoContent,
                            messageRes = "No se obtuvo respuesta al almacenar los datos de facebook."
                        };
                    }
                }
                else
                {
                    return new RegistrarDatosFacebookResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al almacenar los datos de facebook."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
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
                log.Info($"resDtPrincipalUsuario --> " + JsonConvert.SerializeObject(resDtPrincipalUsuario));
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
                log.Error("Error :" + JsonConvert.SerializeObject(e));
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
                log.Info($"resValExisUsuCorreo --> " + JsonConvert.SerializeObject(resValExisUsuCorreo));
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
                    return new ValidarExistenciaUsuarioCorreoResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al validar existencia del usuario correo."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
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
                var resRegCodVer= ctx.SP_REGISTRAR_CODIGO_VERIFICACION(verifyCode, correo, nroCelular, codTipoCodigoVerificacion).FirstOrDefault();
                log.Info($"resRegCodVer --> " + JsonConvert.SerializeObject(resRegCodVer));
                if (resRegCodVer != null)
                {
                    if (resRegCodVer.codeRes.GetValueOrDefault() == 201)
                    {
                        return new RegistrarCodigoVerificacionResponse()
                        {
                            codeRes = HttpStatusCode.Created,
                            messageRes = resRegCodVer.messageRes
                        };
                    }
                    else
                    {
                        return new RegistrarCodigoVerificacionResponse()
                        {
                            codeRes = HttpStatusCode.NoContent,
                            messageRes = "No se obtuvo respuesta al almacenar el código de verificación."
                        };
                    }
                }
                else
                {
                    return new RegistrarCodigoVerificacionResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al almacenar el código de verificación."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
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
                log.Info($"resVerifyCode --> " + JsonConvert.SerializeObject(resVerifyCode));
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
                        return new VerificarCodigoVerificacionResponse()
                        {
                            codeRes = HttpStatusCode.NoContent,
                            messageRes = "No se obtuvo respuesta al verificar el código de verificación."
                        };
                    }
                }
                else
                {
                    return new VerificarCodigoVerificacionResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al verificar el código de verificación."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
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
                log.Info($"resVerifyCode --> " + JsonConvert.SerializeObject(resVerifyCode));
                if (resVerifyCode != null)
                {
                    if (resVerifyCode.codeRes.GetValueOrDefault() == 200)
                    {
                        return new VerificarCodigoAutenticacionResponse()
                        {
                            codeRes = HttpStatusCode.OK,
                            messageRes = resVerifyCode.messageRes,
                            flgCelularValidado = resVerifyCode.flgCelularValidado.GetValueOrDefault(),
                            flgMostrarRegistroUsuario = resVerifyCode.flgMostrarRegistroUsuario.GetValueOrDefault(),
                            codUsuario = resVerifyCode.codUsuario,
                            idUsuario = resVerifyCode.idUsuario.GetValueOrDefault()
                        };
                    }
                    else
                    {
                        return new VerificarCodigoAutenticacionResponse()
                        {
                            codeRes = HttpStatusCode.NoContent,
                            messageRes = "No se obtuvo respuesta al verificar el código de autenticación."
                        };
                    }
                }
                else
                {
                    return new VerificarCodigoAutenticacionResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al verificar el código de autenticación."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new VerificarCodigoAutenticacionResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "No se obtuvo respuesta al verificar el código de autenticación."
                };
            }
        }
        public ValidarExistenciaUsuarioCelularResponse ValidarExistenciaUsuarioCelular(string nroCelular, string cod_aplicacion, string idLogTexto)
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var resValExisUsuCorreo = ctx.SP_VALIDAR_EXISTENCIA_USUARIO_CELULAR(nroCelular, cod_aplicacion).FirstOrDefault();
                log.Info($"resValExisUsuCorreo --> " + JsonConvert.SerializeObject(resValExisUsuCorreo));
                if (resValExisUsuCorreo != null)
                {
                    if (resValExisUsuCorreo.codeRes.GetValueOrDefault() == 200)
                    {
                        return new ValidarExistenciaUsuarioCelularResponse()
                        {
                            codeRes = HttpStatusCode.OK,
                            messageRes = resValExisUsuCorreo.messageRes,
                        };
                    }
                    else
                    {
                        return new ValidarExistenciaUsuarioCelularResponse()
                        {
                            codeRes = HttpStatusCode.Unauthorized,
                            messageRes = resValExisUsuCorreo.messageRes,
                        };
                    }
                }
                else
                {
                    return new ValidarExistenciaUsuarioCelularResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al validar existencia del usuario celular."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ValidarExistenciaUsuarioCelularResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al validar existencia del usuario celular."
                };
            }
        }
    }
}
