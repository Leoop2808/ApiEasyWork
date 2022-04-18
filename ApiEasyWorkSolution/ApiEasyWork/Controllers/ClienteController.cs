using ApiEasyWork.App_Start;
using ApiEasyWork.FiltersAttributes;
using ApiEasyWork.Util;
using EasyWorkBusiness.Contrato;
using EasyWorkEntities;
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
    }
}