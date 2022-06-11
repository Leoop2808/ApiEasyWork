namespace EasyWorkEntities.Cliente.Request
{
    public class RegistrarReseniaRequest
    {
        public int idTrabajadorCategoriaServicio { get; set; }
        public string comentario { get; set; }
        public int valoracion { get; set; }
    }
}
