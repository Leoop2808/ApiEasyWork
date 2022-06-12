using ApiEasyWork.App_Start;
using ApiEasyWork.Util;
using EasyWorkBusiness.Contrato;
using EasyWorkEntities;
using EasyWorkEntities.Tecnico.Request;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace ApiEasyWork.Controllers
{
    [Authorize]
    [RoutePrefix("api/tecnico")]
    public class TecnicoController : ApiController
    {
        private readonly ILog log = LogManager.GetLogger(typeof(UsuarioController));
        private readonly ITecnicoBO _tecnicoBO;
        public TecnicoController(ITecnicoBO tecnicoBO)
        {
            _tecnicoBO = tecnicoBO;
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

        [Route("validar-servicio-en-proceso")]
        [HttpGet]
        public HttpResponseMessage ValidarTecnicoServicioEnProceso()
        {
            string idLogTexto = Guid.NewGuid().ToString();
            log.Info($"request --> ");
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var validToken = HelperToken.LeerToken(principal);
            if (validToken.codigo != 1)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized,
                    new MensajeHttpResponse() { Message = "No se pudo validar el token." });
            }

            var cod_usuario = validToken.cod_usuario;
            var cod_aplicacion = validToken.cod_aplicacion;
            try
            {
                var response = _tecnicoBO.ValidarTecnicoServicioEnProceso(cod_aplicacion, cod_usuario, idLogTexto);

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
                log.Error($"TecnicoController ({idLogTexto}) ->  ValidarTecnicoServicioEnProceso. Usuario: {cod_usuario}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno en el servicio de validar servicio en proceso. " +
                    "Detalle error: " + JsonConvert.SerializeObject(ex));

                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                      new MensajeHttpResponse() { Message = "Error interno en el servicio de validar servicio en proceso." });
            }
        }

        [Route("cancelar-servicio")]
        [HttpPost]
        public HttpResponseMessage TecnicoCancelarServicio(TecnicoFinalizarCancelarServicioRequest request)
        {
            string idLogTexto = Guid.NewGuid().ToString();
            log.Info($"request --> ");
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var validToken = HelperToken.LeerToken(principal);
            if (validToken.codigo != 1)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized,
                    new MensajeHttpResponse() { Message = "No se pudo validar el token." });
            }

            var cod_usuario = validToken.cod_usuario;
            var cod_aplicacion = validToken.cod_aplicacion;
            try
            {
                var response = _tecnicoBO.TecnicoCancelarServicio(request, cod_aplicacion, cod_usuario, idLogTexto);

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
                log.Error($"TecnicoController ({idLogTexto}) ->  TecnicoCancelarServicio. Usuario: {cod_usuario}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno en el servicio de cancelar servicio. " +
                    "Detalle error: " + JsonConvert.SerializeObject(ex));

                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                      new MensajeHttpResponse() { Message = "Error interno en el servicio de cancelar servicio." });
            }
        }

        [Route("finalizar-servicio")]
        [HttpPost]
        public HttpResponseMessage TecnicoFinalizarServicio(TecnicoFinalizarCancelarServicioRequest request)
        {
            string idLogTexto = Guid.NewGuid().ToString();
            log.Info($"request --> ");
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var validToken = HelperToken.LeerToken(principal);
            if (validToken.codigo != 1)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized,
                    new MensajeHttpResponse() { Message = "No se pudo validar el token." });
            }

            var cod_usuario = validToken.cod_usuario;
            var cod_aplicacion = validToken.cod_aplicacion;
            try
            {
                var response = _tecnicoBO.TecnicoFinalizarServicio(request, cod_aplicacion, cod_usuario, idLogTexto);

                if (response.codeRes == HttpStatusCode.OK)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new { Message = response.messageRes });
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
                log.Error($"TecnicoController ({idLogTexto}) ->  TecnicoFinalizarServicio. Usuario: {cod_usuario}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno en el servicio de finalizar servicio. " +
                    "Detalle error: " + JsonConvert.SerializeObject(ex));

                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                      new MensajeHttpResponse() { Message = "Error interno en el servicio de finalizar servicio." });
            }
        }

        [Route("servicio-en-proceso/{idServicioEnProceso}")]
        [HttpGet]
        public HttpResponseMessage TecnicoObtenerServicioEnProceso(int idServicioEnProceso)
        {
            string idLogTexto = Guid.NewGuid().ToString();
            log.Info($"request --> idServicioEnProceso : " + idServicioEnProceso.ToString());
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var validToken = HelperToken.LeerToken(principal);
            if (validToken.codigo != 1)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized,
                    new MensajeHttpResponse() { Message = "No se pudo validar el token." });
            }

            var cod_usuario = validToken.cod_usuario;
            var cod_aplicacion = validToken.cod_aplicacion;
            try
            {
                var response = _tecnicoBO.TecnicoObtenerServicioEnProceso(idServicioEnProceso, cod_aplicacion, cod_usuario, idLogTexto);

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
                log.Error($"TecnicoController ({idLogTexto}) ->  TecnicoObtenerServicioEnProceso. Usuario: {cod_usuario}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno en el servicio de obtener servicio en proceso. " +
                    "Detalle error: " + JsonConvert.SerializeObject(ex));

                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                      new MensajeHttpResponse() { Message = "Error interno en el servicio de obtener servicio en proceso." });
            }
        }

        [Route("solicitudes")]
        [HttpGet]
        public HttpResponseMessage ObtenerSolicitudes()
        {
            string idLogTexto = Guid.NewGuid().ToString();
            log.Info($"request -->" );
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var validToken = HelperToken.LeerToken(principal);
            if (validToken.codigo != 1)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized,
                    new MensajeHttpResponse() { Message = "No se pudo validar el token." });
            }

            var cod_usuario = validToken.cod_usuario;
            var cod_aplicacion = validToken.cod_aplicacion;
            try
            {
                var response = _tecnicoBO.ObtenerSolicitudes(cod_aplicacion, cod_usuario, idLogTexto);

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
                log.Error($"TecnicoController ({idLogTexto}) ->  ObtenerSolicitudes. Usuario: {cod_usuario}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno en el servicio de obtener solicitudes. " +
                    "Detalle error: " + JsonConvert.SerializeObject(ex));

                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                      new MensajeHttpResponse() { Message = "Error interno en el servicio de obtener solicitudes." });
            }
        }

        [Route("solicitudes-generales")]
        [HttpPost]
        public HttpResponseMessage ObtenerSolicitudesGenerales(ObtenerSolicitudesGeneralesRequest request)
        {
            string idLogTexto = Guid.NewGuid().ToString();
            log.Info($"request -->" + JsonConvert.SerializeObject(request));
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var validToken = HelperToken.LeerToken(principal);
            if (validToken.codigo != 1)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized,
                    new MensajeHttpResponse() { Message = "No se pudo validar el token." });
            }

            var cod_usuario = validToken.cod_usuario;
            var cod_aplicacion = validToken.cod_aplicacion;
            try
            {
                var response = _tecnicoBO.ObtenerSolicitudesGenerales(request, cod_aplicacion, cod_usuario, idLogTexto);

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
                log.Error($"TecnicoController ({idLogTexto}) ->  ObtenerSolicitudesGenerales. Usuario: {cod_usuario}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno en el servicio de obtener solicitudes generales. " +
                    "Detalle error: " + JsonConvert.SerializeObject(ex));

                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                      new MensajeHttpResponse() { Message = "Error interno en el servicio de obtener solicitudes generales." });
            }
        }

        [Route("solicitudes-directas")]
        [HttpPost]
        public HttpResponseMessage ObtenerSolicitudesDirectas(ObtenerSolicitudesDirectasRequest request)
        {
            string idLogTexto = Guid.NewGuid().ToString();
            log.Info($"request -->" + JsonConvert.SerializeObject(request));
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var validToken = HelperToken.LeerToken(principal);
            if (validToken.codigo != 1)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized,
                    new MensajeHttpResponse() { Message = "No se pudo validar el token." });
            }

            var cod_usuario = validToken.cod_usuario;
            var cod_aplicacion = validToken.cod_aplicacion;
            try
            {
                var response = _tecnicoBO.ObtenerSolicitudesDirectas(request, cod_aplicacion, cod_usuario, idLogTexto);

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
                log.Error($"TecnicoController ({idLogTexto}) ->  ObtenerSolicitudesDirectas. Usuario: {cod_usuario}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno en el servicio de obtener solicitudes directas. " +
                    "Detalle error: " + JsonConvert.SerializeObject(ex));

                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                      new MensajeHttpResponse() { Message = "Error interno en el servicio de obtener solicitudes directas." });
            }
        }

        [Route("aceptar-solicitud/{idServicio}")]
        [HttpPost]
        public HttpResponseMessage AceptarSolicitudServicio(int idServicio)
        {
            string idLogTexto = Guid.NewGuid().ToString();
            log.Info($"request --> ");
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var validToken = HelperToken.LeerToken(principal);
            if (validToken.codigo != 1)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized,
                    new MensajeHttpResponse() { Message = "No se pudo validar el token." });
            }

            var cod_usuario = validToken.cod_usuario;
            var cod_aplicacion = validToken.cod_aplicacion;
            try
            {
                var response = _tecnicoBO.AceptarSolicitudServicio(idServicio, cod_aplicacion, cod_usuario, idLogTexto);

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
                log.Error($"TecnicoController ({idLogTexto}) ->  AceptarSolicitudServicio. Usuario: {cod_usuario}, Aplicacion: {cod_aplicacion}." +
                    "Mensaje al cliente: Error interno en el servicio de aceptar solicitud de servicio. " +
                    "Detalle error: " + JsonConvert.SerializeObject(ex));

                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                      new MensajeHttpResponse() { Message = "Error interno en el servicio de aceptar solicitud de servicio." });
            }
        }
    }
}