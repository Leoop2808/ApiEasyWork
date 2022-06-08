using System.Collections.Generic;

namespace EasyWorkEntities.Cliente.Response
{
    public class ObtenerListaTecnicosGeneralResponse : GlobalHTTPResponse
    {
        public List<DataTecnicoGeneral> datos { get; set; }       
    }
    public class DataTecnicoGeneral
    {
        public DataTecnico datosTecnico { get; set; }
        public string strDistancia { get; set; }
        public string strTiempoViaje { get; set; }
    }
    public class DataTecnico 
    {
        public string codUsuarioTecnico { get; set; }
        public string nombreTecnico { get; set; }
        public string urlImgTecnico { get; set; }
        public string codUsuarioTecnicoCategoria { get; set; }
        public string codCategoria { get; set; }
        public string categoria { get; set; }
        public string cantidadClientes { get; set; }
        public string valoracion { get; set; }
    }
}
