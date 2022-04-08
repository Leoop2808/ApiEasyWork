namespace EasyWorkEntities.Authentication.Response
{
    public class ObtenerDataPrincipalUsuarioResponse : GlobalHTTPResponse
    {
        public AuthenticatedUserData datos { get; set; }
    }
}
