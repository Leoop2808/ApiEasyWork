using EasyWorkDataAccess.Contrato;
using EasyWorkDataAccess.Models;
using EasyWorkEntities.Usuario.Request;
using EasyWorkEntities.Usuario.Response;
using log4net;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;

namespace EasyWorkDataAccess.Implementacion
{
    public class UsuarioDO : IUsuarioDO
    {
        private readonly ILog log = LogManager.GetLogger(typeof(UsuarioDO));

        public ValidarExistenciaUsuarioResponse ValidarExistenciaUsuario(string correo, string celular, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var resVerifyExisUsu = ctx.SP_VALIDAR_EXISTENCIA_USUARIO_CLIENTE(correo, celular).FirstOrDefault();
                log.Info($"resVerifyExisUsu --> " + JsonConvert.SerializeObject(resVerifyExisUsu));
                if (resVerifyExisUsu != null)
                {
                    if (resVerifyExisUsu.codeRes.GetValueOrDefault() == 200) //USUARIO NO EXISTE Y SE PUEDE REGISTRAR
                    {
                        return new ValidarExistenciaUsuarioResponse()   
                        {
                            codeRes = HttpStatusCode.OK,
                            messageRes = resVerifyExisUsu.messageRes
                        };
                    }
                    else if (resVerifyExisUsu.codeRes.GetValueOrDefault() == 100) //USUARIO EXISTE PERO NO TIENE CONTRASEÑA SE ACTUALIZARA LA CONTRASEÑA
                    {
                        return new ValidarExistenciaUsuarioResponse()
                        {
                            codeRes = HttpStatusCode.Continue,
                            messageRes = resVerifyExisUsu.messageRes
                        };
                    }
                    else
                    {
                        return new ValidarExistenciaUsuarioResponse()
                        {
                            codeRes = HttpStatusCode.NoContent,
                            messageRes = "No se obtuvo respuesta al validar existecia del usuario."
                        };
                    }
                }
                else
                {
                    return new ValidarExistenciaUsuarioResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al verificar el código de verificación."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ValidarExistenciaUsuarioResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "No se obtuvo respuesta al validar existecia del usuario."
                };
            }
        }
        public ObtenerRolPorCodRolResponse ObtenerRolPorCodRol(string codRol, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var resCodRol = ctx.SP_OBTENER_ROL_X_COD_ROL(codRol).FirstOrDefault();
                log.Info($"resCodRol --> " + JsonConvert.SerializeObject(resCodRol));
                if (resCodRol != null)
                {
                    if (resCodRol.codeRes.GetValueOrDefault() == 200)
                    {
                        return new ObtenerRolPorCodRolResponse()
                        {
                            codeRes = HttpStatusCode.OK,
                            messageRes = resCodRol.messageRes,
                            idRol = resCodRol.idRol.GetValueOrDefault()
                        };
                    }
                    else
                    {
                        return new ObtenerRolPorCodRolResponse()
                        {
                            codeRes = HttpStatusCode.NoContent,
                            messageRes = "No se obtuvo respuesta al identificar el rol del usuario."
                        };
                    }
                }
                else
                {
                    return new ObtenerRolPorCodRolResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al identificar el rol del usuario."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerRolPorCodRolResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "No se obtuvo respuesta al identificar el rol del usuario."
                };
            }
        }
        public RegistrarPersonaResponse RegistrarPersona(DataPersona request, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var reRegPersona = ctx.SP_REGISTRAR_PERSONA(request.nombre1, request.nombre2, request.apellido1, request.apellido2,
                    request.celular, request.correo, request.codDistrito, request.codTipoDocumento, request.documento, request.genero, 
                    Convert.ToDecimal(request.latitud), Convert.ToDecimal(request.longitud), cod_aplicacion).FirstOrDefault();
                log.Info($"reRegPersona --> " + JsonConvert.SerializeObject(reRegPersona));
                if (reRegPersona != null)
                {
                    if (reRegPersona.codeRes.GetValueOrDefault() == 201)
                    {
                        return new RegistrarPersonaResponse()
                        {
                            codeRes = HttpStatusCode.Created,
                            messageRes = reRegPersona.messageRes,
                            idPersona = reRegPersona.idPersona.GetValueOrDefault()
                        };
                    }
                    else
                    {
                        return new RegistrarPersonaResponse()
                        {
                            codeRes = HttpStatusCode.NoContent,
                            messageRes = "No se obtuvo respuesta al almacenar los datos de la persona."
                        };
                    }
                }
                else
                {
                    return new RegistrarPersonaResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al almacenar los datos de la persona."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new RegistrarPersonaResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "No se obtuvo respuesta al guardar registro de datos de persona."
                };
            }
        }
        public ObtenerDataSesionResponse ObtenerDataSesion(int id_usuario, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var resDataSesion = ctx.SP_OBTENER_DATA_SESION_X_USUARIO(id_usuario).FirstOrDefault();
                log.Info($"resDataSesion --> " + JsonConvert.SerializeObject(resDataSesion));
                if (resDataSesion != null)
                {
                    if (resDataSesion.codeRes.GetValueOrDefault() == 200)
                    {
                        return new ObtenerDataSesionResponse()
                        {
                            codeRes = HttpStatusCode.OK,
                            messageRes = resDataSesion.messageRes,
                            nombres = resDataSesion.nombres,
                            apellidos = resDataSesion.apellidos,
                            flgMostrarRegistroUsuario = resDataSesion.flgMostrarRegistroUsuario.GetValueOrDefault(),
                            flgCelularValidado = resDataSesion.flgCelularValidado.GetValueOrDefault(),
                            flgCorreoValidado = resDataSesion.flgCorreoValidado.GetValueOrDefault()
                        };
                    }
                    else
                    {
                        return new ObtenerDataSesionResponse()
                        {
                            codeRes = HttpStatusCode.NoContent,
                            messageRes = "No se obtuvo respuesta al obtener data sesión."
                        };
                    }
                }
                else
                {
                    return new ObtenerDataSesionResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al obtener data sesión."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerDataSesionResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "No se obtuvo respuesta al obtener data sesión."
                };
            }
        }
        public RegistrarDispositivoResponse RegistrarDispositivo(RegistrarDispositivoRequest request, string cod_usuario, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var reRegPersona = ctx.SP_REGISTRAR_DISPOSITIVO(request.keyDispositivo, request.versionAndroid,
                request.versionApp, Convert.ToDecimal(request.latitud), Convert.ToDecimal(request.longitud), cod_usuario, cod_aplicacion).FirstOrDefault();
                log.Info($"reRegPersona --> " + JsonConvert.SerializeObject(reRegPersona));
                if (reRegPersona != null)
                {
                    if (reRegPersona.codeRes.GetValueOrDefault() == 200)
                    {
                        return new RegistrarDispositivoResponse()
                        {
                            codeRes = HttpStatusCode.OK,
                            messageRes = reRegPersona.messageRes
                        };
                    }
                    else
                    {
                        return new RegistrarDispositivoResponse()
                        {
                            codeRes = HttpStatusCode.NoContent,
                            messageRes = "No se obtuvo respuesta al almacenar los datos de dispositivo."
                        };
                    }
                }
                else
                {
                    return new RegistrarDispositivoResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al almacenar los datos de dispositivo."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new RegistrarDispositivoResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "No se obtuvo respuesta al guardar registro de datos de dispositivo."
                };
            }
        }
    }
}
