using System.Collections.Generic;

namespace EasyWorkEntities.Cliente.Response
{
    public class ObtenerListaComentariosPerfilTecnicoResponse : GlobalHTTPResponse
    {
        public List<DataComentario> listaComentarios { get; set; }
    }
}
