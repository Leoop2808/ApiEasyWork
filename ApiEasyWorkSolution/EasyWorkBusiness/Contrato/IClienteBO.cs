using EasyWorkEntities.Cliente.Request;
using EasyWorkEntities.Cliente.Response;

namespace EasyWorkBusiness.Contrato
{
    public interface IClienteBO
    {
        ObtenerListaMaestrosResponse ObtenerListaMaestros(string cod_aplicacion, string cod_usuario, string idLogTexto);
        ObtenerListaTecnicosGeneralResponse ObtenerListaTecnicosGeneral(ObtenerListaTecnicosGeneralRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto);
        ObtenerListaTecnicosFavoritosResponse ObtenerListaTecnicosFavoritos(ObtenerListaTecnicosFavoritosRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto);
        ObtenerPerfilTecnicoResponse ObtenerPerfilTecnico(ObtenerPerfilTecnicoRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto);
        ValidarClienteServicioEnProcesoResponse ValidarClienteServicioEnProceso(string cod_aplicacion, string cod_usuario, string idLogTexto);
        RegistrarSolicitudServicioResponse RegistrarSolicitudServicio(RegistrarSolicitudServicioRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto);
        ClienteCancelarServicioResponse ClienteCancelarServicio(ClienteCancelarServicioRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto);
        ClienteObtenerServicioEnProcesoResponse ClienteObtenerServicioEnProceso(int idServicioEnProceso, string cod_aplicacion, string cod_usuario, string idLogTexto);
        RegistrarReseniaResponse RegistrarResenia(RegistrarReseniaRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto);
    }
}
