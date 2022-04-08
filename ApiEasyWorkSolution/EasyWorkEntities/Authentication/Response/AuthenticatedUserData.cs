namespace EasyWorkEntities.Authentication.Response
{
    public class AuthenticatedUserData
    {
        public string codUsuario { get; set; }
        public string codTipoAutenticacion { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
    }
}
