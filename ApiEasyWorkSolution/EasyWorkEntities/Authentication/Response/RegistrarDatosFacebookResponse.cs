namespace EasyWorkEntities.Authentication.Response
{
    public class RegistrarDatosFacebookResponse : GlobalHTTPResponse
    {
        public string codUsuarioCreado { get; set; }
        public bool flgMostrarRegistroUsuario { get; set; }
    }
}
