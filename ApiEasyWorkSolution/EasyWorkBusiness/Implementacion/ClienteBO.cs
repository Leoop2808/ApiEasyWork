using EasyWorkBusiness.Contrato;
using EasyWorkDataAccess.Contrato;
using EasyWorkEntities.Cliente.Request;
using EasyWorkEntities.Cliente.Response;
using GoogleApi.Entities.Maps.Common;
using GoogleApi.Entities.Maps.DistanceMatrix.Request;
using GoogleDistanceMatrix.Clients;
using GoogleDistanceMatrix.Entities;
using GoogleDistanceMatrix.Enums;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;

namespace EasyWorkBusiness.Implementacion
{
    public class ClienteBO : IClienteBO
    {
        private string _apiKeyGoogle = ConfigurationManager.AppSettings["API_KEY_GOOGLE"];
        private readonly ILog log = LogManager.GetLogger(typeof(ClienteBO));
        readonly IClienteDO _clienteDO;
        public ClienteBO(IClienteDO clienteDO)
        {
            _clienteDO = clienteDO;
        }
        public ObtenerListaMaestrosResponse ObtenerListaMaestros(string cod_aplicacion, string cod_usuario, string idLogTexto) 
        {
            try
            {
                var response = new ObtenerListaMaestrosResponse()
                {
                    codeRes = HttpStatusCode.OK,
                    messageRes = "Datos de maestros obtenidos correctamente.",
                    datos = new ListadoMaestros()
                };
                ListadoDatosMaestrosFillData(response, cod_usuario, cod_aplicacion, idLogTexto);

                return response;
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerListaMaestrosResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al obtener datos maestros."
                };
            }
        }

        private void ListadoDatosMaestrosFillData(ObtenerListaMaestrosResponse response, string cod_usuario, string cod_aplicacion, string idLogTexto) 
        {
            var dataCategoriaServicio = _clienteDO.ObtenerCategoriasServicio(cod_usuario, cod_aplicacion, idLogTexto);
            log.Info($"dataCategoriaServicio --> " + JsonConvert.SerializeObject(dataCategoriaServicio));
            if (dataCategoriaServicio != null && dataCategoriaServicio.codeRes == HttpStatusCode.OK)
            {
                response.datos.listaCategoriaServicio = new List<DataCategoriaServicio>();
                response.datos.listaCategoriaServicio = dataCategoriaServicio.datos;
            }
            else
            {
                dataCategoriaServicio.messageRes = dataCategoriaServicio.messageRes;
            }

            var dataTipoDocumento = _clienteDO.ObtenerTiposDocumento(cod_usuario, cod_aplicacion, idLogTexto);
            log.Info($"dataTipoDocumento --> " + JsonConvert.SerializeObject(dataTipoDocumento));
            if (dataTipoDocumento != null && dataTipoDocumento.codeRes == HttpStatusCode.OK)
            {
                response.datos.listaTipoDocumento = new List<DataTipoDocumento>();
                response.datos.listaTipoDocumento = dataTipoDocumento.datos;
            }
            else
            {
                dataTipoDocumento.messageRes += dataTipoDocumento.messageRes;
            }

            var dataDistrito = _clienteDO.ObtenerDistritos(cod_usuario, cod_aplicacion, idLogTexto);
            log.Info($"dataDistrito --> " + JsonConvert.SerializeObject(dataDistrito));
            if (dataDistrito != null && dataDistrito.codeRes == HttpStatusCode.OK)
            {
                response.datos.listaDistrito = new List<DataDistrito>();
                response.datos.listaDistrito = dataDistrito.datos;
            }
            else
            {
                dataDistrito.messageRes += dataDistrito.messageRes;
            }
            var dataMedioPago = _clienteDO.ObtenerMediosPago(cod_usuario, cod_aplicacion, idLogTexto);
            log.Info($"dataMedioPago --> " + JsonConvert.SerializeObject(dataMedioPago));
            if (dataMedioPago != null && dataMedioPago.codeRes == HttpStatusCode.OK)
            {
                response.datos.listaMedioPago = new List<DataMedioPago>();
                response.datos.listaMedioPago = dataMedioPago.datos;
            }
            else
            {
                dataMedioPago.messageRes += dataMedioPago.messageRes;
            }
            var dataTipoTransporte = _clienteDO.ObtenerTiposTransporte(cod_usuario, cod_aplicacion, idLogTexto);
            log.Info($"dataTipoTransporte --> " + JsonConvert.SerializeObject(dataTipoTransporte));
            if (dataTipoTransporte != null && dataTipoTransporte.codeRes == HttpStatusCode.OK)
            {
                response.datos.listaTipoTransporte = new List<DataTipoTransporte>();
                response.datos.listaTipoTransporte = dataTipoTransporte.datos;
            }
            else
            {
                dataTipoTransporte.messageRes += dataTipoTransporte.messageRes;
            }
            var dataTipoBusqueda = _clienteDO.ObtenerTiposBusqueda(cod_usuario, cod_aplicacion, idLogTexto);
            log.Info($"dataTipoBusqueda --> " + JsonConvert.SerializeObject(dataTipoBusqueda));
            if (dataTipoBusqueda != null && dataTipoBusqueda.codeRes == HttpStatusCode.OK)
            {
                response.datos.listaTipoBusqueda = new List<DataTipoBusqueda>();
                response.datos.listaTipoBusqueda = dataTipoBusqueda.datos;
            }
            else
            {
                dataTipoBusqueda.messageRes += dataTipoBusqueda.messageRes;
            }
        }

