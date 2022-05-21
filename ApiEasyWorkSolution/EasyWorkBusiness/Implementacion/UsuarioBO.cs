using EasyWorkBusiness.Contrato;
using EasyWorkDataAccess.Contrato;
using EasyWorkEntities.Usuario.Request;
using EasyWorkEntities.Usuario.Response;
using log4net;
using Newtonsoft.Json;
using System;
using System.Net;

namespace EasyWorkBusiness.Implementacion
{
    public class UsuarioBO : IUsuarioBO
    {
        private readonly ILog log = LogManager.GetLogger(typeof(UsuarioBO));
        readonly IUsuarioDO _usuarioDO;
        public UsuarioBO(IUsuarioDO usuarioDO)
        {
            _usuarioDO = usuarioDO;
        }

        public ValidarExistenciaUsuarioResponse ValidarExistenciaUsuario(string correo, string celular, string cod_aplicacion, string idLogTexto)
        {
            try
            {
                var response = new ValidarExistenciaUsuarioResponse();

                var resValExisUsu = _usuarioDO.ValidarExistenciaUsuario(correo, celular, cod_aplicacion, idLogTexto);
                log.Info($"resValExisUsu --> " + JsonConvert.SerializeObject(resValExisUsu));
                response.codeRes = resValExisUsu.codeRes;
                response.messageRes = resValExisUsu.messageRes;
                return response;
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ValidarExistenciaUsuarioResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al validar existencia de usuario."
                };
            }
        }

        public ObtenerRolPorCodRolResponse ObtenerRolPorCodRol(string codRol, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var response = new ObtenerRolPorCodRolResponse();

                var reCodRol = _usuarioDO.ObtenerRolPorCodRol(codRol, cod_aplicacion, idLogTexto);
                log.Info($"reCodRol --> " + JsonConvert.SerializeObject(reCodRol));
                response.codeRes = reCodRol.codeRes;
                response.messageRes = reCodRol.messageRes;
                return response;
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerRolPorCodRolResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al obtener el rol."
                };
            }
        }

        public RegistrarPersonaResponse RegistrarPersona(DataPersona request, string cod_aplicacion, string idLogTexto)
        {
            try
            {
                var response = new RegistrarPersonaResponse();

                var resRegPersona= _usuarioDO.RegistrarPersona(request, cod_aplicacion, idLogTexto);
                log.Info($"resRegPersona --> " + JsonConvert.SerializeObject(resRegPersona));
                if (resRegPersona.codeRes != HttpStatusCode.Created)
                {
                    response.codeRes = resRegPersona.codeRes;
                    response.messageRes = resRegPersona.messageRes;
                    return response;
                }

                return response;
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new RegistrarPersonaResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al registrar los datos de la persona."
                };
            }
        }

        public ObtenerDataSesionResponse ObtenerDataSesion(int id_usuario, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var response = new ObtenerDataSesionResponse();

                var reDataSesion= _usuarioDO.ObtenerDataSesion(id_usuario, cod_aplicacion, idLogTexto);
                log.Info($"reDataSesion --> " + JsonConvert.SerializeObject(reDataSesion));
                response.codeRes = reDataSesion.codeRes;
                response.messageRes = reDataSesion.messageRes;
                return response;
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerDataSesionResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al obtener datos de sesión."
                };
            }
        }

        public RegistrarDispositivoResponse RegistrarDispositivo(RegistrarDispositivoRequest request, string cod_usuario, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var response = new RegistrarDispositivoResponse();

                var resRegDisp = _usuarioDO.RegistrarDispositivo(request, cod_usuario, cod_aplicacion, idLogTexto);
                log.Info($"resRegDisp --> " + JsonConvert.SerializeObject(resRegDisp));
                if (resRegDisp.codeRes != HttpStatusCode.OK)
                {
                    response.codeRes = resRegDisp.codeRes;
                    response.messageRes = resRegDisp.messageRes;
                    return response;
                }

                return response;
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new RegistrarDispositivoResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al registrar los datos del dispositivo."
                };
            }
        }
    }
}
