namespace EasyWorkEntities.Authentication.Response
{
    public class AutenticarFacebookResponse : GlobalHTTPResponse
    {
        public bool flgMostrarRegistroUsuario { get; set; }
        public bool flgCelularValidado { get; set; }
        public bool flgCorreoValidado { get; set; }
        public AuthenticatedUserData datos { get; set; }

    }
}
