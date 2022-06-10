using System.Collections.Generic;

namespace EasyWorkEntities.Cliente.Response
{
    public class ObtenerListaTecnicosFavoritosResponse : GlobalHTTPResponse
    {
        public List<DataTecnicoFavorito> datos { get; set; }
    }
    public class DataTecnicoFavorito
    {
        public DataTecnico datosTecnico { get; set; }
        public string strDistancia { get; set; }
        public string strTiempoViaje { get; set; }
    }
}
