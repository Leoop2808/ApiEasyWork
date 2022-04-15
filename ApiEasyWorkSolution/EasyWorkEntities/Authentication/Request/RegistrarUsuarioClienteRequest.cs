namespace EasyWorkEntities.Authentication.Request
{
    public class RegistrarUsuarioClienteRequest
    {
        public string correo { get; set; }
        public string celular { get; set; }
        public string nombre1 { get; set; }
        public string nombre2 { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string codTipoDocumento { get; set; }
        public string documento { get; set; }
        public string genero { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public string codDistrito { get; set; }
        public string contrasenia { get; set; }
    }
}
