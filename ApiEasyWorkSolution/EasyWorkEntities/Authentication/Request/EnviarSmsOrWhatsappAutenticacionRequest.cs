namespace EasyWorkEntities.Authentication.Request
{
    public class EnviarSmsOrWhatsappAutenticacionRequest
    {
        public string nroCelular { get; set; }
        public string tipoEnvio { get; set; }
    }
}
