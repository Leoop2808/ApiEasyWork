namespace EasyWorkEntities.Authentication.Request
{
    public class AutenticarTecnicoFacebookRequest
    {
        public string facebook_token { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
    }
}
