using System.Collections.Generic;

namespace EasyWorkEntities.Cliente.Response
{
    public class ObtenerTecnicosDisponiblesResponse : GlobalHTTPResponse
    {
        public List<DataTecnicoDisponible> datos { get; set; }
    }
    public class DataTecnicoDisponible : DataTecnico
    {
        public double latitud { get; set; }
        public double longitud { get; set; }
        public int travelMode { get; set; }
    }
    public class DataTecnicoDisponibleTiempo : DataTecnico
    {
        public double tiempoViaje { get; set; }
        public string strTiempoViaje { get; set; }
        public double distancia { get; set; }
        public string strDistancia { get; set; }
    }
}
