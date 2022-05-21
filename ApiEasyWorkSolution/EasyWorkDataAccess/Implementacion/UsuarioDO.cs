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
                var resVerifyCode = ctx.SP_VALIDAR_EXISTENCIA_USUARIO_CLIENTE(correo, celular).FirstOrDefault();

                if (resVerifyCode != null)
                {
                    if (resVerifyCode.codeRes.GetValueOrDefault() == 200) //USUARIO NO EXISTE Y SE PUEDE REGISTRAR
                    {
                        return new ValidarExistenciaUsuarioResponse()   
                        {
                            codeRes = HttpStatusCode.OK,
                            messageRes = resVerifyCode.messageRes
                        };
                    }
                    else if (resVerifyCode.codeRes.GetValueOrDefault() == 100) //USUARIO EXISTE PERO NO TIENE CONTRASEÑA SE ACTUALIZARA LA CONTRASEÑA
                    {
                        return new ValidarExistenciaUsuarioResponse()
                        {
                            codeRes = HttpStatusCode.Continue,
                            messageRes = resVerifyCode.messageRes
                        };
                    }
                    else
                    {
                        log.Error($"UsuarioDO ({idLogTexto}) ->  ValidarExistenciaUsuario. Aplicacion: {cod_aplicacion}. " +
                        $"Correo: {correo}. " +
                        $"Número de celular: {celular}. " +
                        "Mensaje al cliente: No se obtuvo respuesta al validar existecia del usuario. " +
                        "Detalle error: " + "No se obtuvo respuesta al validar existecia del usuario en la base de datos.");
                        return new ValidarExistenciaUsuarioResponse()
                        {
                            codeRes = HttpStatusCode.NoContent,
                            messageRes = "No se obtuvo respuesta al validar existecia del usuario."
                        };
                    }
                }
                else
                {
                    log.Error($"UsuarioDO ({idLogTexto}) ->  ValidarExistenciaUsuario. Aplicacion: {cod_aplicacion}. " +
                    $"Correo: {correo}. " +
                    $"Número de celular: {celular}. " +
                    "Mensaje al cliente: No se obtuvo respuesta al verificar el código de verificación. " +
                    "Detalle error: " + "No se obtuvo respuesta al verificar el código de verificación en la base de datos.");
                    return new ValidarExistenciaUsuarioResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al verificar el código de verificación."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error($"UsuarioDO ({idLogTexto}) ->  ValidarExistenciaUsuario. Aplicacion: {cod_aplicacion}. " +
                  $"Correo: {correo}. " +
                  $"Número de celular: {celular}. " +
                  "Mensaje al cliente: No se obtuvo respuesta al validar existecia del usuario. " +
                  "Detalle error: " + JsonConvert.SerializeObject(e));
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
                        log.Error($"UsuarioDO ({idLogTexto}) ->  ObtenerRolPorCodRol. Aplicacion: {cod_aplicacion}. " +
                        $"Código rol: {codRol}. " +
                        "Mensaje al cliente: No se obtuvo respuesta al identificar el rol del usuario. " +
                        "Detalle error: " + "No se obtuvo respuesta al identificar el rol del usuario en la base de datos.");
                        return new ObtenerRolPorCodRolResponse()
                        {
                            codeRes = HttpStatusCode.NoContent,
                            messageRes = "No se obtuvo respuesta al identificar el rol del usuario."
                        };
                    }
                }
                else
                {
                    log.Error($"UsuarioDO ({idLogTexto}) ->  ObtenerRolPorCodRol. Aplicacion: {cod_aplicacion}. " +
                    $"Código rol: {codRol}. " +
                    "Mensaje al cliente: No se obtuvo respuesta al identificar el rol del usuario." +
                    "Detalle error: " + "No se obtuvo respuesta al identificar el rol del usuario en la base de datos.");
                    return new ObtenerRolPorCodRolResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al identificar el rol del usuario."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error($"UsuarioDO ({idLogTexto}) ->  ObtenerRolPorCodRol. Aplicacion: {cod_aplicacion}. " +
                  $"Código rol: {codRol}. " +
                  "Mensaje al cliente: No se obtuvo respuesta al identificar el rol del usuario. " +
                  "Detalle error: " + JsonConvert.SerializeObject(e));
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
                        log.Error($"UsuarioDO ({idLogTexto}) ->  RegistrarPersona. Aplicacion: {cod_aplicacion}. " +
                        $"Request: {JsonConvert.SerializeObject(request)}. " +
                        "Mensaje al cliente: No se obtuvo respuesta al registrar los datos de la persona. " +
                        "Detalle error: " + "No se obtuvo respuesta al almacenar los datos de la persona en la base de datos.");
                        return new RegistrarPersonaResponse()
                        {
                            codeRes = HttpStatusCode.NoContent,
                            messageRes = "No se obtuvo respuesta al almacenar los datos de la persona."
                        };
                    }
                }
                else
                {
                    log.Error($"UsuarioDO ({idLogTexto}) ->  RegistrarPersona. Aplicacion: {cod_aplicacion}. " +
                    $"Request: {JsonConvert.SerializeObject(request)}. " +
                    "Mensaje al cliente: No se obtuvo respuesta al almacenar los datos de la persona. " +
                    "Detalle error: " + "No se obtuvo respuesta al almacenar los datos de la persona en la base de datos.");
                    return new RegistrarPersonaResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al almacenar los datos de la persona."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error($"UsuarioDO ({idLogTexto}) ->  RegistrarPersona. Aplicacion: {cod_aplicacion}. " +
                  $"Request: {JsonConvert.SerializeObject(request)}. " +
                  "Mensaje al cliente: No se obtuvo respuesta al guardar registro de datos de persona. " +
                  "Detalle error: " + JsonConvert.SerializeObject(e));
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
                        log.Error($"UsuarioDO ({idLogTexto}) ->  ObtenerDataSesion. Aplicacion: {cod_aplicacion}. " +
                        $"Id Usuario: {id_usuario.ToString()}. " +
                        "Mensaje al cliente: No se obtuvo respuesta al obtener data sesión. " +
                        "Detalle error: " + "No se obtuvo respuesta al obtener data sesión en la base de datos.");
                        return new ObtenerDataSesionResponse()
                        {
                            codeRes = HttpStatusCode.NoContent,
                            messageRes = "No se obtuvo respuesta al obtener data sesión."
                        };
                    }
                }
                else
                {
                    log.Error($"UsuarioDO ({idLogTexto}) ->  ObtenerDataSesion. Aplicacion: {cod_aplicacion}. " +
                    $"Id Usuario: {id_usuario.ToString()}. " +
                    "Mensaje al cliente: No se obtuvo respuesta al obtener data sesión." +
                    "Detalle error: " + "No se obtuvo respuesta al obtener data sesión en la base de datos.");
                    return new ObtenerDataSesionResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al obtener data sesión."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error($"UsuarioDO ({idLogTexto}) ->  ObtenerDataSesion. Aplicacion: {cod_aplicacion}. " +
                  $"Id Usuario: {id_usuario.ToString()}. " +
                  "Mensaje al cliente: No se obtuvo respuesta al obtener data sesión. " +
                  "Detalle error: " + JsonConvert.SerializeObject(e));
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

                if (reRegPersona != null)
                {
                    if (reRegPersona.codeRes.GetValueOrDefault() == 200)
                    {
                        return new RegistrarDispositivoResponse()
                        {
                            codeRes = HttpStatusCode.Created,
                            messageRes = reRegPersona.messageRes
                        };
                    }
                    else
                    {
                        log.Error($"UsuarioDO ({idLogTexto}) ->  RegistrarDispositivo. Aplicacion: {cod_aplicacion}. Usuario: {cod_usuario}." +
                        $"Request: {JsonConvert.SerializeObject(request)}. " +
                        "Mensaje al cliente: No se obtuvo respuesta al registrar los datos de dispositivo. " +
                        "Detalle error: " + "No se obtuvo respuesta al almacenar los datos de dispositivo en la base de datos.");
                        return new RegistrarDispositivoResponse()
                        {
                            codeRes = HttpStatusCode.NoContent,
                            messageRes = "No se obtuvo respuesta al almacenar los datos de dispositivo."
                        };
                    }
                }
                else
                {
                    log.Error($"UsuarioDO ({idLogTexto}) ->  RegistrarDispositivo. Aplicacion: {cod_aplicacion}. Usuario: {cod_usuario}." +
                    $"Request: {JsonConvert.SerializeObject(request)}. " +
                    "Mensaje al cliente: No se obtuvo respuesta al almacenar los datos de dispositivo. " +
                    "Detalle error: " + "No se obtuvo respuesta al almacenar los datos de dispositivo en la base de datos.");
                    return new RegistrarDispositivoResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvo respuesta al almacenar los datos de dispositivo."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error($"UsuarioDO ({idLogTexto}) ->  RegistrarDispositivo. Aplicacion: {cod_aplicacion}. Usuario: {cod_usuario}." +
                  $"Request: {JsonConvert.SerializeObject(request)}. " +
                  "Mensaje al cliente: No se obtuvo respuesta al guardar registro de datos de dispositivo. " +
                  "Detalle error: " + JsonConvert.SerializeObject(e));
                return new RegistrarDispositivoResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "No se obtuvo respuesta al guardar registro de datos de dispositivo."
                };
            }
        }
    }
}
