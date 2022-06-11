namespace EasyWorkEntities.Cliente.Request
{
    public class ClienteCancelarServicioRequest
    {
        public int idServicio { get; set; }
        public string motivoCancelacion { get; set; }
    }
}
