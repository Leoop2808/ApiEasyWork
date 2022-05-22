namespace EasyWorkEntities.Cliente.Response
{
    public class ObtenerListaTecnicosResponse : GlobalHTTPResponse
    {
        public DataTecnico datos { get; set; }       
    }
    public class DataTecnico 
    {
        public string codUsuarioTecnico { get; set; }
        public string nombreTecnico { get; set; }
        public string urlImgTecnico { get; set; }
        public string codUsuarioTecnicoCategoria { get; set; }
        public string codCategoria { get; set; }
        public string categoria { get; set; }
        public string cantidadClientes { get; set; }
        public string valoracion { get; set; }
    }
}
