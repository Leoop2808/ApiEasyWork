using System.Collections.Generic;

namespace EasyWorkEntities.Tecnico.Response
{
    public class ObtenerSolicitudesGeneralesResponse : GlobalHTTPResponse
    {
        public List<DatosSolicitudGeneral> datos { get; set; }
    }
    public class DatosSolicitudGeneral 
    {
        public int idServicio { get; set; }
        public string codUsuarioCliente { get; set; }
        public string nombreUsuarioCliente { get; set; }
        public string codCategoriaServicio { get; set; }
        public string nombreCategoriaServicio { get; set; }
        public string descripcionProblema { get; set; }
        public string codDistrito { get; set; }
        public string nombreDistrito { get; set; }
        public string nombreMedioPago { get; set; }
        public string direccion { get; set; }
    }
}
