namespace EasyWorkEntities.Cliente.Request
{
    public class ObtenerListaTecnicosFavoritosRequest
    {
        public string codCategoria { get; set; }
        public string direccion { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public string codDistrito { get; set; }
        public string descripcionProblema { get; set; }
        public string codMedioPago { get; set; }
    }
}
