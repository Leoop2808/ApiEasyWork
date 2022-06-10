using AutoMapper;
using EasyWorkDataAccess.Contrato;
using EasyWorkDataAccess.Models;
using EasyWorkEntities.Cliente.Request;
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
        public ObtenerTecnicosDisponiblesResponse ObtenerTecnicosDisponibles(ObtenerListaTecnicosGeneralRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto) 
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var response = ctx.SP_OBTENER_TECNICOS_DISPONIBLES(request.codCategoria, request.direccion, Convert.ToDecimal(request.latitud),
                Convert.ToDecimal(request.longitud), request.codDistrito, request.descripcionProblema, request.codMedioPago, cod_aplicacion, cod_usuario).ToList();
                log.Info($"response --> " + JsonConvert.SerializeObject(response));
                if (response != null && response.Count > 0)
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<SP_OBTENER_TECNICOS_DISPONIBLES_Result, DataTecnicoDisponible>();
                    });

                    IMapper mapper = config.CreateMapper();
                    var datosMapeados = mapper.Map<List<SP_OBTENER_TECNICOS_DISPONIBLES_Result>, List<DataTecnicoDisponible>>(response);

                    return new ObtenerTecnicosDisponiblesResponse()
                    {
                        codeRes = HttpStatusCode.OK,
                        messageRes = "Tecnicos disponibles obtenidos correctamente.",
                        datos = datosMapeados.ToList()
                    };
                }
                else
                {
                    return new ObtenerTecnicosDisponiblesResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvieron datos de tecnicos disponibles.",
                        datos = new List<DataTecnicoDisponible>()
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerTecnicosDisponiblesResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno en el listado de datos de tecnicos disponibles."
                };
            }
        }
        public ObtenerTecnicosFavoritosDisponiblesResponse ObtenerTecnicosFavoritosDisponibles(ObtenerListaTecnicosFavoritosRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto) 
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var response = ctx.SP_OBTENER_TECNICOS_FAVORITOS_DISPONIBLES(request.codCategoria, request.direccion, Convert.ToDecimal(request.latitud),
                Convert.ToDecimal(request.longitud), request.codDistrito, request.descripcionProblema, request.codMedioPago, cod_aplicacion, cod_usuario).ToList();
                log.Info($"response --> " + JsonConvert.SerializeObject(response));
                if (response != null && response.Count > 0)
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<SP_OBTENER_TECNICOS_FAVORITOS_DISPONIBLES_Result, DataTecnicoFavoritoDisponible>();
                    });

                    IMapper mapper = config.CreateMapper();
                    var datosMapeados = mapper.Map<List<SP_OBTENER_TECNICOS_FAVORITOS_DISPONIBLES_Result>, List<DataTecnicoFavoritoDisponible>>(response);

                    return new ObtenerTecnicosFavoritosDisponiblesResponse()
                    {
                        codeRes = HttpStatusCode.OK,
                        messageRes = "Técnicos favoritos disponibles obtenidos correctamente.",
                        datos = datosMapeados.ToList()
                    };
                }
                else
                {
                    return new ObtenerTecnicosFavoritosDisponiblesResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvieron datos de técnicos favoritos disponibles.",
                        datos = new List<DataTecnicoFavoritoDisponible>()
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerTecnicosFavoritosDisponiblesResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno en el listado de datos de técnicos favoritos disponibles."
                };
            }
        }
        public ObtenerDatosGeneralesPerfilTecnicoResponse ObtenerDatosGeneralesPerfilTecnico(int idTecnicoCategoriaServicio, string cod_aplicacion, string cod_usuario, string idLogTexto) 
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var response = ctx.SP_OBTENER_DATOS_GENERALES_PERFIL_TECNICO(idTecnicoCategoriaServicio, cod_aplicacion, cod_usuario).FirstOrDefault();
                log.Info($"response --> " + JsonConvert.SerializeObject(response));
                if (response != null)
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<SP_OBTENER_DATOS_GENERALES_PERFIL_TECNICO_Result, DatosGeneralesPerfilTecnico>();
                    });

                    IMapper mapper = config.CreateMapper();
                    var datosMapeados = mapper.Map<SP_OBTENER_DATOS_GENERALES_PERFIL_TECNICO_Result, DatosGeneralesPerfilTecnico>(response);

                    return new ObtenerDatosGeneralesPerfilTecnicoResponse()
                    {
                        codeRes = HttpStatusCode.OK,
                        messageRes = "Datos generales del perfil de técnico obtenidos correctamente.",
                        datos = datosMapeados
                    };
                }
                else
                {
                    return new ObtenerDatosGeneralesPerfilTecnicoResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvieron datos generales de técnicos."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerDatosGeneralesPerfilTecnicoResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno en el listado de datos generales de técnicos."
                };
            }
        }
        public ObtenerDatosValoracionPerfilTecnicoResponse ObtenerDatosValoracionPerfilTecnico(string codUsuarioTecnico, int idPerfilTrabajador, string cod_aplicacion, string cod_usuario, string idLogTexto) 
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var response = ctx.SP_OBTENER_DATOS_VALORACION_PERFIL_TECNICO(codUsuarioTecnico, idPerfilTrabajador, cod_aplicacion, cod_usuario).FirstOrDefault();
                log.Info($"response --> " + JsonConvert.SerializeObject(response));
                if (response != null)
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<SP_OBTENER_DATOS_VALORACION_PERFIL_TECNICO_Result, DataValoracion>();
                    });

                    IMapper mapper = config.CreateMapper();
                    var datosMapeados = mapper.Map<SP_OBTENER_DATOS_VALORACION_PERFIL_TECNICO_Result, DataValoracion>(response);

                    return new ObtenerDatosValoracionPerfilTecnicoResponse()
                    {
                        codeRes = HttpStatusCode.OK,
                        messageRes = "Datos de valoración de técnico obtenidos correctamente.",
                        datosValoracion = datosMapeados
                    };
                }
                else
                {
                    return new ObtenerDatosValoracionPerfilTecnicoResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvieron datos de valoración de técnicos."
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerDatosValoracionPerfilTecnicoResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno en el listado de datos de valoración de técnicos."
                };
            }
        }
        public ObtenerListaComentariosPerfilTecnicoResponse ObtenerListaComentariosPerfilTecnico(string codUsuarioTecnico, int idPerfilTrabajador, string cod_aplicacion, string cod_usuario, string idLogTexto) 
        {
            try
            {
                var ctx = new EasyWorkDBEntities();
                var response = ctx.SP_OBTENER_COMENTARIOS_PERFIL_TECNICO(codUsuarioTecnico, idPerfilTrabajador, cod_aplicacion, cod_usuario).ToList();
                log.Info($"response --> " + JsonConvert.SerializeObject(response));
                if (response != null && response.Count > 0)
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<SP_OBTENER_COMENTARIOS_PERFIL_TECNICO_Result, DataComentario>();
                    });

                    IMapper mapper = config.CreateMapper();
                    var datosMapeados = mapper.Map<List<SP_OBTENER_COMENTARIOS_PERFIL_TECNICO_Result>, List<DataComentario>>(response);

                    return new ObtenerListaComentariosPerfilTecnicoResponse()
                    {
                        codeRes = HttpStatusCode.OK,
                        messageRes = "Comentarios del perfil técnico obtenidos correctamente.",
                        listaComentarios = datosMapeados.ToList()
                    };
                }
                else
                {
                    return new ObtenerListaComentariosPerfilTecnicoResponse()
                    {
                        codeRes = HttpStatusCode.NoContent,
                        messageRes = "No se obtuvieron comentarios del perfil técnico disponibles.",
                        listaComentarios = new List<DataComentario>()
                    };
                }
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerListaComentariosPerfilTecnicoResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno en el listado de comentarios del perfil técnico."
                };
            }
        }
    }
}
