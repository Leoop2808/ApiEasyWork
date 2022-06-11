namespace EasyWorkEntities.Cliente.Response
{
    public class ClienteObtenerServicioEnProcesoResponse : GlobalHTTPResponse
    {
        public DataClienteServicioEnProceso datos { get; set; }
    }
    public class DataClienteServicioEnProceso
    {
        public int idServicio { get; set; }
        public string codUsuarioTecnico { get; set; }
        public string nombreUsuarioTecnico { get; set; }
        public string urlImagenUsuarioTecnico { get; set; }
        public string codCategoriaServicio { get; set; }
        public string nombreCategoriaServicio { get; set; }
        public string descripcionProblema { get; set; }
        public string nombreDistrito { get; set; }
        public string codMedioPago { get; set; }
        public string nombreMedioPago { get; set; }
        public string direccion { get; set; }
    }
}
