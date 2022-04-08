namespace EasyWorkEntities.Authentication.Response
{
    public class AutenticarFacebookResponse : GlobalHTTPResponse
    {
        public bool flgMostrarRegistroUsuario { get; set; }
        public AuthenticatedUserData datos { get; set; }

    }
}
