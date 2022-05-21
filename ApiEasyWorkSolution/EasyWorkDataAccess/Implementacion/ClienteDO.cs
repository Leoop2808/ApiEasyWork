using AutoMapper;
using EasyWorkDataAccess.Contrato;
using EasyWorkDataAccess.Models;
using EasyWorkEntities.Cliente.Response;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace EasyWorkDataAccess.Implementacion
{
    public class ClienteDO : IClienteDO
    {
        private readonly ILog log = LogManager.GetLogger(typeof(ClienteDO));
        public ObtenerCategoriasServicioResponse ObtenerCategoriasServicio(string cod_usuario, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var resCatServ = ctx.SP_OBTENER_CATEGORIAS_SERVICIO().ToList();
                log.Info($"resCatServ --> " + JsonConvert.SerializeObject(resCatServ));
                if (resCatServ != null && resCatServ.Count > 0)
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<SP_OBTENER_CATEGORIAS_SERVICIO_Result, DataCategoriaServicio>();
                    });

                    IMapper mapper = config.CreateMapper();
                    var datosMapeados = mapper.Map<List<SP_OBTENER_CATEGORIAS_SERVICIO_Result>, List<DataCategoriaServicio>>(resCatServ);

                    return new ObtenerCategoriasServicioResponse()
                    {
                        codeRes = HttpStatusCode.OK,
                        messageRes = "Datos de categorias de servicio obtenidos correctamente.",
                        datos = datosMapeados.ToList()
                    };
                }
                else
                {
                    return new ObtenerCategoriasServicioResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvieron datos de categorias de servicio.",
                        datos = new List<DataCategoriaServicio>()
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerCategoriasServicioResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno en el listado de datos de categorias de servicio."
                };
            }
        }
        public ObtenerTiposDocumentoResponse ObtenerTiposDocumento(string cod_usuario, string cod_aplicacion, string idLogTexto) 
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var resTipDoc = ctx.SP_OBTENER_TIPOS_DOCUMENTO().ToList();
                log.Info($"resTipDoc --> " + JsonConvert.SerializeObject(resTipDoc));
                if (resTipDoc != null && resTipDoc.Count > 0)
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<SP_OBTENER_TIPOS_DOCUMENTO_Result, DataTipoDocumento>();
                    });

                    IMapper mapper = config.CreateMapper();
                    var datosMapeados = mapper.Map<List<SP_OBTENER_TIPOS_DOCUMENTO_Result>, List<DataTipoDocumento>>(resTipDoc);

                    return new ObtenerTiposDocumentoResponse()
                    {
                        codeRes = HttpStatusCode.OK,
                        messageRes = "Datos de tipos de documento obtenidos correctamente.",
                        datos = datosMapeados.ToList()
                    };
                }
                else
                {
                    return new ObtenerTiposDocumentoResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvieron datos de tipos de documento.",
                        datos = new List<DataTipoDocumento>()
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerTiposDocumentoResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno en el listado de datos de tipos de documento."
                };
            }
        }
        public ObtenerDistritosResponse ObtenerDistritos(string cod_usuario, string cod_aplicacion, string idLogTexto)
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var resDist = ctx.SP_OBTENER_DISTRITOS().ToList();
                log.Info($"resDist --> " + JsonConvert.SerializeObject(resDist));
                if (resDist != null && resDist.Count > 0)
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<SP_OBTENER_DISTRITOS_Result, DataDistrito>();
                    });

                    IMapper mapper = config.CreateMapper();
                    var datosMapeados = mapper.Map<List<SP_OBTENER_DISTRITOS_Result>, List<DataDistrito>>(resDist);

                    return new ObtenerDistritosResponse()
                    {
                        codeRes = HttpStatusCode.OK,
                        messageRes = "Datos de distritos obtenidos correctamente.",
                        datos = datosMapeados.ToList()
                    };
                }
                else
                {
                    return new ObtenerDistritosResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvieron datos de distritos.",
                        datos = new List<DataDistrito>()
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerDistritosResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno en el listado de datos de distritos."
                };
            }
        }
        public ObtenerMediosPagoResponse ObtenerMediosPago(string cod_usuario, string cod_aplicacion, string idLogTexto)
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var resMedPag = ctx.SP_OBTENER_MEDIOS_PAGO().ToList();
                log.Info($"resMedPag --> " + JsonConvert.SerializeObject(resMedPag));
                if (resMedPag != null && resMedPag.Count > 0)
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<SP_OBTENER_MEDIOS_PAGO_Result, DataMedioPago>();
                    });

                    IMapper mapper = config.CreateMapper();
                    var datosMapeados = mapper.Map<List<SP_OBTENER_MEDIOS_PAGO_Result>, List<DataMedioPago>>(resMedPag);

                    return new ObtenerMediosPagoResponse()
                    {
                        codeRes = HttpStatusCode.OK,
                        messageRes = "Datos de medios de pago obtenidos correctamente.",
                        datos = datosMapeados.ToList()
                    };
                }
                else
                {
                    return new ObtenerMediosPagoResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvieron datos de medios de pago.",
                        datos = new List<DataMedioPago>()
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerMediosPagoResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno en el listado de datos de medios de pago."
                };
            }
        }
        public ObtenerTiposTransporteResponse ObtenerTiposTransporte(string cod_usuario, string cod_aplicacion, string idLogTexto)
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var resTipTrans = ctx.SP_OBTENER_TIPOS_TRANSPORTE().ToList();
                log.Info($"resTipTrans --> " + JsonConvert.SerializeObject(resTipTrans));
                if (resTipTrans != null && resTipTrans.Count > 0)
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<SP_OBTENER_TIPOS_TRANSPORTE_Result, DataTipoTransporte>();
                    });

                    IMapper mapper = config.CreateMapper();
                    var datosMapeados = mapper.Map<List<SP_OBTENER_TIPOS_TRANSPORTE_Result>, List<DataTipoTransporte>>(resTipTrans);

                    return new ObtenerTiposTransporteResponse()
                    {
                        codeRes = HttpStatusCode.OK,
                        messageRes = "Datos de tipos de transporte obtenidos correctamente.",
                        datos = datosMapeados.ToList()
                    };
                }
                else
                {
                    return new ObtenerTiposTransporteResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvieron datos de tipos de transporte.",
                        datos = new List<DataTipoTransporte>()
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerTiposTransporteResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno en el listado de datos de tipos de transporte."
                };
            }
        }
        public ObtenerTiposBusquedaResponse ObtenerTiposBusqueda(string cod_usuario, string cod_aplicacion, string idLogTexto)
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var resTipBus = ctx.SP_OBTENER_TIPOS_BUSQUEDA().ToList();
                log.Info($"resTipBus --> " + JsonConvert.SerializeObject(resTipBus));
                if (resTipBus != null && resTipBus.Count > 0)
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<SP_OBTENER_TIPOS_BUSQUEDA_Result, DataTipoBusqueda>();
                    });

                    IMapper mapper = config.CreateMapper();
                    var datosMapeados = mapper.Map<List<SP_OBTENER_TIPOS_BUSQUEDA_Result>, List<DataTipoBusqueda>>(resTipBus);

                    return new ObtenerTiposBusquedaResponse()
                    {
                        codeRes = HttpStatusCode.OK,
                        messageRes = "Datos de tipos de busqueda obtenidos correctamente.",
                        datos = datosMapeados.ToList()
                    };
                }
                else
                {
                    return new ObtenerTiposBusquedaResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvieron datos de tipos de busqueda.",
                        datos = new List<DataTipoBusqueda>()
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerTiposBusquedaResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno en el listado de datos de tipos de busqueda."
                };
            }
        }
    }
}
