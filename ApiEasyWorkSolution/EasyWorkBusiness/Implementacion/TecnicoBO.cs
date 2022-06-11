using EasyWorkBusiness.Contrato;
using EasyWorkDataAccess.Contrato;
using EasyWorkEntities.Tecnico.Request;
using EasyWorkEntities.Tecnico.Response;
using log4net;
using Newtonsoft.Json;
using System;
using System.Net;

namespace EasyWorkBusiness.Implementacion
{
    public class TecnicoBO : ITecnicoBO
    {
        private readonly ILog log = LogManager.GetLogger(typeof(ClienteBO));
        readonly ITecnicoDO _tecnicoDO;
        public TecnicoBO(ITecnicoDO tecnicoDO)
        {
            _tecnicoDO = tecnicoDO;
        }
        public ValidarTecnicoServicioEnProcesoResponse ValidarTecnicoServicioEnProceso(string cod_aplicacion, string cod_usuario, string idLogTexto) 
        {
            try
            {
                var response = _tecnicoDO.ValidarTecnicoServicioEnProceso(cod_aplicacion, cod_usuario, idLogTexto);
                log.Info($"response --> " + JsonConvert.SerializeObject(response));
                return response;
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ValidarTecnicoServicioEnProcesoResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al validar servicio en proceso."
                };
            }
        }
        public TecnicoFinalizarCancelarServicioResponse TecnicoFinalizarServicio(TecnicoFinalizarCancelarServicioRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto)
        {
            try
            {
                var response = _tecnicoDO.TecnicoFinalizarCancelarServicio(request, true,cod_aplicacion, cod_usuario, idLogTexto);
                log.Info($"response --> " + JsonConvert.SerializeObject(response));
                return response;
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new TecnicoFinalizarCancelarServicioResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al actualizar estado del servicio."
                };
            }
        }
        public TecnicoFinalizarCancelarServicioResponse TecnicoCancelarServicio(TecnicoFinalizarCancelarServicioRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto)
        {
            try
            {
                var response = _tecnicoDO.TecnicoFinalizarCancelarServicio(request, false, cod_aplicacion, cod_usuario, idLogTexto);
                log.Info($"response --> " + JsonConvert.SerializeObject(response));
                return response;
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new TecnicoFinalizarCancelarServicioResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al actualizar estado del servicio."
                };
            }
        }
        public TecnicoObtenerServicioEnProcesoResponse TecnicoObtenerServicioEnProceso(int idServicioEnProceso, string cod_aplicacion, string cod_usuario, string idLogTexto)
        {
            try
            {
                var response = _tecnicoDO.TecnicoObtenerServicioEnProceso(idServicioEnProceso, cod_aplicacion, cod_usuario, idLogTexto);
                log.Info($"response --> " + JsonConvert.SerializeObject(response));
                return response;
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new TecnicoObtenerServicioEnProcesoResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al obtener servicio en proceso."
                };
            }
        }
        public ObtenerSolicitudesResponse ObtenerSolicitudes(string cod_aplicacion, string cod_usuario, string idLogTexto)
        {
            try
            {
                var response = _tecnicoDO.ObtenerSolicitudes(cod_aplicacion, cod_usuario, idLogTexto);
                log.Info($"response --> " + JsonConvert.SerializeObject(response));
                return response;
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerSolicitudesResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al obtener solicitudes."
                };
            }
        }
        public ObtenerSolicitudesGeneralesResponse ObtenerSolicitudesGenerales(ObtenerSolicitudesGeneralesRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto)
        {
            try
            {
                var response = _tecnicoDO.ObtenerSolicitudesGenerales(request, cod_aplicacion, cod_usuario, idLogTexto);
                log.Info($"response --> " + JsonConvert.SerializeObject(response));
                return response;
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerSolicitudesGeneralesResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al obtener solicitudes generales."
                };
            }
        }
        public ObtenerSolicitudesDirectasResponse ObtenerSolicitudesDirectas(ObtenerSolicitudesDirectasRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto)
        {
            try
            {
                var response = _tecnicoDO.ObtenerSolicitudesDirectas(request, cod_aplicacion, cod_usuario, idLogTexto);
                log.Info($"response --> " + JsonConvert.SerializeObject(response));
                return response;
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerSolicitudesDirectasResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al obtener solicitudes directas."
                };
            }
        }
        public AceptarSolicitudServicioResponse AceptarSolicitudServicio(int idServicio, string cod_aplicacion, string cod_usuario, string idLogTexto)
        {
            try
            {
                var response = _tecnicoDO.AceptarSolicitudServicio(idServicio, cod_aplicacion, cod_usuario, idLogTexto);
                log.Info($"response --> " + JsonConvert.SerializeObject(response));
                return response;
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new AceptarSolicitudServicioResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al aceptar solicitud de servicio."
                };
            }
        }
    }
}
