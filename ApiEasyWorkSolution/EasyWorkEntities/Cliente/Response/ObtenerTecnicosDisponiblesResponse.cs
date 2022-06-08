using System.Collections.Generic;

namespace EasyWorkEntities.Cliente.Response
{
    public class ObtenerTecnicosDisponiblesResponse : GlobalHTTPResponse
    {
        public List<DataTecnicoDisponible> datos { get; set; }
    }
    public class DataTecnicoDisponible
    {
        public DataTecnico datosTecnico { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public int travelMode { get; set; }
    }
    public class DataTecnicoDisponibleTiempo
    {
        public DataTecnico datosTecnico { get; set; }
        public double tiempoViaje { get; set; }
        public string strTiempoViaje { get; set; }
        public double distancia { get; set; }
        public string strDistancia { get; set; }
    }
}
