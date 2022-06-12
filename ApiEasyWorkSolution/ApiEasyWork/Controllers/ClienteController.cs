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
        [HttpPost]
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
        [HttpPost]
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
        [HttpPost]
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
            catch (Exception ex)
            {
                log.Error($"ClienteController ({idLogTexto}) ->  ObtenerPerfilTecnico. Usuario: {cod_usuario}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno en el servicio de obtener perfil del tecnico. " +
                    "Detalle error: " + JsonConvert.SerializeObject(ex));

                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                      new MensajeHttpResponse() { Message = "Error interno en el servicio de obtener perfil del tecnico." });
            }
        }

        [Route("validar-servicio-en-proceso")]
        [HttpGet]
        public HttpResponseMessage ValidarClienteServicioEnProceso()
        {
            string idLogTexto = Guid.NewGuid().ToString();
            log.Info($"request --> ");
            var cod_usuario = User.Identity.GetUserId();
            var cod_aplicacion = AplicationData.codAplicacion;
            try
            {
                var response = _clienteBO.ValidarClienteServicioEnProceso(cod_aplicacion, cod_usuario, idLogTexto);

                if (response.codeRes == HttpStatusCode.OK)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new { Message = response.messageRes, data = response.datos });
                }
                else if (response.codeRes == HttpStatusCode.NoContent)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                return Request.CreateResponse(response.codeRes,
                    new MensajeHttpResponse() { Message = response.messageRes });
            }
            catch (Exception ex)
            {
                log.Error($"ClienteController ({idLogTexto}) ->  ValidarClienteServicioEnProceso. Usuario: {cod_usuario}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno en el servicio de validar servicio en proceso. " +
                    "Detalle error: " + JsonConvert.SerializeObject(ex));

                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                      new MensajeHttpResponse() { Message = "Error interno en el servicio de validar servicio en proceso." });
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
                var response = _clienteBO.RegistrarSolicitudServicio(request, cod_aplicacion, cod_usuario, idLogTexto);
                if (response.codeRes == HttpStatusCode.OK)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new { Message = response.messageRes, idServicio = response.idServicio });
                }
                else if (response.codeRes == HttpStatusCode.NoContent)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                return Request.CreateResponse(response.codeRes,
                    new MensajeHttpResponse() { Message = response.messageRes });
            }
            catch (Exception ex)
            {
                log.Error($"ClienteController ({idLogTexto}) ->  RegistrarSolicitudServicio. Usuario: {cod_usuario}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno en el servicio de registrar solicitud de servicio. " +
                    "Detalle error: " + JsonConvert.SerializeObject(ex));

                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                      new MensajeHttpResponse() { Message = "Error interno en el servicio de registrar solicitud de servicio." });
            }
        }

        [Route("cancelar-servicio")]
        [HttpPost]
        public HttpResponseMessage ClienteCancelarServicio(ClienteCancelarServicioRequest request)
        {
            string idLogTexto = Guid.NewGuid().ToString();
            log.Info($"request --> ");
            var cod_usuario = User.Identity.GetUserId();
            var cod_aplicacion = AplicationData.codAplicacion;
            try
            {
                var respListaMaestros = _clienteBO.ClienteCancelarServicio(request, cod_aplicacion, cod_usuario, idLogTexto);
                if (respListaMaestros.codeRes == HttpStatusCode.OK)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new { Message = respListaMaestros.messageRes });
                }
                else if (respListaMaestros.codeRes == HttpStatusCode.NoContent)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                return Request.CreateResponse(respListaMaestros.codeRes,
                    new MensajeHttpResponse() { Message = respListaMaestros.messageRes });
            }
            catch (Exception ex)
            {
                log.Error($"ClienteController ({idLogTexto}) ->  ClienteCancelarServicio. Usuario: {cod_usuario}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno en el servicio de cancelar servicio. " +
                    "Detalle error: " + JsonConvert.SerializeObject(ex));

                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                      new MensajeHttpResponse() { Message = "Error interno en el servicio de cancelar servicio." });
            }
        }

        [Route("servicio-en-proceso/{idServicioEnProceso}")]
        [HttpGet]
        public HttpResponseMessage ClienteObtenerServicioEnProceso(int idServicioEnProceso)
        {
            string idLogTexto = Guid.NewGuid().ToString();
            log.Info($"request --> idServicioEnProceso : " + idServicioEnProceso.ToString());
            var cod_usuario = User.Identity.GetUserId();
            var cod_aplicacion = AplicationData.codAplicacion;
            try
            {
                var response = _clienteBO.ClienteObtenerServicioEnProceso(idServicioEnProceso, cod_aplicacion, cod_usuario, idLogTexto);

                if (response.codeRes == HttpStatusCode.OK)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new { Message = response.messageRes, data = response.datos });
                }
                else if (response.codeRes == HttpStatusCode.NoContent)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                return Request.CreateResponse(response.codeRes,
                    new MensajeHttpResponse() { Message = response.messageRes });
            }
            catch (Exception ex)
            {
                log.Error($"ClienteController ({idLogTexto}) ->  ClienteObtenerServicioEnProceso. Usuario: {cod_usuario}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno en el servicio de obtener servicio en proceso. " +
                    "Detalle error: " + JsonConvert.SerializeObject(ex));

                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                      new MensajeHttpResponse() { Message = "Error interno en el servicio de obtener servicio en proceso." });
            }
        }

        [Route("registrar-resenia")]
        [HttpPost]
        public HttpResponseMessage RegistrarResenia(RegistrarReseniaRequest request)
        {
            string idLogTexto = Guid.NewGuid().ToString();
            log.Info($"request --> " + JsonConvert.SerializeObject(request));
            var cod_usuario = User.Identity.GetUserId();
            var cod_aplicacion = AplicationData.codAplicacion;
            try
            {
                var response = _clienteBO.RegistrarResenia(request, cod_aplicacion, cod_usuario, idLogTexto);
                if (response.codeRes == HttpStatusCode.OK)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new { Message = response.messageRes});
                }
                else if (response.codeRes == HttpStatusCode.NoContent)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                return Request.CreateResponse(response.codeRes,
                    new MensajeHttpResponse() { Message = response.messageRes });
            }
            catch (Exception ex)
            {
                log.Error($"ClienteController ({idLogTexto}) ->  RegistrarResenia. Usuario: {cod_usuario}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno en el servicio de registrar reseña. " +
                    "Detalle error: " + JsonConvert.SerializeObject(ex));

                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                      new MensajeHttpResponse() { Message = "Error interno en el servicio de registrar reseña." });
            }
        }
    }
}