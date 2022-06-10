namespace EasyWorkEntities.Cliente.Response
{
    public class ObtenerDatosGeneralesPerfilTecnicoResponse : GlobalHTTPResponse
    {
        public DatosGeneralesPerfilTecnico datos { get; set; }        
    }
    public class DatosGeneralesPerfilTecnico 
    {
        public int idPerfilTrabajador { get; set; }
        public string codUsuarioTecnico { get; set; }
        public string nombreTecnico { get; set; }
        public string categoria { get; set; }
        public int idTecnicoCategoriaServicio { get; set; }
        public string codCategoria { get; set; }
        public string cantidadClientes { get; set; }
        public bool flgCorazon { get; set; }
        public string cantidadResenias { get; set; }
    }
}
