using System.Collections.Generic;

namespace EasyWorkEntities.Cliente.Response
{
    public class ObtenerListaMaestrosResponse : GlobalHTTPResponse
    {
        public ListadoMaestros datos { get; set; }       
    }
    public class ListadoMaestros 
    {
        public List<DataCategoriaServicio> listaCategoriaServicio { get; set; }
        public List<DataTipoDocumento> listaTipoDocumento { get; set; }
        public List<DataDistrito> listaDistrito { get; set; }
        public List<DataMedioPago> listaMedioPago { get; set; }
        public List<DataTipoTransporte> listaTipoTransporte { get; set; }
        public List<DataTipoBusqueda> listaTipoBusqueda { get; set; }
    }
    public class ObtenerCategoriasServicioResponse : GlobalHTTPResponse
    {
        public List<DataCategoriaServicio> datos { get; set; }
    }
    public class DataCategoriaServicio 
    {
        public string codCategoriaServicio { get; set; }
        public string siglaCategoriaServicio { get; set; }
        public string nombreImgCategoriaServicio { get; set; }
    }
    public class ObtenerTiposDocumentoResponse : GlobalHTTPResponse
    {
        public List<DataTipoDocumento> datos { get; set; }
    }
    public class DataTipoDocumento
    {
        public string codTipoDocumento { get; set; }
        public string siglaTipoDocumento { get; set; }
    }
    public class ObtenerDistritosResponse : GlobalHTTPResponse
    {
        public List<DataDistrito> datos { get; set; }
    }
    public class DataDistrito
    {
        public string codDistrito { get; set; }
        public string siglaDistrito { get; set; }
    }
    public class ObtenerMediosPagoResponse : GlobalHTTPResponse
    {
        public List<DataMedioPago> datos { get; set; }
    }
    public class DataMedioPago
    {
        public string codMedioPago { get; set; }
        public string siglaMedioPago { get; set; }
    }
    public class ObtenerTiposTransporteResponse : GlobalHTTPResponse
    {
        public List<DataTipoTransporte> datos { get; set; }
    }
    public class DataTipoTransporte
    {
        public string codTipoTransporte { get; set; }
        public string siglaTipoTransporte { get; set; }
    }
    public class ObtenerTiposBusquedaResponse : GlobalHTTPResponse
    {
        public List<DataTipoBusqueda> datos { get; set; }
    }
    public class DataTipoBusqueda
    {
        public string codTipoBusqueda { get; set; }
        public string siglaTipoBusqueda { get; set; }
    }
}
