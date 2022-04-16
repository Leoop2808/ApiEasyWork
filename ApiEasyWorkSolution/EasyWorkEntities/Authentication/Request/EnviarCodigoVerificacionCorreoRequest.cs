namespace EasyWorkEntities.Authentication.Request
{
    public class EnviarCodigoVerificacionCorreoRequest
    {
        public string correo { get; set; }
        public string codTipoEnvioCorreo { get; set; }
    }
}
