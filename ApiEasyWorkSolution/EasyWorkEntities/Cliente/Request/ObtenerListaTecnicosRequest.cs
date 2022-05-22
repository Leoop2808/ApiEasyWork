using System.Collections.Generic;

namespace EasyWorkEntities.Cliente.Request
{
    public class ObtenerListaTecnicosRequest
    {
        public string codCategoria { get; set; }
        public string direccion { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public string codDistrito { get; set; }
        public string descripcionProblema { get; set; }
        public string codMedioPago { get; set; }
        public string codTipoBusqueda { get; set; }
    }
}
