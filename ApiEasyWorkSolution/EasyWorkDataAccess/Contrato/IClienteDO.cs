using EasyWorkEntities.Cliente.Request;
using EasyWorkEntities.Cliente.Response;

namespace EasyWorkDataAccess.Contrato
{
    public interface IClienteDO
    {
        ObtenerCategoriasServicioResponse ObtenerCategoriasServicio(string cod_usuario, string cod_aplicacion, string idLogTexto);
        ObtenerTiposDocumentoResponse ObtenerTiposDocumento(string cod_usuario, string cod_aplicacion, string idLogTexto);
        ObtenerDistritosResponse ObtenerDistritos(string cod_usuario, string cod_aplicacion, string idLogTexto);
        ObtenerMediosPagoResponse ObtenerMediosPago(string cod_usuario, string cod_aplicacion, string idLogTexto);
        ObtenerTiposTransporteResponse ObtenerTiposTransporte(string cod_usuario, string cod_aplicacion, string idLogTexto);
        ObtenerTiposBusquedaResponse ObtenerTiposBusqueda(string cod_usuario, string cod_aplicacion, string idLogTexto);
        ObtenerTecnicosDisponiblesResponse ObtenerTecnicosDisponibles(ObtenerListaTecnicosGeneralRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto);
        ObtenerTecnicosFavoritosDisponiblesResponse ObtenerTecnicosFavoritosDisponibles(ObtenerListaTecnicosFavoritosRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto);
        ObtenerDatosGeneralesPerfilTecnicoResponse ObtenerDatosGeneralesPerfilTecnico(int idTecnicoCategoriaServicio, string cod_aplicacion, string cod_usuario, string idLogTexto);
        ObtenerDatosValoracionPerfilTecnicoResponse ObtenerDatosValoracionPerfilTecnico(string codUsuarioTecnico, int idPerfilTrabajador, string cod_aplicacion, string cod_usuario, string idLogTexto);
        ObtenerListaComentariosPerfilTecnicoResponse ObtenerListaComentariosPerfilTecnico(string codUsuarioTecnico, int idPerfilTrabajador, string cod_aplicacion, string cod_usuario, string idLogTexto);
    }
}
