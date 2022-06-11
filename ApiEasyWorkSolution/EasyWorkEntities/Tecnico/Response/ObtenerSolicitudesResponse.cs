namespace EasyWorkEntities.Tecnico.Response
{
    public class ObtenerSolicitudesResponse : GlobalHTTPResponse
    {
        public DatosSolicitudes datos { get; set; }
    }
    public class DatosSolicitudes 
    {
        public int cantidadSolicitudesDia { get; set; }
        public int cantidadSolicitudesDirectas { get; set; }
        public int cantidadSolicitudesGenerales { get; set; }
    }
}
