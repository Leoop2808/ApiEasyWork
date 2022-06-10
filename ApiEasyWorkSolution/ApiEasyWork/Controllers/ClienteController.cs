using ApiEasyWork.App_Start;
using ApiEasyWork.FiltersAttributes;
using ApiEasyWork.Util;
using EasyWorkBusiness.Contrato;
using EasyWorkEntities;
using EasyWorkEntities.Cliente.Request;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiEasyWork.Controllers
{
    [Authorize]
    [RoutePrefix("api/cliente")]
    public class ClienteController : ApiController
    {
        private TimeSpan tokenExpirationTimeSpan = TimeSpan.FromHours(24);
        private readonly ILog log = LogManager.GetLogger(typeof(UsuarioController));
        private readonly IClienteBO _clienteBO;
        public ClienteController(IClienteBO clienteBO)
        {
            _clienteBO = clienteBO;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [Route("lista-maestros")]
        [HttpGet]
        public HttpResponseMessage ObtenerListaMaestros()
        {
            string idLogTexto = Guid.NewGuid().ToString();
            log.Info($"request --> ");
            var cod_usuario = User.Identity.GetUserId();
            var cod_aplicacion = AplicationData.codAplicacion;
            try
            {                
                var respListaMaestros = _clienteBO.ObtenerListaMaestros(cod_aplicacion, cod_usuario, idLogTexto);
                if (respListaMaestros.codeRes == HttpStatusCode.OK)
                {
                    if (respListaMaestros.codeRes == HttpStatusCode.OK)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                            new { Message = respListaMaestros.messageRes, data = respListaMaestros.datos });
                    }
                    else if (respListaMaestros.codeRes == HttpStatusCode.NoContent)
                    {
                        return Request.CreateResponse(HttpStatusCode.NoContent);
                    }
                    return Request.CreateResponse(respListaMaestros.codeRes,
                        new MensajeHttpResponse() { Message = respListaMaestros.messageRes });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError,
                          new MensajeHttpResponse() { Message = "Error interno al obtener respuesta." });
                }
            }
            catch (Exception ex)
            {
                log.Error($"ClienteController ({idLogTexto}) ->  ObtenerListaMaestros. Usuario: {cod_usuario}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno en el servicio de obtener datos de los maestros. " +
                    "Detalle error: " + JsonConvert.SerializeObject(ex));

                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                      new MensajeHttpResponse() { Message = "Error interno en el servicio de obtener datos de los maestros." });
            }
           
        }

        [Route("busqueda-tecnicos-general")]
        [HttpGet]
        public HttpResponseMessage ObtenerListaTecnicosGeneral(ObtenerListaTecnicosGeneralRequest request)
        {
            string idLogTexto = Guid.NewGuid().ToString();
            log.Info($"request --> ");
            var cod_usuario = User.Identity.GetUserId();
            var cod_aplicacion = AplicationData.codAplicacion;
            try
            {
                var respListaTecnicos = _clienteBO.ObtenerListaTecnicosGeneral(request, cod_aplicacion, cod_usuario, idLogTexto);
                if (respListaTecnicos.codeRes == HttpStatusCode.OK)
                {
                    if (respListaTecnicos.codeRes == HttpStatusCode.OK)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                            new { Message = respListaTecnicos.messageRes, data = respListaTecnicos.datos });
                    }
                    else if (respListaTecnicos.codeRes == HttpStatusCode.NoContent)
                    {
                        return Request.CreateResponse(HttpStatusCode.NoContent);
                    }
                    return Request.CreateResponse(respListaTecnicos.codeRes,
                        new MensajeHttpResponse() { Message = respListaTecnicos.messageRes });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError,
                          new MensajeHttpResponse() { Message = "Error interno al obtener respuesta." });
                }
            }
            catch (Exception ex)
            {
                log.Error($"ClienteController ({idLogTexto}) ->  ObtenerListaTecnicosGeneral. Usuario: {cod_usuario}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno en el servicio de obtener datos de los técnicos. " +
                    "Detalle error: " + JsonConvert.SerializeObject(ex));

                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                      new MensajeHttpResponse() { Message = "Error interno en el servicio de obtener datos de los técnicos." });
            }

        }

        [Route("busqueda-tecnicos-favoritos")]
        [HttpGet]
        public HttpResponseMessage ObtenerListaTecnicosFavoritos(ObtenerListaTecnicosFavoritosRequest request)
        {
            string idLogTexto = Guid.NewGuid().ToString();
            log.Info($"request --> ");
            var cod_usuario = User.Identity.GetUserId();
            var cod_aplicacion = AplicationData.codAplicacion;
            try
            {
                var respListaTecnicos = _clienteBO.ObtenerListaTecnicosFavoritos(request, cod_aplicacion, cod_usuario, idLogTexto);
                if (respListaTecnicos.codeRes == HttpStatusCode.OK)
                {
                    if (respListaTecnicos.codeRes == HttpStatusCode.OK)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                            new { Message = respListaTecnicos.messageRes, data = respListaTecnicos.datos });
                    }
                    else if (respListaTecnicos.codeRes == HttpStatusCode.NoContent)
                    {
                        return Request.CreateResponse(HttpStatusCode.NoContent);
                    }
                    return Request.CreateResponse(respListaTecnicos.codeRes,
                        new MensajeHttpResponse() { Message = respListaTecnicos.messageRes });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError,
                          new MensajeHttpResponse() { Message = "Error interno al obtener respuesta." });
                }
            }
            catch (Exception ex)
            {
                log.Error($"ClienteController ({idLogTexto}) ->  ObtenerListaTecnicosFavoritos. Usuario: {cod_usuario}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno en el servicio de obtener datos de los técnicos favoritos. " +
                    "Detalle error: " + JsonConvert.SerializeObject(ex));

                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                      new MensajeHttpResponse() { Message = "Error interno en el servicio de obtener datos de los técnicos favoritos." });
            }

        }

        [Route("perfil-tecnico")]
        [HttpGet]
        public HttpResponseMessage ObtenerPerfilTecnico(ObtenerPerfilTecnicoRequest request)
        {
            string idLogTexto = Guid.NewGuid().ToString();
            log.Info($"request --> ");
            var cod_usuario = User.Identity.GetUserId();
            var cod_aplicacion = AplicationData.codAplicacion;
            try
            {
                var respListaMaestros = _clienteBO.ObtenerPerfilTecnico(request, cod_aplicacion, cod_usuario, idLogTexto);
                if (respListaMaestros.codeRes == HttpStatusCode.OK)
                {
                    if (respListaMaestros.codeRes == HttpStatusCode.OK)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                            new { Message = respListaMaestros.messageRes, data = respListaMaestros.datos });
                    }
                    else if (respListaMaestros.codeRes == HttpStatusCode.NoContent)
                    {
                        return Request.CreateResponse(HttpStatusCode.NoContent);
                    }
                    return Request.CreateResponse(respListaMaestros.codeRes,
                        new MensajeHttpResponse() { Message = respListaMaestros.messageRes });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError,
                          new MensajeHttpResponse() { Message = "Error interno al obtener respuesta." });
                }
            }
            catch (Exception ex)
            {
                log.Error($"ClienteController ({idLogTexto}) ->  ObtenerPerfilTecnico. Usuario: {cod_usuario}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno en el servicio de obtener perfil del tecnico. " +
                    "Detalle error: " + JsonConvert.SerializeObject(ex));

                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                      new MensajeHttpResponse() { Message = "Error interno en el servicio de obtener perfil del tecnico." });
            }
        }

        [Route("solicitar-servicio")]
        [HttpPost]
        public HttpResponseMessage RegistrarSolicitudServicio(RegistrarSolicitudServicioRequest request)
        {
            string idLogTexto = Guid.NewGuid().ToString();
            log.Info($"request --> ");
            var cod_usuario = User.Identity.GetUserId();
            var cod_aplicacion = AplicationData.codAplicacion;
            try
            {
                var respListaMaestros = _clienteBO.RegistrarSolicitudServicio(request, cod_aplicacion, cod_usuario, idLogTexto);
                if (respListaMaestros.codeRes == HttpStatusCode.OK)
                {
                    if (respListaMaestros.codeRes == HttpStatusCode.OK)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                            new { Message = respListaMaestros.messageRes, idServicio = respListaMaestros.idServicio });
                    }
                    else if (respListaMaestros.codeRes == HttpStatusCode.NoContent)
                    {
                        return Request.CreateResponse(HttpStatusCode.NoContent);
                    }
                    return Request.CreateResponse(respListaMaestros.codeRes,
                        new MensajeHttpResponse() { Message = respListaMaestros.messageRes });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError,
                          new MensajeHttpResponse() { Message = "Error interno al obtener respuesta." });
                }
            }
            catch (Exception ex)
            {
                log.Error($"ClienteController ({idLogTexto}) ->  ObtenerPerfilTecnico. Usuario: {cod_usuario}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno en el servicio de obtener perfil del tecnico. " +
                    "Detalle error: " + JsonConvert.SerializeObject(ex));

                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                      new MensajeHttpResponse() { Message = "Error interno en el servicio de obtener perfil del tecnico." });
            }
        }
    }
}