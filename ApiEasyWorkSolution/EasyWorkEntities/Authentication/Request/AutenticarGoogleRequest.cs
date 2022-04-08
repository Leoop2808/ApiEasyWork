namespace EasyWorkEntities.Authentication.Request
{
    public class AutenticarGoogleRequest
    {
        public string google_token { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
    }
}
