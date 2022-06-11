namespace EasyWorkEntities.Tecnico.Response
{
    public class ValidarTecnicoServicioEnProcesoResponse : GlobalHTTPResponse
    {
        public DatosTecnicoServicioEnProceso datos { get; set; }
    }

    public class DatosTecnicoServicioEnProceso 
    {
        public bool flgServicioEnProceso { get; set; }
        public int idServicioEnProceso { get; set; }
    }
}
