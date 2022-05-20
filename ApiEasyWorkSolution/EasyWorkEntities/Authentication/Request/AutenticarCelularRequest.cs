namespace EasyWorkEntities.Authentication.Request
{
    public class AutenticarCelularRequest
    {
        public string nroCelular { get; set; }
        public string codVerificacion { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
    }
}
