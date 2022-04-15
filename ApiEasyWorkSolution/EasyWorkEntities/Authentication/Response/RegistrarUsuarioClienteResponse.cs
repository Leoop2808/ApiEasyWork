namespace EasyWorkEntities.Authentication.Response
{
    public class RegistrarUsuarioClienteResponse : GlobalHTTPResponse
    {
        public bool flgMostrarRegistroUsuario { get; set; }
        public AuthenticatedUserData datos { get; set; }
    }
}
