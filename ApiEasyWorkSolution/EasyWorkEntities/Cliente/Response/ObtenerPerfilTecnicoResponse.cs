using System.Collections.Generic;

namespace EasyWorkEntities.Cliente.Response
{
    public class ObtenerPerfilTecnicoResponse : GlobalHTTPResponse
    {
        public DatosPerfilTecnico datos { get; set; }        
    }
    public class DatosPerfilTecnico 
    {
        public int idPerfilTrabajador { get; set; }
        public string codUsuarioTecnico { get; set; }
        public string nombreTecnico { get; set; }
        public string categoria { get; set; }
        public string codUsuarioTecnicoCategoria { get; set; }
        public string codCategoria { get; set; }
        public string cantidadClientes { get; set; }
        public bool flgCorazon { get; set; }
        public DataValoracion datosValoracion { get; set; }
        public string cantidadReseñas { get; set; }
        public List<DataComentario> listaComentarios { get; set; }
    }
    public class DataValoracion 
    {
        public int cantidad5Estrellas { get; set; }
        public int cantidad4Estrellas { get; set; }
        public int cantidad3Estrellas { get; set; }
        public int cantidad2Estrellas { get; set; }
        public int cantidad1Estrellas { get; set; }
        public int promedioEstrellas { get; set; }
    }
    public class DataComentario
    {
        public int cantEstrellas{ get; set; }
        public string nombreUsuario { get; set; }
        public string fechaComentario { get; set; }
        public bool flgServicioVerificado { get; set; }
        public string comentario { get; set; }
    }
}
