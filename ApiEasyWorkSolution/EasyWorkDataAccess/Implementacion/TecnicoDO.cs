using AutoMapper;
using EasyWorkDataAccess.Contrato;
using EasyWorkDataAccess.Models;
using EasyWorkEntities.Tecnico.Request;
using EasyWorkEntities.Tecnico.Response;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace EasyWorkDataAccess.Implementacion
{
    public class TecnicoDO : ITecnicoDO
    {
        private readonly ILog log = LogManager.GetLogger(typeof(ClienteDO));
        public ValidarTecnicoServicioEnProcesoResponse ValidarTecnicoServicioEnProceso(string cod_aplicacion, string cod_usuario, string idLogTexto) 
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var dataRes = ctx.SP_VALIDAR_TECNICO_SERVICIO_EN_PROCESO(cod_usuario).FirstOrDefault();
                if (dataRes != null)
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<SP_VALIDAR_TECNICO_SERVICIO_EN_PROCESO_Result, DatosTecnicoServicioEnProceso>();
                    });

                    IMapper mapper = config.CreateMapper();
                    var datosMapeados = mapper.Map<SP_VALIDAR_TECNICO_SERVICIO_EN_PROCESO_Result, DatosTecnicoServicioEnProceso>(dataRes);

                    return new ValidarTecnicoServicioEnProcesoResponse()
                    {
                        codeRes = HttpStatusCode.OK,
                        messageRes = "Validación servicio en proceso exitoso.",
                        datos = datosMapeados
                    };
                }
                else
                {
                    return new ValidarTecnicoServicioEnProcesoResponse()
                    {
                        codeRes = HttpStatusCode.BadRequest,
                        messageRes = "No se obtuvo respuesta del servicio de validar servicio en proceso."
                    };
                }
            }
            catch (Exception e)
            {
                return new ValidarTecnicoServicioEnProcesoResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno en la validación de servicio en proceso."
                };
            }
        }
        public TecnicoFinalizarCancelarServicioResponse TecnicoFinalizarCancelarServicio(TecnicoFinalizarCancelarServicioRequest request, bool flgFinalizarCancelarServicio, string cod_aplicacion, string cod_usuario, string idLogTexto)
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var dataRes = ctx.SP_TECNICO_CANCELAR_FINALIZAR_SERVICIO(request.idServicio, request.motivoCancelacion, cod_usuario, flgFinalizarCancelarServicio).FirstOrDefault();

                if (dataRes != null)
                {
                    return new TecnicoFinalizarCancelarServicioResponse()
                    {
                        codeRes = (HttpStatusCode)dataRes.codeRes.GetValueOrDefault(),
                        messageRes = dataRes.messageRes
                    };
                }
                else
                {
                    return new TecnicoFinalizarCancelarServicioResponse()
                    {
                        codeRes = HttpStatusCode.NotFound,
                        messageRes = "No se obtuvo respuesta del servicio de actualizar estado servicio"
                    };
                }
            }
            catch (Exception)
            {
                return new TecnicoFinalizarCancelarServicioResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno en el servicio de actualizar estado servicio"
                };
            }
        }
        public TecnicoObtenerServicioEnProcesoResponse TecnicoObtenerServicioEnProceso(int idServicioEnProceso, string cod_aplicacion, string cod_usuario, string idLogTexto)
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var dataRes = ctx.SP_TECNICO_OBTENER_SERVICIO_EN_PROCESO(idServicioEnProceso, cod_usuario).FirstOrDefault();
                if (dataRes != null)
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<SP_TECNICO_OBTENER_SERVICIO_EN_PROCESO_Result, DataTecnicoServicioEnProceso>();
                    });

                    IMapper mapper = config.CreateMapper();
                    var datosMapeados = mapper.Map<SP_TECNICO_OBTENER_SERVICIO_EN_PROCESO_Result, DataTecnicoServicioEnProceso>(dataRes);

                    return new TecnicoObtenerServicioEnProcesoResponse()
                    {
                        codeRes = HttpStatusCode.OK,
                        messageRes = "Datos servicio en proceso obtenidos correctamente.",
                        datos = datosMapeados
                    };
                }
                else
                {
                    return new TecnicoObtenerServicioEnProcesoResponse()
                    {
                        codeRes = HttpStatusCode.BadRequest,
                        messageRes = "No se obtuvo respuesta del servicio de obtener servicio en proceso."
                    };
                }
            }
            catch (Exception e)
            {
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
                var ctx = new EasyWorkDBEntities();
                var response = ctx.SP_OBTENER_SOLICITUDES(cod_aplicacion, cod_usuario).FirstOrDefault();
                log.Info($"response --> " + JsonConvert.SerializeObject(response));
                if (response != null)
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<SP_OBTENER_SOLICITUDES_Result, DatosSolicitudes>();
                    });

                    IMapper mapper = config.CreateMapper();
                    var datosMapeados = mapper.Map<SP_OBTENER_SOLICITUDES_Result, DatosSolicitudes>(response);

                    return new ObtenerSolicitudesResponse()
                    {
                        codeRes = HttpStatusCode.OK,
                        messageRes = "Solicitudes obtenidas correctamente.",
                        datos = datosMapeados
                    };
                }
                else
                {
                    return new ObtenerSolicitudesResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvieron solicitudes."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerSolicitudesResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno en el listado de solicitudes."
                };
            }
        }
        public ObtenerSolicitudesGeneralesResponse ObtenerSolicitudesGenerales(ObtenerSolicitudesGeneralesRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto)
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var response = ctx.SP_OBTENER_SOLICITUDES_GENERALES(request.flgOrderByCategoria, request.flgOrderByZonas, cod_aplicacion, cod_usuario).ToList();
                log.Info($"response --> " + JsonConvert.SerializeObject(response));
                if (response != null && response.Count > 0)
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<SP_OBTENER_SOLICITUDES_GENERALES_Result, DatosSolicitudGeneral>();
                    });

                    IMapper mapper = config.CreateMapper();
                    var datosMapeados = mapper.Map<List<SP_OBTENER_SOLICITUDES_GENERALES_Result>, List<DatosSolicitudGeneral>>(response);

                    return new ObtenerSolicitudesGeneralesResponse()
                    {
                        codeRes = HttpStatusCode.OK,
                        messageRes = "Solicitudes generales obtenidas correctamente.",
                        datos = datosMapeados.ToList()
                    };
                }
                else
                {
                    return new ObtenerSolicitudesGeneralesResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvieron solicitudes generales.",
                        datos = new List<DatosSolicitudGeneral>()
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerSolicitudesGeneralesResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno en el listado de solicitudes generales."
                };
            }
        }
        public ObtenerSolicitudesDirectasResponse ObtenerSolicitudesDirectas(ObtenerSolicitudesDirectasRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto)
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var response = ctx.SP_OBTENER_SOLICITUDES_DIRECTAS(request.flgOrderByCategoria, request.flgOrderByZonas, cod_aplicacion, cod_usuario).ToList();
                log.Info($"response --> " + JsonConvert.SerializeObject(response));
                if (response != null && response.Count > 0)
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<SP_OBTENER_SOLICITUDES_DIRECTAS_Result, DatosSolicitudDirecta>();
                    });

                    IMapper mapper = config.CreateMapper();
                    var datosMapeados = mapper.Map<List<SP_OBTENER_SOLICITUDES_DIRECTAS_Result>, List<DatosSolicitudDirecta>>(response);

                    return new ObtenerSolicitudesDirectasResponse()
                    {
                        codeRes = HttpStatusCode.OK,
                        messageRes = "Solicitudes directas obtenidas correctamente.",
                        datos = datosMapeados.ToList()
                    };
                }
                else
                {
                    return new ObtenerSolicitudesDirectasResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvieron solicitudes directas.",
                        datos = new List<DatosSolicitudDirecta>()
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerSolicitudesDirectasResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno en el listado de solicitudes directas."
                };
            }
        }
        public AceptarSolicitudServicioResponse AceptarSolicitudServicio(int idServicio, string cod_aplicacion, string cod_usuario, string idLogTexto)
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var dataRes = ctx.SP_ACEPTAR_SOLICITUD_SERVICIO(idServicio, cod_usuario).FirstOrDefault();

                if (dataRes != null)
                {
                    return new AceptarSolicitudServicioResponse()
                    {
                        codeRes = (HttpStatusCode)dataRes.codeRes.GetValueOrDefault(),
                        messageRes = dataRes.messageRes
                    };
                }
                else
                {
                    return new AceptarSolicitudServicioResponse()
                    {
                        codeRes = HttpStatusCode.NotFound,
                        messageRes = "No se obtuvo respuesta del servicio de aceptar solicitud servicio"
                    };
                }
            }
            catch (Exception)
            {
                return new AceptarSolicitudServicioResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno en el servicio de aceptar solicitud servicio"
                };
            }
        }
    }
}
