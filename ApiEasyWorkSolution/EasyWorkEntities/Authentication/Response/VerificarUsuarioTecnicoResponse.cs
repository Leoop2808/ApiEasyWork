namespace EasyWorkEntities.Authentication.Response
{
    public class VerificarUsuarioTecnicoResponse : GlobalHTTPResponse
    {
        public string codUsuario { get; set; }
        public int idUsuario { get; set; }
        public bool flgMostrarRegistroUsuario { get; set; }
        public bool flgCelularValidado { get; set; }
    }
}
