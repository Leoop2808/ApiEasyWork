namespace EasyWorkEntities.Tecnico.Response
{
    public class TecnicoObtenerServicioEnProcesoResponse : GlobalHTTPResponse
    {
        public DataTecnicoServicioEnProceso datos { get; set; }
    }

    public class DataTecnicoServicioEnProceso
    {
        public int idServicio { get; set; }
        public string codUsuarioCliente { get; set; }
        public string nombreUsuarioCliente { get; set; }
        public string codCategoriaServicio { get; set; }
        public string nombreCategoriaServicio { get; set; }
        public string descripcionProblema { get; set; }
        public string nombreDistrito { get; set; }
        public string codMedioPago { get; set; }
        public string nombreMedioPago { get; set; }
        public string direccion { get; set; }
    }
}
