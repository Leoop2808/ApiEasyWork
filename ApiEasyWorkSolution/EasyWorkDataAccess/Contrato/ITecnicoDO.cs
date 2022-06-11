using EasyWorkEntities.Tecnico.Request;
using EasyWorkEntities.Tecnico.Response;

namespace EasyWorkDataAccess.Contrato
{
    public interface ITecnicoDO
    {
        ValidarTecnicoServicioEnProcesoResponse ValidarTecnicoServicioEnProceso(string cod_aplicacion, string cod_usuario, string idLogTexto);
        TecnicoFinalizarCancelarServicioResponse TecnicoFinalizarCancelarServicio(TecnicoFinalizarCancelarServicioRequest request, bool flgFinalizarCancelarServicio,string cod_aplicacion, string cod_usuario, string idLogTexto);
        TecnicoObtenerServicioEnProcesoResponse TecnicoObtenerServicioEnProceso(int idServicioEnProceso, string cod_aplicacion, string cod_usuario, string idLogTexto);
        ObtenerSolicitudesResponse ObtenerSolicitudes(string cod_aplicacion, string cod_usuario, string idLogTexto);
        ObtenerSolicitudesGeneralesResponse ObtenerSolicitudesGenerales(ObtenerSolicitudesGeneralesRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto);
        ObtenerSolicitudesDirectasResponse ObtenerSolicitudesDirectas(ObtenerSolicitudesDirectasRequest request, string cod_aplicacion, string cod_usuario, string idLogTexto);
        AceptarSolicitudServicioResponse AceptarSolicitudServicio(int idServicio, string cod_aplicacion, string cod_usuario, string idLogTexto);
    }
}
