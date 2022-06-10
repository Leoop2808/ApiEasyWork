using System.Collections.Generic;

namespace EasyWorkEntities.Cliente.Response
{
    public class ObtenerTecnicosFavoritosDisponiblesResponse : GlobalHTTPResponse
    {
        public List<DataTecnicoFavoritoDisponible> datos { get; set; }
    }
    public class DataTecnicoFavoritoDisponible : DataTecnico
    {
        public double latitud { get; set; }
        public double longitud { get; set; }
        public int travelMode { get; set; }
    }
    public class DataTecnicoFavoritoDisponibleTiempo : DataTecnico
    {
        public double tiempoViaje { get; set; }
        public string strTiempoViaje { get; set; }
        public double distancia { get; set; }
        public string strDistancia { get; set; }
    }
}
