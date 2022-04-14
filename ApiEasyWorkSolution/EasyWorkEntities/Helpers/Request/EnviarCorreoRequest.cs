namespace EasyWorkEntities.Helpers.Request
{
    public class EnviarCorreoRequest
    {
        public string correo { get; set; }
        public string asunto { get; set; }
        public string body { get; set; }
        public string idLogTexto { get; set; }
        public string cod_aplicacion { get; set; }
    }
}
