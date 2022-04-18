namespace EasyWorkEntities.Usuario.Response
{
    public class ObtenerDataSesionResponse : GlobalHTTPResponse
    {
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public bool flgMostrarRegistroUsuario { get; set; }
        public bool flgCelularValidado { get; set; }
        public bool flgCorreoValidado { get; set; }
    }
}
