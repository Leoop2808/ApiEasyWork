namespace EasyWorkEntities.Cliente.Response
{
    public class ValidarClienteServicioEnProcesoResponse : GlobalHTTPResponse
    {
        public DatosClienteServicioEnProceso datos { get; set; }
    }
    public class DatosClienteServicioEnProceso
    {
        public bool flgServicioEnProceso { get; set; }
        public int idServicioEnProceso { get; set; }
    }
}