        public ObtenerListaTecnicosResponse ObtenerListaTecnicos(ObtenerListaTecnicosRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto) 
        {
            try
            {
            //    var requestParameters = new RequestParameters();
            //    requestParameters.Key = _apiKeyGoogle;
            //    requestParameters.Origins = "-12.0464867%2C-76.9809687";
            //    requestParameters.Destinations = "-12.0459677%2C-76.9704462";
            //    requestParameters.Mode = TravelMode.Driving;
            //    requestParameters.DepartureTime = DateTime.Now;
            //    var responseDistanceMatrix = new GoogleDistanceMatrixClient(_apiKeyGoogle).GetDistanceMatrixReponse(requestParameters, OutputFormat.JSON);

                var response = new ObtenerListaTecnicosResponse()
                {
                    codeRes = HttpStatusCode.OK,
                    messageRes = "Prueba respuesta"
                };
                return response;
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerListaTecnicosResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al obtener lista tecnicos."
                };
            }
        }
        public ObtenerPerfilTecnicoResponse ObtenerPerfilTecnico(ObtenerPerfilTecnicoRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto) 
        {
            try
            {
                var response = new ObtenerPerfilTecnicoResponse()
                {
                    codeRes = HttpStatusCode.OK,
                    messageRes = "Prueba respuesta"
                };

                //var resPerfilTecnico = _clienteDO.ObtenerPerfilTecnico(cod_aplicacion, cod_aplicacion, idLogTexto);
                //log.Info($"resPerfilTecnico --> " + JsonConvert.SerializeObject(resPerfilTecnico));
                //response.codeRes = resPerfilTecnico.codeRes;
                //response.messageRes = resPerfilTecnico.messageRes;
                return response;
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new ObtenerPerfilTecnicoResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al obtener perfil del técnico."
                };
            }
        }

        public RegistrarSolicitudServicioResponse RegistrarSolicitudServicio(RegistrarSolicitudServicioRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto) 
        {
            try
            {
                var response = new RegistrarSolicitudServicioResponse()
                {
                    codeRes = HttpStatusCode.OK,
                    messageRes = "Prueba respuesta"
                };

                //var resRegSolServ = _clienteDO.RegistrarSolicitudServicio(cod_aplicacion, cod_aplicacion, idLogTexto);
                //log.Info($"resRegSolServ --> " + JsonConvert.SerializeObject(resRegSolServ));
                //response.codeRes = resRegSolServ.codeRes;
                //response.messageRes = resRegSolServ.messageRes;
                return response;
            }
            catch (Exception e)
            {
                log.Error("Error :" + JsonConvert.SerializeObject(e));
                return new RegistrarSolicitudServicioResponse()
                {
                    codeRes = HttpStatusCode.InternalServerError,
                    messageRes = "Error interno al registrar solicitud de servicio."
                };
            }
        }
    }
}
